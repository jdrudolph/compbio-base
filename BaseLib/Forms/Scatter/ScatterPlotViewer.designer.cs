
namespace BaseLib.Forms.Scatter{
	public partial class ScatterPlotViewer {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScatterPlotViewer));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.modeDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesButton = new System.Windows.Forms.ToolStripButton();
			this.zoomInButton = new System.Windows.Forms.ToolStripButton();
			this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.zoomOutHorizontalButton = new System.Windows.Forms.ToolStripButton();
			this.zoomOutVerticalButton = new System.Windows.Forms.ToolStripButton();
			this.fullRangesButton = new System.Windows.Forms.ToolStripButton();
			this.moveUpButton = new System.Windows.Forms.ToolStripButton();
			this.moveDownButton = new System.Windows.Forms.ToolStripButton();
			this.moveLeftButton = new System.Windows.Forms.ToolStripButton();
			this.moveRightButton = new System.Windows.Forms.ToolStripButton();
			this.saveAsButton = new System.Windows.Forms.ToolStripButton();
			this.sizeLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeDropDownButton,
            this.propertiesButton,
            this.zoomInButton,
            this.zoomOutButton,
            this.zoomOutHorizontalButton,
            this.zoomOutVerticalButton,
            this.fullRangesButton,
            this.moveUpButton,
            this.moveDownButton,
            this.moveLeftButton,
            this.moveRightButton,
            this.saveAsButton,
            this.sizeLabel});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(714, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// modeDropDownButton
			// 
			this.modeDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.modeDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem,
            this.selectToolStripMenuItem});
			this.modeDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("modeDropDownButton.Image")));
			this.modeDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.modeDropDownButton.Name = "modeDropDownButton";
			this.modeDropDownButton.Size = new System.Drawing.Size(29, 22);
			this.modeDropDownButton.Text = "Mode";
			// 
			// zoomToolStripMenuItem
			// 
			this.zoomToolStripMenuItem.Checked = true;
			this.zoomToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.zoomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomToolStripMenuItem.Image")));
			this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
			this.zoomToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.zoomToolStripMenuItem.Text = "Zoom";
			this.zoomToolStripMenuItem.Click += new System.EventHandler(this.ZoomToolStripMenuItemClick);
			// 
			// selectToolStripMenuItem
			// 
			this.selectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectToolStripMenuItem.Image")));
			this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
			this.selectToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.selectToolStripMenuItem.Text = "Select";
			this.selectToolStripMenuItem.Click += new System.EventHandler(this.SelectToolStripMenuItemClick);
			// 
			// propertiesButton
			// 
			this.propertiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.propertiesButton.Image = ((System.Drawing.Image)(resources.GetObject("propertiesButton.Image")));
			this.propertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.propertiesButton.Name = "propertiesButton";
			this.propertiesButton.Size = new System.Drawing.Size(23, 22);
			this.propertiesButton.Text = "Configure";
			this.propertiesButton.Click += new System.EventHandler(this.ConfigureButtonClick);
			// 
			// zoomInButton
			// 
			this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
			this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomInButton.Name = "zoomInButton";
			this.zoomInButton.Size = new System.Drawing.Size(23, 22);
			this.zoomInButton.Text = "Zoom in";
			this.zoomInButton.Click += new System.EventHandler(this.ZoomInButtonClick);
			// 
			// zoomOutButton
			// 
			this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
			this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomOutButton.Name = "zoomOutButton";
			this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
			this.zoomOutButton.Text = "Zoom out";
			this.zoomOutButton.Click += new System.EventHandler(this.ZoomOutButtonClick);
			// 
			// zoomOutHorizontalButton
			// 
			this.zoomOutHorizontalButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomOutHorizontalButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutHorizontalButton.Image")));
			this.zoomOutHorizontalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomOutHorizontalButton.Name = "zoomOutHorizontalButton";
			this.zoomOutHorizontalButton.Size = new System.Drawing.Size(23, 22);
			this.zoomOutHorizontalButton.Text = "Zoom out horiz.";
			this.zoomOutHorizontalButton.Click += new System.EventHandler(this.ZoomOutHorizontalButtonClick);
			// 
			// zoomOutVerticalButton
			// 
			this.zoomOutVerticalButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.zoomOutVerticalButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutVerticalButton.Image")));
			this.zoomOutVerticalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.zoomOutVerticalButton.Name = "zoomOutVerticalButton";
			this.zoomOutVerticalButton.Size = new System.Drawing.Size(23, 22);
			this.zoomOutVerticalButton.Text = "Zoom out vert.";
			this.zoomOutVerticalButton.Click += new System.EventHandler(this.ZoomOutVerticalButtonClick);
			// 
			// fullRangesButton
			// 
			this.fullRangesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fullRangesButton.Image = ((System.Drawing.Image)(resources.GetObject("fullRangesButton.Image")));
			this.fullRangesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.fullRangesButton.Name = "fullRangesButton";
			this.fullRangesButton.Size = new System.Drawing.Size(23, 22);
			this.fullRangesButton.Text = "Full ranges";
			this.fullRangesButton.Click += new System.EventHandler(this.FullRangesButtonClick);
			// 
			// moveUpButton
			// 
			this.moveUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("moveUpButton.Image")));
			this.moveUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(23, 22);
			this.moveUpButton.Text = "Move up";
			this.moveUpButton.Click += new System.EventHandler(this.MoveUpButtonClick);
			// 
			// moveDownButton
			// 
			this.moveDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("moveDownButton.Image")));
			this.moveDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(23, 22);
			this.moveDownButton.Text = "Move down";
			this.moveDownButton.Click += new System.EventHandler(this.MoveDownButtonClick);
			// 
			// moveLeftButton
			// 
			this.moveLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("moveLeftButton.Image")));
			this.moveLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveLeftButton.Name = "moveLeftButton";
			this.moveLeftButton.Size = new System.Drawing.Size(23, 22);
			this.moveLeftButton.Text = "Move left";
			this.moveLeftButton.Click += new System.EventHandler(this.MoveLeftButtonClick);
			// 
			// moveRightButton
			// 
			this.moveRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.moveRightButton.Image = ((System.Drawing.Image)(resources.GetObject("moveRightButton.Image")));
			this.moveRightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.moveRightButton.Name = "moveRightButton";
			this.moveRightButton.Size = new System.Drawing.Size(23, 22);
			this.moveRightButton.Text = "Move right";
			this.moveRightButton.Click += new System.EventHandler(this.MoveRightButtonClick);
			// 
			// saveAsButton
			// 
			this.saveAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveAsButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAsButton.Image")));
			this.saveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveAsButton.Name = "saveAsButton";
			this.saveAsButton.Size = new System.Drawing.Size(23, 22);
			this.saveAsButton.Text = "Export image";
			this.saveAsButton.Click += new System.EventHandler(this.SaveAsButtonClick);
			// 
			// sizeLabel
			// 
			this.sizeLabel.Name = "sizeLabel";
			this.sizeLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// PlaneViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Name = "";
			this.Size = new System.Drawing.Size(714, 695);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton zoomInButton;
		private System.Windows.Forms.ToolStripButton zoomOutButton;
		private System.Windows.Forms.ToolStripButton fullRangesButton;
		private System.Windows.Forms.ToolStripButton moveUpButton;
		private System.Windows.Forms.ToolStripButton moveDownButton;
		private System.Windows.Forms.ToolStripButton moveLeftButton;
		private System.Windows.Forms.ToolStripButton moveRightButton;
		private System.Windows.Forms.ToolStripButton zoomOutHorizontalButton;
		private System.Windows.Forms.ToolStripButton zoomOutVerticalButton;
		private System.Windows.Forms.ToolStripButton propertiesButton;
		private System.Windows.Forms.ToolStripDropDownButton modeDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton saveAsButton;
		private System.Windows.Forms.ToolStripLabel sizeLabel;

	}
}