﻿using System.Collections.Generic;
using System.Linq;
using MoBi.Core.Domain.Services;
using MoBi.Presentation.Tasks.Edit;
using OSPSuite.Core.Domain.Builder;

namespace MoBi.Presentation.Tasks.Interaction
{
   public interface IInteractionTasksForExpressionProfileBuildingBlock : IInteractionTasksForBuildingBlock<ExpressionProfileBuildingBlock>, IInteractionTasksForPathAndValueEntity<ExpressionProfileBuildingBlock, ExpressionParameter>
   {
      IReadOnlyList<ExpressionProfileBuildingBlock> LoadFromPKML();
   }

   public class InteractionTasksForExpressionProfileBuildingBlock : InteractionTasksForPathAndValueEntity<ExpressionProfileBuildingBlock, ExpressionParameter>, IInteractionTasksForExpressionProfileBuildingBlock
   {
      public InteractionTasksForExpressionProfileBuildingBlock(IInteractionTaskContext interactionTaskContext, IEditTasksForBuildingBlock<ExpressionProfileBuildingBlock> editTask, IMoBiFormulaTask formulaTask) :
         base(interactionTaskContext, editTask, formulaTask)
      {
      }

      public IReadOnlyList<ExpressionProfileBuildingBlock> LoadFromPKML()
      {
         var filename = AskForPKMLFileToOpen();
         return (string.IsNullOrEmpty(filename) ? Enumerable.Empty<ExpressionProfileBuildingBlock>() : LoadItems(filename)).ToList();
      }
   }
}