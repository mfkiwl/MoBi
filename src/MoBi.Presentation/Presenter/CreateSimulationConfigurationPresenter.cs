using System.Collections.Generic;
using MoBi.Core.Commands;
using MoBi.Core.Domain.Model;
using MoBi.Core.Services;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Mappers;
using MoBi.Presentation.Presenter.Simulation;
using MoBi.Presentation.Settings;
using MoBi.Presentation.Views;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Builder;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Core.Services;
using OSPSuite.Presentation.Core;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Utility.Extensions;

namespace MoBi.Presentation.Presenter
{
   public interface ICreateSimulationConfigurationPresenter : IWizardPresenter, IPresenter<ICreateSimulationConfigurationView>
   {
      SimulationConfiguration CreateBasedOn(IMoBiSimulation simulation, bool isNew = true);
      string SimulationName { get; }
   }

   public class CreateSimulationConfigurationPresenter : WizardPresenter<ICreateSimulationConfigurationView, ICreateSimulationConfigurationPresenter, ISimulationConfigurationItemPresenter>, ICreateSimulationConfigurationPresenter
   {
      private SimulationDTO _simulationDTO;
      private readonly IForbiddenNamesRetriever _forbiddenNamesRetriever;
      protected readonly IUserSettings _userSettings;
      private readonly IModuleConfigurationDTOToModuleConfigurationMapper _moduleConfigurationMapper;
      private readonly ISimulationConfigurationFactory _simulationConfigurationFactory;
      private readonly ICloneManagerForBuildingBlock _cloneManager;

      public CreateSimulationConfigurationPresenter(
         ICreateSimulationConfigurationView view,
         ISubPresenterItemManager<ISimulationConfigurationItemPresenter> subPresenterManager,
         IDialogCreator dialogCreator,
         IForbiddenNamesRetriever forbiddenNamesRetriever,
         IUserSettings userSettings,
         IModuleConfigurationDTOToModuleConfigurationMapper moduleConfigurationMapper,
         ISimulationConfigurationFactory simulationConfigurationFactory,
         ICloneManagerForBuildingBlock cloneManager)
         : base(view, subPresenterManager, SimulationItems.All, dialogCreator)
      {
         _forbiddenNamesRetriever = forbiddenNamesRetriever;
         IMoBiMacroCommand commands = new MoBiMacroCommand();
         _moduleConfigurationMapper = moduleConfigurationMapper;
         _simulationConfigurationFactory = simulationConfigurationFactory;
         _cloneManager = cloneManager;
         _userSettings = userSettings;
         InitializeWith(commands);
         AllowQuickFinish = false;
      }

      public SimulationConfiguration CreateBasedOn(IMoBiSimulation moBiSimulation, bool isNew = true)
      {
         edit(moBiSimulation, isNew);
         UpdateControls();
         _view.Display();
         if (_view.Canceled)
         {
            return null;
         }

         // When creating a simulationConfiguration based on an existing simulation, use the settings from the existing
         // simulation instead of creating with project default simulation settings
         var simulationConfiguration = _simulationConfigurationFactory.Create(settings: isNew ? null : _cloneManager.Clone(moBiSimulation.Settings));

         updateSimulationConfiguration(simulationConfiguration);
         return simulationConfiguration;
      }

      public string SimulationName => _simulationDTO.Name;

      private void edit(IMoBiSimulation simulation, bool allowNaming)
      {
         _simulationDTO = new SimulationDTO(simulation).WithName(simulation.Name);
         _simulationDTO.CreateAllProcessRateParameters = simulation.Configuration.CreateAllProcessRateParameters;
         
         if (allowNaming)
            _simulationDTO.AddUsedNames(nameOfSimulationAlreadyUsed(simulation));
         else
            _view.DisableNaming();
         _subPresenterItemManager.AllSubPresenters.Each(x => x.Edit(simulation.Configuration));
         _view.BindTo(_simulationDTO);
      }

      private IEnumerable<string> nameOfSimulationAlreadyUsed(IMoBiSimulation simulation)
      {
         return _forbiddenNamesRetriever.For(simulation);
      }

      protected override void UpdateControls(int currentIndex)
      {
         if (currentIndex == FirstIndex)
         {
            View.NextEnabled = PresenterAt(SimulationItems.ModuleConfiguration).CanClose;
         }

         View.OkEnabled = CanClose;
      }

      private void updateSimulationConfiguration(SimulationConfiguration simulationConfiguration)
      {
         var moduleConfigurations = PresenterAt(SimulationItems.ModuleConfiguration).ModuleConfigurationDTOs.MapAllUsing(_moduleConfigurationMapper);
         var individualAndExpressionPresenter = PresenterAt(SimulationItems.IndividualAndExpressionConfiguration);
         var selectedExpressions = individualAndExpressionPresenter.ExpressionProfiles;
         var selectedIndividual = individualAndExpressionPresenter.SelectedIndividual;

         moduleConfigurations.Each(simulationConfiguration.AddModuleConfiguration);
         simulationConfiguration.Individual = selectedIndividual;
         selectedExpressions.Each(simulationConfiguration.AddExpressionProfile);

         simulationConfiguration.ShouldValidate = true;
         simulationConfiguration.PerformCircularReferenceCheck = _userSettings.CheckCircularReference;

         simulationConfiguration.CreateAllProcessRateParameters = _simulationDTO.CreateAllProcessRateParameters;
      }
   }
}