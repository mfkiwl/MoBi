﻿using System.Collections.Generic;
using System.Linq;
using MoBi.Assets;
using OSPSuite.Core.Commands.Core;
using OSPSuite.Utility.Collections;
using OSPSuite.Utility.Extensions;
using MoBi.Core.Commands;
using MoBi.Core.Domain.Builder;
using MoBi.Core.Domain.Extensions;
using MoBi.Core.Domain.Model;
using MoBi.Core.Domain.Services;
using MoBi.Core.Services;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Mappers;
using MoBi.Presentation.Tasks.Edit;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Core.Domain.Formulas;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Core.Domain.UnitSystem;
using OSPSuite.Assets;

namespace MoBi.Presentation.Tasks.Interaction
{
   public interface IMoleculeStartValuesTask : IStartValuesTask<IMoleculeStartValuesBuildingBlock, MoleculeStartValue>
   {
      IMoBiCommand SetIsPresent(IMoleculeStartValuesBuildingBlock moleculeStartValues, IEnumerable<MoleculeStartValue> startValues, bool isPresent);

      IMoBiCommand SetNegativeValuesAllowed(IMoleculeStartValuesBuildingBlock moleculeStartValues, IEnumerable<MoleculeStartValue> startValues, bool negativeValuesAllowed);
      
      /// <summary>
      ///    Updates the scale divisor for a start value
      /// </summary>
      /// <param name="buildingBlock">The building block that this start value is part of</param>
      /// <param name="startValue">The start value being updated</param>
      /// <param name="newScaleDivisor">The new value of the scale divisor</param>
      /// <param name="oldScaleDivisor">The old value of the scale divisor</param>
      /// <returns>The command used to modify the start value</returns>
      IMoBiCommand UpdateStartValueScaleDivisor(IMoleculeStartValuesBuildingBlock buildingBlock, MoleculeStartValue startValue, double newScaleDivisor, double oldScaleDivisor);
   }

   public class MoleculeStartValuesTask : AbstractStartValuesTask<IMoleculeStartValuesBuildingBlock, MoleculeStartValue>, IMoleculeStartValuesTask
   {
      private readonly IMoleculeStartValuesCreator _startValuesCreator;
      private readonly IReactionDimensionRetriever _dimensionRetriever;
      private readonly IMoleculeResolver _moleculeResolver;

      public MoleculeStartValuesTask(
         IInteractionTaskContext interactionTaskContext,
         IEditTasksForBuildingBlock<IMoleculeStartValuesBuildingBlock> editTask,
         IMoleculeStartValuesCreator startValuesCreator,
         IImportedQuantityToMoleculeStartValueMapper dtoMapper,
         IMoleculeStartValueBuildingBlockMergeManager startValueBuildingBlockMergeManager,
         ICloneManagerForBuildingBlock cloneManagerForBuildingBlock,
         IReactionDimensionRetriever dimensionRetriever,
         IMoBiFormulaTask moBiFormulaTask,
         IMoBiSpatialStructureFactory spatialStructureFactory, IMoleculeStartValuePathTask moleculeStartValuePathTask, IMoleculeResolver moleculeResolver)
         : base(interactionTaskContext, editTask, startValueBuildingBlockMergeManager, cloneManagerForBuildingBlock, moBiFormulaTask, spatialStructureFactory, dtoMapper, moleculeStartValuePathTask)
      {
         _startValuesCreator = startValuesCreator;
         _dimensionRetriever = dimensionRetriever;
         _moleculeResolver = moleculeResolver;
      }

      public override void ExtendStartValues(IMoleculeStartValuesBuildingBlock moleculeStartValuesBuildingBlock)
      {
         var newStartValues = createStartValuesBasedOnUsedTemplates(moleculeStartValuesBuildingBlock);
         updateDefaultIsPresentToFalseForSpecificExtendedValues(newStartValues, moleculeStartValuesBuildingBlock.ToCache());
         AddCommand(Extend(newStartValues, moleculeStartValuesBuildingBlock));
      }

      private IMoleculeStartValuesBuildingBlock createStartValuesBasedOnUsedTemplates(IMoleculeStartValuesBuildingBlock moleculeStartValues)
      {
         var molecules = BuildingBlockById<IMoleculeBuildingBlock>(moleculeStartValues.MoleculeBuildingBlockId);
         var spatialStructure = BuildingBlockById<ISpatialStructure>(moleculeStartValues.SpatialStructureId);
         return _startValuesCreator.CreateFrom(spatialStructure, molecules);
      }

      public override IMoleculeStartValuesBuildingBlock CreateStartValuesForSimulation(IMoBiBuildConfiguration buildConfiguration)
      {
         var simulationStartValues = _startValuesCreator.CreateFrom(buildConfiguration.SpatialStructureInfo.TemplateBuildingBlock, buildConfiguration.MoleculesInfo.TemplateBuildingBlock)
            .WithName(buildConfiguration.MoleculeStartValuesInfo.TemplateBuildingBlock.Name);

         var templateValues = UpdateValuesFromTemplate(simulationStartValues, buildConfiguration.MoleculeStartValuesInfo);
         updateDefaultIsPresentToFalseForSpecificExtendedValues(simulationStartValues, templateValues);
         return simulationStartValues;
      }

