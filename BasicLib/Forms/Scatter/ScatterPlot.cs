using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms.Colors;
using BasicLib.Graphic;
using BasicLib.Symbol;
using BasicLib.Util;

namespace BasicLib.Forms.Scatter{
	public partial class ScatterPlot : UserControl{
		private ScatterPlotData scatterPlotData;
		private int[] selection = new int[0];
		private Action<int> labelTypeChange;
		public event EventHandler SelectionChanged;
		internal ScatterPlotValues zvals;
		internal IList<string> labels;

		internal void FireSelectionChanged(){
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public ScatterPlot(){
			InitializeComponent();
			scatterPlotViewer.InitializeZoomablePlane(this);
			showLabelsComboBox.SelectedIndex = 0;
			labelEditComboBox.SelectedIndex = 1;
			labelTypeComboBox.SelectedIndexChanged += LabelTypeComboBoxSelectedIndexChanged;
			labelEditComboBox.SelectedIndexChanged += LabelEditComboBoxSelectedIndexChanged;
			showLabelsComboBox.SelectedIndexChanged += ShowLabelsComboBoxSelectedIndexChanged;
			scatterPlotViewer.FullAxesVisible = false;
			ColorMax = double.NaN;
			ColorMin = double.NaN;
		}

		public ScatterPlotValues XValues { get; set; }
		public ScatterPlotValues YValues { get; set; }
		public ScatterPlotValues ColorValues {
			get { return zvals; }
			set {
				ColorMin = double.NaN;
				ColorMax = double.NaN;
				zvals = value;
			}
		}
		public IList<string> Labels { get { return labels; } set { labels = value; } }

		public double ColorMin { get; set; }
		public double ColorMax { get; set; }
		public string ColorLabel { get; set; }
		public bool HasLabels { get { return labels != null; } }

		public string GetLabelAt(int index) {
			return labels[index];
		}

		public bool IsEmpty { get { return XValues == null || XValues.Length == 0; } }

		public int[] Selection {
			get { return selection; }
			set {
				selection = value;
				Array.Sort(selection);
			}
		}

		public void Select(double x1, double x2, double y1, double y2, bool add, bool toggle) {
			if (toggle){
				HashSet<int> sel = add ? new HashSet<int>(Selection) : new HashSet<int>();
				for (int i = 0; i < XValues.Length; i++){
					if (XValues.SingleValues[i] >= x1 && XValues.SingleValues[i] <= x2 &&
						YValues.SingleValues[i] >= y1 && YValues.SingleValues[i] <= y2){
						if (sel.Contains(i)){
							sel.Remove(i);
						} else{
							sel.Add(i);
						}
					}
				}
				Selection = ArrayUtils.ToArray(sel);
			} else{
				List<int> sel = new List<int>();
				for (int i = 0; i < XValues.Length; i++){
					if (XValues.SingleValues[i] >= x1 && XValues.SingleValues[i] <= x2 &&
						YValues.SingleValues[i] >= y1 && YValues.SingleValues[i] <= y2){
						sel.Add(i);
					}
				}
				if (add && Selection.Length > 0){
					Selection = ArrayUtils.UniqueValues(ArrayUtils.Concat(sel.ToArray(), Selection));
				} else{
					Selection = sel.ToArray();
				}
			}
			FireSelectionChanged();
		}

		public bool IsSelected(int ind) {
			return Array.BinarySearch(selection, ind) >= 0;
		}

		public void AddPoint(double x, double y) {
			XValues.AddValue(x);
			YValues.AddValue(y);
		}

		public void AddPoint(double x, double y, double z) {
			XValues.AddValue(x);
			YValues.AddValue(y);
			ColorValues.AddValue(z);
		}

		public void AddPoint(double[] x, double[] y) {
			XValues.AddValue(x);
			YValues.AddValue(y);
		}

		public void AddPoint(double[] x, double[] y, double[] z) {
			XValues.AddValue(x);
			YValues.AddValue(y);
			ColorValues.AddValue(z);
		}

		public double[] GetDataAt(int index) {
			if (XValues == null || YValues == null) {
				return new double[0];
			}
			return XValues.Length == YValues.Length
				? new[] { XValues.SingleValues[index], YValues.SingleValues[index] } : new double[] { };
		}

		private int count;
		public void Reset() {
			count = 0;
		}

		public void GetData(out double[] x, out double[] y, out double[] z, out string[] label, out int index) {
			x = null;
			y = null;
			z = null;
			label = null;
			index = count;
			if (XValues == null || YValues == null || count >= XValues.Length || XValues.Length != YValues.Length) {
				return;
			}
			if (!XValues.IsMulti && !YValues.IsMulti) {
				try {
					x = new[] { XValues.SingleValues[count] };
					y = new[] { YValues.SingleValues[count] };
					if (zvals != null && XValues.Length == zvals.Length) {
						z = new[] { zvals.SingleValues[count] };
					}
					if (labels != null && count < labels.Count) {
						label = new[] { labels[count] };
					}
				} catch (Exception) { }
				count++;
				return;
			}
			if (!XValues.IsMulti && YValues.IsMulti) {
				y = YValues.MultiValues[count];
				x = ArrayUtils.FillArray(i => XValues.SingleValues[count], y.Length);
				if (zvals != null && XValues.Length == zvals.Length) {
					if (zvals.IsMulti) {
						if (zvals.MultiValues[count].Length == y.Length) {
							z = zvals.MultiValues[count];
						}
					} else {
						z = ArrayUtils.FillArray(i => zvals.SingleValues[count], y.Length);
					}
				}
				if (labels != null && count < labels.Count) {
					label = ArrayUtils.FillArray(i => labels[count], y.Length);
				}
				count++;
				return;
			}
			if (XValues.IsMulti && !YValues.IsMulti) {
				x = XValues.MultiValues[count];
				y = ArrayUtils.FillArray(i => YValues.SingleValues[count], x.Length);
				if (zvals != null && XValues.Length == zvals.Length) {
					if (zvals.IsMulti) {
						if (zvals.MultiValues[count].Length == x.Length) {
							z = zvals.MultiValues[count];
						}
					} else {
						z = ArrayUtils.FillArray(i => zvals.SingleValues[count], x.Length);
					}
				}
				if (labels != null && count < labels.Count) {
					label = ArrayUtils.FillArray(i => labels[count], x.Length);
				}
				count++;
				return;
			}
			if (XValues.IsMulti && YValues.IsMulti) {
				x = XValues.MultiValues[count];
				y = YValues.MultiValues[count];
				if (zvals != null && XValues.Length == zvals.Length) {
					if (zvals.IsMulti) {
						if (zvals.MultiValues[count].Length == x.Length) {
							z = zvals.MultiValues[count];
						}
					} else {
						z = ArrayUtils.FillArray(i => zvals.SingleValues[count], x.Length);
					}
				}
				if (labels != null && count < labels.Count) {
					label = ArrayUtils.FillArray(i => labels[count], x.Length);
				}
				count++;
			}
		}

		public Icon Icon { set { scatterPlotViewer.Icon = value; } }
		public Func<int, SymbolProperties> GetPointProperties { set { ScatterPlotPlane.GetPointProperties = value; } }
		internal ScatterPlotPlaneView ScatterPlotPlane { get { return scatterPlotViewer.ScatterPlotPlane; } }
		public Func<int, PolygonProperties> GetPolygonProperties { set { ScatterPlotPlane.GetPolygonProperties = value; } }
		public Func<int, PolygonData> GetPolygon { set { ScatterPlotPlane.GetPolygon = value; } }
		public Func<int> GetPolygonCount { set { ScatterPlotPlane.GetPolygonCount = value; } }
		public ColorScale ColorScale { set { ScatterPlotPlane.ColorScale = value; } }
		public
			Action
				<IGraphics, int, int, Func<double, int, int>, Func<double, int, int>, Func<int, int, double>, Func<int, int, double>
					> DrawFunctions { set { ScatterPlotPlane.drawFunctions = value; } }

		public void AddToolStripItem(ToolStripItem item){
			toolStrip.Items.Add(item);
		}

		public int FontSize { get { return int.Parse(fontSizeTextBox.Text); } }
		public FontStyle FontStyle { get { return boldButton.Checked ? FontStyle.Bold : FontStyle.Regular; } }
		private bool menuStripVisible;
		public bool MenuStripVisible{
			get { return menuStripVisible; }
			set{
				menuStripVisible = value;
				toolStrip.Visible = value;
			}
		}

		public void SetLabelOptions(string[] s, Action<int> labelTypeChange1){
			labelTypeComboBox.Visible = true;
			labelTypeComboBox.Items.AddRange(s);
			labelTypeChange = labelTypeChange1;
			if (labelTypeComboBox.Items.Count > 0){
				labelTypeComboBox.SelectedIndex = 0;
			}
		}

		internal ScatterPlotLabelMode GetLabelMode(){
			int index = showLabelsComboBox.SelectedIndex;
			switch (index){
				case 0:
					return ScatterPlotLabelMode.None;
				case 1:
					return ScatterPlotLabelMode.Selected;
				case 2:
					return ScatterPlotLabelMode.All;
				default:
					throw new Exception("Never get here.");
			}
		}

		public void SetLabels(IList<string> labels){
			Labels = labels;
		}

		public ScatterPlotData ScatterPlotData{
			get { return scatterPlotData; }
			set{
				scatterPlotData = value;
				if (value != null){
					if (IsEmpty){
						SetRange(-3, 3, -3, 3);
					} else{
						CalcRanges();
					}
				}
				ScatterPlotPlane.SetScatterPlotData(scatterPlotViewer.MainWidth, scatterPlotViewer.MainHeight);
			}
		}
		//TODO: zrange
		private void CalcRanges(){
			double xmin = double.MaxValue;
			double xmax = double.MinValue;
			double ymin = double.MaxValue;
			double ymax = double.MinValue;
			for (;;){
				double[] x;
				double[] y;
				double[] z;
				string[] labels;
				int index;
				GetData(out x, out y, out z, out labels, out index);
				if (x == null || y == null){
					break;
				}
				for (int i = 0; i < x.Length; i++){
					if (double.IsInfinity(x[i]) || double.IsInfinity(y[i])){
						continue;
					}
					if (x[i] < xmin){
						xmin = x[i];
					}
					if (x[i] > xmax){
						xmax = x[i];
					}
					if (y[i] < ymin){
						ymin = y[i];
					}
					if (y[i] > ymax){
						ymax = y[i];
					}
				}
			}
			double dx = xmax - xmin;
			double dy = ymax - ymin;
			xmin -= 0.05*dx;
			xmax += 0.05*dx;
			ymin -= 0.05*dy;
			ymax += 0.05*dy;
			Reset();
			SetRange(xmin, xmax, ymin, ymax);
		}

		public bool HasSelectButton { get { return scatterPlotViewer.HasSelectButton; } set { scatterPlotViewer.HasSelectButton = value; } }
		public string XLabel { get { return scatterPlotViewer.XLabel; } set { scatterPlotViewer.XLabel = value; } }
		public string YLabel { get { return scatterPlotViewer.YLabel; } set { scatterPlotViewer.YLabel = value; } }
		public Color SelectionColor { set { ScatterPlotPlane.SelectionColor = value; } get { return ScatterPlotPlane.SelectionColor; } }
		public bool CutLabels { get { return labelEditComboBox.SelectedIndex == 1; } }
		public ScatterPlotViewer ScatterPlotViewer { get { return scatterPlotViewer; } }

		public void SetRange(double xMin, double xMax, double yMin, double yMax){
			scatterPlotViewer.SetRange(xMin, xMax, yMin, yMax);
		}

		public void InvalidateData(){
			ColorMin = double.NaN;
			ColorMax = double.NaN;
			ScatterPlotPlane.InvalidateData(true);
			ScatterPlotPlane.Invalidate();
		}

		private void ShowLabelsComboBoxSelectedIndexChanged(object sender, EventArgs e){
			Invalidate(true);
		}

		private void LabelEditComboBoxSelectedIndexChanged(object sender, EventArgs e){
			if (labelTypeChange == null){
				return;
			}
			if (labelTypeComboBox != null){
				labelTypeChange(labelTypeComboBox.SelectedIndex);
				Invalidate(true);
			}
		}

		private void LabelTypeComboBoxSelectedIndexChanged(object sender, EventArgs e){
			if (labelTypeChange == null){
				return;
			}
			if (labelTypeComboBox != null){
				labelTypeChange(labelTypeComboBox.SelectedIndex);
				Invalidate(true);
			}
		}

		private void BoldButtonClick(object sender, EventArgs e){
			Invalidate(true);
		}

		private void LabelEditComboBoxClick(object sender, EventArgs e){
			Invalidate(true);
		}
	}
}