using System;
using BaseLib.Graphic;
using BaseLibS.Graph;
using BaseLibS.Graph.Base;

namespace BaseLib.Forms.Scroll{
	internal sealed class SimpleScrollableControlMainView : BasicView{
		private const int maxOverviewSize = 100;
		private ZoomButtonState state = ZoomButtonState.Neutral;
		private readonly SimpleScrollableControl main;
		private int indicatorX1 = -1;
		private int indicatorX2 = -1;
		private int indicatorY1 = -1;
		private int indicatorY2 = -1;
		private float zoomFactor = 1;
		private const float zoomStep = 1.2f;

		internal SimpleScrollableControlMainView(SimpleScrollableControl main){
			this.main = main;
		}

		public static void PaintZoomButtons(IGraphics g, int width, int height, int bsize, ZoomButtonState state){
			g.SmoothingMode = SmoothingMode2.AntiAlias;
			Brush2 b = GraphUtil.zoomBrush;
			switch (state){
				case ZoomButtonState.HighlightPlus:
					b = GraphUtil.zoomBrushHighlight;
					break;
				case ZoomButtonState.PressPlus:
					b = GraphUtil.zoomBrushPress;
					break;
			}
			PaintPlusZoomButton(g, b, width - bsize - 4, height - 2*bsize - 8, bsize);
			b = GraphUtil.zoomBrush;
			switch (state){
				case ZoomButtonState.HighlightMinus:
					b = GraphUtil.zoomBrushHighlight;
					break;
				case ZoomButtonState.PressMinus:
					b = GraphUtil.zoomBrushPress;
					break;
			}
			PaintMinusZoomButton(g, b, width - bsize - 4, height - bsize - 4, bsize);
			g.SmoothingMode = SmoothingMode2.Default;
		}

		public static void PaintPlusZoomButton(IGraphics g, Brush2 b, int x, int y, int bsize){
			Pen2 w = new Pen2(Color2.White, 2);
			PaintRoundButton(g, b, w, x, y, bsize);
			g.DrawLine(w, x + 4, y + bsize/2, x + bsize - 4, y + bsize/2);
			g.DrawLine(w, x + bsize - bsize/2, y + 4, x + bsize/2, y + bsize - 4);
		}

		public static void PaintMinusZoomButton(IGraphics g, Brush2 b, int x, int y, int bsize){
			Pen2 w = new Pen2(Color2.White, 2);
			PaintRoundButton(g, b, w, x, y, bsize);
			g.DrawLine(w, x + 4, y + bsize/2, x + bsize - 4, y + bsize/2);
		}

		public static void PaintRoundButton(IGraphics g, Brush2 b, Pen2 w, int x, int y, int size){
			g.FillEllipse(b, x, y, size, size);
			g.DrawEllipse(w, x + 2, y + 2, size - 4, size - 4);
		}

		public override void OnPaint(IGraphics g, int width, int height){
			main.OnPaintMainView?.Invoke(g, main.VisibleX, main.VisibleY, width, height);
			PaintZoomButtons(g, width, height, GraphUtil.zoomButtonSize, state);
			PaintOverview(g, width, height, main.TotalWidth(), main.TotalHeight(), main.VisibleX, main.VisibleY,
				main.VisibleWidth, main.VisibleHeight, (overviewWidth, overviewHeight) =>{
					BitmapGraphics bg = new BitmapGraphics(main.TotalWidth(), main.TotalHeight());
					main.OnPaintMainView?.Invoke(bg, 0, 0, main.TotalWidth(), main.TotalHeight());
					return GraphUtils.ToBitmap2(GraphUtils.ResizeImage(bg.Bitmap, overviewWidth, overviewHeight));
				});
		}

		public static Size2 CalcOverviewSize(int width, int height, int totalWidth, int totalHeight){
			int maxSize = Math.Min(Math.Min(maxOverviewSize, height), width - 20);
			if (totalWidth > totalHeight){
				return new Size2(maxSize, (int) Math.Round(totalHeight/(float) totalWidth*maxSize));
			}
			return new Size2((int) Math.Round(totalWidth/(float) totalHeight*maxSize), maxSize);
		}

