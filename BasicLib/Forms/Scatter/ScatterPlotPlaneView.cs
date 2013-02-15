using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using BasicLib.Forms.Base;
using BasicLib.Forms.Colors;
using BasicLib.Graphic;
using BasicLib.Symbol;
using BasicLib.Util;

namespace BasicLib.Forms.Scatter{
	internal enum ScatterPlotMouseMode{
		Zoom,
		Select
	}

	public enum GridType{
		None,
		Major,
		All
	}

	public delegate void ViewZoom2DChangeHandler(
		object source, int imin, int imax, int jmin, int jmax, bool relativeToVisibleAxis, int width, int height);

	public enum ScatterPlotLabelMode{
		None,
		Selected,
		All
	}

	internal class ScatterPlotPlaneView : BasicView{
		internal event ViewZoom2DChangeHandler OnZoomChange;
		private double zoomXMin;
		private double zoomXMax;
		private double zoomYMin;
		private double zoomYMax;
		private int indicatorX1 = -1;
		private int indicatorX2 = -1;
		private int indicatorY1 = -1;
		private int indicatorY2 = -1;
		internal ToolStripStatusLabel statusLabel;
		protected UnsafeBitmap backBuffer;
		private int prevWidth = -1;
		private int prevHeight = -1;
		protected internal Thread recalcValuesThread;
		private bool paintBackground = true;
		private bool lockBackBuffer;
		internal ScatterPlotMouseMode MouseMode { get; set; }
		internal Color IndicatorColor { get; set; }
		internal double TotalXMin { get; set; }
		internal double TotalXMax { get; set; }
		internal double TotalYMin { get; set; }
		internal double TotalYMax { get; set; }
		internal Color HorizontalGridColor { get; set; }
		internal Color VerticalGridColor { get; set; }
		internal int HorizontalGridWidth { get; set; }
		internal int VerticalGridWidth { get; set; }
		internal Color HorizontalZeroColor { get; set; }
		internal Color VerticalZeroColor { get; set; }
		internal int HorizontalZeroWidth { get; set; }
		internal int VerticalZeroWidth { get; set; }
		internal bool HorizontalZeroVisible { get; set; }
		internal bool VerticalZeroVisible { get; set; }
		internal double[][] XTics { get; set; }
		internal double[][] YTics { get; set; }
		internal GridType HorizontalGrid { get; set; }
		internal GridType VerticalGrid { get; set; }
		internal int PrevWidth { set { prevWidth = value; } }
		internal int PrevHeight { set { prevHeight = value; } }
		internal bool PaintBackground { get { return paintBackground; } set { paintBackground = value; } }
		private ScatterPlotData scatterPlotData;
		private readonly Dictionary<SymbolProperties, Tuple<int, bool[,]>> values =
			new Dictionary<SymbolProperties, Tuple<int, bool[,]>>();
		private  ScatterPlot scatterPlot;
		private Dictionary<SymbolProperties, double[,]> zValues;
		private ColorScale colorScale;
		private Color selectionColor = Color.Red;
		private Rectangle area = Rectangle.Empty;
		private bool vectorGraphics;
		internal Func<int, SymbolProperties> GetPointProperties { get; set; }
		internal Func<int, PolygonProperties> GetPolygonProperties { get; set; }
		internal Func<int, PolygonData> GetPolygon { get; set; }
		internal Func<int> GetPolygonCount { get; set; }
		private static readonly SymbolProperties defaultSymbol = new SymbolProperties(Color.DarkGray, 4, 1);
		internal Action<IGraphics, int, int,Func<double, int, int> , Func<double, int, int> ,
			Func<int, int, double>, Func<int, int, double>> drawFunctions;


		internal ScatterPlot ScatterPlot{
			set { scatterPlot = value; }
		}

		protected internal override void OnResize(EventArgs e, int width, int height){
			NeedRecalcValues(width, height);
		}

		internal ScatterPlotPlaneView(){
			VerticalGrid = GridType.None;
			HorizontalGrid = GridType.None;
			VerticalGridWidth = 1;
			HorizontalGridWidth = 1;
			VerticalGridColor = Color.LightGray;
			HorizontalGridColor = Color.LightGray;
			MouseMode = ScatterPlotMouseMode.Zoom;
			VerticalZeroWidth = 1;
			HorizontalZeroWidth = 1;
			VerticalZeroColor = Color.Black;
			HorizontalZeroColor = Color.Black;
			MouseMode = ScatterPlotMouseMode.Zoom;
		}

