using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms.Axis;
using BasicLib.Forms.Base;

namespace BasicLib.Forms.Scatter{
	public partial class ScatterPlotViewer : UserControl{
		private readonly ScatterPlotPlaneView zoomablePlane;
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

		internal ScatterPlotViewer(){
			bottomAxis = new NumericAxisView();
			leftAxis = new NumericAxisView();
			topAxis = new NumericAxisView();
			rightAxis = new NumericAxisView();
			zoomablePlane = new ScatterPlotPlaneView();
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
			tableLayoutView.Add(zoomablePlane, 1, 1);
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
			zoomablePlane.BackColor = Color.White;
			zoomablePlane.ForeColor = SystemColors.HotTrack;
			zoomablePlane.IndicatorColor = Color.Transparent;
			zoomablePlane.MouseMode = ScatterPlotMouseMode.Zoom;
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
			zoomablePlane.OnZoomChange += UpdateZoomFromMap;
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

		public float NumbersFontSize{
			get { return bottomAxis.numbersFont.Size; }
			set{
				topAxis.numbersFont.Size = value;
				bottomAxis.numbersFont.Size = value;
				leftAxis.numbersFont.Size = value;
				rightAxis.numbersFont.Size = value;
			}
		}
		public float LabelFontSize{
			get { return bottomAxis.labelFont.Size; }
			set{
				topAxis.labelFont.Size = value;
				bottomAxis.labelFont.Size = value;
				leftAxis.labelFont.Size = value;
				rightAxis.labelFont.Size = value;
			}
		}
		public bool NumbersFontBold{
			get { return bottomAxis.numbersFont.Bold; }
			set{
				topAxis.numbersFont.Bold = value;
				bottomAxis.numbersFont.Bold = value;
				leftAxis.numbersFont.Bold = value;
				rightAxis.numbersFont.Bold = value;
			}
		}
		public bool LabelFontBold{
			get { return bottomAxis.labelFont.Bold; }
			set{
				topAxis.labelFont.Bold = value;
				bottomAxis.labelFont.Bold = value;
				leftAxis.labelFont.Bold = value;
				rightAxis.labelFont.Bold = value;
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
		public bool XIsLogarithmic{
			get { return bottomAxis.IsLogarithmic; }
			set{
				bottomAxis.IsLogarithmic = value;
				topAxis.IsLogarithmic = value;
			}
		}
		public bool YIsLogarithmic{
			get { return leftAxis.IsLogarithmic; }
			set{
				leftAxis.IsLogarithmic = value;
				rightAxis.IsLogarithmic = value;
			}
		}
		public bool MenuStripVisible { set { toolStrip1.Visible = value; } get { return toolStrip1.Visible; } }

		private void DeselectMode(){
			try{
				zoomToolStripMenuItem.Checked = false;
				selectToolStripMenuItem.Checked = false;
			} catch (InvalidOperationException){}
		}

		internal void SetRange(double xMin, double xMax, double yMin, double yMax){
			rightAxis.TotalMin = yMin;
			rightAxis.ZoomMin = yMin;
			rightAxis.TotalMax = yMax;
			rightAxis.ZoomMax = yMax;
			leftAxis.TotalMin = yMin;
			leftAxis.ZoomMin = yMin;
			leftAxis.TotalMax = yMax;
			leftAxis.ZoomMax = yMax;
			topAxis.TotalMin = xMin;
			topAxis.ZoomMin = xMin;
			topAxis.TotalMax = xMax;
			topAxis.ZoomMax = xMax;
			bottomAxis.TotalMin = xMin;
			bottomAxis.ZoomMin = xMin;
			bottomAxis.TotalMax = xMax;
			bottomAxis.ZoomMax = xMax;
			zoomablePlane.TotalYMin = rightAxis.TotalMin;
			zoomablePlane.TotalYMax = rightAxis.TotalMax;
			zoomablePlane.TotalXMin = topAxis.TotalMin;
			zoomablePlane.TotalXMax = topAxis.TotalMax;
			zoomablePlane.ZoomYMin = rightAxis.TotalMin;
			zoomablePlane.ZoomYMax = rightAxis.TotalMax;
			zoomablePlane.ZoomXMin = topAxis.TotalMin;
			zoomablePlane.ZoomXMax = topAxis.TotalMax;
			rightAxis.Invalidate();
			leftAxis.Invalidate();
			topAxis.Invalidate();
			bottomAxis.Invalidate();
			zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			zoomablePlane.Invalidate();
		}

		internal void SetRange(double xMin, double xMax, double yMin, double yMax, double zMin, double zMax){
			rightAxis.TotalMin = zMin;
			rightAxis.ZoomMin = zMin;
			rightAxis.TotalMax = zMax;
			rightAxis.ZoomMax = zMax;
			leftAxis.TotalMin = yMin;
			leftAxis.ZoomMin = yMin;
			leftAxis.TotalMax = yMax;
			leftAxis.ZoomMax = yMax;
			topAxis.TotalMin = xMin;
			topAxis.ZoomMin = xMin;
			topAxis.TotalMax = xMax;
			topAxis.ZoomMax = xMax;
			bottomAxis.TotalMin = xMin;
			bottomAxis.ZoomMin = xMin;
			bottomAxis.TotalMax = xMax;
			bottomAxis.ZoomMax = xMax;
			zoomablePlane.TotalYMin = rightAxis.TotalMin;
			zoomablePlane.TotalYMax = rightAxis.TotalMax;
			zoomablePlane.TotalXMin = topAxis.TotalMin;
			zoomablePlane.TotalXMax = topAxis.TotalMax;
			zoomablePlane.ZoomYMin = rightAxis.TotalMin;
			zoomablePlane.ZoomYMax = rightAxis.TotalMax;
			zoomablePlane.ZoomXMin = topAxis.TotalMin;
			zoomablePlane.ZoomXMax = topAxis.TotalMax;
			rightAxis.Invalidate();
			leftAxis.Invalidate();
			topAxis.Invalidate();
			bottomAxis.Invalidate();
			zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			zoomablePlane.Invalidate();
		}

		internal void SetMouseModes(AxisMouseMode axisMouseMode, ScatterPlotMouseMode zoomablePlaneMouseMode){
			topAxis.MouseMode = axisMouseMode;
			rightAxis.MouseMode = axisMouseMode;
			bottomAxis.MouseMode = axisMouseMode;
			leftAxis.MouseMode = axisMouseMode;
			zoomablePlane.MouseMode = zoomablePlaneMouseMode;
		}

		internal void InitializeZoomablePlane(ScatterPlot scatterPlot){
			zoomablePlane.ScatterPlot = scatterPlot;
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
		internal ScatterPlotPlaneView ScatterPlotPlane { get { return zoomablePlane; } }

		protected override void OnResize(EventArgs e){
			base.OnResize(e);
			if (zoomablePlane == null){
				return;
			}
			if (tableLayoutView == null){
				return;
			}
			if (bottomAxis != null){
				zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			}
			if (leftAxis != null){
				zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			}
			zoomablePlane.InvalidateImage();
			zoomablePlane.Invalidate();
		}

		private void UpdateZoomBottom(object source, double min, double max){
			bottomAxis.ZoomMin = min;
			bottomAxis.ZoomMax = max;
			zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			bottomAxis.Invalidate();
			zoomablePlane.ZoomXMin = min;
			zoomablePlane.ZoomXMax = max;
			zoomablePlane.Invalidate();
		}

		private void UpdateZoomTop(object source, double min, double max){
			topAxis.ZoomMin = min;
			topAxis.ZoomMax = max;
			zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			topAxis.Invalidate();
			zoomablePlane.ZoomXMin = min;
			zoomablePlane.ZoomXMax = max;
			zoomablePlane.Invalidate();
		}

		private void UpdateZoomLeft(object source, double min, double max){
			rightAxis.ZoomMin = min;
			rightAxis.ZoomMax = max;
			zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			rightAxis.Invalidate();
			zoomablePlane.ZoomYMin = min;
			zoomablePlane.ZoomYMax = max;
			zoomablePlane.Invalidate();
		}

		private void UpdateZoomRight(object source, double min, double max){
			leftAxis.ZoomMin = min;
			leftAxis.ZoomMax = max;
			zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
			zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			leftAxis.Invalidate();
			zoomablePlane.ZoomYMin = min;
			zoomablePlane.ZoomYMax = max;
			zoomablePlane.Invalidate();
		}

		private void UpdateZoomFromMap(Object source, int imin, int imax, int jmin, int jmax, bool relative){
			if (relative){
				if (leftAxis.Zoomable){
					leftAxis.SetZoomFromView(jmin, jmax, MainHeight);
				}
				if (bottomAxis.Zoomable){
					bottomAxis.SetZoomFromView(imin, imax, MainWidth);
				}
				zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
				zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
			} else{
				if (rightAxis.Zoomable){
					rightAxis.FullZoom();
				}
				if (topAxis.Zoomable){
					topAxis.FullZoom();
				}
				zoomablePlane.XTics = bottomAxis.GetTics(MainWidth);
				zoomablePlane.YTics = leftAxis.GetTics(MainHeight);
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
			PlanePropertiesForm f = new PlanePropertiesForm(zoomablePlane.HorizontalGridColor, zoomablePlane.VerticalGridColor,
				zoomablePlane.HorizontalGrid, zoomablePlane.VerticalGrid, zoomablePlane.HorizontalGridWidth,
				zoomablePlane.VerticalGridWidth, FillColor, zoomablePlane.BackColor, LineColor, MajorTickLength, MajorTickLineWidth,
				MinorTickLength, MinorTickLineWidth, topAxis.Visible, rightAxis.Visible, LineWidth, (int) NumbersFontSize,
				NumbersFontBold, (int) LabelFontSize, LabelFontBold, zoomablePlane.HorizontalZeroColor,
				zoomablePlane.VerticalZeroColor, zoomablePlane.HorizontalZeroWidth, zoomablePlane.VerticalZeroWidth,
				zoomablePlane.HorizontalZeroVisible, zoomablePlane.VerticalZeroVisible);
			f.ShowDialog(ParentForm);
			if (f.Ok){
				zoomablePlane.HorizontalGrid = f.HorizontalGrid;
				zoomablePlane.VerticalGrid = f.VerticalGrid;
				zoomablePlane.HorizontalGridColor = f.HorizontalGridColor;
				zoomablePlane.VerticalGridColor = f.VerticalGridColor;
				zoomablePlane.HorizontalGridWidth = f.HorizontalGridWidth;
				zoomablePlane.VerticalGridWidth = f.VerticalGridWidth;
				zoomablePlane.HorizontalZeroColor = f.HorizontalZeroColor;
				zoomablePlane.VerticalZeroColor = f.VerticalZeroColor;
				zoomablePlane.HorizontalZeroWidth = f.HorizontalZeroWidth;
				zoomablePlane.VerticalZeroWidth = f.VerticalZeroWidth;
				zoomablePlane.HorizontalZeroVisible = f.HorizontalZeroVisible;
				zoomablePlane.VerticalZeroVisible = f.VerticalZeroVisible;
				FillColor = f.FillColor;
				LineColor = f.LineColor;
				zoomablePlane.BackColor = f.BackgroundColor;
				topAxis.Visible = f.TopAxesVisible;
				rightAxis.Visible = f.RightAxesVisible;
				MajorTickLength = f.MajorTickLength;
				MajorTickLineWidth = f.MajorTickLineWidth;
				MinorTickLength = f.MinorTickLength;
				MinorTickLineWidth = f.MinorTickLineWidth;
				LineWidth = f.LineWidth;
				NumbersFontSize = f.NumbersFontSize;
				NumbersFontBold = f.NumbersFontBold;
				LabelFontSize = f.TitleFontSize;
				LabelFontBold = f.TitleFontBold;
				zoomablePlane.InvalidateImage();
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
			SetMouseModes(AxisMouseMode.Zoom, ScatterPlotMouseMode.Zoom);
		}

		private void SelectToolStripMenuItemClick(object sender, EventArgs e){
			ComponentResourceManager resources = new ComponentResourceManager(typeof (ScatterPlotViewer));
			modeDropDownButton.Image = ((Image) (resources.GetObject("selectToolStripMenuItem.Image")));
			DeselectMode();
			selectToolStripMenuItem.Checked = true;
			SetMouseModes(AxisMouseMode.Zoom, ScatterPlotMouseMode.Select);
		}

		private void SaveAsButtonClick(object sender, EventArgs e){
			//ExportGraphics.ExportGraphic(tableLayoutControl, "ScatterPlot");
			Printing.Print(tableLayoutView, "ScatterPlot", tableLayoutControl.Width, tableLayoutControl.Height);
		}

		internal Control GetTableLayoutPanel(){
			return tableLayoutControl;
		}

		protected override void OnSizeChanged(EventArgs e){
			base.OnSizeChanged(e);
			if (sizeLabel != null && zoomablePlane != null){
				sizeLabel.Text = "width=" + Width + ", height=" + (Height - 24);
			}
		}
	}
}