		public static Rectangle2 CalcWin(Size2 overview, int totalWidth, int totalHeight, int visibleX, int visibleY,
			int visibleWidth, int visibleHeight){
			int winX = (int) Math.Round(visibleX*overview.Width/(float) totalWidth);
			int winWidth = (int) Math.Round(visibleWidth*overview.Width/(float) totalWidth);
			if (winX + winWidth > overview.Width){
				winWidth = overview.Width - winX;
			}
			int winY = (int) Math.Round(visibleY*overview.Height/(float) totalHeight);
			int winHeight = (int) Math.Round(visibleHeight*overview.Height/(float) totalHeight);
			if (winY + winHeight > overview.Height){
				winHeight = overview.Height - winY;
			}
			return new Rectangle2(winX, winY, winWidth, winHeight);
		}

		public static void PaintOverview(IGraphics g, int width, int height, int totalWidth, int totalHeight, int visibleX,
			int visibleY, int visibleWidth, int visibleHeight, Func<int, int, Bitmap2> getOverviewBitmap){
			Size2 overview = CalcOverviewSize(width, height, totalWidth, totalHeight);
			Rectangle2 win = CalcWin(overview, totalWidth, totalHeight, visibleX, visibleY, visibleWidth, visibleHeight);
			g.FillRectangle(Brushes2.White, 0, height - overview.Height, overview.Width, overview.Height);
			g.DrawImageUnscaled(getOverviewBitmap(overview.Width, overview.Height), 0, height - overview.Height);
			Brush2 b = new Brush2(Color2.FromArgb(30, 0, 0, 255));
			if (win.X > 0){
				g.FillRectangle(b, 0, height - overview.Height, win.X, overview.Height);
			}
			if (overview.Width - win.X - win.Width > 0){
				g.FillRectangle(b, win.X + win.Width, height - overview.Height, overview.Width - win.X - win.Width, overview.Height);
			}
			if (win.Y > 0){
				g.FillRectangle(b, win.X, height - overview.Height, win.Width, win.Y);
			}
			if (overview.Height - win.Y - win.Height > 0){
				g.FillRectangle(b, win.X, height - overview.Height + win.Y + win.Height, win.Width,
					overview.Height - win.Y - win.Height);
			}
			g.DrawRectangle(Pens2.Black, 0, height - overview.Height - 1, overview.Width, overview.Height);
			g.DrawRectangle(Pens2.Blue, win.X, height - overview.Height - 1 + win.Y, win.Width, win.Height);
		}

		public override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveMainView?.Invoke(e);
			ZoomButtonState newState = ZoomButtonState.Neutral;
			if (HitsMinusButton(e.X, e.Y, e.Width, e.Height)){
				newState = ZoomButtonState.HighlightMinus;
			} else if (HitsPlusButton(e.X, e.Y, e.Width, e.Height)){
				newState = ZoomButtonState.HighlightPlus;
			}
			if (newState != state){
				state = newState;
				Invalidate();
			}
		}

		public override void OnMouseHover(EventArgs e){
			main.OnMouseHoverMainView?.Invoke(e);
		}

