
namespace BaseLib.Forms.Scatter{
	partial class ScatterPlot {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScatterPlot));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.showLabelsComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.fontSizeTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.titleLabel = new System.Windows.Forms.ToolStripLabel();
			this.boldButton = new System.Windows.Forms.ToolStripButton();
			this.labelTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.labelEditComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.scatterPlotViewer = new ScatterPlotViewer();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLabelsComboBox,
            this.fontSizeTextBox,
            this.titleLabel,
            this.boldButton,
            this.labelTypeComboBox,
            this.labelEditComboBox});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(554, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// showLabelsComboBox
			// 
			this.showLabelsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.showLabelsComboBox.Items.AddRange(new object[] {
            "No labels",
            "Selected labels",
            "All labels"});
			this.showLabelsComboBox.Name = "showLabelsComboBox";
			this.showLabelsComboBox.Size = new System.Drawing.Size(80, 25);
			this.showLabelsComboBox.ToolTipText = "Show labels";
			// 
			// fontSizeTextBox
			// 
			this.fontSizeTextBox.Name = "fontSizeTextBox";
			this.fontSizeTextBox.Size = new System.Drawing.Size(25, 25);
			this.fontSizeTextBox.Text = "10";
			this.fontSizeTextBox.ToolTipText = "Label font size";
			// 
			// titleLabel
			// 
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// boldButton
			// 
			this.boldButton.Checked = true;
			this.boldButton.CheckOnClick = true;
			this.boldButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.boldButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.boldButton.Image = ((System.Drawing.Image)(resources.GetObject("boldButton.Image")));
			this.boldButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.boldButton.Name = "boldButton";
			this.boldButton.Size = new System.Drawing.Size(23, 22);
			this.boldButton.ToolTipText = "Bold labels";
			this.boldButton.Click += new System.EventHandler(this.BoldButtonClick);
			// 
			// labelTypeComboBox
			// 
			this.labelTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.labelTypeComboBox.Name = "labelTypeComboBox";
			this.labelTypeComboBox.Size = new System.Drawing.Size(121, 25);
			this.labelTypeComboBox.ToolTipText = "Label type";
			this.labelTypeComboBox.Visible = false;
			// 
			// labelEditComboBox
			// 
			this.labelEditComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.labelEditComboBox.Items.AddRange(new object[] {
            "Complete labels",
            "Up to first \';\'"});
			this.labelEditComboBox.Name = "labelEditComboBox";
			this.labelEditComboBox.Size = new System.Drawing.Size(90, 25);
			this.labelEditComboBox.ToolTipText = "Truncate labels";
			this.labelEditComboBox.Click += new System.EventHandler(this.LabelEditComboBoxClick);
			// 
			// planeViewer
			// 
			this.scatterPlotViewer.BackColor = System.Drawing.Color.White;
			this.scatterPlotViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scatterPlotViewer.FillColor = System.Drawing.Color.White;
			this.scatterPlotViewer.FullAxesVisible = false;
			this.scatterPlotViewer.HasSelectButton = true;
			this.scatterPlotViewer.LineColor = System.Drawing.Color.Black;
			this.scatterPlotViewer.LineWidth = 0.5F;
			this.scatterPlotViewer.Location = new System.Drawing.Point(0, 25);
			this.scatterPlotViewer.MajorTickLength = 6;
			this.scatterPlotViewer.MajorTickLineWidth = 1F;
			this.scatterPlotViewer.MenuStripVisible = true;
			this.scatterPlotViewer.MinorTickLength = 3;
			this.scatterPlotViewer.MinorTickLineWidth = 1F;
			this.scatterPlotViewer.Name = "scatterPlotViewer";
			this.scatterPlotViewer.Size = new System.Drawing.Size(554, 346);
			this.scatterPlotViewer.TabIndex = 1;
			this.scatterPlotViewer.XLabel = "x";
			this.scatterPlotViewer.YLabel = "y";
			this.scatterPlotViewer.ZLabel = "z";
			// 
			// ScatterPlot
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scatterPlotViewer);
			this.Controls.Add(this.toolStrip);
			this.Name = "ScatterPlot";
			this.Size = new System.Drawing.Size(554, 371);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		protected ScatterPlotViewer scatterPlotViewer;
		private System.Windows.Forms.ToolStripComboBox showLabelsComboBox;
		private System.Windows.Forms.ToolStripTextBox fontSizeTextBox;
		private System.Windows.Forms.ToolStripLabel titleLabel;
		private System.Windows.Forms.ToolStripComboBox labelTypeComboBox;
		private System.Windows.Forms.ToolStripComboBox labelEditComboBox;
		private System.Windows.Forms.ToolStripButton boldButton;

	}
}