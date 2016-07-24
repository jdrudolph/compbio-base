using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Graphic;
using BaseLibS.Graph;
using BaseLibS.Num;

namespace BaseLib.Forms.Base{
	public class BasicTableLayoutView : BasicView{
		private static readonly Color borderColor = Color.FromArgb(240, 240, 240);
		private static readonly Brush borderBrush = new SolidBrush(borderColor);
		private readonly object lockThis = new object();
		public int BorderSize { get; set; }
		public BasicColumnStyles ColumnStyles { get; }
		public BasicRowStyles RowStyles { get; }
		private readonly Dictionary<Tuple<int, int>, BasicView> components = new Dictionary<Tuple<int, int>, BasicView>();
		private int[] widths;
		private int[] xpos;
		private int[] heights;
		private int[] ypos;

		public BasicTableLayoutView(){
			ColumnStyles = new BasicColumnStyles(this);
			RowStyles = new BasicRowStyles(this);
			BorderSize = 0;
		}

		public void Add(BasicView bv, int column, int row){
			bv.Activate(this);
			components.Add(new Tuple<int, int>(row, column), bv);
		}

		private void InitSizes(int width, int height){
			lock (lockThis){
				widths = InitSizes(width, ColumnStyles.ToArray(), BorderSize);
				xpos = InitPositions(widths, BorderSize);
				heights = InitSizes(height, RowStyles.ToArray(), BorderSize);
				ypos = InitPositions(heights, BorderSize);
			}
		}

		private int GetSeparatorInd(int[] pos, int x){
			if (pos == null){
				return -1;
			}
			int ci = ArrayUtils.ClosestIndex(pos, x);
			if (ci < 1){
				return -1;
			}
			if (Fits(pos[ci], x)){
				return ci - 1;
			}
			return -1;
		}

		private bool Fits(int pos, int x){
			if (BorderSize < 3){
				return Math.Abs(pos - x) < 2;
			}
			return x >= pos - BorderSize && x <= pos;
		}

		private static int[] InitPositions(IList<int> sizes, int borderSize){
			int[] pos = new int[sizes.Count];
			if (pos.Length < 2){
				return pos;
			}
			for (int i = 1; i < sizes.Count; i++){
				pos[i] = pos[i - 1] + sizes[i - 1] + borderSize;
			}
			return pos;
		}

		private static int[] InitSizes(int width, IList<BasicTableLayoutStyle> styles, int borderSize){
			int[] widths1 = new int[styles.Count];
			if (styles.Count < 1){
				return widths1;
			}
			int remaining = width - (styles.Count - 1)*borderSize;
			bool[] taken = new bool[widths1.Length];
			for (int i = 0; i < styles.Count; i++){
				BasicTableLayoutStyle s = styles[i];
				if (s.SizeType == BasicSizeType.Absolute){
					widths1[i] = (int) Math.Round(s.Size);
					remaining -= widths1[i];
					taken[i] = true;
				}
			}
			if (remaining <= 0){
				return widths1;
			}
			double totalAbsoluteResizable = 0;
			foreach (BasicTableLayoutStyle s in styles){
				if (s.SizeType == BasicSizeType.AbsoluteResizeable){
					totalAbsoluteResizable += s.Size;
				}
			}
			if (totalAbsoluteResizable >= remaining){
				double factor = remaining/totalAbsoluteResizable;
				for (int i = 0; i < styles.Count; i++){
					BasicTableLayoutStyle s = styles[i];
					if (s.SizeType == BasicSizeType.AbsoluteResizeable){
						widths1[i] = (int) Math.Round(s.Size*factor);
						remaining -= widths1[i];
						taken[i] = true;
					}
				}
				return widths1;
			}
			for (int i = 0; i < styles.Count; i++){
				BasicTableLayoutStyle s = styles[i];
				if (s.SizeType == BasicSizeType.AbsoluteResizeable){
					widths1[i] = (int) Math.Round(s.Size);
					remaining -= widths1[i];
					taken[i] = true;
				}
			}
			if (remaining <= 0){
				return widths1;
			}
			float totalPercentage = 0;
			foreach (BasicTableLayoutStyle s in styles){
				if (s.SizeType == BasicSizeType.Percent){
					totalPercentage += s.Size;
				}
			}
			double factor1 = remaining/totalPercentage;
			for (int i = 0; i < styles.Count; i++){
				BasicTableLayoutStyle s = styles[i];
				if (s.SizeType == BasicSizeType.Percent){
					widths1[i] = (int) Math.Round(s.Size*factor1);
					remaining -= widths1[i];
					taken[i] = true;
				}
			}
			return widths1;
		}

