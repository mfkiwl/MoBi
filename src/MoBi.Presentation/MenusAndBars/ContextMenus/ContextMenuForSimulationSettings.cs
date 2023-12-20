﻿using System.Collections.Generic;
using MoBi.Assets;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.DTO;
using MoBi.Presentation.UICommand;
using OSPSuite.Assets;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Presentation.Core;
using OSPSuite.Presentation.MenuAndBars;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Presentation.Presenters.ContextMenus;
using OSPSuite.Utility.Container;
using OSPSuite.Utility.Extensions;

namespace MoBi.Presentation.MenusAndBars.ContextMenus
{
   public class ContextMenuForSimulationSettings : ContextMenuBase
   {
      private readonly List<IMenuBarItem> _allMenuItems = new List<IMenuBarItem>();
      private readonly IContainer _container;

      public ContextMenuForSimulationSettings(IContainer container)
      {
         _container = container;
      }

      public override IEnumerable<IMenuBarItem> AllMenuItems() => _allMenuItems;

      public IContextMenu InitializeWith(SimulationSettingsDTO viewItem, IPresenterWithContextMenu<IViewItem> presenter)
      {
         _allMenuItems.Add(CreateMenuButton.WithCaption(AppConstants.MenuNames.RefreshSettingsFromProjectDefault)
            .WithIcon(ApplicationIcons.Refresh)
            .WithCommandFor<RefreshSimulationSettingsUICommand, IMoBiSimulation>(viewItem.Simulation, _container));

         _allMenuItems.Add(CreateMenuButton.WithCaption(AppConstants.MenuNames.MakeSettingsProjectDefault)
            .WithIcon(ApplicationIcons.Commit)
            .WithCommandFor<CommitSimulationSettingsUICommand, SimulationSettings>(viewItem.Simulation.Settings, _container));

         return this;
      }
   }

   public class ContextMenuSpecificationFactoryForSimulationSettingsViewItem : IContextMenuSpecificationFactory<IViewItem>
   {
      private readonly IContainer _container;

      public ContextMenuSpecificationFactoryForSimulationSettingsViewItem(IContainer container)
      {
         _container = container;
      }

      public IContextMenu CreateFor(IViewItem viewItem, IPresenterWithContextMenu<IViewItem> presenter)
      {
         var contextMenu = new ContextMenuForSimulationSettings(_container);
         return contextMenu.InitializeWith(viewItem.DowncastTo<SimulationSettingsDTO>(), presenter);
      }

      public bool IsSatisfiedBy(IViewItem viewItem, IPresenterWithContextMenu<IViewItem> presenter) => viewItem is SimulationSettingsDTO;
   }
}