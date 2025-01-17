﻿using MoBi.Assets;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.DTO;
using MoBi.Presentation.UICommand;
using OSPSuite.Assets;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Presentation.Core;
using OSPSuite.Presentation.MenuAndBars;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Presentation.Presenters.ContextMenus;
using OSPSuite.Utility.Container;

namespace MoBi.Presentation.MenusAndBars.ContextMenus
{
   public class ContextMenuForExpressionProfileBuildingBlock : ContextMenuForProjectBuildingBlock<ExpressionProfileBuildingBlock>
   {
      public ContextMenuForExpressionProfileBuildingBlock(IMoBiContext context, IObjectTypeResolver objectTypeResolver, IContainer container) : base(context, objectTypeResolver, container)
      {
      }

      public override IContextMenu InitializeWith(ObjectBaseDTO dto, IPresenter presenter)
      {
         var buildingBlock = _context.Get<ExpressionProfileBuildingBlock>(dto.Id);
         base.InitializeWith(dto, presenter);

         _allMenuItems.Add(CreateMenuButton.WithCaption(AppConstants.MenuNames.ExportToExcel)
            .WithIcon(ApplicationIcons.ExportToExcel)
            .WithCommandFor<ExportExpressionProfilesBuildingBlockToExcelUICommand, ExpressionProfileBuildingBlock>(buildingBlock, _container));

         return this;
      }
   }
}