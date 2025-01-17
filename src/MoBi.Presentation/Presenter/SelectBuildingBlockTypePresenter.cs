using System.Collections.Generic;
using MoBi.Assets;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Views;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Extensions;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Utility.Format;

namespace MoBi.Presentation.Presenter
{
   public class BuildingBlockTypeFormatter : IFormatter<BuildingBlockType>
   {
      public string Format(BuildingBlockType valueToFormat)
      {
         return valueToFormat.ToString().SplitToUpperCase();
      }
   }

   public interface ISelectBuildingBlockTypePresenter : IDisposablePresenter
   {
      BuildingBlockType GetBuildingBlockType(Module module);
   }

   public class SelectBuildingBlockTypePresenter : AbstractDisposablePresenter<ISelectBuildingBlockTypeView, ISelectBuildingBlockTypePresenter>,
      ISelectBuildingBlockTypePresenter
   {
      public SelectBuildingBlockTypePresenter(ISelectBuildingBlockTypeView view) : base(view)
      {
      }

      public BuildingBlockType GetBuildingBlockType(Module module)
      {
         _view.Caption = AppConstants.Captions.LoadBuildingBlockToModule(module.Name);

         var selectBuildingBlockTypeDTO = new SelectBuildingBlockTypeDTO(module);
         _view.BindTo(selectBuildingBlockTypeDTO);
         _view.Display();

         return _view.Canceled ? BuildingBlockType.None : selectBuildingBlockTypeDTO.SelectedBuildingBlockType;
      }
   }
}