		internal ColorScale ColorScale{
			set{
				colorScale = value;
				if (value != null){
					colorScale.InitColors(
						new[]{Color.FromArgb(0, 0, 255, 0), Color.Yellow, Color.Orange, Color.Red, Color.Blue, Color.Cyan},
						new[]{0.0, 0.05, 0.1, 0.25, 0.5, 1.0});
					colorScale.OnColorChange += UpdateColor;
					zValues = new Dictionary<SymbolProperties, double[,]>();
				}
			}
		}

		internal void UpdateColor(){
			InvalidateData(false);
			InvalidateImage();
			Invalidate();
		}

		internal void SetScatterPlotData(ScatterPlotData value, int width, int height){
			scatterPlotData = value;
			InvalidateData(false);
			if (value != null && !value.IsEmpty){
				NeedRecalcValues(width, height);
			} else {
				RecalcValuesImpl(width, height);
				PrevWidth = width;
				PrevHeight = height;				
			}
			Invalidate();
		}

		internal ScatterPlotData GetScatterPlotData(){
			return scatterPlotData;
		}

		internal Color SelectionColor { get { return selectionColor; } set { selectionColor = value; } }

		protected bool IsInitialized(){
			return scatterPlotData != null;
		}

		internal void InvalidateData(bool deleteBuffer){
			values.Clear();
			area = Rectangle.Empty;
			if (zValues != null){
				zValues.Clear();
			}
		}

		internal void NeedRecalcValues(int width, int height){
			if (ValuesNeedRecalc()){
				RecalcValuesImpl(width, height);
				PrevWidth = width;
				PrevHeight = height;
			}
		}

		internal bool ValuesNeedRecalc(){
			if (scatterPlotData != null && scatterPlotData.IsEmpty){
				return false;
			}
			return scatterPlotData != null && values.Count == 0;
		}

		protected internal override void OnPaint(IGraphics g, int width, int height){
			if (width <= 0 || height <= 0){
				return;
			}
			if (!Visible){
				return;
			}
			Brush foreBrush = new SolidBrush(ForeColor);
			if (paintBackground && IsInitialized() && !IsDragging()){
				g.DrawString("Loading...", Font, foreBrush, width/2 - 20, height/2 - 10);
			}
			if (scatterPlotData == null){
				return;
			}
			if (width != prevWidth || height != prevHeight){
				InvalidateData(true);
			}
			prevHeight = height;
			prevWidth = width;
			if (ValuesNeedRecalc()){
				if (!IsDragging()){
					RecalcValues(width, height);
				}
				return;
			}
			if (backBuffer == null){
				lockBackBuffer = true;
				backBuffer = new UnsafeBitmap(width, height);
				backBuffer.LockBitmap();
				PaintOnBackBuffer(width, height);
				backBuffer.UnlockBitmap();
				lockBackBuffer = false;
			}
			vectorGraphics = !(g is CGraphics);
			if (vectorGraphics){
				PaintOnGraphicsVector(g, width, height);
			} else{
				PaintOnGraphicsBitmap(g, width, height);
			}
			if (indicatorX1 != -1 && indicatorX2 != -1 && indicatorY1 != -1 && indicatorY2 != -1){
				g.SmoothingMode = SmoothingMode.AntiAlias;
				Pen indicatorPen = new Pen(Color.Black){DashStyle = DashStyle.Dot};
				switch (MouseMode){
					case ScatterPlotMouseMode.Select:
					case ScatterPlotMouseMode.Zoom:{
						g.DrawRectangle(indicatorPen, MinIndicatorX, MinIndicatorY, DeltaIndicatorX, DeltaIndicatorY);
					}
						break;
				}
				g.SmoothingMode = SmoothingMode.Default;
			}
		}