      private void updateDefaultIsPresentToFalseForSpecificExtendedValues(IMoleculeStartValuesBuildingBlock startValues, ICache<string, MoleculeStartValue> templateValues)
      {
         var startValuesThatShouldPotentiallyNotBePresent = startValues.ToCache().KeyValues.Where(x => AppConstants.Organs.DefaultIsPresentShouldBeFalse.Any(organ=> x.Key.Contains(organ)));
         var extendedStartValuesThatShouldNotBePresent = startValuesThatShouldPotentiallyNotBePresent.Where(x => !templateValues.Contains(x.Key));
         extendedStartValuesThatShouldNotBePresent.Each(x => x.Value.IsPresent = false);
      }

      public IMoBiCommand SetIsPresent(IMoleculeStartValuesBuildingBlock moleculeStartValues, IEnumerable<MoleculeStartValue> startValues, bool isPresent)
      {
         var macroCommand = new MoBiMacroCommand
         {
            CommandType = AppConstants.Commands.EditCommand,
            Description = AppConstants.Commands.SettingIsPresentCommandDescription(isPresent),
            ObjectType = ObjectTypes.MoleculeStartValue,
         };

         startValues.Where(x => x.IsPresent != isPresent).Each(msv =>
            macroCommand.Add(updateIsPresent(moleculeStartValues, msv, isPresent)));

         return macroCommand;
      }

      private IMoBiCommand updateIsPresent(IMoleculeStartValuesBuildingBlock moleculeStartValues, MoleculeStartValue msv, bool isPresent)
      {
         return new UpdateMoleculeStartValueIsPresentCommand(moleculeStartValues, msv, isPresent).Run(Context);
      }

      public IMoBiCommand SetNegativeValuesAllowed(IMoleculeStartValuesBuildingBlock moleculeStartValues, IEnumerable<MoleculeStartValue> startValues, bool negativeValuesAllowed)
      {
         var macroCommand = new MoBiMacroCommand
         {
            CommandType = AppConstants.Commands.EditCommand,
            Description = AppConstants.Commands.SettingNegativeValuesAllowedCommandDescription(negativeValuesAllowed),
            ObjectType = ObjectTypes.MoleculeStartValue,
         };

         startValues.Where(x => x.NegativeValuesAllowed != negativeValuesAllowed).Each(msv =>
            macroCommand.Add(updateNegativeValuesAllowed(moleculeStartValues, msv, negativeValuesAllowed)));

         return macroCommand;
      }

      private IMoBiCommand updateNegativeValuesAllowed(IMoleculeStartValuesBuildingBlock moleculeStartValues, MoleculeStartValue msv, bool negativeValuesAllowed)
      {
         return new UpdateMoleculeStartValueNegativeValuesAllowedCommand(moleculeStartValues, msv, negativeValuesAllowed).Run(Context);
      }

      public ICommand<IMoBiContext> AddNewFormulaAtBuildingBlock(IMoleculeStartValuesBuildingBlock buildingBlock, MoleculeStartValue moleculeStartValue)
      {
         return AddNewFormulaAtBuildingBlock(buildingBlock, moleculeStartValue, referenceParameter: null);
      }

      public override IMoBiCommand AddStartValueToBuildingBlock(IMoleculeStartValuesBuildingBlock buildingBlock, MoleculeStartValue moleculeStartValue)
      {
         return GenerateAddCommand(buildingBlock, moleculeStartValue).Run(Context);
      }

      public override IMoBiCommand ImportStartValuesToBuildingBlock(IMoleculeStartValuesBuildingBlock startValuesBuildingBlock, IEnumerable<ImportedQuantityDTO> startValues)
      {
         var macroCommand = new BulkUpdateMacroCommand
         {
            CommandType = AppConstants.Commands.ImportCommand,
            Description = AppConstants.Commands.ImportMoleculeStartValues,
            ObjectType = ObjectTypes.MoleculeStartValue
         };

         GetImportStartValuesMacroCommand(startValuesBuildingBlock, startValues, macroCommand);

         return macroCommand.Run(Context);
      }

      public override IMoBiCommand RemoveStartValueFromBuildingBlockCommand(MoleculeStartValue startValue, IMoleculeStartValuesBuildingBlock buildingBlock)
      {
         return new RemoveMoleculeStartValueFromBuildingBlockCommand(buildingBlock, startValue.Path);
      }

