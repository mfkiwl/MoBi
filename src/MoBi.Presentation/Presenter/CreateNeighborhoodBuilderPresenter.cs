﻿using System.Collections.Generic;
using MoBi.Assets;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Tasks.Edit;
using MoBi.Presentation.Views;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Presentation.Presenters;

namespace MoBi.Presentation.Presenter
{
   public interface ICreateNeighborhoodBuilderPresenter : ICreatePresenter<NeighborhoodBuilder>, IPresenterWithBuildingBlock
   {
      void UpdateName(string name);
   }

   public class CreateNeighborhoodBuilderPresenter : AbstractCommandCollectorPresenter<ICreateNeighborhoodBuilderView, ICreateNeighborhoodBuilderPresenter>, ICreateNeighborhoodBuilderPresenter
   {
      private readonly IEditTaskFor<NeighborhoodBuilder> _editTask;
      private readonly ISelectNeighborPathPresenter _firstNeighborPresenter;
      private readonly ISelectNeighborPathPresenter _secondNeighborPresenter;
      private ObjectBaseDTO _objectBaseDTO;
      private NeighborhoodBuilder _neighborhoodBuilder;
      public IBuildingBlock BuildingBlock { get; set; }

      public CreateNeighborhoodBuilderPresenter(ICreateNeighborhoodBuilderView view,
         IEditTaskFor<NeighborhoodBuilder> editTask,
         ISelectNeighborPathPresenter firstNeighborPresenter,
         ISelectNeighborPathPresenter secondNeighborPresenter) : base(view)
      {
         _editTask = editTask;
         _firstNeighborPresenter = firstNeighborPresenter;
         _secondNeighborPresenter = secondNeighborPresenter;
         AddSubPresenters(_firstNeighborPresenter, _secondNeighborPresenter);
         _view.AddFirstNeighborView(_firstNeighborPresenter.BaseView);
         _view.AddSecondNeighborView(_secondNeighborPresenter.BaseView);

         _firstNeighborPresenter.StatusChanged += (o, e) => updateFirstNeighbor();
         _secondNeighborPresenter.StatusChanged += (o, e) => updateSecondNeighbor();
      }

      private void updateSecondNeighbor()
      {
         _neighborhoodBuilder.SecondNeighborPath = _secondNeighborPresenter.NeighborPath;
      }

      private void updateFirstNeighbor()
      {
         _neighborhoodBuilder.FirstNeighborPath = _firstNeighborPresenter.NeighborPath;
      }

      public void Edit(NeighborhoodBuilder neighborhoodBuilder, IReadOnlyList<IObjectBase> existingObjectsInParent)
      {
         _objectBaseDTO = new ObjectBaseDTO(neighborhoodBuilder);
         _neighborhoodBuilder = neighborhoodBuilder;
         _objectBaseDTO.AddUsedNames(_editTask.GetForbiddenNamesWithoutSelf(neighborhoodBuilder, existingObjectsInParent));
         _firstNeighborPresenter.Init(spatialStructure, AppConstants.Captions.FirstNeighbor);
         _secondNeighborPresenter.Init(spatialStructure, AppConstants.Captions.SecondNeighbor);

         _view.BindTo(_objectBaseDTO);
      }

      public void UpdateName(string name)
      {
         _neighborhoodBuilder.Name = name;
      }

      private SpatialStructure spatialStructure => BuildingBlock as SpatialStructure;
   }
}