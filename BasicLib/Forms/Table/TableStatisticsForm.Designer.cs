using BasicLib.Forms.Axis;
using BasicLib.Forms.Scatter;

namespace BasicLib.Forms.Table{
	partial class TableStatisticsForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tabControl = new System.Windows.Forms.TabControl();
			this.scatterPlotPage = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelScatter = new System.Windows.Forms.TableLayoutPanel();
			this.scatterPlot = new ScatterPlot();
			this.panelScatter = new System.Windows.Forms.Panel();
			this.legendButton = new System.Windows.Forms.Button();
			this.label17 = new System.Windows.Forms.Label();
			this.labelsComboBox = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.selectionColorPanel = new System.Windows.Forms.Panel();
			this.colorScale = new BasicLib.Forms.Colors.ColorScale();
			this.label4 = new System.Windows.Forms.Label();
			this.colorsTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.yAxisTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.xAxisTypeComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.colorsComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.yAxisComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.xAxisComboBox = new System.Windows.Forms.ComboBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.label18 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label27 = new System.Windows.Forms.Label();
			this.comboBox4 = new System.Windows.Forms.ComboBox();
			this.tabControl.SuspendLayout();
			this.scatterPlotPage.SuspendLayout();
			this.tableLayoutPanelScatter.SuspendLayout();
			this.panelScatter.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.scatterPlotPage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(742, 504);
			this.tabControl.TabIndex = 0;
			// 
			// scatterPlotPage
			// 
			this.scatterPlotPage.Controls.Add(this.tableLayoutPanelScatter);
			this.scatterPlotPage.Location = new System.Drawing.Point(4, 22);
			this.scatterPlotPage.Name = "scatterPlotPage";
			this.scatterPlotPage.Padding = new System.Windows.Forms.Padding(3);
			this.scatterPlotPage.Size = new System.Drawing.Size(734, 478);
			this.scatterPlotPage.TabIndex = 1;
			this.scatterPlotPage.Text = "Scatter Plot";
			this.scatterPlotPage.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelScatter
			// 
			this.tableLayoutPanelScatter.ColumnCount = 2;
			this.tableLayoutPanelScatter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelScatter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
			this.tableLayoutPanelScatter.Controls.Add(this.scatterPlot, 0, 0);
			this.tableLayoutPanelScatter.Controls.Add(this.panelScatter, 1, 0);
			this.tableLayoutPanelScatter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelScatter.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanelScatter.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanelScatter.Name = "tableLayoutPanelScatter";
			this.tableLayoutPanelScatter.RowCount = 1;
			this.tableLayoutPanelScatter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelScatter.Size = new System.Drawing.Size(728, 472);
			this.tableLayoutPanelScatter.TabIndex = 4;
			// 
			// scatterPlot
			// 
			this.scatterPlot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scatterPlot.HasSelectButton = true;
			this.scatterPlot.Location = new System.Drawing.Point(0, 0);
			this.scatterPlot.Margin = new System.Windows.Forms.Padding(0);
			this.scatterPlot.MenuStripVisible = true;
			this.scatterPlot.Name = "scatterPlot";
			this.scatterPlot.ScatterPlotData = null;
			this.scatterPlot.SelectionColor = System.Drawing.Color.Red;
			this.scatterPlot.Size = new System.Drawing.Size(526, 472);
			this.scatterPlot.TabIndex = 3;
			this.scatterPlot.XLabel = "";
			this.scatterPlot.YLabel = "";
			// 
			// panelScatter
			// 
			this.panelScatter.Controls.Add(this.legendButton);
			this.panelScatter.Controls.Add(this.label17);
			this.panelScatter.Controls.Add(this.labelsComboBox);
			this.panelScatter.Controls.Add(this.label7);
			this.panelScatter.Controls.Add(this.selectionColorPanel);
			this.panelScatter.Controls.Add(this.colorScale);
			this.panelScatter.Controls.Add(this.label4);
			this.panelScatter.Controls.Add(this.colorsTypeComboBox);
			this.panelScatter.Controls.Add(this.label5);
			this.panelScatter.Controls.Add(this.yAxisTypeComboBox);
			this.panelScatter.Controls.Add(this.label6);
			this.panelScatter.Controls.Add(this.xAxisTypeComboBox);
			this.panelScatter.Controls.Add(this.label3);
			this.panelScatter.Controls.Add(this.colorsComboBox);
			this.panelScatter.Controls.Add(this.label2);
			this.panelScatter.Controls.Add(this.yAxisComboBox);
			this.panelScatter.Controls.Add(this.label1);
			this.panelScatter.Controls.Add(this.xAxisComboBox);
			this.panelScatter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelScatter.Location = new System.Drawing.Point(526, 0);
			this.panelScatter.Margin = new System.Windows.Forms.Padding(0);
			this.panelScatter.Name = "panelScatter";
			this.panelScatter.Size = new System.Drawing.Size(202, 472);
			this.panelScatter.TabIndex = 4;
			// 
			// legendButton
			// 
			this.legendButton.Location = new System.Drawing.Point(179, 186);
			this.legendButton.Name = "legendButton";
			this.legendButton.Size = new System.Drawing.Size(20, 23);
			this.legendButton.TabIndex = 17;
			this.legendButton.Text = "L";
			this.legendButton.UseVisualStyleBackColor = true;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(6, 372);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(38, 13);
			this.label17.TabIndex = 16;
			this.label17.Text = "Labels";
			// 
			// labelsComboBox
			// 
			this.labelsComboBox.FormattingEnabled = true;
			this.labelsComboBox.Location = new System.Drawing.Point(5, 387);
			this.labelsComboBox.Name = "labelsComboBox";
			this.labelsComboBox.Size = new System.Drawing.Size(193, 21);
			this.labelsComboBox.TabIndex = 15;
			this.labelsComboBox.SelectedIndexChanged += new System.EventHandler(this.LabelsComboBoxSelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(26, 341);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(78, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Selection Color";
			// 
			// selectionColorPanel
			// 
			this.selectionColorPanel.BackColor = System.Drawing.Color.Red;
			this.selectionColorPanel.Location = new System.Drawing.Point(5, 339);
			this.selectionColorPanel.Name = "selectionColorPanel";
			this.selectionColorPanel.Size = new System.Drawing.Size(19, 17);
			this.selectionColorPanel.TabIndex = 13;
			// 
			// colorScale
			// 
			this.colorScale.BackColor = System.Drawing.SystemColors.Control;
			this.colorScale.IsLogarithmic = false;
			this.colorScale.Location = new System.Drawing.Point(4, 261);
			this.colorScale.Max = 1;
			this.colorScale.Min = 0;
			this.colorScale.Name = "colorScale";
			this.colorScale.Positioning = AxisPositioning.Top;
			this.colorScale.Reverse = false;
			this.colorScale.Size = new System.Drawing.Size(193, 69);
			this.colorScale.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 215);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Color Axis Type";
			// 
			// colorsTypeComboBox
			// 
			this.colorsTypeComboBox.FormattingEnabled = true;
			this.colorsTypeComboBox.Location = new System.Drawing.Point(4, 230);
			this.colorsTypeComboBox.Name = "colorsTypeComboBox";
			this.colorsTypeComboBox.Size = new System.Drawing.Size(193, 21);
			this.colorsTypeComboBox.TabIndex = 10;
			this.colorsTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ColorsTypeComboBoxSelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 130);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(63, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Y Axis Type";
			// 
			// yAxisTypeComboBox
			// 
			this.yAxisTypeComboBox.FormattingEnabled = true;
			this.yAxisTypeComboBox.Location = new System.Drawing.Point(4, 145);
			this.yAxisTypeComboBox.Name = "yAxisTypeComboBox";
			this.yAxisTypeComboBox.Size = new System.Drawing.Size(193, 21);
			this.yAxisTypeComboBox.TabIndex = 8;
			this.yAxisTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.YAxisTypeComboBoxSelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "X Axis Type";
			// 
			// xAxisTypeComboBox
			// 
			this.xAxisTypeComboBox.FormattingEnabled = true;
			this.xAxisTypeComboBox.Location = new System.Drawing.Point(4, 61);
			this.xAxisTypeComboBox.Name = "xAxisTypeComboBox";
			this.xAxisTypeComboBox.Size = new System.Drawing.Size(193, 21);
			this.xAxisTypeComboBox.TabIndex = 6;
			this.xAxisTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.XAxisTypeComboBoxSelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 172);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Colors";
			// 
			// colorsComboBox
			// 
			this.colorsComboBox.FormattingEnabled = true;
			this.colorsComboBox.Location = new System.Drawing.Point(4, 187);
			this.colorsComboBox.Name = "colorsComboBox";
			this.colorsComboBox.Size = new System.Drawing.Size(173, 21);
			this.colorsComboBox.TabIndex = 4;
			this.colorsComboBox.SelectedIndexChanged += new System.EventHandler(this.ColorsComboBoxSelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Y Axis";
			// 
			// yAxisComboBox
			// 
			this.yAxisComboBox.FormattingEnabled = true;
			this.yAxisComboBox.Location = new System.Drawing.Point(4, 103);
			this.yAxisComboBox.Name = "yAxisComboBox";
			this.yAxisComboBox.Size = new System.Drawing.Size(193, 21);
			this.yAxisComboBox.TabIndex = 2;
			this.yAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.YAxisComboBoxSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "X Axis";
			// 
			// xAxisComboBox
			// 
			this.xAxisComboBox.FormattingEnabled = true;
			this.xAxisComboBox.Location = new System.Drawing.Point(4, 18);
			this.xAxisComboBox.Name = "xAxisComboBox";
			this.xAxisComboBox.Size = new System.Drawing.Size(193, 21);
			this.xAxisComboBox.TabIndex = 0;
			this.xAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.XAxisComboBoxSelectedIndexChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
			this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(728, 472);
			this.tableLayoutPanel2.TabIndex = 6;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.checkBox3);
			this.panel2.Controls.Add(this.checkBox4);
			this.panel2.Controls.Add(this.label18);
			this.panel2.Controls.Add(this.comboBox1);
			this.panel2.Controls.Add(this.label19);
			this.panel2.Controls.Add(this.comboBox2);
			this.panel2.Controls.Add(this.label26);
			this.panel2.Controls.Add(this.comboBox3);
			this.panel2.Controls.Add(this.label27);
			this.panel2.Controls.Add(this.comboBox4);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(526, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(202, 472);
			this.panel2.TabIndex = 4;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(9, 241);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(75, 17);
			this.checkBox3.TabIndex = 11;
			this.checkBox3.Text = "Show Grid";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(9, 218);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(87, 17);
			this.checkBox4.TabIndex = 10;
			this.checkBox4.Text = "Show Labels";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(5, 130);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(63, 13);
			this.label18.TabIndex = 9;
			this.label18.Text = "Y Axis Type";
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(4, 145);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(193, 21);
			this.comboBox1.TabIndex = 8;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(6, 45);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(63, 13);
			this.label19.TabIndex = 7;
			this.label19.Text = "X Axis Type";
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(4, 61);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(193, 21);
			this.comboBox2.TabIndex = 6;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(5, 88);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(36, 13);
			this.label26.TabIndex = 3;
			this.label26.Text = "Y Axis";
			// 
			// comboBox3
			// 
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(4, 103);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(193, 21);
			this.comboBox3.TabIndex = 2;
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(6, 3);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(36, 13);
			this.label27.TabIndex = 1;
			this.label27.Text = "X Axis";
			// 
			// comboBox4
			// 
			this.comboBox4.FormattingEnabled = true;
			this.comboBox4.Location = new System.Drawing.Point(4, 18);
			this.comboBox4.Name = "comboBox4";
			this.comboBox4.Size = new System.Drawing.Size(193, 21);
			this.comboBox4.TabIndex = 0;
			// 
			// TableStatisticsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(742, 504);
			this.Controls.Add(this.tabControl);
			this.Name = "TableStatisticsForm";
			this.Text = "Statistics";
			this.tabControl.ResumeLayout(false);
			this.scatterPlotPage.ResumeLayout(false);
			this.tableLayoutPanelScatter.ResumeLayout(false);
			this.panelScatter.ResumeLayout(false);
			this.panelScatter.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage scatterPlotPage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelScatter;
		private System.Windows.Forms.Panel panelScatter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox xAxisComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox yAxisComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox colorsComboBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox colorsTypeComboBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox yAxisTypeComboBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox xAxisTypeComboBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel selectionColorPanel;
		private BasicLib.Forms.Colors.ColorScale colorScale;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ComboBox labelsComboBox;
		private ScatterPlot scatterPlot;
		private System.Windows.Forms.Button legendButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox comboBox4;

	}
}