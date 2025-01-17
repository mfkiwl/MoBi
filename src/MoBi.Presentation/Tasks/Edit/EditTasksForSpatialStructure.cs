﻿using System.Collections.Generic;
using System.Linq;
using MoBi.Assets;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.Presenter;
using MoBi.Presentation.Tasks.Interaction;
using OSPSuite.Core.Commands.Core;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Utility.Extensions;

namespace MoBi.Presentation.Tasks.Edit
{
   public interface IEditTasksForSpatialStructure : IEditTasksForBuildingBlock<MoBiSpatialStructure>
   {
   }

   public class EditTasksForSpatialStructure : EditTasksForBuildingBlock<MoBiSpatialStructure>, IEditTasksForSpatialStructure
   {
      public EditTasksForSpatialStructure(IInteractionTaskContext interactionTaskContext) : base(interactionTaskContext)
      {
      }

      public override bool EditEntityModal(MoBiSpatialStructure entity, IEnumerable<IObjectBase> existingObjectsInParent, ICommandCollector commandCollector, IBuildingBlock buildingBlock)
      {
         // we edit the properties of the top container, not of Spatial Structure.
         var topContainer = entity.TopContainers.First();
         bool resolved;
         using (var modalPresenter = _applicationController.GetCreateViewFor(topContainer, commandCollector))
         {
            modalPresenter.Text = AppConstants.Captions.NewWindow(ObjectName);
            var editContainerPresenter = modalPresenter.SubPresenter.DowncastTo<IEditContainerPresenter>();
            editContainerPresenter.BuildingBlock = entity;
            editContainerPresenter.Edit(topContainer);
            resolved = modalPresenter.Show();
            //Set SpatialStructure name to container to have them more speaking
            entity.Name = topContainer.Name;
         }

         return resolved;
      }
   }
}