using System;
using BaseLib.Graphic;
using BaseLibS.Graph;
using BaseLibS.Graph.Base;

namespace BaseLib.Forms.Scroll{
	internal sealed class SimpleScrollableControlMainView : BasicView{
		private const int maxOverviewSize = 100;
		private ZoomButtonState state = ZoomButtonState.Neutral;
		private readonly SimpleScrollableControl main;

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

		private static void CalcOverviewSize(int width, int height, int totalWidth, int totalHeight, out int overviewWidth,
			out int overviewHeight){
			int maxSize = Math.Min(Math.Min(maxOverviewSize, height), width - 20);
			if (totalWidth > totalHeight){
				overviewWidth = maxSize;
				overviewHeight = (int) Math.Round(totalHeight/(float) totalWidth*maxOverviewSize);
			} else{
				overviewHeight = maxSize;
				overviewWidth = (int) Math.Round(totalWidth/(float) totalHeight*maxOverviewSize);
			}
		}

		private static void PaintOverview(IGraphics g, int width, int height, int totalWidth, int totalHeight, int visibleX,
			int visibleY, int visibleWidth, int visibleHeight, Func<int, int, Bitmap2> getOverviewBitmap){
			int overviewWidth;
			int overviewHeight;
			CalcOverviewSize(width, height, totalWidth, totalHeight, out overviewWidth, out overviewHeight);
			int maxSize = Math.Min(Math.Min(maxOverviewSize, height), width - 20);
			if (totalWidth > totalHeight){
				overviewWidth = maxSize;
				overviewHeight = (int) Math.Round(totalHeight/(float) totalWidth*maxOverviewSize);
			} else{
				overviewHeight = maxSize;
				overviewWidth = (int) Math.Round(totalWidth/(float) totalHeight*maxOverviewSize);
			}
			int winX = (int) Math.Round(visibleX*overviewWidth/(float) totalWidth);
			int winWidth = (int) Math.Round(visibleWidth*overviewWidth/(float) totalWidth);
			if (winX + winWidth > overviewWidth){
				winWidth = overviewWidth - winX;
			}
			int winY = (int) Math.Round(visibleY*overviewHeight/(float) totalHeight);
			int winHeight = (int) Math.Round(visibleHeight*overviewHeight/(float) totalHeight);
			if (winY + winHeight > overviewHeight){
				winHeight = overviewHeight - winY;
			}
			g.FillRectangle(Brushes2.White, 0, height - overviewHeight, overviewWidth, overviewHeight);
			g.DrawImageUnscaled(getOverviewBitmap(overviewWidth, overviewHeight), 0, height - overviewHeight);
			Brush2 b = new Brush2(Color2.FromArgb(30, 0, 0, 255));
			if (winX > 0){
				g.FillRectangle(b, 0, height - overviewHeight, winX, overviewHeight);
			}
			if (overviewWidth - winX - winWidth > 0){
				g.FillRectangle(b, winX + winWidth, height - overviewHeight, overviewWidth - winX - winWidth, overviewHeight);
			}
			if (winY > 0){
				g.FillRectangle(b, winX, height - overviewHeight, winWidth, winY);
			}
			if (overviewHeight - winY - winHeight > 0){
				g.FillRectangle(b, winX, height - overviewHeight + winY + winHeight, winWidth, overviewHeight - winY - winHeight);
			}
			g.DrawRectangle(Pens2.Black, 0, height - overviewHeight - 1, overviewWidth, overviewHeight);
			g.DrawRectangle(Pens2.Blue, winX, height - overviewHeight - 1 + winY, winWidth, winHeight);
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
			int overviewWidth;
			int overviewHeight;
			CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight(), out overviewWidth, out overviewHeight);
			if (e.X < overviewWidth && e.Y > e.Height - overviewHeight){
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
			int overviewWidth;
			int overviewHeight;
			CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight(), out overviewWidth, out overviewHeight);
			if (e.X < overviewWidth && e.Y > e.Height - overviewHeight){
				OnMouseIsDownOverview(e);
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

		private void OnMouseIsDownOverview(BasicMouseEventArgs e){
		}

		private void ZoomOut(){}
		private void ZoomIn(){}

		public override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpMainView?.Invoke(e);
			state = ZoomButtonState.Neutral;
			Invalidate();
		}

		public override void OnMouseDragged(BasicMouseEventArgs e){
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