		private static int GetCurrentComponentInd(int[] pos, int p1){
			if (pos == null){
				return -1;
			}
			return ArrayUtils.FloorIndex(pos, p1);
		}

		public int RowCount => RowStyles.Count;
		public int ColumnCount => ColumnStyles.Count;

		protected internal override void OnPaint(IGraphics g, int width, int height){
			if (widths == null){
				InitSizes(width, height);
			}
			PaintSplitters(g, width, height);
			for (int row = 0; row < RowCount; row++){
				for (int col = 0; col < ColumnCount; col++){
					Tuple<int, int> key = new Tuple<int, int>(row, col);
					if (components.ContainsKey(key)){
						BasicView v = components[key];
						g.SetClip(new Rectangle(xpos[col], ypos[row], widths[col], heights[row]));
						g.TranslateTransform(xpos[col], ypos[row]);
						v.OnPaint(g, widths[col], heights[row]);
						g.ResetTransform();
						g.ResetClip();
					}
				}
			}
		}

		private void PaintSplitters(IGraphics g, int width, int height){
			if (BorderSize <= 0){
				return;
			}
			for (int i = 1; i < RowCount; i++){
				g.FillRectangle(borderBrush, 0, heights[i] + (i - 1)*BorderSize, width, BorderSize);
			}
			for (int i = 1; i < ColumnCount; i++){
				g.FillRectangle(borderBrush, widths[i] + (i - 1)*BorderSize, 0, BorderSize, height);
			}
		}

		protected internal override void OnPaintBackground(IGraphics g, int width, int height){
			if (widths == null){
				InitSizes(width, height);
			}
			for (int row = 0; row < RowCount; row++){
				for (int col = 0; col < ColumnCount; col++){
					Tuple<int, int> key = new Tuple<int, int>(row, col);
					if (components.ContainsKey(key)){
						BasicView v = components[key];
						g.TranslateTransform(xpos[col], ypos[row]);
						v.OnPaintBackground(g, widths[col], heights[row]);
						g.ResetTransform();
					}
				}
			}
		}

		protected internal override void OnMouseCaptureChanged(EventArgs e){
			Tuple<int, int> key = new Tuple<int, int>(currentComponentY, currentComponentX);
			if (components.ContainsKey(key)){
				BasicView v = components[key];
				v.OnMouseCaptureChanged(e);
			}
		}

		protected internal override void OnMouseEnter(EventArgs e){
			Tuple<int, int> key = new Tuple<int, int>(currentComponentY, currentComponentX);
			if (components.ContainsKey(key)){
				BasicView v = components[key];
				v.OnMouseEnter(e);
			}
		}

		protected internal override void OnMouseHover(EventArgs e){
			Tuple<int, int> key = new Tuple<int, int>(currentComponentY, currentComponentX);
			if (components.ContainsKey(key)){
				BasicView v = components[key];
				v.OnMouseHover(e);
			}
		}

		protected internal override void OnMouseLeave(EventArgs e){
			Tuple<int, int> key = new Tuple<int, int>(currentComponentY, currentComponentX);
			if (components.ContainsKey(key)){
				BasicView v = components[key];
				v.OnMouseLeave(e);
			}
		}

		protected internal override void OnResize(EventArgs e, int width, int height){
			widths = null;
			heights = null;
			xpos = null;
			ypos = null;
			InitSizes(width, height);
			for (int row = 0; row < RowCount; row++){
				for (int col = 0; col < ColumnCount; col++){
					Tuple<int, int> key = new Tuple<int, int>(row, col);
					if (components.ContainsKey(key)){
						BasicView v = components[key];
						v.OnResize(e, widths[col], heights[row]);
					}
				}
			}
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			int indX;
			int indY;
			BasicView v = GetComponentAt(e.X, e.Y, out indX, out indY);
			v?.OnMouseClick(new BasicMouseEventArgs(e, xpos[indX], ypos[indY], widths[indX], heights[indY]));
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			int indX;
			int indY;
			BasicView v = GetComponentAt(e.X, e.Y, out indX, out indY);
			v?.OnMouseDoubleClick(new BasicMouseEventArgs(e, xpos[indX], ypos[indY], widths[indX], heights[indY]));
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			if (dragging){}
			BasicView v = GetComponentAt(mouseDownX, mouseDownY);
			v?.OnMouseDragged(new BasicMouseEventArgs(e, xpos[mouseDownX], ypos[mouseDownY], widths[mouseDownX],
				heights[mouseDownY]));
			//TODO: splitter
		}

