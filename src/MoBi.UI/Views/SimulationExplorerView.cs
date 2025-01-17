﻿using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using MoBi.Presentation.Nodes;
using MoBi.Presentation.Presenter.Main;
using MoBi.Presentation.Views;
using OSPSuite.UI.Services;
using OSPSuite.UI.Views;
using OSPSuite.Utility.Extensions;

namespace MoBi.UI.Views
{
   public partial class SimulationExplorerView : BaseExplorerView, ISimulationExplorerView
   {
      public SimulationExplorerView(IImageListRetriever imageListRetriever) : base(imageListRetriever)
      {
         InitializeComponent();
         treeView.CompareNodeValues += compareNodeValues;
      }

      public void AttachPresenter(ISimulationExplorerPresenter presenter)
      {
         base.AttachPresenter(presenter);
      }

      private void compareNodeValues(object sender, CompareNodeValuesEventArgs e)
      {
         //we only want to sort for the top nodes (level 0)
         if (e.Node1 == null)
            return;

         //we do not want to sort the root nodes
         if (e.Node1.Level == 0)
            e.Result = 0;

         //we do not want to sort the items under the simulation node (i.e. no children). Otherwise, Nodes are sorted alphabetically
         else if (nodeIsSimulationNode(e.Node1.ParentNode))
            e.Result = 0;
      }

      private bool nodeIsSimulationNode(TreeListNode node) => node != null && node.Tag.IsAnImplementationOf<SimulationNode>();
   }
}