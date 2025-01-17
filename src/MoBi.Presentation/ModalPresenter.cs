﻿using System;
using System.Drawing;
using MoBi.Presentation.Views;
using OSPSuite.Presentation.Presenters;
using OSPSuite.Utility.Events;

namespace MoBi.Presentation
{
   public interface IModalPresenter : IDisposablePresenter
   {
      void Encapsulate(IPresenter subPresenter);
      string Text { get; set; }
      IPresenter SubPresenter { get; }
      bool CanCancel { get; set; }
      bool Show(Size? modalSize = null);
   }

   internal class ModalPresenter : AbstractDisposablePresenter<IContainerModalView, IModalPresenter>, IModalPresenter
   {
      private readonly IEventPublisher _eventPublisher;
      public IPresenter SubPresenter { get; protected set; }

      public bool CanCancel
      {
         get => _view.CancelVisible;
         set
         {
            _view.CanClose = value;
            _view.CancelVisible = value;
         }
      }

      public bool Show(Size? modalSize) => modalSize == null ? _view.Show() : _view.Show(modalSize.Value);

      public ModalPresenter(IContainerModalView view, IEventPublisher eventPublisher) : base(view) => _eventPublisher = eventPublisher;

      public void Encapsulate(IPresenter subPresenter)
      {
         SubPresenter = subPresenter;
         SubPresenter.StatusChanged += subPresenterChanged;
         _view.AddSubView(subPresenter.BaseView);
      }

      private void subPresenterChanged(object sender, EventArgs eventArgs) => updateView();

      private void updateView() => _view.OkEnabled = CanClose;

      public bool Show()
      {
         updateView();
         return _view.Show();
      }

      public string Text
      {
         get => _view.Caption;
         set => _view.Caption = value;
      }

      public override bool CanClose
      {
         get
         {
            if (SubPresenter != null)
               return SubPresenter.CanClose;

            return true;
         }
      }

      protected override void Cleanup()
      {
         try
         {
            if (SubPresenter == null) return;
            SubPresenter.StatusChanged -= subPresenterChanged;
            SubPresenter.ReleaseFrom(_eventPublisher);
         }
         finally
         {
            base.Cleanup();
         }
      }
   }
}