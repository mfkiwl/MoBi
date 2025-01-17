﻿using OSPSuite.UI.Controls;

namespace MoBi.UI.Views.BaseDiagram
{
   partial class ChartOptionsView
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.chkTopContainerInCurveName = new OSPSuite.UI.Controls.UxCheckEdit();
         this.layoutControl = new OSPSuite.UI.Controls.UxLayoutControl();
         this.diagramColorEdit = new OSPSuite.UI.Controls.UxColorPickEditWithHistory();
         this.chartBackgroundColorEdit = new OSPSuite.UI.Controls.UxColorPickEditWithHistory();
         this.cbPreferredChartYScaling = new OSPSuite.UI.Controls.UxComboBoxEdit();
         this.cbeDefaultLayoutName = new OSPSuite.UI.Controls.UxComboBoxEdit();
         this.chkSimulationInCurveName = new OSPSuite.UI.Controls.UxCheckEdit();
         this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
         this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
         this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
         this.defaultLayoutLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
         this.defaultYScalingLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
         this.chartBackgroundColorLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
         this.chartDiagramBackgroundColorLayoutItem = new DevExpress.XtraLayout.LayoutControlItem();
         this.timer1 = new System.Windows.Forms.Timer(this.components);
         this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
         this.chckColorGroupObservedData = new DevExpress.XtraEditors.CheckEdit();
         this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkTopContainerInCurveName.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
         this.layoutControl.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.diagramColorEdit.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chartBackgroundColorEdit.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.cbPreferredChartYScaling.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.cbeDefaultLayoutName.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkSimulationInCurveName.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.defaultLayoutLayoutItem)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.defaultYScalingLayoutItem)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chartBackgroundColorLayoutItem)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chartDiagramBackgroundColorLayoutItem)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.chckColorGroupObservedData.Properties)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
         this.SuspendLayout();
         // 
         // chkTopContainerInCurveName
         // 
         this.chkTopContainerInCurveName.AllowClicksOutsideControlArea = false;
         this.chkTopContainerInCurveName.EditValue = true;
         this.chkTopContainerInCurveName.Location = new System.Drawing.Point(30, 87);
         this.chkTopContainerInCurveName.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.chkTopContainerInCurveName.Name = "chkTopContainerInCurveName";
         this.chkTopContainerInCurveName.Properties.Caption = "Show Top Container Name in Curve Name";
         this.chkTopContainerInCurveName.Size = new System.Drawing.Size(828, 47);
         this.chkTopContainerInCurveName.StyleController = this.layoutControl;
         this.chkTopContainerInCurveName.TabIndex = 100;
         // 
         // layoutControl
         // 
         this.layoutControl.AllowCustomization = false;
         this.layoutControl.Controls.Add(this.chckColorGroupObservedData);
         this.layoutControl.Controls.Add(this.diagramColorEdit);
         this.layoutControl.Controls.Add(this.chartBackgroundColorEdit);
         this.layoutControl.Controls.Add(this.cbPreferredChartYScaling);
         this.layoutControl.Controls.Add(this.cbeDefaultLayoutName);
         this.layoutControl.Controls.Add(this.chkSimulationInCurveName);
         this.layoutControl.Controls.Add(this.chkTopContainerInCurveName);
         this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.layoutControl.Location = new System.Drawing.Point(0, 0);
         this.layoutControl.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.layoutControl.Name = "layoutControl";
         this.layoutControl.Root = this.layoutControlGroup;
         this.layoutControl.Size = new System.Drawing.Size(888, 551);
         this.layoutControl.TabIndex = 108;
         this.layoutControl.Text = "layoutControl1";
         // 
         // diagramColorEdit
         // 
         this.diagramColorEdit.EditValue = System.Drawing.Color.Empty;
         this.diagramColorEdit.Location = new System.Drawing.Point(557, 318);
         this.diagramColorEdit.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.diagramColorEdit.Name = "diagramColorEdit";
         this.diagramColorEdit.Properties.AutomaticColor = System.Drawing.Color.Black;
         this.diagramColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.diagramColorEdit.Size = new System.Drawing.Size(301, 48);
         this.diagramColorEdit.StyleController = this.layoutControl;
         this.diagramColorEdit.TabIndex = 109;
         // 
         // chartBackgroundColorEdit
         // 
         this.chartBackgroundColorEdit.EditValue = System.Drawing.Color.Empty;
         this.chartBackgroundColorEdit.Location = new System.Drawing.Point(557, 260);
         this.chartBackgroundColorEdit.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.chartBackgroundColorEdit.Name = "chartBackgroundColorEdit";
         this.chartBackgroundColorEdit.Properties.AutomaticColor = System.Drawing.Color.Black;
         this.chartBackgroundColorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.chartBackgroundColorEdit.Size = new System.Drawing.Size(301, 48);
         this.chartBackgroundColorEdit.StyleController = this.layoutControl;
         this.chartBackgroundColorEdit.TabIndex = 108;
         // 
         // cbPreferredChartYScaling
         // 
         this.cbPreferredChartYScaling.Location = new System.Drawing.Point(557, 202);
         this.cbPreferredChartYScaling.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.cbPreferredChartYScaling.Name = "cbPreferredChartYScaling";
         this.cbPreferredChartYScaling.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.cbPreferredChartYScaling.Size = new System.Drawing.Size(301, 48);
         this.cbPreferredChartYScaling.StyleController = this.layoutControl;
         this.cbPreferredChartYScaling.TabIndex = 107;
         // 
         // cbeDefaultLayoutName
         // 
         this.cbeDefaultLayoutName.Location = new System.Drawing.Point(557, 144);
         this.cbeDefaultLayoutName.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.cbeDefaultLayoutName.Name = "cbeDefaultLayoutName";
         this.cbeDefaultLayoutName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
         this.cbeDefaultLayoutName.Size = new System.Drawing.Size(301, 48);
         this.cbeDefaultLayoutName.StyleController = this.layoutControl;
         this.cbeDefaultLayoutName.TabIndex = 103;
         // 
         // chkSimulationInCurveName
         // 
         this.chkSimulationInCurveName.AllowClicksOutsideControlArea = false;
         this.chkSimulationInCurveName.EditValue = true;
         this.chkSimulationInCurveName.Location = new System.Drawing.Point(30, 30);
         this.chkSimulationInCurveName.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
         this.chkSimulationInCurveName.Name = "chkSimulationInCurveName";
         this.chkSimulationInCurveName.Properties.Caption = "Show Simulation Name in Curve Name";
         this.chkSimulationInCurveName.Size = new System.Drawing.Size(828, 47);
         this.chkSimulationInCurveName.StyleController = this.layoutControl;
         this.chkSimulationInCurveName.TabIndex = 101;
         // 
         // layoutControlGroup
         // 
         this.layoutControlGroup.CustomizationFormText = "layoutControlGroup1";
         this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
         this.layoutControlGroup.GroupBordersVisible = false;
         this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.defaultLayoutLayoutItem,
            this.defaultYScalingLayoutItem,
            this.chartBackgroundColorLayoutItem,
            this.chartDiagramBackgroundColorLayoutItem,
            this.layoutControlItem5});
         this.layoutControlGroup.Name = "layoutControlGroup";
         this.layoutControlGroup.Size = new System.Drawing.Size(888, 551);
         this.layoutControlGroup.TextVisible = false;
         // 
         // layoutControlItem2
         // 
         this.layoutControlItem2.Control = this.chkTopContainerInCurveName;
         this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
         this.layoutControlItem2.Location = new System.Drawing.Point(0, 57);
         this.layoutControlItem2.Name = "layoutControlItem2";
         this.layoutControlItem2.Size = new System.Drawing.Size(838, 57);
         this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem2.TextVisible = false;
         // 
         // layoutControlItem4
         // 
         this.layoutControlItem4.Control = this.chkSimulationInCurveName;
         this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
         this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
         this.layoutControlItem4.Name = "layoutControlItem4";
         this.layoutControlItem4.Size = new System.Drawing.Size(838, 57);
         this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem4.TextVisible = false;
         // 
         // defaultLayoutLayoutItem
         // 
         this.defaultLayoutLayoutItem.Control = this.cbeDefaultLayoutName;
         this.defaultLayoutLayoutItem.CustomizationFormText = "defaultLayoutLayoutItem";
         this.defaultLayoutLayoutItem.Location = new System.Drawing.Point(0, 114);
         this.defaultLayoutLayoutItem.Name = "defaultLayoutLayoutItem";
         this.defaultLayoutLayoutItem.Size = new System.Drawing.Size(838, 58);
         this.defaultLayoutLayoutItem.TextSize = new System.Drawing.Size(497, 33);
         // 
         // defaultYScalingLayoutItem
         // 
         this.defaultYScalingLayoutItem.Control = this.cbPreferredChartYScaling;
         this.defaultYScalingLayoutItem.CustomizationFormText = "defaultYScalingLayoutItem";
         this.defaultYScalingLayoutItem.Location = new System.Drawing.Point(0, 172);
         this.defaultYScalingLayoutItem.Name = "defaultYScalingLayoutItem";
         this.defaultYScalingLayoutItem.Size = new System.Drawing.Size(838, 58);
         this.defaultYScalingLayoutItem.TextSize = new System.Drawing.Size(497, 33);
         // 
         // chartBackgroundColorLayoutItem
         // 
         this.chartBackgroundColorLayoutItem.Control = this.chartBackgroundColorEdit;
         this.chartBackgroundColorLayoutItem.CustomizationFormText = "chartBackgroundColorLayoutItem";
         this.chartBackgroundColorLayoutItem.Location = new System.Drawing.Point(0, 230);
         this.chartBackgroundColorLayoutItem.Name = "chartBackgroundColorLayoutItem";
         this.chartBackgroundColorLayoutItem.Size = new System.Drawing.Size(838, 58);
         this.chartBackgroundColorLayoutItem.TextSize = new System.Drawing.Size(497, 33);
         // 
         // chartDiagramBackgroundColorLayoutItem
         // 
         this.chartDiagramBackgroundColorLayoutItem.Control = this.diagramColorEdit;
         this.chartDiagramBackgroundColorLayoutItem.CustomizationFormText = "chartDiagramBackgroundColorLayoutItem";
         this.chartDiagramBackgroundColorLayoutItem.Location = new System.Drawing.Point(0, 288);
         this.chartDiagramBackgroundColorLayoutItem.Name = "chartDiagramBackgroundColorLayoutItem";
         this.chartDiagramBackgroundColorLayoutItem.Size = new System.Drawing.Size(838, 58);
         this.chartDiagramBackgroundColorLayoutItem.TextSize = new System.Drawing.Size(497, 33);
         // 
         // layoutControlItem3
         // 
         this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
         this.layoutControlItem3.Location = new System.Drawing.Point(0, 53);
         this.layoutControlItem3.Name = "layoutControlItem3";
         this.layoutControlItem3.Size = new System.Drawing.Size(226, 30);
         this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 20);
         // 
         // chckColorGroupObservedData
         // 
         this.chckColorGroupObservedData.Location = new System.Drawing.Point(30, 376);
         this.chckColorGroupObservedData.Name = "chckColorGroupObservedData";
         this.chckColorGroupObservedData.Properties.Caption = "checkEdit2";
         this.chckColorGroupObservedData.Size = new System.Drawing.Size(828, 47);
         this.chckColorGroupObservedData.StyleController = this.layoutControl;
         this.chckColorGroupObservedData.TabIndex = 111;
         // 
         // layoutControlItem5
         // 
         this.layoutControlItem5.Control = this.chckColorGroupObservedData;
         this.layoutControlItem5.Location = new System.Drawing.Point(0, 346);
         this.layoutControlItem5.Name = "layoutControlItem5";
         this.layoutControlItem5.Size = new System.Drawing.Size(838, 155);
         this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
         this.layoutControlItem5.TextVisible = false;
         // 
         // ChartOptionsView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.layoutControl);
         this.Margin = new System.Windows.Forms.Padding(20, 20, 20, 20);
         this.Name = "ChartOptionsView";
         this.Size = new System.Drawing.Size(888, 551);
         ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkTopContainerInCurveName.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
         this.layoutControl.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.diagramColorEdit.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chartBackgroundColorEdit.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.cbPreferredChartYScaling.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.cbeDefaultLayoutName.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chkSimulationInCurveName.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.defaultLayoutLayoutItem)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.defaultYScalingLayoutItem)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chartBackgroundColorLayoutItem)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chartDiagramBackgroundColorLayoutItem)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.chckColorGroupObservedData.Properties)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private OSPSuite.UI.Controls.UxComboBoxEdit cbeDefaultLayoutName;
      private System.Windows.Forms.Timer timer1;
      private UxLayoutControl layoutControl;
      private OSPSuite.UI.Controls.UxComboBoxEdit cbPreferredChartYScaling;
      private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
      private DevExpress.XtraLayout.LayoutControlItem defaultLayoutLayoutItem;
      private DevExpress.XtraLayout.LayoutControlItem defaultYScalingLayoutItem;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
      private UxCheckEdit chkTopContainerInCurveName;
      private UxCheckEdit chkSimulationInCurveName;
      private UxColorPickEditWithHistory diagramColorEdit;
      private UxColorPickEditWithHistory chartBackgroundColorEdit;
      private DevExpress.XtraLayout.LayoutControlItem chartBackgroundColorLayoutItem;
      private DevExpress.XtraLayout.LayoutControlItem chartDiagramBackgroundColorLayoutItem;
      private DevExpress.XtraEditors.CheckEdit chckColorGroupObservedData;
      private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
   }
}