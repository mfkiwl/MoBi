using MoBi.Core.Domain.Model;
using MoBi.Presentation.Tasks.Interaction;
using OSPSuite.Core.Domain;
using OSPSuite.Presentation.UICommands;

namespace MoBi.Presentation.UICommand
{
   public class LoadBuildingBlockToModuleUICommand : ObjectUICommand<Module> //maybe just UICommand
   {
      private readonly IInteractionTasksForModule _interactionTasks;

      public LoadBuildingBlockToModuleUICommand(IInteractionTasksForModule interactionTasksForModule)
      {
         _interactionTasks = interactionTasksForModule;
      }

      protected override void PerformExecute()
      {
         _interactionTasks.LoadBuildingBlockToModule(Subject);
      }
   }
}