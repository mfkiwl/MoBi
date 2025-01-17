﻿using System.Collections.Generic;

using MoBi.Presentation.DTO;
using MoBi.Presentation.Presenter;
using OSPSuite.Presentation.Views;

namespace MoBi.Presentation.Views
{
   public interface ISelectManyView<T> : IView<ISelectManyPresenter<T>>, ISelectionView<T>
   {
      
      IEnumerable<ListItemDTO<T>> Selections { get; }
   }
}