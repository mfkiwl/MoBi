﻿using MoBi.Assets;
using MoBi.Core.Domain.Extensions;
using MoBi.Core.Domain.Model;
using MoBi.Presentation.DTO;
using MoBi.Presentation.UICommand;
using OSPSuite.Assets;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Services;
using OSPSuite.Presentation.Core;
using OSPSuite.Presentation.MenuAndBars;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Presentation.Presenters.ContextMenus;

namespace MoBi.Presentation.MenusAndBars.ContextMenus
{
   public interface IContextMenuForContainer : IContextMenuFor<IContainer>
   {
   }

   public class ContextMenuForContainerBase<TContainer> : ContextMenuFor<TContainer> where TContainer : class, IContainer
   {
      public ContextMenuForContainerBase(IMoBiContext context, IObjectTypeResolver objectTypeResolver) : base(context, objectTypeResolver)
      {
      }

      public override IContextMenu InitializeWith(ObjectBaseDTO dto, IPresenter presenter)
      {
         base.InitializeWith(dto, presenter);
         var container = _context.Get<TContainer>(dto.Id);
         _allMenuItems.Add(CreateAddNewChild<IParameter>(container));
         _allMenuItems.Add(createAddExistingChild<IParameter>(container));
         _allMenuItems.Add(createAddExistingFromTemplateItemFor<IParameter>(container));
         return this;
      }

      protected virtual IMenuBarItem CreateAddExistingItemFor(TContainer selectedObject)
      {
         return createAddExistingChild<TContainer>(selectedObject);
      }

      protected virtual IMenuBarItem CreateAddNewItemFor(TContainer selectedObject)
      {
         return CreateAddNewChild<TContainer>(selectedObject);
      }

      protected override IMenuBarItem CreateDeleteItemFor(TContainer objectToRemove)
      {
         if (objectToRemove.ParentContainer != null)
            return CreateMenuButton.WithCaption(AppConstants.MenuNames.Delete)
               .WithCommandFor<RemoveCommandForContainer, IContainer>(objectToRemove)
               .WithIcon(ApplicationIcons.Delete);

         return CreateMenuButton.WithCaption(AppConstants.MenuNames.Delete)
            .WithCommandFor<RemoveTopContainerCommand, IContainer>(objectToRemove)
            .WithIcon(ApplicationIcons.Delete);
      }

      private IMenuBarItem createAddExistingChild<T>(TContainer container) where T : class, IEntity
      {
         var typeName = _objectTypeResolver.TypeFor<T>();
         return CreateMenuButton.WithCaption(AppConstants.MenuNames.AddExisting(typeName))
            .WithIcon(ApplicationIcons.LoadIconFor(typeName))
            .WithCommandFor<AddExistingCommandFor<IContainer, T>, IContainer>(container);
      }

      private IMenuBarItem createAddExistingFromTemplateItemFor<T>(TContainer parent) where T : class
      {
         var typeName = _objectTypeResolver.TypeFor<T>();
         return CreateMenuButton.WithCaption(AppConstants.MenuNames.AddExistingFromTemplate(typeName))
            .WithIcon(ApplicationIcons.LoadTemplateIconFor(typeName))
            .WithCommandFor<AddExistingFromTemplateCommandFor<IContainer, T>, IContainer>(parent);
      }

      protected IMenuBarItem CreateAddNewChild<T>(TContainer container) where T : class, IEntity
      {
         var typeName = _objectTypeResolver.TypeFor<T>();
         return CreateMenuButton.WithCaption(AppConstants.MenuNames.AddNew(typeName))
            .WithIcon(ApplicationIcons.AddIconFor(typeName))
            .WithCommandFor<AddNewCommandFor<IContainer, T>, IContainer>(container);
      }
   }

   public class ContextMenuForContainer : ContextMenuForContainerBase<IContainer>, IContextMenuForContainer
   {
      public ContextMenuForContainer(IMoBiContext context, IObjectTypeResolver objectTypeResolver) : base(context, objectTypeResolver)
      {
      }

      public override IContextMenu InitializeWith(ObjectBaseDTO dto, IPresenter presenter)
      {
         base.InitializeWith(dto, presenter);
         var container = _context.Get<IContainer>(dto.Id);
         if (!dto.Name.IsSpecialName())
         {
            _allMenuItems.Add(CreateAddNewItemFor(container));
            _allMenuItems.Add(CreateAddExistingItemFor(container));
         }

         _allMenuItems.Add(CreateAddNewChild<IDistributedParameter>(container));
         return this;
      }
   }

   public interface IContextMenuForContainerInEventGroups : IContextMenuFor<IContainer>
   {
   }

   public class ContextMenuForContainerInEventGroups : ContextMenuForContainerBase<IContainer>, IContextMenuForContainerInEventGroups
   {
      public ContextMenuForContainerInEventGroups(IMoBiContext context, IObjectTypeResolver objectTypeResolver) : base(context, objectTypeResolver)
      {
      }

      protected override IMenuBarItem CreateDeleteItemFor(IContainer objectToRemove)
      {
         return CreateMenuButton.WithCaption(AppConstants.MenuNames.Delete)
            .WithIcon(ApplicationIcons.Delete)
            .WithCommandFor<RemoveCommandForContainerAtEventGroup, IContainer>(objectToRemove);
      }
   }
}