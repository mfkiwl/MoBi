﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using MoBi.Assets;
using MoBi.Presentation.DTO;
using MoBi.Presentation.Presenter;
using MoBi.Presentation.Views;
using OSPSuite.Assets;
using OSPSuite.DataBinding;
using OSPSuite.DataBinding.DevExpress;
using OSPSuite.DataBinding.DevExpress.XtraGrid;
using OSPSuite.Presentation.Views;
using OSPSuite.UI.Binders;
using OSPSuite.UI.Controls;
using OSPSuite.UI.Extensions;
using OSPSuite.UI.RepositoryItems;
using OSPSuite.Utility.Extensions;
using OSPSuite.Utility.Reflection;
using OSPSuite.Utility.Validation;

namespace MoBi.UI.Views
{
   public abstract partial class BasePathAndValueEntityView<TPathAndValueEntity, T> : BaseUserControl, IPathAndValueEntitiesView<TPathAndValueEntity> where TPathAndValueEntity : BreadCrumbsDTO<T>, IPathAndValueEntityDTO where T : IValidatable, INotifier
   {
      private readonly ValueOriginBinder<TPathAndValueEntity> _valueOriginBinder;
      protected readonly GridViewBinder<TPathAndValueEntity> _gridViewBinder;
      private readonly IList<IGridViewColumn> _pathElementsColumns = new List<IGridViewColumn>();
      protected IStartValuesPresenter<TPathAndValueEntity> _presenter;

      private readonly RepositoryItemButtonEdit _removeButtonRepository = new UxRepositoryItemButtonEdit(ButtonPredefines.Delete);
      private readonly IList<string> _pathValues;
      private readonly RepositoryItemComboBox _pathRepositoryItemComboBox;

      private IGridViewColumn<TPathAndValueEntity> _deleteColumn;
      public bool CanCreateNewFormula { get; set; }

      protected BasePathAndValueEntityView(ValueOriginBinder<TPathAndValueEntity> valueOriginBinder)
      {
         _valueOriginBinder = valueOriginBinder;
         InitializeComponent();
         configureGridView();
         _gridViewBinder = new GridViewBinder<TPathAndValueEntity>(gridView);
         _pathValues = new List<string>();

         _pathRepositoryItemComboBox = new RepositoryItemComboBox {TextEditStyle = TextEditStyles.Standard};
         _pathRepositoryItemComboBox.SelectedValueChanged += (o, e) => gridView.PostEditor();
      }

      public void HideIsPresentView()
      {
         layoutItemIsPresent.Visibility = LayoutVisibility.Never;
      }

      public void HideNegativeValuesAllowedView()
      {
         layoutItemNegativeValuesAllowed.Visibility = LayoutVisibility.Never;
      }

      public IReadOnlyList<TPathAndValueEntity> SelectedStartValues
      {
         get { return gridView.GetSelectedRows().Select(rowHandle => _gridViewBinder.ElementAt(rowHandle)).ToList(); }
      }

      public IReadOnlyList<TPathAndValueEntity> VisibleStartValues => gridView.DataController.GetAllFilteredAndSortedRows().Cast<TPathAndValueEntity>().ToList();

      public void AddDeleteStartValuesView(IView view)
      {
         panelDeleteStartValues.FillWith(view);
      }

      public void HideDeleteView()
      {
         layoutItemDelete.Visibility = LayoutVisibility.Never;
      }

      public void HideDeleteColumn()
      {
         _deleteColumn.AsHidden();
      }

      public void HideSubPresenterGrouping()
      {
         emptySpaceItem.Visibility = LayoutVisibility.Never;
         layoutGroupPanel.GroupBordersVisible = false;
      }

      public void RefreshData()
      {
         gridView.RefreshData();
      }

      public TPathAndValueEntity FocusedStartValue
      {
         get => _gridViewBinder.FocusedElement;
         set
         {
            if (value == null)
               return;

            gridView.FocusedRowHandle = _gridViewBinder.RowHandleFor(value);
         }
      }

      protected virtual bool IsEditable(GridColumn column)
      {
         return true;
      }

      protected abstract void DoInitializeBinding();