		protected void PaintOnGraphicsVector(IGraphics g, int width, int height){
			if (scatterPlotData == null){
				return;
			}
			if (!area.IsEmpty){
				g.FillRectangle(Brushes.LightGray, 0, 0, width, height);
				g.FillRectangle(Brushes.White, area.X, area.Y, area.Width, area.Height);
			} else{
				g.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);
			}
			PaintGridVector(g, width, height);
			SymbolProperties[] gg = ArrayUtils.GetKeys(values);
			int[] counts = new int[gg.Length];
			for (int i = 0; i < counts.Length; i++){
				counts[i] = values[gg[i]].Item1;
			}
			int[] o = ArrayUtils.Order(counts);
			Array.Reverse(o);
			gg = ArrayUtils.SubArray(gg, o);
			foreach (SymbolProperties g1 in gg){
				bool[,] vals = values[g1].Item2;
				double[,] zvals = null;
				if (zValues != null){
					zvals = zValues[g1];
				}
				for (int i = 0; i < width; i++){
					for (int j = 0; j < height; j++){
						if (vals[i, j]){
							Color color;
							if (zvals != null && !double.IsNaN(zvals[i, j]) && colorScale != null){
								color = colorScale.GetColor(zvals[i, j]);
							} else{
								color = g1.Color;
							}
							Pen p = new Pen(color);
							Brush b = new SolidBrush(color);
							SymbolType symbolType = SymbolType.allSymbols[g1.Type];
							int symbolSize = g1.Size;
							if (symbolSize == 0){
								continue;
							}
							int[] pathX;
							int[] pathY;
							symbolType.GetPath(symbolSize, out pathX, out pathY);
							symbolType.Draw(symbolSize, i, j, g, p, b);
						}
					}
				}
			}
			// selected data-points
			Brush selectionBrush = new SolidBrush(selectionColor);
			Pen selectionPen = new Pen(selectionColor);
			foreach (int s in scatterPlotData.Selection){
				double[] w = scatterPlotData.GetDataAt(s);
				if (w.Length > 0){
					double x = w[0];
					double y = w[1];
					SymbolType symbolType = SymbolType.cross; //TODO
					const int symbolSize = 7; //TODO
					symbolType.Draw(symbolSize, ModelToViewX(x, width), ModelToViewY(y, height), g, selectionPen, selectionBrush);
				}
			}
			// labels
			ScatterPlotLabelMode labelMode = scatterPlot.GetLabelMode();
			bool cutLabels = scatterPlot.CutLabels;
			if (labelMode == ScatterPlotLabelMode.All && scatterPlotData.HasLabels){
				Font font = new Font("Arial", scatterPlot.FontSize, scatterPlot.FontStyle);
				for (;;){
					double[] x;
					double[] y;
					double[] z;
					string[] labels;
					int index;
					scatterPlotData.GetData(out x, out y, out z, out labels, out index);
					if (x == null){
						break;
					}
					SymbolProperties gx = GetPointProperties != null ? GetPointProperties(index) : defaultSymbol;
					for (int i = 0; i < x.Length; i++){
						if (labelMode == ScatterPlotLabelMode.All || scatterPlotData.IsSelected(i)){
							int ix = ModelToViewX(x[i], width);
							int iy = ModelToViewY(y[i], height);
							Color c;
							if (z != null && colorScale != null){
								c = colorScale.GetColor(z[i]);
							} else{
								c = gx.Color;
							}
							Brush b = new SolidBrush(c);
							if (ix >= 0 && iy >= 0 && ix < width && iy < height){
								if (labels[i] != null){
									string s = labels[i];
									if (cutLabels){
										if (s.Contains(";")){
											s = s.Substring(0, s.IndexOf(';'));
										}
									}
									g.DrawString(s, font, b, ix, iy);
								}
							}
						}
					}
				}
				scatterPlotData.Reset();
			}
			if (labelMode == ScatterPlotLabelMode.Selected && scatterPlotData.HasLabels){
				Font font = new Font("Arial", scatterPlot.FontSize, scatterPlot.FontStyle);
				foreach (int s in scatterPlotData.Selection){
					double[] w = scatterPlotData.GetDataAt(s);
					string label = scatterPlotData.GetLabelAt(s);
					double x = w[0];
					double y = w[1];
					int ix = ModelToViewX(x, width);
					int iy = ModelToViewY(y, height);
					if (ix >= 0 && iy >= 0 && ix < width && iy < height){
						if (label != null){
							if (cutLabels){
								if (label.Contains(";")){
									label = label.Substring(0, label.IndexOf(';'));
								}
							}
							g.DrawString(label, font, selectionBrush, ix, iy);
						}
					}
				}
			}
			DrawPolygons(g, width, height);
			if (drawFunctions != null) {
				drawFunctions(g, width, height, ModelToViewX, ModelToViewY, ViewToModelX, ViewToModelY);
			}
		}

