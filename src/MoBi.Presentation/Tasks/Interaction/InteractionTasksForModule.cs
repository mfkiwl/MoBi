using MoBi.Assets;
using MoBi.Core.Commands;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.Presenter;
using MoBi.Presentation.Tasks.Edit;
using OSPSuite.Core.Commands.Core;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;

namespace MoBi.Presentation.Tasks.Interaction
{
   public interface IInteractionTasksForModule : IInteractionTasksForChildren<IMoBiProject, Module>
   {
      void CreateNewModuleWithBuildingBlocks();
      void AddBuildingBlocksToModule(Module module);
   }

   public class InteractionTasksForModule : InteractionTasksForChildren<IMoBiProject, Module>, IInteractionTasksForModule
   {
      public InteractionTasksForModule(IInteractionTaskContext interactionTaskContext, IEditTaskForModule editTask) : base(interactionTaskContext,
         editTask)
      {
      }

      public override IMoBiCommand GetRemoveCommand(Module objectToRemove, IMoBiProject parent, IBuildingBlock buildingBlock)
      {
         return new RemoveModuleCommand(objectToRemove);
      }

      public override IMoBiCommand GetAddCommand(Module itemToAdd, IMoBiProject parent, IBuildingBlock buildingBlock)
      {
         return new AddModuleCommand(itemToAdd);
      }

      public IMoBiCommand GetAddBuildingBlocksToModuleCommand(Module existingModule, Module moduleWithNewBuildingBlocks)
      {
         return new AddBuildingBlocksToModuleCommand(existingModule, moduleWithNewBuildingBlocks);
      }

      protected override void SetAddCommandDescription(Module child, IMoBiProject parent, IMoBiCommand addCommand, MoBiMacroCommand macroCommand,
         IBuildingBlock buildingBlock)
      {
         addCommand.Description = AppConstants.Commands.AddToProjectDescription(addCommand.ObjectType, child.Name);
         macroCommand.Description = addCommand.Description;
      }

      public void CreateNewModuleWithBuildingBlocks()
      {
         using (var presenter = _interactionTaskContext.ApplicationController.Start<ICreateModulePresenter>())
         {
            var module = presenter.CreateModule();

            if (module == null)
               return;

            _interactionTaskContext.Context.AddToHistory(GetAddCommand(module, _interactionTaskContext.Context.CurrentProject, null)
               .Run(_interactionTaskContext.Context));
         }
      }

      public void AddBuildingBlocksToModule(Module module)
      {
         using (var presenter = _interactionTaskContext.ApplicationController.Start<IAddBuildingBlocksToModulePresenter>())
         {
            var moduleWithNewBuildingBlocks = presenter.AddBuildingBlocksToModule(module);

            if (moduleWithNewBuildingBlocks == null)
               return;

            _interactionTaskContext.Context.AddToHistory(GetAddBuildingBlocksToModuleCommand(module, moduleWithNewBuildingBlocks)
               .Run(_interactionTaskContext.Context));
         }
      }
   }
}