      public override void InitializeBinding()
      {
         base.InitializeBinding();

         initPathElementColumn(dto => dto.PathElement0, Captions.PathElement(0));
         initPathElementColumn(dto => dto.PathElement1, Captions.PathElement(1));
         initPathElementColumn(dto => dto.PathElement2, Captions.PathElement(2));
         initPathElementColumn(dto => dto.PathElement3, Captions.PathElement(3));
         initPathElementColumn(dto => dto.PathElement4, Captions.PathElement(4));
         initPathElementColumn(dto => dto.PathElement5, Captions.PathElement(5));
         initPathElementColumn(dto => dto.PathElement6, Captions.PathElement(6));
         initPathElementColumn(dto => dto.PathElement7, Captions.PathElement(7));
         initPathElementColumn(dto => dto.PathElement8, Captions.PathElement(8));
         initPathElementColumn(dto => dto.PathElement9, Captions.PathElement(9));

         DoInitializeBinding();

         _deleteColumn = _gridViewBinder.AddUnboundColumn();

         _deleteColumn.WithCaption(OSPSuite.UI.UIConstants.EMPTY_COLUMN)
            .WithShowButton(ShowButtonModeEnum.ShowAlways)
            .WithRepository(dto => _removeButtonRepository)
            .WithFixedWidth(OSPSuite.UI.UIConstants.Size.EMBEDDED_BUTTON_WIDTH);

         _removeButtonRepository.ButtonClick += (o, e) => OnEvent(() => removeStartValue(_gridViewBinder.FocusedElement));
      }

      public void HideValueOriginColumn()
      {
         _valueOriginBinder.ValueOriginColumn.AsHidden().WithShowInColumnChooser(true);
      }

      protected void InitializeValueOriginBinding()
      {
         _valueOriginBinder.InitializeBinding(_gridViewBinder, (o, e) => OnEvent(() => _presenter.SetValueOrigin(o, e)));
      }

      private void removeStartValue(TPathAndValueEntity elementToRemove)
      {
         _presenter.RemoveStartValue(elementToRemove);
      }

      public GridControl GridControl => gridControl;

      public GridView GridView => gridView;

      public GridViewBinder<TPathAndValueEntity> Binder => _gridViewBinder;

      public void OnPathElementSet(TPathAndValueEntity pathAndValueEntity, PropertyValueSetEventArgs<string> eventArgs, int index)
      {
         if (index == AppConstants.NotFoundIndex)
            return;
         _presenter.UpdateStartValueContainerPath(pathAndValueEntity, index, eventArgs.NewValue);
      }

      private void initPathElementColumn(Expression<Func<TPathAndValueEntity, string>> expression, string caption)
      {
         var index = _pathElementsColumns.Count;

         _pathElementsColumns.Add(
            _gridViewBinder.Bind(expression)
               .WithCaption(caption)
               .WithRepository(x => _pathRepositoryItemComboBox)
               .WithOnValueUpdating((o, e) => OnEvent(() => OnPathElementSet(o, e, index))));
      }

      public void BindTo(IEnumerable<TPathAndValueEntity> startValues)
      {
         _gridViewBinder.BindToSource(startValues);
      }

      public void InitializePathColumns()
      {
         _pathRepositoryItemComboBox.FillComboBoxRepositoryWith(_pathValues);
         initColumnVisibility();
      }

      public void AddPathItems(IEnumerable<string> pathValues)
      {
         pathValues.Each(x =>
         {
            if (!_pathValues.Contains(x))
               _pathValues.Add(x);
         });
      }

      public void ClearPathItems()
      {
         _pathValues.Clear();
      }

      private void initColumnVisibility()
      {
         for (var i = 0; i < _pathElementsColumns.Count; i++)
         {
            _pathElementsColumns[i].Visible = _presenter.HasAtLeastTwoDistinctValues(i);
         }
      }

      protected void OnNameSet(TPathAndValueEntity startValueDTO, PropertyValueSetEventArgs<string> eventArgs)
      {
         _presenter.UpdateStartValueName(startValueDTO, eventArgs.NewValue);
      }

      private void configureGridView()
      {
         gridView.ShouldUseColorForDisabledCell = false;
         gridView.OptionsSelection.MultiSelect = true;
         gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
         gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
      }

      protected void OnFormulaButtonClick(object sender, ButtonPressedEventArgs e)
      {
         if (!e.Button.Kind.Equals(ButtonPredefines.Plus)) return;

         var startValueDTO = _gridViewBinder.ElementAt(gridView.FocusedRowHandle);
         _presenter.AddNewFormula(startValueDTO);
         
         if (sender is ComboBoxEdit comboBox)
            comboBox.FillComboBoxEditorWith(_presenter.AllFormulas());
      }

      protected RepositoryItem CreateFormulaRepository()
      {
         var repository = new UxRepositoryItemComboBox(gridView);
         repository.FillComboBoxRepositoryWith(_presenter.AllFormulas());
         if (CanCreateNewFormula)
         {
            repository.Buttons.Add(new EditorButton(ButtonPredefines.Plus));
            repository.ButtonClick += OnFormulaButtonClick;
         }

         return repository;
      }
   }
}