		private void PaintGridVector(IGraphics g, int width, int height){
			if (XTics != null && VerticalGrid != GridType.None){
				Color c = VerticalGridColor;
				Pen p = new Pen(c, VerticalGridWidth){DashStyle = DashStyle.Dot};
				double[][] tics = XTics;
				foreach (double a in tics[0]){
					int b = ModelToViewX(a, width);
					g.DrawLine(p, b, 0, b, height - 1);
				}
				if (VerticalGrid == GridType.All){
					foreach (double a in tics[1]){
						int b = ModelToViewX(a, width);
						g.DrawLine(p, b, 0, b, height - 1);
					}
				}
			}
			if (YTics != null && HorizontalGrid != GridType.None){
				Color c = HorizontalGridColor;
				Pen p = new Pen(c, HorizontalGridWidth){DashStyle = DashStyle.Dot};
				double[][] tics = YTics;
				foreach (double a in tics[0]){
					int b = ModelToViewY(a, height);
					g.DrawLine(p, 0, b, width - 1, b);
				}
				if (HorizontalGrid == GridType.All){
					foreach (double a in tics[1]){
						int b = ModelToViewY(a, height);
						g.DrawLine(p, 0, b, width - 1, b);
					}
				}
			}
			if (HorizontalZeroVisible){
				Color c = HorizontalZeroColor;
				Pen p = new Pen(c, HorizontalZeroWidth);
				int b = ModelToViewY(0, height);
				g.DrawLine(p, 0, b, width - 1, b);
			}
			if (VerticalZeroVisible){
				Color c = VerticalZeroColor;
				Pen p = new Pen(c, VerticalZeroWidth);
				int b = ModelToViewX(0, width);
				g.DrawLine(p, b, 0, b, height - 1);
			}
		}

		protected void PaintGrid(int width, int height){
			if (XTics != null && VerticalGrid != GridType.None){
				Color c = VerticalGridColor;
				double[][] tics = XTics;
				foreach (double a in tics[0]){
					int b = ModelToViewX(a, width);
					backBuffer.DrawLine(c, b, 0, b, height - 1, true, VerticalGridWidth);
				}
				if (VerticalGrid == GridType.All){
					foreach (double a in tics[1]){
						int b = ModelToViewX(a, width);
						backBuffer.DrawLine(c, b, 0, b, height - 1, true, VerticalGridWidth);
					}
				}
			}
			if (YTics != null && HorizontalGrid != GridType.None){
				Color c = HorizontalGridColor;
				double[][] tics = YTics;
				foreach (double a in tics[0]){
					int b = ModelToViewY(a, height);
					backBuffer.DrawLine(c, 0, b, width - 1, b, true, HorizontalGridWidth);
				}
				if (HorizontalGrid == GridType.All){
					foreach (double a in tics[1]){
						int b = ModelToViewY(a, height);
						backBuffer.DrawLine(c, 0, b, width - 1, b, true, HorizontalGridWidth);
					}
				}
			}
			if (HorizontalZeroVisible){
				Color c = HorizontalZeroColor;
				int b = ModelToViewY(0, height);
				backBuffer.DrawLine(c, 0, b, width - 1, b, false, HorizontalZeroWidth);
			}
			if (VerticalZeroVisible){
				Color c = VerticalZeroColor;
				int b = ModelToViewX(0, width);
				backBuffer.DrawLine(c, b, 0, b, height - 1, false, VerticalZeroWidth);
			}
		}

