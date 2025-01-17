﻿using MoBi.Assets;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.DTO;
using MoBi.Presentation.UICommand;
using OSPSuite.Assets;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Core.Extensions;
using OSPSuite.Presentation.Core;
using OSPSuite.Presentation.MenuAndBars;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Presentation.Presenters.ContextMenus;
using OSPSuite.Utility.Container;

namespace MoBi.Presentation.MenusAndBars.ContextMenus
{
   public class ContextMenuForProjectBuildingBlock<TBuildingBlock> : ContextMenuForBuildingBlock<TBuildingBlock> where TBuildingBlock : class, IBuildingBlock
   {
      public ContextMenuForProjectBuildingBlock(IMoBiContext context, IObjectTypeResolver objectTypeResolver, IContainer container) : base(context, objectTypeResolver, container)
      {
      }

      public override IContextMenu InitializeWith(ObjectBaseDTO dto, IPresenter presenter)
      {
         base.InitializeWith(dto, presenter);
         var buildingBlock = _context.Get<TBuildingBlock>(dto.Id);
         _allMenuItems.Add(createCloneMenuItem(buildingBlock));
         return this;
      }

      protected override IMenuBarItem CreateDeleteItemFor(TBuildingBlock objectBase)
      {
         return CreateMenuButton.WithCaption(AppConstants.MenuNames.Delete)
            .WithIcon(ApplicationIcons.Delete)
            .WithRemoveCommand(_context.CurrentProject, objectBase);
      }

      private IMenuBarItem createCloneMenuItem(TBuildingBlock buildingBlock)
      {
         return CreateMenuButton.WithCaption(AppConstants.MenuNames.Clone.WithEllipsis())
            .WithIcon(ApplicationIcons.Clone)
            .WithCommandFor<CloneProjectBuildingBlockUICommand<TBuildingBlock>, TBuildingBlock>(buildingBlock, _container);
      }
   }
}