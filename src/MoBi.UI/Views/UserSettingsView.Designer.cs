﻿using OSPSuite.UI.Controls;

namespace MoBi.UI.Views
{
   partial class UserSettingsView
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         _screenBinder.Dispose();
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      protected void InitializeComponent()
      {
         this.tabControl = new DevExpress.XtraTab.XtraTabControl();
         this.tabGeneral = new DevExpress.XtraTab.XtraTabPage();
         this.layoutControl1 = new OSPSuite.UI.Controls.UxLayoutControl();
         this.tbNumberOfProcessors = new DevExpress.XtraEditors.TextEdit();
         this.pnlValidationOptions = new DevExpress.XtraEditors.PanelControl();
         this.tbDecimalPlace = new DevExpress.XtraEditors.TextEdit();
         this.tbMRUFiles = new DevExpress.XtraEditors.TextEdit();
         this.chkRenameDependent = new OSPSuite.UI.Controls.UxCheckEdit();
         this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItemRenameDependent = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutItemDecimalPlace = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutItemNumberOfRecentProjects = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutItemNumberOfProcessors = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutGroupValidationItems = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutItemValidationItems = new DevExpress.XtraLayout.LayoutControlItem();
         this.tabDiagramOptions = new DevExpress.XtraTab.XtraTabPage();
         this.tabFlowLayout = new DevExpress.XtraTab.XtraTabPage();
         this.tabChartOptions = new DevExpress.XtraTab.XtraTabPage();
         this.tabDisplayUnits = new DevExpress.XtraTab.XtraTabPage();
         this.tabApplicationSettings = new DevExpress.XtraTab.XtraTabPage();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
         this.tabControl.SuspendLayout();
         this.tabGeneral.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
         this.layoutControl1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.tbNumberOfProcessors.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pnlValidationOptions)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.tbDecimalPlace.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.tbMRUFiles.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkRenameDependent.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRenameDependent)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemDecimalPlace)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemNumberOfRecentProjects)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemNumberOfProcessors)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutGroupValidationItems)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemValidationItems)).BeginInit();
         this.SuspendLayout();
         // 
         // tabControl
         // 
         this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabControl.Location = new System.Drawing.Point(0, 0);
         this.tabControl.Name = "tabControl";
         this.tabControl.SelectedTabPage = this.tabGeneral;
         this.tabControl.Size = new System.Drawing.Size(797, 451);
         this.tabControl.TabIndex = 0;
         this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGeneral,
            this.tabDiagramOptions,
            this.tabFlowLayout,
            this.tabChartOptions,
            this.tabDisplayUnits,
            this.tabApplicationSettings});
         // 
         // tabGeneral
         // 
         this.tabGeneral.Controls.Add(this.layoutControl1);
         this.tabGeneral.Name = "tabGeneral";
         this.tabGeneral.Size = new System.Drawing.Size(791, 423);
         this.tabGeneral.Text = "General";
         // 
         // layoutControl1
         // 
         this.layoutControl1.AllowCustomization = false;
         this.layoutControl1.Controls.Add(this.tbNumberOfProcessors);
         this.layoutControl1.Controls.Add(this.pnlValidationOptions);
         this.layoutControl1.Controls.Add(this.tbDecimalPlace);
         this.layoutControl1.Controls.Add(this.tbMRUFiles);
         this.layoutControl1.Controls.Add(this.chkRenameDependent);
         this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl1.Location = new System.Drawing.Point(0, 0);
         this.layoutControl1.Name = "layoutControl1";
         this.layoutControl1.Root = this.layoutControlGroup1;
         this.layoutControl1.Size = new System.Drawing.Size(791, 423);
         this.layoutControl1.TabIndex = 8;
         this.layoutControl1.Text = "layoutControl1";
         // 
         // tbNumberOfProcessors
         // 
         this.tbNumberOfProcessors.Location = new System.Drawing.Point(189, 83);
         this.tbNumberOfProcessors.Name = "tbNumberOfProcessors";
         this.tbNumberOfProcessors.Size = new System.Drawing.Size(590, 20);
         this.tbNumberOfProcessors.StyleController = this.layoutControl1;
         this.tbNumberOfProcessors.TabIndex = 9;
         // 
         // pnlValidationOptions
         // 
         this.pnlValidationOptions.Location = new System.Drawing.Point(24, 137);
         this.pnlValidationOptions.Name = "pnlValidationOptions";
         this.pnlValidationOptions.Size = new System.Drawing.Size(743, 262);
         this.pnlValidationOptions.TabIndex = 8;
         // 
         // tbDecimalPlace
         // 
         this.tbDecimalPlace.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
         this.tbDecimalPlace.Location = new System.Drawing.Point(189, 59);
         this.tbDecimalPlace.Name = "tbDecimalPlace";
         this.tbDecimalPlace.Size = new System.Drawing.Size(590, 20);
         this.tbDecimalPlace.StyleController = this.layoutControl1;
         this.tbDecimalPlace.TabIndex = 7;
         // 
         // tbMRUFiles
         // 
         this.tbMRUFiles.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
         this.tbMRUFiles.Location = new System.Drawing.Point(189, 35);
         this.tbMRUFiles.Name = "tbMRUFiles";
         this.tbMRUFiles.Size = new System.Drawing.Size(590, 20);
         this.tbMRUFiles.StyleController = this.layoutControl1;
         this.tbMRUFiles.TabIndex = 3;
         // 
         // chkRenameDependent
         // 
         this.chkRenameDependent.AllowClicksOutsideControlArea = false;
         this.chkRenameDependent.Location = new System.Drawing.Point(12, 12);
         this.chkRenameDependent.Name = "chkRenameDependent";
         this.chkRenameDependent.Properties.Caption = "Rename Dependent Elements";
         this.chkRenameDependent.Size = new System.Drawing.Size(767, 19);
         this.chkRenameDependent.StyleController = this.layoutControl1;
         this.chkRenameDependent.TabIndex = 0;
         // 
         // layoutControlGroup1
         // 
         this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
         this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup1.GroupBordersVisible = false;
         this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemRenameDependent,
            this.layoutItemDecimalPlace,
            this.layoutItemNumberOfRecentProjects,
            this.layoutItemNumberOfProcessors,
            this.layoutGroupValidationItems});
         this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
         this.layoutControlGroup1.Name = "layoutControlGroup1";
         this.layoutControlGroup1.Size = new System.Drawing.Size(791, 423);
         this.layoutControlGroup1.TextVisible = false;
         // 
         // layoutControlItemRenameDependent
         // 
         this.layoutControlItemRenameDependent.Control = this.chkRenameDependent;
         this.layoutControlItemRenameDependent.CustomizationFormText = "layoutControlItem1";
         this.layoutControlItemRenameDependent.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItemRenameDependent.Name = "layoutControlItemRenameDependent";
         this.layoutControlItemRenameDependent.Size = new System.Drawing.Size(771, 23);
         this.layoutControlItemRenameDependent.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItemRenameDependent.TextVisible = false;
         // 
         // layoutItemDecimalPlace
         // 
         this.layoutItemDecimalPlace.Control = this.tbDecimalPlace;
         this.layoutItemDecimalPlace.CustomizationFormText = "layoutItemDecimalPlace";
         this.layoutItemDecimalPlace.Location = new System.Drawing.Point(0, 47);
         this.layoutItemDecimalPlace.Name = "layoutItemDecimalPlace";
         this.layoutItemDecimalPlace.Size = new System.Drawing.Size(771, 24);
         this.layoutItemDecimalPlace.TextSize = new System.Drawing.Size(174, 13);
         // 
         // layoutItemNumberOfRecentProjects
         // 
         this.layoutItemNumberOfRecentProjects.Control = this.tbMRUFiles;
         this.layoutItemNumberOfRecentProjects.CustomizationFormText = "layoutItemNumberOfRecentProjects";
         this.layoutItemNumberOfRecentProjects.Location = new System.Drawing.Point(0, 23);
         this.layoutItemNumberOfRecentProjects.Name = "layoutItemNumberOfRecentProjects";
         this.layoutItemNumberOfRecentProjects.Size = new System.Drawing.Size(771, 24);
         this.layoutItemNumberOfRecentProjects.TextSize = new System.Drawing.Size(174, 13);
         // 
         // layoutItemNumberOfProcessors
         // 
         this.layoutItemNumberOfProcessors.Control = this.tbNumberOfProcessors;
         this.layoutItemNumberOfProcessors.Location = new System.Drawing.Point(0, 71);
         this.layoutItemNumberOfProcessors.Name = "layoutItemNumberOfProcessors";
         this.layoutItemNumberOfProcessors.Size = new System.Drawing.Size(771, 24);
         this.layoutItemNumberOfProcessors.TextSize = new System.Drawing.Size(174, 13);
         // 
         // layoutGroupValidationItems
         // 
         this.layoutGroupValidationItems.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutItemValidationItems});
         this.layoutGroupValidationItems.Location = new System.Drawing.Point(0, 95);
         this.layoutGroupValidationItems.Name = "layoutGroupValidationItems";
         this.layoutGroupValidationItems.Size = new System.Drawing.Size(771, 308);
         // 
         // layoutItemValidationItems
         // 
         this.layoutItemValidationItems.Control = this.pnlValidationOptions;
         this.layoutItemValidationItems.CustomizationFormText = "layoutItemValidationItems";
         this.layoutItemValidationItems.Location = new System.Drawing.Point(0, 0);
         this.layoutItemValidationItems.Name = "layoutItemValidationItems";
         this.layoutItemValidationItems.Size = new System.Drawing.Size(747, 266);
         this.layoutItemValidationItems.TextSize = new System.Drawing.Size(0, 0);
         this.layoutItemValidationItems.TextVisible = false;
         // 
         // tabDiagramOptions
         // 
         this.tabDiagramOptions.Name = "tabDiagramOptions";
         this.tabDiagramOptions.Size = new System.Drawing.Size(791, 423);
         this.tabDiagramOptions.Text = "Diagram Options";
         // 
         // tabFlowLayout
         // 
         this.tabFlowLayout.Name = "tabFlowLayout";
         this.tabFlowLayout.Size = new System.Drawing.Size(791, 423);
         this.tabFlowLayout.Text = "Diagram Auto Layout";
         // 
         // tabChartOptions
         // 
         this.tabChartOptions.Name = "tabChartOptions";
         this.tabChartOptions.Size = new System.Drawing.Size(791, 423);
         this.tabChartOptions.Text = "Chart Options";
         // 
         // tabDisplayUnits
         // 
         this.tabDisplayUnits.Name = "tabDisplayUnits";
         this.tabDisplayUnits.Padding = new System.Windows.Forms.Padding(10);
         this.tabDisplayUnits.Size = new System.Drawing.Size(791, 423);
         this.tabDisplayUnits.Text = "tabDisplayUnits";
         // 
         // tabApplicationSettings
         // 
         this.tabApplicationSettings.Name = "tabApplicationSettings";
         this.tabApplicationSettings.Size = new System.Drawing.Size(791, 423);
         this.tabApplicationSettings.Text = "tabApplicationSettings";
         // 
         // UserSettingsView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(797, 497);
         this.Controls.Add(this.tabControl);
         this.Name = "UserSettingsView";
         this.Controls.SetChildIndex(this.tabControl, 0);
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
         this.tabControl.ResumeLayout(false);
         this.tabGeneral.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
         this.layoutControl1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.tbNumberOfProcessors.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pnlValidationOptions)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.tbDecimalPlace.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.tbMRUFiles.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkRenameDependent.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRenameDependent)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemDecimalPlace)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemNumberOfRecentProjects)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemNumberOfProcessors)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutGroupValidationItems)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutItemValidationItems)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private DevExpress.XtraTab.XtraTabControl tabControl;
      private DevExpress.XtraTab.XtraTabPage tabGeneral;
      private DevExpress.XtraTab.XtraTabPage tabFlowLayout;
      private DevExpress.XtraTab.XtraTabPage tabDiagramOptions;
      private DevExpress.XtraTab.XtraTabPage tabChartOptions;
      private OSPSuite.UI.Controls.UxLayoutControl layoutControl1;
      private DevExpress.XtraEditors.TextEdit tbDecimalPlace;
      private DevExpress.XtraEditors.TextEdit tbMRUFiles;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRenameDependent;
      private DevExpress.XtraLayout.LayoutControlItem layoutItemDecimalPlace;
      private DevExpress.XtraLayout.LayoutControlItem layoutItemNumberOfRecentProjects;
      private DevExpress.XtraEditors.PanelControl pnlValidationOptions;
      private DevExpress.XtraLayout.LayoutControlItem layoutItemValidationItems;
      private DevExpress.XtraTab.XtraTabPage tabDisplayUnits;
      private UxCheckEdit chkRenameDependent;
      private DevExpress.XtraEditors.TextEdit tbNumberOfProcessors;
      private DevExpress.XtraLayout.LayoutControlItem layoutItemNumberOfProcessors;
      private DevExpress.XtraLayout.LayoutControlGroup layoutGroupValidationItems;
      private DevExpress.XtraTab.XtraTabPage tabApplicationSettings;
   }
}
