﻿using System.Collections.Generic;
using System.Linq;
using MoBi.Core.Domain.Repository;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Mappers;
using MoBi.Presentation.Presenter.Simulation;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Presentation.Nodes;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Presentation.Views;
using OSPSuite.Utility.Extensions;
using ITreeNodeFactory = MoBi.Presentation.Nodes.ITreeNodeFactory;

namespace MoBi.Presentation.Presenter
{
   public interface IEditIndividualAndExpressionConfigurationsPresenter : ISimulationConfigurationItemPresenter, ICanDragDropPresenter
   {
      void ProjectExpressionSelectionChanged(ITreeNode selectedNode);
      void SimulationExpressionSelectionChanged(ITreeNode selectedNode);
      void RemoveSelectedExpression(ITreeNode selectedNode);
      void AddSelectedExpression(ITreeNode selectedNode);
      int CompareSelectedNodes(ITreeNode node1, ITreeNode node2);
      IndividualBuildingBlock SelectedIndividual { get; }
      IReadOnlyList<ExpressionProfileBuildingBlock> ExpressionProfiles { get; }
   }

   public class EditIndividualAndExpressionConfigurationsPresenter : AbstractSubPresenter<IEditIndividualAndExpressionConfigurationsView, IEditIndividualAndExpressionConfigurationsPresenter>, IEditIndividualAndExpressionConfigurationsPresenter
   {
      private readonly ISelectedIndividualToIndividualSelectionDTOMapper _selectedIndividualDTOMapper;
      private readonly ITreeNodeFactory _treeNodeFactory;
      private IndividualSelectionDTO _individualSelectionDTO;
      private readonly IBuildingBlockRepository _buildingBlockRepository;
      private readonly List<ExpressionProfileBuildingBlock> _selectedExpressions;

      public EditIndividualAndExpressionConfigurationsPresenter(IEditIndividualAndExpressionConfigurationsView view, ISelectedIndividualToIndividualSelectionDTOMapper selectedIndividualDTOMapper,
         ITreeNodeFactory treeNodeFactory, IBuildingBlockRepository buildingBlockRepository) : base(view)
      {
         _selectedExpressions = new List<ExpressionProfileBuildingBlock>();
         _selectedIndividualDTOMapper = selectedIndividualDTOMapper;
         _treeNodeFactory = treeNodeFactory;
         _buildingBlockRepository = buildingBlockRepository;
      }

      public void Edit(SimulationConfiguration simulationConfiguration)
      {
         _individualSelectionDTO = _selectedIndividualDTOMapper.MapFrom(simulationConfiguration.Individual);

         addUnusedExpressionsToSelectionView();
         addUsedExpressionsToSelectedView(simulationConfiguration);

         _view.BindTo(_individualSelectionDTO);
      }

      private void addUsedExpressionsToSelectedView(SimulationConfiguration simulationConfiguration)
      {
         simulationConfiguration.ExpressionProfiles.Each(profile =>
         {
            var projectProfile = _buildingBlockRepository.ExpressionProfileByName(profile.Name);
            addUsedExpressionToSelectedView(projectProfile);
            _selectedExpressions.Add(projectProfile);
         });
      }

      private void addUsedExpressionToSelectedView(ExpressionProfileBuildingBlock expression)
      {
         _view.AddUsedExpression(_treeNodeFactory.CreateFor(expression));
      }

      private void addUnusedExpressionsToSelectionView()
      {
         _buildingBlockRepository.ExpressionProfileCollection.Where(x => !_selectedExpressions.ExistsByName(x.Name)).Each(addUnusedExpressionToSelectionView);
      }

      private void addUnusedExpressionToSelectionView(ExpressionProfileBuildingBlock expression)
      {
         _view.AddUnusedExpression(_treeNodeFactory.CreateFor(expression));
      }

      public void ProjectExpressionSelectionChanged(ITreeNode selectedNode)
      {
         _view.EnableAdd = selectedNode != null;
      }

      public void SimulationExpressionSelectionChanged(ITreeNode selectedNode)
      {
         _view.EnableRemove = selectedNode != null;
      }

      public void RemoveSelectedExpression(ITreeNode selectedNode)
      {
         if (!(selectedNode.TagAsObject is ExpressionProfileBuildingBlock expression))
            return;

         _selectedExpressions.Remove(expression);
         addUnusedExpressionToSelectionView(expression);
         _view.RemoveUsedExpression(selectedNode);
      }

      public void AddSelectedExpression(ITreeNode selectedNode)
      {
         if (!(selectedNode.TagAsObject is ExpressionProfileBuildingBlock expression))
            return;

         _selectedExpressions.Add(expression);
         addUsedExpressionToSelectedView(expression);
         _view.RemoveUnusedExpression(selectedNode);
      }

      public int CompareSelectedNodes(ITreeNode node1, ITreeNode node2)
      {
         var expression1 = node1.TagAsObject as ExpressionProfileBuildingBlock;
         var expression2 = node2.TagAsObject as ExpressionProfileBuildingBlock;

         return _selectedExpressions.IndexOf(expression1) - _selectedExpressions.IndexOf(expression2);
      }

      public IndividualBuildingBlock SelectedIndividual => _individualSelectionDTO.IsNull() ? null : _individualSelectionDTO.SelectedIndividualBuildingBlock;
      public IReadOnlyList<ExpressionProfileBuildingBlock> ExpressionProfiles => _selectedExpressions;

      public bool CanDrag(ITreeNode node)
      {
         return node.TagAsObject is ExpressionProfileBuildingBlock;
      }

      public bool CanDrop(ITreeNode dragNode, ITreeNode targetNode)
      {
         return targetNode?.TagAsObject is ExpressionProfileBuildingBlock;
      }

      public void MoveNode(ITreeNode dragNode, ITreeNode targetNode)
      {
         var movingExpression = dragNode.TagAsObject as ExpressionProfileBuildingBlock;
         var targetExpression = targetNode.TagAsObject as ExpressionProfileBuildingBlock;

         _selectedExpressions.Remove(movingExpression);
         _selectedExpressions.Insert(_selectedExpressions.IndexOf(targetExpression), movingExpression);
         _view.SortSelectedExpressions();
      }
   }

   public interface IEditIndividualAndExpressionConfigurationsView : IView<IEditIndividualAndExpressionConfigurationsPresenter>
   {
      void BindTo(IndividualSelectionDTO individualSelectionDTO);
      void AddUnusedExpression(ITreeNode treeNodeToAdd);
      void AddUsedExpression(ITreeNode treeNodeToAdd);
      bool EnableAdd { get; set; }
      bool EnableRemove { get; set; }
      void RemoveUsedExpression(ITreeNode selectedNode);
      void RemoveUnusedExpression(ITreeNode selectedNode);
      void SortSelectedExpressions();
   }
}