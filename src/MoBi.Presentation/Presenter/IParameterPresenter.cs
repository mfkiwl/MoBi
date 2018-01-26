using System.Collections.Generic;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.UnitSystem;
using OSPSuite.Presentation.DTO;
using OSPSuite.Presentation.Presenters;

namespace MoBi.Presentation.Presenter
{
   public interface IParameterPresenter : IPresenterWithFormulaCache, ICommandCollectorPresenter
   {
      void SetParamterUnit(IParameterDTO parameterDTO, Unit displayUnit);
      bool IsFixedValue(IParameterDTO parameterDTO);
      void OnParameterValueSet(IParameterDTO parameterDTO, double valueInGuiUnit);
      void OnParameterValueOriginSet(IParameterDTO parameterDTO, ValueOrigin valueOrigin);
      void SetDimensionFor(IParameterDTO parameterDTO, IDimension newDimension);
      IEnumerable<IDimension> GetDimensions();
      void SetIsFavorite(IParameterDTO parameterDTO, bool isFavorite);
      void ResetValueFor(IParameterDTO parameterDTO);
   }
}