﻿using System;
using System.IO;
using MoBi.Core.Domain.Model;
using MoBi.Core.Services;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Core.Domain.Data;
using OSPSuite.Core.Domain.Formulas;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Core.Domain.UnitSystem;
using OSPSuite.Utility.Events;

namespace MoBi.Helpers
{
   public static class DomainHelperForSpecs
   {
      static DomainHelperForSpecs()
      {
         TimeDimension.AddUnit(new Unit("seconds", 1 / 60.0, 0.0));
         AmountDimension.AddUnit(new Unit("mmol", 1000.0, 0.0));
      }

      /// <summary>
      ///    Returns the full path of the test data file
      /// </summary>
      /// <param name="fileName">The filename and extension</param>
      /// <returns>The full path including the name and extension</returns>
      public static string TestFileFullPath(string fileName)
      {
         var dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestFiles");
         return Path.Combine(dataFolder, fileName);
      }

      public static IDimension AmountDimension { get; } = new Dimension(new BaseDimensionRepresentation { AmountExponent = 1 }, Constants.Dimension.MOLAR_AMOUNT, "µmol");

      public static ParameterValue ParameterValue { get; } = new ParameterValue { Name = "_name", Value = 1.0, Formula = null, Dimension = AmountDimension };

      public static IDimension ConcentrationDimension { get; } = new Dimension(new BaseDimensionRepresentation { LengthExponent = -3, MassExponent = 1, TimeExponent = -1 }, Constants.Dimension.MOLAR_CONCENTRATION, "µmol/l");

      public static IDimension FractionDimension { get; } = new Dimension(new BaseDimensionRepresentation(), Constants.Dimension.FRACTION, "");

      public static IDimension ConcentrationPerTimeDimension { get; } = new Dimension(new BaseDimensionRepresentation { LengthExponent = -3, AmountExponent = 1, TimeExponent = -1 }, Constants.Dimension.MOLAR_CONCENTRATION_PER_TIME, "µmol/l/min");

      public static IDimension AmountPerTimeDimension { get; } = new Dimension(new BaseDimensionRepresentation { AmountExponent = 1, TimeExponent = -1 }, Constants.Dimension.AMOUNT_PER_TIME, "µmol/min");

      public static IDimension TimeDimension { get; } = new Dimension(new BaseDimensionRepresentation { TimeExponent = 1 }, Constants.Dimension.TIME, "min");

      public static QuantityValueInSimulationChangeTracker QuantityValueChangeTracker(IEventPublisher eventPublisher)
      {
         var entityPathResolver = new EntityPathResolver(new ObjectPathFactoryForSpecs());
         return new QuantityValueInSimulationChangeTracker(new QuantityToOriginalQuantityValueMapper(entityPathResolver), eventPublisher);
      }

      public static DataRepository ObservedData(string id = "TestData", IDimension timeDimension = null, IDimension concentrationDimension = null, string obsDataColumnName = null)
      {
         var observedData = new DataRepository(id).WithName(id);
         var baseGrid = new BaseGrid("Time", timeDimension ?? TimeDimension)
         {
            Values = new[] { 1.0f, 2.0f, 3.0f }
         };
         observedData.Add(baseGrid);

         var data = ConcentrationColumnForObservedData(baseGrid, concentrationDimension, obsDataColumnName);
         observedData.Add(data);

         return observedData;
      }

      public static DataColumn ConcentrationColumnForObservedData(BaseGrid baseGrid, IDimension concentrationDimension = null, string obsDataColumnName = null)
      {
         var data = new DataColumn(obsDataColumnName ?? "Col", concentrationDimension ?? ConcentrationDimension, baseGrid)
         {
            Values = new[] { 10f, 20f, 30f },
            DataInfo = { Origin = ColumnOrigins.Observation }
         };
         return data;
      }

      public static IParameter ConstantParameterWithValue(double value = 10, bool isDefault = false, bool visible = true)
      {
         var parameter = new Parameter
         {
            Visible = visible,
            Dimension = AmountDimension,
            IsFixedValue = true,
            IsDefault = isDefault,
            Formula = new ConstantFormula(value).WithId("constantFormulaId")
         };
         return parameter;
      }

      public static MoBiProject NewProject()
      {
         return new MoBiProject { SimulationSettings = new SimulationSettings() };
      }
   }

   public class ObjectPathFactoryForSpecs : ObjectPathFactory
   {
      public ObjectPathFactoryForSpecs() : base(new AliasCreator())
      {
      }
   }

   public class EntityPathResolverForSpecs : EntityPathResolver
   {
      public EntityPathResolverForSpecs() : base(new ObjectPathFactoryForSpecs())
      {
      }
   }

   public class PathCacheForSpecs<T> : PathCache<T> where T : class, IEntity
   {
      public PathCacheForSpecs() : base(new EntityPathResolverForSpecs())
      {
      }
   }
}