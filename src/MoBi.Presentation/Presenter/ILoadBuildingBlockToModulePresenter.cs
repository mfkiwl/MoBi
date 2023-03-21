using MoBi.Presentation.Views;
using OSPSuite.Presentation.Presenters;

namespace MoBi.Presentation.Presenter
{
   public interface ILoadBuildingBlockToModulePresenter : IDisposablePresenter
   {
   }

   
   public class LoadBuildingBlockToModulePresenter : AbstractDisposablePresenter<ILoadBuildingBlockToModuleView, ILoadBuildingBlockToModulePresenter>,
      ILoadBuildingBlockToModulePresenter
   {
      public LoadBuildingBlockToModulePresenter(ILoadBuildingBlockToModuleView view) : base(view)
      {
      }
   }

}