		private bool dragging;
		private bool dragX;
		private int dragIndex;

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			int indX1 = GetSeparatorInd(xpos, e.X);
			int indY1 = GetSeparatorInd(ypos, e.Y);
			if (indX1 >= 0){
				indY1 = -1;
			}
			if (indX1 >= 0 || indY1 >= 0){
				dragging = true;
				dragX = indX1 >= 0;
				dragIndex = indX1 >= 0 ? indX1 : indY1;
				return;
			}
			int indX;
			int indY;
			BasicView v = GetComponentAt(e.X, e.Y, out indX, out indY);
			if (v != null){
				v.OnMouseIsDown(new BasicMouseEventArgs(e, xpos[indX], ypos[indY], widths[indX], heights[indY]));
				mouseDownX = indX;
				mouseDownY = indY;
			}
		}

		private int mouseDownX;
		private int mouseDownY;

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			if (dragging){
				dragging = false;
				return;
			}
			BasicView v = GetComponentAt(mouseDownX, mouseDownY);
			v?.OnMouseIsUp(new BasicMouseEventArgs(e, xpos[mouseDownX], ypos[mouseDownY], widths[mouseDownX], heights[mouseDownY]));
		}

		private BasicView GetComponentAt(int x, int y, out int indX1, out int indY1){
			if (xpos == null || ypos == null) {
				indX1 = -1;
				indY1 = -1;
				return null;
			}
			int indX = GetSeparatorInd(xpos, x);
			int indY = GetSeparatorInd(ypos, y);
			if (indX >= 0 || indY >= 0){
				indX1 = -1;
				indY1 = -1;
				return null;
			}
			indX1 = GetCurrentComponentInd(xpos, x);
			indY1 = GetCurrentComponentInd(ypos, y);
			Tuple<int, int> key = new Tuple<int, int>(indY1, indX1);
			return components.ContainsKey(key) ? components[key] : null;
		}

		private BasicView GetComponentAt(int indX1, int indY1){
			Tuple<int, int> key = new Tuple<int, int>(indY1, indX1);
			return components.ContainsKey(key) ? components[key] : null;
		}

		private int currentComponentX;
		private int currentComponentY;

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			if (xpos == null || ypos == null){
				return;
			}
			int indX = GetSeparatorInd(xpos, e.X);
			int indY = GetSeparatorInd(ypos, e.Y);
			if (indX >= 0){
				indY = -1;
			}
			if (indX >= 0 || indY >= 0){
				Cursor = indX >= 0 ? Cursors.VSplit : Cursors.HSplit;
				currentComponentX = -1;
				currentComponentY = -1;
			} else{
				ResetCursor();
				currentComponentX = GetCurrentComponentInd(xpos, e.X);
				currentComponentY = GetCurrentComponentInd(ypos, e.Y);
				Tuple<int, int> key = new Tuple<int, int>(currentComponentY, currentComponentX);
				if (components.ContainsKey(key)){
					BasicView v = components[key];
					v.OnMouseMoved(new BasicMouseEventArgs(e, xpos[currentComponentX], ypos[currentComponentY],
						widths[currentComponentX], heights[currentComponentY]));
				}
			}
		}

		protected internal override void OnMouseWheel(BasicMouseEventArgs e){
			int indX;
			int indY;
			BasicView v = GetComponentAt(e.X, e.Y, out indX, out indY);
			v?.OnMouseWheel(new BasicMouseEventArgs(e, xpos[indX], ypos[indY], widths[indX], heights[indY]));
		}

		public void InvalidateSizes(){
			widths = null;
			heights = null;
			xpos = null;
			ypos = null;
		}

		public int[] GetRowHeights(int width, int height){
			if (widths == null){
				InitSizes(width, height);
			}
			return widths;
		}

		public int[] GetColumnWidths(int width, int height){
			if (heights == null){
				InitSizes(width, height);
			}
			return heights;
		}
	}
}