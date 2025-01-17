﻿using MoBi.Presentation.DTO;
using MoBi.Presentation.Presenter;
using OSPSuite.Presentation.Views;

namespace MoBi.Presentation.Views
{
   public interface ISelectNeighborPathView : IView<ISelectNeighborPathPresenter>
   {
      void AddContainerCriteriaView(IView view);
      void BindTo(NeighborhoodObjectPathDTO objectPathDTO);
      string Label { get; set; }
      void ValidateNeighborhood();
   }
}