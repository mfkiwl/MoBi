using System.Collections.Generic;
using MoBi.Assets;
using OSPSuite.Presentation.MenuAndBars;
using OSPSuite.Utility.Extensions;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Presenter;
using MoBi.Presentation.UICommand;
using OSPSuite.Presentation.Core;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Presentation.Presenters.ContextMenus;
using OSPSuite.Assets;
using OSPSuite.Utility.Container;

namespace MoBi.Presentation.MenusAndBars.ContextMenus
{
   public class RootContextMenuForSpatialStructure : ContextMenuBase
   {
      private readonly IContainer _container;

      public RootContextMenuForSpatialStructure(IContainer container)
      {
         _container = container;
      }
      public override IEnumerable<IMenuBarItem> AllMenuItems()
      {
         yield return CreateMenuButton.WithCaption(AppConstants.MenuNames.AddNew("Top Container"))
            .WithIcon(ApplicationIcons.ContainerAdd)
            .WithCommand<AddNewTopContainerCommand>(_container);

         yield return CreateMenuButton.WithCaption(AppConstants.MenuNames.AddExisting("Top Container"))
            .WithIcon(ApplicationIcons.ContainerLoad)
            .WithCommand<AddExistingTopContainerCommand>(_container);
      }
   }

   public class RootContextMenuForSpatialStructureFactory : IContextMenuSpecificationFactory<IViewItem>
   {
      private readonly IContainer _container;

      public RootContextMenuForSpatialStructureFactory(IContainer container)
      {
         _container = container;
      }

      public IContextMenu CreateFor(IViewItem objectRequestingContextMenu, IPresenterWithContextMenu<IViewItem> presenter)
      {
         return new RootContextMenuForSpatialStructure(_container);
      }

      public bool IsSatisfiedBy(IViewItem objectRequestingContextMenu, IPresenterWithContextMenu<IViewItem> presenter)
      {
         return objectRequestingContextMenu.IsAnImplementationOf<SpatialStructureRootItem>() && presenter.IsAnImplementationOf<IHierarchicalSpatialStructurePresenter>();
      }
   }
}