		protected void PaintOnGraphicsBitmap(IGraphics g, int width, int height){
			if (backBuffer == null || scatterPlotData == null){
				return;
			}
			UnsafeBitmap copyBackBuffer = new UnsafeBitmap(backBuffer);
			copyBackBuffer.LockBitmap();
			// selected data-points
			foreach (int s in scatterPlotData.Selection){
				double[] w = scatterPlotData.GetDataAt(s);
				if (w.Length > 0){
					double x = w[0];
					double y = w[1];
					SymbolProperties gx = GetPointProperties != null ? GetPointProperties(s) : defaultSymbol;
					SymbolType symbolType = SymbolType.allSymbols[gx.Type];
					int symbolSize = gx.Size;
					if (symbolSize > 0){
						int[] pathX;
						int[] pathY;
						symbolType.GetPath(symbolSize, out pathX, out pathY);
						copyBackBuffer.DrawPath(selectionColor, ModelToViewX(x, width), ModelToViewY(y, height), pathX, pathY);
					}
				}
			}
			copyBackBuffer.UnlockBitmap();
			// labels
			ScatterPlotLabelMode labelMode = scatterPlot.GetLabelMode();
			bool cutLabels = scatterPlot.CutLabels;
			if (labelMode == ScatterPlotLabelMode.All && scatterPlotData.HasLabels){
				Font font = new Font("Arial", scatterPlot.FontSize, scatterPlot.FontStyle);
				for (;;){
					double[] x;
					double[] y;
					double[] z;
					string[] labels;
					int index;
					try{
						scatterPlotData.GetData(out x, out y, out z, out labels, out index);
					} catch (IndexOutOfRangeException){
						break;
					}
					if (x == null){
						break;
					}
					SymbolProperties gx = GetPointProperties != null ? GetPointProperties(index) : defaultSymbol;
					for (int i = 0; i < x.Length; i++){
						if (labelMode == ScatterPlotLabelMode.All || scatterPlotData.IsSelected(i)){
							int ix = ModelToViewX(x[i], width);
							int iy = ModelToViewY(y[i], height);
							Color c;
							if (z != null && colorScale != null){
								c = colorScale.GetColor(z[i]);
							} else{
								c = gx.Color;
							}
							Brush b = new SolidBrush(c);
							if (ix >= 0 && iy >= 0 && ix < width && iy < height){
								if (labels[i] != null){
									string s = labels[i];
									if (cutLabels){
										if (s.Contains(";")){
											s = s.Substring(0, s.IndexOf(';'));
										}
									}
									copyBackBuffer.DrawString(s, font, b, ix, iy);
								}
							}
						}
					}
				}
				scatterPlotData.Reset();
			}
			if (labelMode == ScatterPlotLabelMode.Selected && scatterPlotData.HasLabels){
				Brush br = new SolidBrush(selectionColor);
				Font font = new Font("Arial", scatterPlot.FontSize, scatterPlot.FontStyle);
				foreach (int s in scatterPlotData.Selection){
					double[] w = scatterPlotData.GetDataAt(s);
					string label = scatterPlotData.GetLabelAt(s);
					double x = w[0];
					double y = w[1];
					int ix = ModelToViewX(x, width);
					int iy = ModelToViewY(y, height);
					if (ix >= 0 && iy >= 0 && ix < width && iy < height){
						if (label != null){
							if (cutLabels){
								if (label.Contains(";")){
									label = label.Substring(0, label.IndexOf(';'));
								}
							}
							copyBackBuffer.DrawString(label, font, br, ix, iy);
						}
					}
				}
			}
			// draw the image
			g.DrawImageUnscaled(copyBackBuffer.Bitmap, 0, 0);
			copyBackBuffer.Dispose();
			g.SmoothingMode = SmoothingMode.AntiAlias;
			DrawPolygons(g, width, height);
			if (drawFunctions != null){
				drawFunctions(g, width, height, ModelToViewX, ModelToViewY, ViewToModelX, ViewToModelY);
			}
			g.SmoothingMode = SmoothingMode.Default;
		}

