﻿using FakeItEasy;
using MoBi.Assets;
using MoBi.Core.Domain.Builder;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Mappers;
using OSPSuite.Assets;
using OSPSuite.BDDHelper;
using OSPSuite.BDDHelper.Extensions;
using OSPSuite.Core.Domain;

namespace MoBi.Presentation.Mapper
{
   public class concern_for_CreateModuleDTOToModuleMapper : ContextSpecification<CreateModuleDTOToModuleMapper>
   {
      protected IMoBiSpatialStructureFactory _spatialStructureFactory;
      protected IReactionBuildingBlockFactory _reactionBuildingBlockFactory;
      private IMoBiContext _context;

      protected override void Context()
      {
         _context = A.Fake<IMoBiContext>();
         _spatialStructureFactory = A.Fake<IMoBiSpatialStructureFactory>();
         _reactionBuildingBlockFactory = A.Fake<IReactionBuildingBlockFactory>();
         sut = new CreateModuleDTOToModuleMapper(_context, _reactionBuildingBlockFactory, _spatialStructureFactory);
      }
   }

   public class When_mapping_dto_to_module_with_no_building_blocks : concern_for_CreateModuleDTOToModuleMapper
   {
      private ModuleContentDTO _dto;
      private Module _result;

      protected override void Context()
      {
         base.Context();
         _dto = new ModuleContentDTO
         {
            WithReaction = false,
            WithEventGroup = false,
            WithSpatialStructure = false,
            WithMolecule = false,
            WithObserver = false,
            WithPassiveTransport = false,
            WithParameterValues = false,
            WithInitialConditions = false
         };
      }

      protected override void Because()
      {
         _result = sut.MapFrom(_dto);
      }

      [Observation]
      public void the_module_should_contain_no_building_blocks()
      {
         _result.SpatialStructure.ShouldBeNull();
         _result.Reactions.ShouldBeNull();
         _result.EventGroups.ShouldBeNull();
         _result.InitialConditionsCollection.ShouldBeEmpty();
         _result.ParameterValuesCollection.ShouldBeEmpty();
         _result.PassiveTransports.ShouldBeNull();
         _result.Observers.ShouldBeNull();
         _result.Molecules.ShouldBeNull();
      }
   }

   public class When_mapping_dto_to_module_with_all_building_blocks : concern_for_CreateModuleDTOToModuleMapper
   {
      private ModuleContentDTO _dto;
      private Module _result;

      protected override void Context()
      {
         base.Context();
         _dto = new ModuleContentDTO
         {
            WithReaction = true,
            WithEventGroup = true,
            WithSpatialStructure = true,
            WithMolecule = true,
            WithObserver = true,
            WithPassiveTransport = true,
            WithParameterValues = true,
            WithInitialConditions = true
         };
      }

      protected override void Because()
      {
         _result = sut.MapFrom(_dto);
      }

      [Observation]
      public void the_module_should_contain_all_building_blocks()
      {
         _result.SpatialStructure.ShouldNotBeNull();
         _result.Reactions.ShouldNotBeNull();
         _result.EventGroups.ShouldNotBeNull();
         _result.InitialConditionsCollection.ShouldNotBeEmpty();
         _result.ParameterValuesCollection.ShouldNotBeEmpty();
         _result.PassiveTransports.ShouldNotBeNull();
         _result.Observers.ShouldNotBeNull();
         _result.Molecules.ShouldNotBeNull();
      }

      [Observation]
      public void should_create_a_new_reaction_building_block()
      {
         A.CallTo(() => _reactionBuildingBlockFactory.Create()).MustHaveHappened();
      }

      [Observation]
      public void should_create_a_new_spatial_structure()
      {
         A.CallTo(() => _spatialStructureFactory.CreateDefault(null)).MustHaveHappened();
      }

   }
}