      public override IMoBiCommand RefreshStartValuesFromBuildingBlocks(IMoleculeStartValuesBuildingBlock buildingBlock, IEnumerable<MoleculeStartValue> startValuesToRefresh)
      {
         var macroCommand = new MoBiMacroCommand
         {
            CommandType = AppConstants.Commands.EditCommand,
            Description = AppConstants.Commands.RefreshStartValuesFromBuildingBlocks,
            ObjectType = ObjectTypes.MoleculeStartValue
         };

         startValuesToRefresh.Each(startValue =>
         {
            var moleculeBuilder = _moleculeResolver.Resolve(startValue.ContainerPath, startValue.MoleculeName, SpatialStructureReferencedBy(buildingBlock), MoleculeBuildingBlockReferencedBy(buildingBlock));
            if (moleculeBuilder == null) return;

            var originalStartValue = moleculeBuilder.GetDefaultMoleculeStartValue();
            var originalUnit = moleculeBuilder.DisplayUnit;

            if (!ValueComparer.AreValuesEqual(Constants.DEFAULT_SCALE_DIVISOR, startValue.ScaleDivisor))
            {
               macroCommand.Add(UpdateStartValueScaleDivisor(buildingBlock, startValue, Constants.DEFAULT_SCALE_DIVISOR, startValue.ScaleDivisor));
            }

            if (!HasEquivalentDimension(startValue, moleculeBuilder))
            {
               macroCommand.Add(UpdateStartValueDimension(buildingBlock, startValue, moleculeBuilder.Dimension));
            }

            if (!HasEquivalentStartValue(startValue, originalStartValue))
            {
               macroCommand.Add(SetValueWithUnit(startValue, originalStartValue, originalUnit, buildingBlock));
            }

            if (!HasEquivalentFormula(startValue, moleculeBuilder.DefaultStartFormula))
            {
               macroCommand.Add(ChangeValueFormulaCommand(buildingBlock, startValue,
                  moleculeBuilder.DefaultStartFormula.IsConstant() ? null : _cloneManagerForBuildingBlock.Clone(moleculeBuilder.DefaultStartFormula, buildingBlock.FormulaCache)));
            }
         });

         return macroCommand;
      }

      public IMoBiCommand UpdateStartValueScaleDivisor(IMoleculeStartValuesBuildingBlock buildingBlock, MoleculeStartValue startValue, double newScaleDivisor, double oldScaleDivisor)
      {
         return new UpdateMoleculeStartValueScaleDivisorCommand(buildingBlock, startValue, newScaleDivisor, oldScaleDivisor).Run(Context);
      }

      public override bool IsEquivalentToOriginal(MoleculeStartValue startValue, IMoleculeStartValuesBuildingBlock buildingBlock)
      {
         var moleculeBuilder = _moleculeResolver.Resolve(startValue.ContainerPath, startValue.MoleculeName, SpatialStructureReferencedBy(buildingBlock), MoleculeBuildingBlockReferencedBy(buildingBlock));

         if (moleculeBuilder == null)
            return false;

         return HasEquivalentDimension(startValue, moleculeBuilder) &&
                HasEquivalentFormula(startValue, moleculeBuilder.DefaultStartFormula) &&
                HasEquivalentStartValue(startValue, moleculeBuilder.GetDefaultMoleculeStartValue());
      }

      public override IDimension GetDefaultDimension()
      {
         return _dimensionRetriever.MoleculeDimension;
      }

      public override bool CanResolve(IMoleculeStartValuesBuildingBlock buildingBlock, MoleculeStartValue startValue)
      {
         return _moleculeResolver.Resolve(startValue.ContainerPath, startValue.MoleculeName, SpatialStructureReferencedBy(buildingBlock), MoleculeBuildingBlockReferencedBy(buildingBlock)) != null;
      }

      protected override IMoBiCommand GetUpdateStartValueInBuildingBlockCommand(IMoleculeStartValuesBuildingBlock startValuesBuildingBlock, ImportedQuantityDTO dto)
      {
         var scaleDisivor = dto.IsScaleDivisorSpecified ? dto.ScaleDivisor : startValuesBuildingBlock[dto.Path].ScaleDivisor;
         var startValue = dto.IsQuantitySpecified ? dto.QuantityInBaseUnit : startValuesBuildingBlock[dto.Path].Value;

         return new UpdateMoleculeStartValueInBuildingBlockCommand(startValuesBuildingBlock, dto.Path, startValue, dto.IsPresent, scaleDisivor, dto.NegativeValuesAllowed);
      }

      protected override IMoBiCommand GenerateAddCommand(IMoleculeStartValuesBuildingBlock targetBuildingBlock, MoleculeStartValue startValueToAdd)
      {
         return new AddMoleculeStartValueToBuildingBlockCommand(targetBuildingBlock, startValueToAdd);
      }

      protected override bool AreEquivalentItems(MoleculeStartValue first, MoleculeStartValue second)
      {
         return first.IsEquivalentTo(second);
      }

      protected override IMoBiCommand GenerateRemoveCommand(IMoleculeStartValuesBuildingBlock targetBuildingBlock, MoleculeStartValue startValueToRemove)
      {
         return new RemoveMoleculeStartValueFromBuildingBlockCommand(targetBuildingBlock, startValueToRemove.Path);
      }
   }
}