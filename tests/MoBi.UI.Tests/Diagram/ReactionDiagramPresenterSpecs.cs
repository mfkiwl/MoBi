﻿using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using MoBi.Core.Domain.Model;
using MoBi.Core.Domain.Model.Diagram;
using MoBi.Core.Domain.Repository;
using MoBi.Core.Services;
using MoBi.Presentation;
using MoBi.Presentation.Presenter;
using MoBi.Presentation.Settings;
using MoBi.Presentation.Tasks.Interaction;
using MoBi.Presentation.UICommand;
using MoBi.Presentation.Views.BaseDiagram;
using MoBi.UI.Diagram.DiagramManagers;
using MoBi.UI.Presenters;
using Northwoods.Go;
using NUnit.Framework;
using OSPSuite.BDDHelper;
using OSPSuite.BDDHelper.Extensions;
using OSPSuite.Core;
using OSPSuite.Core.Commands.Core;
using OSPSuite.Core.Diagram;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Core.Services;
using OSPSuite.Presentation.Diagram.Elements;
using OSPSuite.Presentation.Services;
using OSPSuite.UI.Diagram.Elements;
using OSPSuite.Utility.Extensions;

namespace MoBi.UI.Diagram
{
   public abstract class concern_for_ReactionDiagramPresenter : ContextSpecification<ReactionDiagramPresenter>
   {
      protected IReactionDiagramView _reactionDiagramView;
      private IContainerBaseLayouter _containerBaseLayouter;
      protected IMoBiContext _moBiContext;
      private IUserSettings _userSettings;
      protected IDialogCreator _dialogCreator;
      protected IMoBiApplicationController _moBiApplicationController;
      private IDiagramTask _diagramTask;
      private IDiagramLayoutTask _diagramLayoutTask;
      private ICommandCollector _commandCollector;
      private IStartOptions _runOptions;
      private IDiagramModelFactory _diagramModelFactory;
      private IMoBiProjectRetriever _moBiProjectRetriever;
      private BuildingBlockRepository _buildingBlockRepository;

      protected override void Context()
      {
         _reactionDiagramView = A.Fake<IReactionDiagramView>();
         _containerBaseLayouter = A.Fake<IContainerBaseLayouter>();
         _moBiContext = A.Fake<IMoBiContext>();
         _userSettings = A.Fake<IUserSettings>();
         _dialogCreator = A.Fake<IDialogCreator>();
         _moBiApplicationController = A.Fake<IMoBiApplicationController>();
         _diagramTask = A.Fake<IDiagramTask>();
         _diagramLayoutTask = A.Fake<IDiagramLayoutTask>();
         _commandCollector = A.Fake<ICommandCollector>();
         _runOptions = A.Fake<IStartOptions>();
         _diagramModelFactory = A.Fake<IDiagramModelFactory>();
         _moBiProjectRetriever = new MoBiProjectRetriever(_moBiContext);
         _buildingBlockRepository = new BuildingBlockRepository(_moBiProjectRetriever);
         
         sut = new ReactionDiagramPresenter(_reactionDiagramView, _containerBaseLayouter, _moBiContext, _userSettings,
            _dialogCreator, _moBiApplicationController, _diagramTask, _diagramLayoutTask, _runOptions, _diagramModelFactory, _buildingBlockRepository);

         sut.InitializeWith(_commandCollector);
      }
   }

   public class When_adding_molecule_to_the_diagram : concern_for_ReactionDiagramPresenter
   {
      private IMultipleStringSelectionPresenter _multipleStringSelectionPresenter;
      private IEnumerable<string> _possibleMoleculeNames;
      private MoBiProject _moBiProject;

      protected override void Context()
      {
         base.Context();
         _multipleStringSelectionPresenter = A.Fake<IMultipleStringSelectionPresenter>();
         A.CallTo(() => _moBiApplicationController.Start<IMultipleStringSelectionPresenter>()).Returns(_multipleStringSelectionPresenter);

         A.CallTo(() => _multipleStringSelectionPresenter.Show(A<string>._, A<string>._, A<IEnumerable<string>>._, A<string>._, true)).Invokes(x => _possibleMoleculeNames = x.GetArgument<IEnumerable<string>>(2)).Returns(Enumerable.Empty<string>());

         _moBiProject = new MoBiProject();
         _moBiProject.AddModule(new Module { new MoleculeBuildingBlock { new MoleculeBuilder { Name = "b" }, new MoleculeBuilder { Name = "a" } } });
         _moBiProject.AddModule(new Module { new MoleculeBuildingBlock { new MoleculeBuilder { Name = "a" }, new MoleculeBuilder { Name = "b" } } });
         
         A.CallTo(() => _moBiContext.CurrentProject).Returns(_moBiProject);
      }

      protected override void Because()
      {
         sut.AddMoleculeNode();
      }

      [Test]
      public void should_show_a_list_of_unique_names_and_those_names_should_be_ordered_alphabetically()
      {
         _possibleMoleculeNames.ShouldOnlyContainInOrder("a", "b");
      }
   }

   public abstract class When_deleteing_nodes_from_a_reaction_building_block : concern_for_ReactionDiagramPresenter
   {
      protected IReadOnlyList<GoObject> _objectsToRemove;
      protected IMoleculeNode _moleculeNode;
      protected ReactionNode _reactionNode;
      protected MoBiReactionBuildingBlock _reactionBuildingBlock;
      private IReactionDiagramManager<MoBiReactionBuildingBlock> _moBiReactionDiagramManager;
      private MoleculeBuilder _molecule;
      protected ReactionBuilder _reaction;
      private IInteractionTasksForChildren<MoBiReactionBuildingBlock, ReactionBuilder> _interactionTask;
      private IActiveSubjectRetriever _activeSubjectRetriever;