		private void DrawPolygons(IGraphics g, int width, int height){
			if (GetPolygonCount == null || GetPolygon == null){
				return;
			}
			for (int i = 0; i < GetPolygonCount(); i++){
				PolygonData pol = GetPolygon(i);
				PolygonProperties lp = GetPolygonProperties(i);
				double[] xc = pol.x;
				double[] yc = pol.y;
				Pen linePen = new Pen(lp.LineColor, lp.LineWidth) { DashStyle = lp.LineDashStyle };
				List<Point> ps = new List<Point>();
				for (int j = 0; j < xc.Length; j++) {
					if (double.IsNaN(xc[j]) || double.IsNaN(yc[j]) || double.IsInfinity(xc[j]) || double.IsInfinity(yc[j])) {
						continue;
					}
					int xi1 = ModelToViewX(xc[j], width);
					int yi1 = ModelToViewY(yc[j], height);
					ps.Add(new Point(xi1, yi1));
				}
				if (lp.LineWidth > 0) {
					g.DrawLines(linePen, ps.ToArray());
				}
				SymbolType symbolType = SymbolType.allSymbols[lp.SymbolType];
				int symbolSize = lp.SymbolSize;
				Pen p = new Pen(lp.SymbolColor);
				Brush b = new SolidBrush(lp.SymbolColor);
				if (symbolSize > 0){
					foreach (Point t in ps) {
						symbolType.Draw(symbolSize, t.X, t.Y, g, p, b);
					}
				}
				int w2 = lp.ErrorSize/2;
				if (lp.HorizErrors) {
					Pen errorPen = new Pen(lp.SymbolColor, lp.ErrorLineWidth) { DashStyle = DashStyle.Solid };
					for (int j = 0; j < xc.Length; j++) {
						double x = xc[j];
						double y = yc[j];
						if (double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y)) {
							continue;
						}
						int xi = ModelToViewX(x, width);
						int yi = ModelToViewY(y, height);
						double ed = pol.xErrDown[j];
						if (!double.IsNaN(ed) && !double.IsInfinity(ed) && ed >= 0) {
							int xi2 = ModelToViewX(x - ed, width);
							g.DrawLine(errorPen, xi, yi, xi2, yi);
							if (w2 > 0) {
								g.DrawLine(errorPen, xi2, yi - w2, xi2, yi + w2);
							}
						}
						double eu = pol.xErrUp[j];
						if (!double.IsNaN(eu) && !double.IsInfinity(eu) && eu >= 0) {
							int xi3 = ModelToViewX(x + eu, width);
							g.DrawLine(errorPen, xi, yi, xi3, yi);
							if (w2 > 0) {
								g.DrawLine(errorPen, xi3, yi - w2, xi3, yi + w2);
							}
						}
					}
				}
				if (lp.VertErrors) {
					Pen errorPen = new Pen(lp.SymbolColor, lp.ErrorLineWidth) { DashStyle = DashStyle.Solid };
					for (int j = 0; j < xc.Length; j++) {
						double x = xc[j];
						double y = yc[j];
						if (double.IsNaN(x) || double.IsNaN(y) || double.IsInfinity(x) || double.IsInfinity(y)) {
							continue;
						}
						int xi = ModelToViewX(x, width);
						int yi = ModelToViewY(y, height);
						double ed = pol.yErrDown[j];
						if (!double.IsNaN(ed) && !double.IsInfinity(ed) && ed >= 0) {
							int yi2 = ModelToViewY(y - ed, height);
							g.DrawLine(errorPen, xi, yi, xi, yi2);
							if (w2 > 0) {
								g.DrawLine(errorPen, xi - w2, yi2, xi + w2, yi2);
							}
						}
						double eu = pol.yErrUp[j];
						if (!double.IsNaN(eu) && !double.IsInfinity(eu) && eu >= 0) {
							int yi3 = ModelToViewY(y + eu, height);
							g.DrawLine(errorPen, xi, yi, xi, yi3);
							if (w2 > 0) {
								g.DrawLine(errorPen, xi - w2, yi3, xi + w2, yi3);
							}
						}
					}
				}
			}
		}

		internal void Select(int x1, int x2, int y1, int y2, bool add, int width, int height){
			if (scatterPlotData != null){
				double mx1 = ViewToModelX(x1, width);
				double mx2 = ViewToModelX(x2, width);
				double my1 = ViewToModelY(y1, height);
				double my2 = ViewToModelY(y2, height);
				scatterPlotData.Select(mx1, mx2, my1, my2, add, false);
			}
		}

		internal void SelectAt(int x, int y, bool add, int width, int height){
			if (scatterPlotData != null){
				scatterPlotData.Select(ViewToModelX(x - 2, width), ViewToModelX(x + 2, width), ViewToModelY(y + 2, height),
					ViewToModelY(y - 2, height), add, true);
			}
		}

		protected void RecalcValuesImpl(int width, int height){
			if (width < 0 || height < 0){
				return;
			}
			if (scatterPlotData != null){
				InvalidateData(false);
				double[] rx = scatterPlotData.XRange;
				double[] ry = scatterPlotData.YRange;
				if (rx != null || ry != null){
					int x = 0;
					int w = width;
					if (rx != null){
						x = ModelToViewX(rx[0], width);
						w = ModelToViewX(rx[1], width) - ModelToViewX(rx[0], width);
					}
					int y = 0;
					int h = height;
					if (ry != null){
						y = ModelToViewY(ry[0], height);
						h = ModelToViewY(ry[1], height) - ModelToViewY(ry[0], height);
					}
					area = new Rectangle(x, y, w, h);
				}
				for (;;){
					double[] x;
					double[] y;
					double[] z;
					string[] labels;
					int index;
					scatterPlotData.GetData(out x, out y, out z, out labels, out index);
					if (x == null){
						break;
					}
					if (z != null){
						if (zValues == null){
							zValues = new Dictionary<SymbolProperties, double[,]>();
						}
						double min;
						double max;
						ArrayUtils.MinMax(z, out min, out max);
						if (double.IsNaN(scatterPlotData.ColorMax) || max > scatterPlotData.ColorMax){
							scatterPlotData.ColorMax = max;
						}
						if (double.IsNaN(scatterPlotData.ColorMin) || min < scatterPlotData.ColorMin){
							scatterPlotData.ColorMin = min;
						}
					} else{
						zValues = null;
					}
					SymbolProperties prop = GetPointProperties != null ? GetPointProperties(index) : defaultSymbol;
					Tuple<int, bool[,]> vals;
					double[,] zVals = null;
					if (values.ContainsKey(prop)){
						try{
							vals = values[prop];
						} catch (Exception){
							break;
						}
					} else{
						vals = new Tuple<int, bool[,]>(0, new bool[width,height]);
						values.Add(prop, vals);
					}
					if (zValues != null){
						if (zValues.ContainsKey(prop)){
							try{
								zVals = zValues[prop];
							} catch (Exception){
								break;
							}
						} else{
							zVals = new double[width,height];
							zValues.Add(prop, zVals);
						}
					}
					for (int i = 0; i < x.Length; i++){
						int ix = ModelToViewX(x[i], width);
						int iy = ModelToViewY(y[i], height);
						if (ix >= 0 && iy >= 0 && ix < vals.Item2.GetLength(0) && iy < vals.Item2.GetLength(1)){
							vals.Item2[ix, iy] = true;
							vals = new Tuple<int, bool[,]>(vals.Item1 + 1, vals.Item2);
							if (zVals != null && z != null){
								zVals[ix, iy] = z[i];
							}
						}
					}
				}
				scatterPlotData.Reset();
				if (colorScale != null){
					colorScale.Max = scatterPlotData.ColorMax;
					colorScale.Min = scatterPlotData.ColorMin;
				}
			}
			InvalidateImage();
			Invalidate();
		}

		protected void PaintOnBackBuffer(int width, int height){
			if (vectorGraphics){
				return;
			}
			if (!area.IsEmpty){
				backBuffer.FillRectangle(Color.LightGray, 0, 0, width, height);
				backBuffer.FillRectangle(Color.White, area.X, area.Y, area.Width, area.Height);
			} else{
				backBuffer.FillRectangle(BackColor, 0, 0, width, height);
			}
			PaintGrid(width, height);
			SymbolProperties[] gg = ArrayUtils.GetKeys(values);
			int[] counts = new int[gg.Length];
			for (int i = 0; i < counts.Length; i++){
				counts[i] = values[gg[i]].Item1;
			}
			int[] o = ArrayUtils.Order(counts);
			Array.Reverse(o);
			gg = ArrayUtils.SubArray(gg, o);
			foreach (SymbolProperties g in gg){
				bool[,] vals = values[g].Item2;
				double[,] zvals = null;
				if (zValues != null){
					zvals = zValues[g];
				}
				for (int i = 0; i < width; i++){
					for (int j = 0; j < height; j++){
						if (vals[i, j] && backBuffer != null){
							Color color;
							if (zvals != null && !double.IsNaN(zvals[i, j]) && colorScale != null){
								color = colorScale.GetColor(zvals[i, j]);
							} else{
								color = g.Color;
							}
							SymbolType symbolType = SymbolType.allSymbols[g.Type];
							int symbolSize = g.Size;
							if (symbolSize > 0){
								int[] pathX;
								int[] pathY;
								symbolType.GetPath(symbolSize, out pathX, out pathY);
								backBuffer.DrawPath(color, i, j, pathX, pathY);
							}
						}
					}
				}
			}
		}

		internal void SetStatusLabel(ToolStripStatusLabel statusLabel1){
			statusLabel = statusLabel1;
		}

		internal double ZoomXMin{
			get { return zoomXMin; }
			set{
				zoomXMin = value;
				TotalXMin = Math.Min(zoomXMin, TotalXMin);
				InvalidateData(true);
			}
		}
		internal double ZoomXMax{
			get { return zoomXMax; }
			set{
				zoomXMax = value;
				TotalXMax = Math.Max(zoomXMax, TotalXMax);
				InvalidateData(true);
			}
		}
		internal double ZoomYMin{
			get { return zoomYMin; }
			set{
				zoomYMin = value;
				TotalYMin = Math.Min(zoomYMin, TotalYMin);
				InvalidateData(true);
			}
		}
		internal double ZoomYMax{
			get { return zoomYMax; }
			set{
				zoomYMax = value;
				TotalYMax = Math.Max(zoomYMax, TotalYMax);
				InvalidateData(true);
			}
		}

		protected int ModelToViewX(double m, int width){
			return (int) Math.Round((m - ZoomXMin)/(ZoomXMax - ZoomXMin)*width);
		}

		protected int ModelToViewY(double t, int height){
			int y = (int) Math.Round((t - ZoomYMin)/(ZoomYMax - ZoomYMin)*height);
			return height - y - 1;
		}

		internal double ViewToModelX(int x, int width){
			return ZoomXMin + x*(ZoomXMax - ZoomXMin)/width;
		}

		private double ViewToModelY(int y, int height){
			y = height - 1 - y;
			return (ZoomYMin + y*(ZoomYMax - ZoomYMin)/height);
		}

		private bool IsDragging(){
			return indicatorX1 != -1 || indicatorX2 != -1;
		}

		private bool HasMoved(){
			return indicatorX1 != indicatorX2 || indicatorY1 != indicatorY2;
		}

		private void RecalcValues(int width, int height){
			if (recalcValuesThread != null){
				recalcValuesThread.Abort();
			}
			recalcValuesThread = new Thread(() => RecalcValuesImpl(width, height));
			recalcValuesThread.Start();
		}

		internal virtual void InvalidateImage(){
			if (!lockBackBuffer){
				backBuffer = null;
			}
		}

		private int MinIndicatorX { get { return Math.Min(indicatorX1, indicatorX2); } }
		private int MaxIndicatorX { get { return Math.Max(indicatorX1, indicatorX2); } }
		private int DeltaIndicatorX { get { return Math.Abs((indicatorX1 - indicatorX2)); } }
		private int MinIndicatorY { get { return Math.Min(indicatorY1, indicatorY2); } }
		private int MaxIndicatorY { get { return Math.Max(indicatorY1, indicatorY2); } }
		private int DeltaIndicatorY { get { return Math.Abs((indicatorY1 - indicatorY2)); } }

		private void FireZoom(bool relativeToVisibleAxis, int width, int height){
			if (OnZoomChange != null){
				OnZoomChange(this, MinIndicatorX, MaxIndicatorX, MinIndicatorY, MaxIndicatorY, relativeToVisibleAxis, width, height);
			}
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			indicatorX1 = e.X;
			indicatorX2 = indicatorX1;
			indicatorY1 = e.Y;
			indicatorY2 = indicatorY1;
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			switch (MouseMode){
				case ScatterPlotMouseMode.Zoom:
					FireZoom(true, e.Width, e.Height);
					break;
				case ScatterPlotMouseMode.Select:
					if (HasMoved()){
						bool add = e.ControlPressed;
						Select(MinIndicatorX, MaxIndicatorX, MaxIndicatorY, MinIndicatorY, add, e.Width, e.Height);
					}
					break;
			}
			indicatorX1 = -1;
			indicatorX2 = -1;
			indicatorY1 = -1;
			indicatorY2 = -1;
			Invalidate();
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			switch (MouseMode){
				case ScatterPlotMouseMode.Zoom:
				case ScatterPlotMouseMode.Select:
					if (!HasMoved()){
						bool add = e.ControlPressed;
						SelectAt(e.X, e.Y, add, e.Width, e.Height);
						Invalidate();
					}
					break;
			}
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			indicatorX1 = -1;
			indicatorX2 = -1;
			indicatorY1 = -1;
			indicatorY2 = -1;
			FireZoom(false, e.Width, e.Height);
			Invalidate();
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			indicatorX2 = e.X;
			indicatorX2 = Math.Max(0, indicatorX2);
			indicatorX2 = Math.Min(e.Width - 1, indicatorX2);
			indicatorY2 = e.Y;
			indicatorY2 = Math.Max(0, indicatorY2);
			indicatorY2 = Math.Min(e.Height - 1, indicatorY2);
			Invalidate();
		}

		protected internal override void OnMouseLeave(EventArgs e){
			if (statusLabel != null){
				statusLabel.Text = null;
			}
		}

		protected internal override void Dispose(bool disposing){
			if (recalcValuesThread != null){
				recalcValuesThread.Abort();
				recalcValuesThread = null;
			}
			base.Dispose(disposing);
		}
	}
}