using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms.Colors;
using BasicLib.Graphic;
using BasicLib.Symbol;

namespace BasicLib.Forms.Scatter{
	public partial class ScatterPlot : UserControl{
		private ScatterPlotData scatterPlotData;
		private Action<int> labelTypeChange;

		public ScatterPlot(){
			InitializeComponent();
			scatterPlotViewer.InitializeZoomablePlane(this);
			showLabelsComboBox.SelectedIndex = 0;
			labelEditComboBox.SelectedIndex = 1;
			labelTypeComboBox.SelectedIndexChanged += LabelTypeComboBoxSelectedIndexChanged;
			labelEditComboBox.SelectedIndexChanged += LabelEditComboBoxSelectedIndexChanged;
			showLabelsComboBox.SelectedIndexChanged += ShowLabelsComboBoxSelectedIndexChanged;
			scatterPlotViewer.FullAxesVisible = false;
		}

		public Func<int, SymbolProperties> GetPointProperties { set { ScatterPlotPlane.GetPointProperties = value; } }
		internal ScatterPlotPlaneView ScatterPlotPlane { get { return scatterPlotViewer.ScatterPlotPlane; } }
		public Func<int, PolygonProperties> GetPolygonProperties { set { ScatterPlotPlane.GetPolygonProperties = value; } }
		public Func<int, PolygonData> GetPolygon { set { ScatterPlotPlane.GetPolygon = value; } }
		public Func<int> GetPolygonCount { set { ScatterPlotPlane.GetPolygonCount = value; } }
		public ColorScale ColorScale { set { ScatterPlotPlane.ColorScale = value; } }
		public Action<IGraphics, int, int, Func<double, int, int>, Func<double, int, int>, Func<int, int, double>, Func<int, int, double>>
			DrawFunctions { set { ScatterPlotPlane.drawFunctions = value; } }

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
			scatterPlotData.Labels = labels;
		}

		public ScatterPlotData ScatterPlotData{
			get { return scatterPlotData; }
			set{
				scatterPlotData = value;
				if (value != null){
					scatterPlotViewer.XIsLogarithmic = scatterPlotData.XIsLogarithmic;
					scatterPlotViewer.YIsLogarithmic = scatterPlotData.YIsLogarithmic;
					if (scatterPlotData.IsEmpty){
						SetRange(-3, 3, -3, 3);
					} else{
						if (scatterPlotData.AutoRange){
							CalcRanges(scatterPlotData);
						} else{
							SetRange(scatterPlotData.XMin, scatterPlotData.XMax, scatterPlotData.YMin, scatterPlotData.YMax);
						}
					}
					if (scatterPlotData.XLabel != null){
						XLabel = scatterPlotData.XLabel;
					}
					if (scatterPlotData.YLabel != null){
						YLabel = scatterPlotData.YLabel;
					}
					ScatterPlotPlane.SetScatterPlotData(value, scatterPlotViewer.MainWidth, scatterPlotViewer.MainHeight);
				}
			}
		}
		//TODO: zrange
		private void CalcRanges(ScatterPlotData scatterPlotDat){
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
				scatterPlotDat.GetData(out x, out y, out z, out labels, out index);
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
			scatterPlotDat.Reset();
			if (!Double.IsNaN(scatterPlotDat.XMin)){
				xmin = scatterPlotDat.XMin;
			}
			if (!Double.IsNaN(scatterPlotDat.XMax)){
				xmax = scatterPlotDat.XMax;
			}
			if (!Double.IsNaN(scatterPlotDat.YMin)){
				ymin = scatterPlotDat.YMin;
			}
			if (!Double.IsNaN(scatterPlotDat.YMax)){
				ymax = scatterPlotDat.YMax;
			}
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
			if (scatterPlotData != null){
				scatterPlotData.InvalidateData();
			}
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