      protected override void Context()
      {
         base.Context();
         _reactionBuildingBlock = new MoBiReactionBuildingBlock
         {
            DiagramModel = new DiagramModel(),
            DiagramManager = new MoBiReactionDiagramManager()
         };

         _moBiReactionDiagramManager = _reactionBuildingBlock.DiagramManager.DowncastTo<IReactionDiagramManager<MoBiReactionBuildingBlock>>();

         _moBiReactionDiagramManager.InitializeWith(_reactionBuildingBlock, A.Fake<IDiagramOptions>());

         _molecule = new MoleculeBuilder().WithId("moleculeId").WithName("moleculeName");
         _reaction = new ReactionBuilder().WithId("reactionId");
         _reaction.AddEduct(new ReactionPartnerBuilder(_molecule.Name, 1.0));
         _reactionBuildingBlock.Add(_reaction);
         _moBiReactionDiagramManager.AddObjectBase(_reaction);
         _moBiReactionDiagramManager.AddMolecule(_reaction, "moleculeName");
         _moleculeNode = _moBiReactionDiagramManager.GetMoleculeNodes("moleculeName").FirstOrDefault();
         _reactionNode = _moBiReactionDiagramManager.PkModel.DiagramModel.GetAllChildren<ReactionNode>().FirstOrDefault();
         _interactionTask = A.Fake<IInteractionTasksForChildren<MoBiReactionBuildingBlock, ReactionBuilder>>();
         _activeSubjectRetriever = A.Fake<IActiveSubjectRetriever>();

         var reactionLink = new ReactionLink();

         reactionLink.Initialize(ReactionLinkType.Educt, _reactionNode, _moleculeNode);

         sut.Edit(_reactionBuildingBlock);

         var removeReactionCommand = new RemoveCommandFor<MoBiReactionBuildingBlock, ReactionBuilder>(_interactionTask, _moBiContext, _activeSubjectRetriever);
         A.CallTo(() => _moBiContext.Get<ReactionBuilder>(_reaction.Id)).Returns(_reaction);
         A.CallTo(() => _moBiContext.Resolve<RemoveCommandFor<MoBiReactionBuildingBlock, ReactionBuilder>>()).Returns(removeReactionCommand);
         A.CallTo(() => _activeSubjectRetriever.Active<IBuildingBlock>()).Returns(_reactionBuildingBlock);
         A.CallTo(() => _interactionTask.Remove(_reaction, _reactionBuildingBlock, _reactionBuildingBlock, A<bool>._)).Invokes(x => _reactionBuildingBlock.Remove(_reaction));
      }
   }

   public class When_deleting_a_node_that_is_connected_to_a_reaction_that_will_not_be_removed : When_deleteing_nodes_from_a_reaction_building_block
   {
      protected override void Context()
      {
         base.Context();
         _objectsToRemove = new List<GoObject> { _moleculeNode as MoleculeNode };
      }

      protected override void Because()
      {
         sut.RemoveSelection(_objectsToRemove);
      }

      [Observation]
      public void the_user_should_be_informed_of_the_requirement_to_remove_reactions()
      {
         A.CallTo(() => _dialogCreator.MessageBoxInfo(A<string>._)).MustHaveHappened();
      }

      [Observation]
      public void the_building_block_should_not_have_objects_removed()
      {
         _reactionBuildingBlock.ShouldContain(_reaction);
      }
   }

   public class When_deleting_a_molecule_node_that_is_connected_to_a_reaction_node_in_the_diagram : When_deleteing_nodes_from_a_reaction_building_block
   {
      protected override void Context()
      {
         base.Context();
         _objectsToRemove = new List<GoObject> { _reactionNode, _moleculeNode as MoleculeNode };
      }

      protected override void Because()
      {
         sut.RemoveSelection(_objectsToRemove);
      }

      [Observation]
      public void the_building_block_should_have_objects_removed()
      {
         _reactionBuildingBlock.ShouldNotContain(_reaction);
      }
   }

   public class When_the_reaction_diagram_presenter_is_asked_to_select_a_reaction : concern_for_ReactionDiagramPresenter
   {
      private ReactionBuilder _reaction;
      private IReactionNode _reactionNode;
      private IMoBiReactionDiagramManager _reactionDiagramManager;
      private MoBiReactionBuildingBlock _reactionBuildingBlock;

      protected override void Context()
      {
         base.Context();
         _reaction = A.Fake<ReactionBuilder>();
         _reactionNode = A.Fake<IReactionNode>();
         _reactionDiagramManager = A.Fake<IMoBiReactionDiagramManager>();
         _reactionBuildingBlock = new MoBiReactionBuildingBlock
         {
            DiagramManager = _reactionDiagramManager
         };
         sut.Edit(_reactionBuildingBlock);

         A.CallTo(() => _reactionDiagramManager.ReactionNodeFor(_reaction)).Returns(_reactionNode);
      }

      protected override void Because()
      {
         sut.Select(_reaction);
      }

      [Observation]
      public void it_should_deselect_and_previous_selection()
      {
         A.CallTo(() => _reactionDiagramView.ClearSelection()).MustHaveHappened();
      }

      [Observation]
      public void it_should_select_the_node_corresponding_to_the_reaction()
      {
         A.CallTo(() => _reactionDiagramView.Select(_reactionNode)).MustHaveHappened();
      }
   }
}