		public override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveMainView?.Invoke(e);
			state = ZoomButtonState.Neutral;
			Invalidate();
		}

		public override void OnMouseClick(BasicMouseEventArgs e){
			Size2 overview = CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight());
			if (e.X < overview.Width && e.Y > e.Height - overview.Height){
				return;
			}
			if (HitsMinusButton(e.X, e.Y, e.Width, e.Height)){
				return;
			}
			if (HitsPlusButton(e.X, e.Y, e.Width, e.Height)){
				return;
			}
			main.OnMouseClickMainView?.Invoke(e);
		}

		public override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickMainView?.Invoke(e);
		}

		public override void OnMouseIsDown(BasicMouseEventArgs e){
			Size2 overview = CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight());
			if (e.X < overview.Width && e.Y > e.Height - overview.Height){
				OnMouseIsDownOverview(e.X, e.Y - e.Height + overview.Height, e.Width, e.Height);
				return;
			}
			ZoomButtonState newState = ZoomButtonState.Neutral;
			bool hitsButton = false;
			if (HitsMinusButton(e.X, e.Y, e.Width, e.Height)){
				newState = ZoomButtonState.PressMinus;
				hitsButton = true;
				ZoomOut();
			}
			if (HitsPlusButton(e.X, e.Y, e.Width, e.Height)){
				newState = ZoomButtonState.PressPlus;
				hitsButton = true;
				ZoomIn();
			}
			if (newState != state){
				state = newState;
				Invalidate();
			}
			if (hitsButton){
				return;
			}
			main.OnMouseIsDownMainView?.Invoke(e);
		}

		private void OnMouseIsDownOverview(int x, int y, int width, int height){
			Size2 overview = CalcOverviewSize(width, height, main.TotalWidth(), main.TotalHeight());
			Rectangle2 win = CalcWin(overview, main.TotalWidth(), main.TotalHeight(), main.VisibleX, main.VisibleY,
				main.VisibleWidth, main.VisibleHeight);
			if (win.Contains(x, y)){
				indicatorX1 = x;
				indicatorX2 = indicatorX1;
				indicatorY1 = y;
				indicatorY2 = indicatorY1;
				visibleXStart = main.VisibleX;
				visibleYStart = main.VisibleY;
			}else{
				int x1 = x - win.Width / 2;
				int y1 = y - win.Height / 2;
				int newX = (int)Math.Round(x1 * main.TotalWidth() / (float)overview.Width);
				int newY = (int)Math.Round(y1 * main.TotalHeight() / (float)overview.Height);
				newX = Math.Min(Math.Max(newX, 0), main.TotalWidth() - main.VisibleWidth);
				main.VisibleX = newX;
				newY = Math.Min(Math.Max(newY, 0), main.TotalHeight() - main.VisibleHeight);
				main.VisibleY = newY;
				invalidate();
			}
		}

		int visibleXStart = -1;
		int visibleYStart = -1;

		private void ZoomOut(){
			zoomFactor /= zoomStep;
			invalidate();
		}

		private void ZoomIn(){
			zoomFactor *= zoomStep;
			invalidate();
		}

		public override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpMainView?.Invoke(e);
			indicatorX1 = -1;
			indicatorX2 = -1;
			indicatorY1 = -1;
			indicatorY2 = -1;
			visibleXStart = -1;
			visibleYStart = -1;
			state = ZoomButtonState.Neutral;
			Invalidate();
		}

		public override void OnMouseDragged(BasicMouseEventArgs e){
			if (visibleXStart != -1){
				Size2 overview = CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight());
				indicatorX2 = e.X;
				indicatorY2 = e.Y - e.Height +overview.Height;
				int newX = visibleXStart + (int) Math.Round((indicatorX2 - indicatorX1)*main.TotalWidth()/(float) overview.Width);
				newX = Math.Min(Math.Max(newX, 0), main.TotalWidth()-main.VisibleWidth);
				main.VisibleX = newX;
				int newY = visibleYStart + (int) Math.Round((indicatorY2 - indicatorY1)*main.TotalHeight()/(float) overview.Height);
				newY = Math.Min(Math.Max(newY, 0), main.TotalHeight() - main.VisibleHeight);
				main.VisibleY = newY;
				invalidate();
				return;
			}
			main.OnMouseDraggedMainView?.Invoke(e);
		}

		public static bool HitsPlusButton(int x, int y, int width, int height){
			if (x < width - GraphUtil.zoomButtonSize - 4){
				return false;
			}
			if (x > width - 4){
				return false;
			}
			if (y < height - 2*GraphUtil.zoomButtonSize - 8){
				return false;
			}
			return y <= height - GraphUtil.zoomButtonSize - 8;
		}

		public static bool HitsMinusButton(int x, int y, int width, int height){
			if (x < width - GraphUtil.zoomButtonSize - 4){
				return false;
			}
			if (x > width - 4){
				return false;
			}
			if (y < height - GraphUtil.zoomButtonSize - 4){
				return false;
			}
			return y <= height - 4;
		}
	}
}