using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Forms.Axis;
using BaseLib.Forms.Base;

namespace BaseLib.Forms.Scatter{
	public partial class ScatterPlotViewer : UserControl{
		private readonly ScatterPlotPlaneView scatterPlotPlane;
		private readonly NumericAxisView leftAxis;
		private readonly NumericAxisView rightAxis;
		private readonly NumericAxisView topAxis;
		private readonly NumericAxisView bottomAxis;
		private readonly BasicTableLayoutView tableLayoutView;
		private readonly BasicView spacer1;
		private readonly BasicView spacer2;
		private readonly BasicView spacer3;
		private readonly BasicView spacer4;
		private readonly BasicControl tableLayoutControl;
		private bool hasSelectButton;

		public ScatterPlotViewer(){
			bottomAxis = new NumericAxisView();
			leftAxis = new NumericAxisView();
			topAxis = new NumericAxisView();
			rightAxis = new NumericAxisView();
			scatterPlotPlane = new ScatterPlotPlaneView();
			spacer1 = new BasicView();
			spacer2 = new BasicView();
			spacer3 = new BasicView();
			spacer4 = new BasicView();
			spacer1.BackColor = Color.White;
			spacer2.BackColor = Color.White;
			spacer3.BackColor = Color.White;
			spacer4.BackColor = Color.White;
			tableLayoutView = new BasicTableLayoutView();
			InitializeComponent();
			tableLayoutView.Add(spacer1, 0, 0);
			tableLayoutView.Add(spacer2, 0, 2);
			tableLayoutView.Add(spacer3, 2, 0);
			tableLayoutView.Add(spacer4, 2, 2);
			tableLayoutView.Add(scatterPlotPlane, 1, 1);
			tableLayoutView.Add(bottomAxis, 1, 2);
			tableLayoutView.Add(leftAxis, 0, 1);
			tableLayoutView.Add(topAxis, 1, 0);
			tableLayoutView.Add(rightAxis, 2, 1);
			tableLayoutView.BackColor = Color.Transparent;
			tableLayoutView.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.Absolute, 37F));
			tableLayoutView.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.Percent, 100F));
			tableLayoutView.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.Absolute, 37F));
			tableLayoutView.RowStyles.Add(new BasicRowStyle(BasicSizeType.Absolute, 37F));
			tableLayoutView.RowStyles.Add(new BasicRowStyle(BasicSizeType.Percent, 100F));
			tableLayoutView.RowStyles.Add(new BasicRowStyle(BasicSizeType.Absolute, 37F));
			tableLayoutControl = tableLayoutView.CreateControl();
			Controls.Add(tableLayoutControl);
			Controls.Add(toolStrip1);
			scatterPlotPlane.BackColor = Color.White;
			scatterPlotPlane.ForeColor = SystemColors.HotTrack;
			scatterPlotPlane.IndicatorColor = Color.Transparent;
			scatterPlotPlane.MouseMode = ScatterPlotMouseMode.Zoom;
			bottomAxis.Configurable = true;
			bottomAxis.Positioning = AxisPositioning.Bottom;
			bottomAxis.Reverse = false;
			bottomAxis.ZoomType = AxisZoomType.Zoom;
			leftAxis.Configurable = true;
			leftAxis.Positioning = AxisPositioning.Left;
			leftAxis.Reverse = true;
			leftAxis.ZoomType = AxisZoomType.Zoom;
			topAxis.Configurable = false;
			topAxis.Positioning = AxisPositioning.Top;
			topAxis.Reverse = false;
			topAxis.ZoomType = AxisZoomType.Indicate;
			rightAxis.Configurable = false;
			rightAxis.Positioning = AxisPositioning.Right;
			rightAxis.Reverse = true;
			rightAxis.ZoomType = AxisZoomType.Indicate;
			topAxis.OnZoomChange += UpdateZoomBottom;
			bottomAxis.OnZoomChange += UpdateZoomTop;
			rightAxis.OnZoomChange += UpdateZoomRight;
			leftAxis.OnZoomChange += UpdateZoomLeft;
			scatterPlotPlane.OnZoomChange += UpdateZoomFromMap;
			bottomAxis.BackColor = Color.White;
			bottomAxis.Font = new Font("Lucida Sans Unicode", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			bottomAxis.ForeColor = Color.Black;
			leftAxis.BackColor = Color.White;
			leftAxis.Font = new Font("Lucida Sans Unicode", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			leftAxis.ForeColor = Color.Black;
			topAxis.BackColor = Color.White;
			topAxis.Font = new Font("Lucida Sans Unicode", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			topAxis.ForeColor = Color.Black;
			rightAxis.BackColor = Color.White;
			rightAxis.Font = new Font("Lucida Sans Unicode", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			rightAxis.ForeColor = Color.Black;
		}

		public Font NumbersFont{
			get { return bottomAxis.numbersFont; }
			set{
				topAxis.numbersFont = value;
				bottomAxis.numbersFont = value;
				leftAxis.numbersFont = value;
				rightAxis.numbersFont = value;
			}
		}
		public Font LabelFont{
			get { return bottomAxis.labelFont; }
			set{
				topAxis.labelFont = value;
				bottomAxis.labelFont = value;
				leftAxis.labelFont = value;
				rightAxis.labelFont = value;
			}
		}
		public Color LineColor{
			get { return bottomAxis.ForeColor; }
			set{
				topAxis.ForeColor = value;
				bottomAxis.ForeColor = value;
				leftAxis.ForeColor = value;
				rightAxis.ForeColor = value;
			}
		}
		public float LineWidth{
			get { return bottomAxis.LineWidth; }
			set{
				topAxis.LineWidth = value;
				bottomAxis.LineWidth = value;
				leftAxis.LineWidth = value;
				rightAxis.LineWidth = value;
			}
		}
		public int MajorTickLength{
			get { return bottomAxis.MajorTickLength; }
			set{
				topAxis.MajorTickLength = value;
				bottomAxis.MajorTickLength = value;
				leftAxis.MajorTickLength = value;
				rightAxis.MajorTickLength = value;
			}
		}
		public int MinorTickLength{
			get { return bottomAxis.MinorTickLength; }
			set{
				topAxis.MinorTickLength = value;
				bottomAxis.MinorTickLength = value;
				leftAxis.MinorTickLength = value;
				rightAxis.MinorTickLength = value;
			}
		}
		public float MajorTickLineWidth{
			get { return bottomAxis.MajorTickLineWidth; }
			set{
				topAxis.MajorTickLineWidth = value;
				bottomAxis.MajorTickLineWidth = value;
				leftAxis.MajorTickLineWidth = value;
				rightAxis.MajorTickLineWidth = value;
			}
		}
		public float MinorTickLineWidth{
			get { return bottomAxis.MinorTickLineWidth; }
			set{
				topAxis.MinorTickLineWidth = value;
				bottomAxis.MinorTickLineWidth = value;
				leftAxis.MinorTickLineWidth = value;
				rightAxis.MinorTickLineWidth = value;
			}
		}
		public override Color BackColor{
			get { return base.BackColor; }
			set{
				topAxis.BackColor = value;
				bottomAxis.BackColor = value;
				leftAxis.BackColor = value;
				rightAxis.BackColor = value;
				spacer1.BackColor = value;
				spacer2.BackColor = value;
				spacer3.BackColor = value;
				spacer4.BackColor = value;
				base.BackColor = value;
			}
		}
		public Color FillColor { get; set; }
		public bool FullAxesVisible{
			get { return topAxis.Visible && rightAxis.Visible; }
			set{
				topAxis.Visible = value;
				rightAxis.Visible = value;
			}
		}
		public string XLabel{
			get { return bottomAxis.Text; }
			set{
				topAxis.Text = value + " (full)";
				bottomAxis.Text = value;
			}
		}
		public string YLabel{
			get { return leftAxis.Text; }
			set{
				rightAxis.Text = value + " (full)";
				leftAxis.Text = value;
			}
		}
		public string ZLabel{
			get{
				string s = rightAxis.Text;
				return s.Contains("full") ? s.Substring(0, s.Length - 7) : s;
			}
			set { rightAxis.Text = value; }
		}
		public bool HasSelectButton{
			get { return hasSelectButton; }
			set{
				hasSelectButton = value;
				if (!value){
					selectToolStripMenuItem.Visible = false;
				}
			}
		}
		public bool MenuStripVisible { set { toolStrip1.Visible = value; } get { return toolStrip1.Visible; } }

		private void DeselectMode(){
			try{
				zoomToolStripMenuItem.Checked = false;
				selectToolStripMenuItem.Checked = false;
			} catch (InvalidOperationException){}
		}

		internal void UpdateView(){
			rightAxis.Invalidate();
			leftAxis.Invalidate();
			topAxis.Invalidate();
			bottomAxis.Invalidate();
			scatterPlotPlane.XTics = bottomAxis.GetTics(MainWidth);
			scatterPlotPlane.YTics = leftAxis.GetTics(MainHeight);
			scatterPlotPlane.Invalidate();
		}

		internal double YMin{
			set{
				rightAxis.TotalMin = value;
				rightAxis.ZoomMin = value;
				leftAxis.TotalMin = value;
				leftAxis.ZoomMin = value;
				scatterPlotPlane.TotalYMin = value;
				scatterPlotPlane.ZoomYMin = value;
			}
			get { return rightAxis.TotalMin; }
		}
		internal double YMax{
			set{
				rightAxis.TotalMax = value;
				rightAxis.ZoomMax = value;
				leftAxis.TotalMax = value;
				leftAxis.ZoomMax = value;
				scatterPlotPlane.TotalYMax = value;
				scatterPlotPlane.ZoomYMax = value;
			}
			get { return rightAxis.TotalMax; }
		}
		internal double XMin{
			set{
				topAxis.TotalMin = value;
				topAxis.ZoomMin = value;
				bottomAxis.TotalMin = value;
				bottomAxis.ZoomMin = value;
				scatterPlotPlane.TotalXMin = value;
				scatterPlotPlane.ZoomXMin = value;
			}
			get { return topAxis.TotalMin; }
		}
		internal double XMax{
			set{
				topAxis.TotalMax = value;
				topAxis.ZoomMax = value;
				bottomAxis.TotalMax = value;
				bottomAxis.ZoomMax = value;
				scatterPlotPlane.TotalXMax = value;
				scatterPlotPlane.ZoomXMax = value;
			}
			get { return topAxis.TotalMax; }
		}

		internal void SetMouseModes(ScatterPlotMouseMode zoomablePlaneMouseMode){
			scatterPlotPlane.MouseMode = zoomablePlaneMouseMode;
		}

		internal void InitializeZoomablePlane(ScatterPlot scatterPlot){
			scatterPlotPlane.ScatterPlot = scatterPlot;
		}

		internal int MainWidth{
			get{
				if (tableLayoutControl == null){
					return 0;
				}
				int[] x = tableLayoutView.GetColumnWidths(tableLayoutControl.Width, tableLayoutControl.Height);
				return x.Length < 2 ? 0 : x[1];
			}
		}
		internal int MainHeight{
			get{
				if (tableLayoutControl == null){
					return 0;
				}
				int[] x = tableLayoutView.GetRowHeights(tableLayoutControl.Width, tableLayoutControl.Height);
				return x.Length < 2 ? 0 : x[1];
			}
		}
		internal ScatterPlotPlaneView ScatterPlotPlane { get { return scatterPlotPlane; } }
		public Icon Icon { get; set; }

		protected override void OnResize(EventArgs e){
			base.OnResize(e);
			if (scatterPlotPlane == null){
				return;
			}
			if (tableLayoutView == null){
				return;
			}
			if (bottomAxis != null){
				scatterPlotPlane.XTics = bottomAxis.GetTics(MainWidth);
			}
			if (leftAxis != null){
				scatterPlotPlane.YTics = leftAxis.GetTics(MainHeight);
			}
			scatterPlotPlane.InvalidateImage();
			scatterPlotPlane.Invalidate();
		}

		private void UpdateZoomBottom(object source, double min, double max){
			bottomAxis.ZoomMin = min;
			bottomAxis.ZoomMax = max;
			scatterPlotPlane.XTics = bottomAxis.GetTics(MainWidth);
			scatterPlotPlane.YTics = leftAxis.GetTics(MainHeight);
			bottomAxis.Invalidate();
			scatterPlotPlane.ZoomXMin = min;
			scatterPlotPlane.ZoomXMax = max;
			scatterPlotPlane.Invalidate();
		}

		private void UpdateZoomTop(object source, double min, double max){
			topAxis.ZoomMin = min;
			topAxis.ZoomMax = max;
			scatterPlotPlane.XTics = bottomAxis.GetTics(MainWidth);
			scatterPlotPlane.YTics = leftAxis.GetTics(MainHeight);
			topAxis.Invalidate();
			scatterPlotPlane.ZoomXMin = min;
			scatterPlotPlane.ZoomXMax = max;
			scatterPlotPlane.Invalidate();
		}

		private void UpdateZoomLeft(object source, double min, double max){
			rightAxis.ZoomMin = min;
			rightAxis.ZoomMax = max;
			scatterPlotPlane.XTics = bottomAxis.GetTics(MainWidth);
			scatterPlotPlane.YTics = leftAxis.GetTics(MainHeight);
			rightAxis.Invalidate();
			scatterPlotPlane.ZoomYMin = min;
			scatterPlotPlane.ZoomYMax = max;
			scatterPlotPlane.Invalidate();
		}

		private void UpdateZoomRight(object source, double min, double max){
			leftAxis.ZoomMin = min;
			leftAxis.ZoomMax = max;
			scatterPlotPlane.XTics = bottomAxis.GetTics(MainWidth);
			scatterPlotPlane.YTics = leftAxis.GetTics(MainHeight);
			leftAxis.Invalidate();
			scatterPlotPlane.ZoomYMin = min;
			scatterPlotPlane.ZoomYMax = max;
			scatterPlotPlane.Invalidate();
		}

		private void UpdateZoomFromMap(object source, int imin, int imax, int jmin, int jmax, bool relative, int width,
			int height){
			if (relative){
				if (leftAxis.Zoomable){
					leftAxis.SetZoomFromView(jmin, jmax, height);
				}
				if (bottomAxis.Zoomable){
					bottomAxis.SetZoomFromView(imin, imax, width);
				}
				scatterPlotPlane.XTics = bottomAxis.GetTics(width);
				scatterPlotPlane.YTics = leftAxis.GetTics(height);
			} else{
				if (rightAxis.Zoomable){
					rightAxis.FullZoom();
				}
				if (topAxis.Zoomable){
					topAxis.FullZoom();
				}
				scatterPlotPlane.XTics = bottomAxis.GetTics(width);
				scatterPlotPlane.YTics = leftAxis.GetTics(height);
			}
		}

		private void ZoomInButtonClick(object sender, EventArgs e){
			if (leftAxis.Zoomable){
				leftAxis.ZoomIn(MainHeight);
			}
			if (bottomAxis.Zoomable){
				bottomAxis.ZoomIn(MainWidth);
			}
		}

		private void ZoomOutButtonClick(object sender, EventArgs e){
			if (leftAxis.Zoomable){
				leftAxis.ZoomOut(MainHeight);
			}
			if (bottomAxis.Zoomable){
				bottomAxis.ZoomOut(MainWidth);
			}
		}

		private void FullRangesButtonClick(object sender, EventArgs e){
			if (rightAxis.Zoomable){
				rightAxis.SetZoomFromView(-1, MainHeight, MainHeight);
			}
			if (topAxis.Zoomable){
				topAxis.SetZoomFromView(-1, MainWidth, MainWidth);
			}
		}

		internal void MoveUp(){
			if (leftAxis.Zoomable){
				leftAxis.MoveDown(MainHeight);
			}
		}

		internal void MoveDown(){
			if (leftAxis.Zoomable){
				leftAxis.MoveUp(MainHeight);
			}
		}

		internal void MoveLeft(){
			if (bottomAxis.Zoomable){
				bottomAxis.MoveDown(MainWidth);
			}
		}

		internal void MoveRight(){
			if (bottomAxis.Zoomable){
				bottomAxis.MoveUp(MainWidth);
			}
		}

		private void MoveUpButtonClick(object sender, EventArgs e){
			MoveUp();
		}

		private void MoveDownButtonClick(object sender, EventArgs e){
			MoveDown();
		}

		private void MoveLeftButtonClick(object sender, EventArgs e){
			MoveLeft();
		}

		private void MoveRightButtonClick(object sender, EventArgs e){
			MoveRight();
		}

		private void ZoomOutHorizontalButtonClick(object sender, EventArgs e){
			if (bottomAxis.Zoomable){
				bottomAxis.ZoomOut(MainWidth);
			}
		}

		private void ZoomOutVerticalButtonClick(object sender, EventArgs e){
			if (leftAxis.Zoomable){
				leftAxis.ZoomOut(MainHeight);
			}
		}

		private void ConfigureButtonClick(object sender, EventArgs e){
			PlanePropertiesForm f = new PlanePropertiesForm(scatterPlotPlane.HorizontalGridColor,
				scatterPlotPlane.VerticalGridColor, scatterPlotPlane.HorizontalGrid, scatterPlotPlane.VerticalGrid,
				scatterPlotPlane.HorizontalGridWidth, scatterPlotPlane.VerticalGridWidth, FillColor, scatterPlotPlane.BackColor,
				LineColor, MajorTickLength, MajorTickLineWidth, MinorTickLength, MinorTickLineWidth, topAxis.Visible,
				rightAxis.Visible, LineWidth, (int) NumbersFont.Size, NumbersFont.Bold, (int) LabelFont.Size, LabelFont.Bold,
				scatterPlotPlane.HorizontalZeroColor, scatterPlotPlane.VerticalZeroColor, scatterPlotPlane.HorizontalZeroWidth,
				scatterPlotPlane.VerticalZeroWidth, scatterPlotPlane.HorizontalZeroVisible, scatterPlotPlane.VerticalZeroVisible);
			if (Icon != null){
				f.Icon = Icon;
			}
			f.ShowDialog(ParentForm);
			if (f.Ok){
				scatterPlotPlane.HorizontalGrid = f.HorizontalGrid;
				scatterPlotPlane.VerticalGrid = f.VerticalGrid;
				scatterPlotPlane.HorizontalGridColor = f.HorizontalGridColor;
				scatterPlotPlane.VerticalGridColor = f.VerticalGridColor;
				scatterPlotPlane.HorizontalGridWidth = f.HorizontalGridWidth;
				scatterPlotPlane.VerticalGridWidth = f.VerticalGridWidth;
				scatterPlotPlane.HorizontalZeroColor = f.HorizontalZeroColor;
				scatterPlotPlane.VerticalZeroColor = f.VerticalZeroColor;
				scatterPlotPlane.HorizontalZeroWidth = f.HorizontalZeroWidth;
				scatterPlotPlane.VerticalZeroWidth = f.VerticalZeroWidth;
				scatterPlotPlane.HorizontalZeroVisible = f.HorizontalZeroVisible;
				scatterPlotPlane.VerticalZeroVisible = f.VerticalZeroVisible;
				FillColor = f.FillColor;
				LineColor = f.LineColor;
				scatterPlotPlane.BackColor = f.BackgroundColor;
				topAxis.Visible = f.TopAxesVisible;
				rightAxis.Visible = f.RightAxesVisible;
				MajorTickLength = f.MajorTickLength;
				MajorTickLineWidth = f.MajorTickLineWidth;
				MinorTickLength = f.MinorTickLength;
				MinorTickLineWidth = f.MinorTickLineWidth;
				LineWidth = f.LineWidth;
				NumbersFont = new Font("Arial", f.NumbersFontSize, f.NumbersFontBold ? FontStyle.Bold : FontStyle.Regular);
				LabelFont = new Font("Arial Unicode MS", f.TitleFontSize, f.TitleFontBold ? FontStyle.Bold : FontStyle.Regular);
				scatterPlotPlane.InvalidateImage();
				Invalidate(true);
			}
			f.Dispose();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
			switch (keyData){
				case Keys.Down:{
					MoveDown();
				}
					break;
				case Keys.Up:{
					MoveUp();
				}
					break;
				case Keys.Left:{
					MoveLeft();
				}
					break;
				case Keys.Right:{
					MoveRight();
				}
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void ZoomToolStripMenuItemClick(object sender, EventArgs e){
			ComponentResourceManager resources = new ComponentResourceManager(typeof (ScatterPlotViewer));
			modeDropDownButton.Image = ((Image) (resources.GetObject("zoomToolStripMenuItem.Image")));
			DeselectMode();
			zoomToolStripMenuItem.Checked = true;
			SetMouseModes(ScatterPlotMouseMode.Zoom);
		}

		private void SelectToolStripMenuItemClick(object sender, EventArgs e){
			ComponentResourceManager resources = new ComponentResourceManager(typeof (ScatterPlotViewer));
			modeDropDownButton.Image = ((Image) (resources.GetObject("selectToolStripMenuItem.Image")));
			DeselectMode();
			selectToolStripMenuItem.Checked = true;
			SetMouseModes(ScatterPlotMouseMode.Select);
		}

		private void SaveAsButtonClick(object sender, EventArgs e){
			Printing.Print(tableLayoutView, "ScatterPlot", tableLayoutControl.Width, tableLayoutControl.Height);
			scatterPlotPlane.Invalidate();
		}

		internal Control GetTableLayoutPanel(){
			return tableLayoutControl;
		}

		protected override void OnSizeChanged(EventArgs e){
			base.OnSizeChanged(e);
			if (sizeLabel != null && scatterPlotPlane != null){
				sizeLabel.Text = "width=" + Width + ", height=" + (Height - 24);
			}
		}

		public void InvalidatePane(){
			scatterPlotPlane.InvalidateData();
			scatterPlotPlane.Invalidate();
		}
	}
}