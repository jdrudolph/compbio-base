using System;
using BaseLib.Graphic;
using BaseLibS.Graph;
using BaseLibS.Graph.Base;

namespace BaseLib.Forms.Scroll{
	internal sealed class SimpleScrollableControlMainView : BasicView{
		private ZoomButtonState state = ZoomButtonState.Neutral;
		private readonly SimpleScrollableControl main;
		private int indicatorX1 = -1;
		private int indicatorX2 = -1;
		private int indicatorY1 = -1;
		private int indicatorY2 = -1;
		private int visibleXStart = -1;
		private int visibleYStart = -1;

		internal SimpleScrollableControlMainView(SimpleScrollableControl main){
			this.main = main;
		}

		public override void OnPaint(IGraphics g, int width, int height){
			main.OnPaintMainView?.Invoke(main.zoomFactor == 1f ? g : new ScaledGraphics(g, main.zoomFactor), main.VisibleX,
				main.VisibleY, width, height);
			GraphUtil.PaintZoomButtons(g, width, height, GraphUtil.zoomButtonSize, state);
			GraphUtil.PaintOverview(g, width, height, main.TotalWidth(), main.TotalHeight(), main.VisibleX, main.VisibleY,
				main.VisibleWidth, main.VisibleHeight, (overviewWidth, overviewHeight) =>{
					BitmapGraphics bg = new BitmapGraphics(main.TotalWidth(), main.TotalHeight());
					main.OnPaintMainView?.Invoke(bg, 0, 0, main.TotalWidth(), main.TotalHeight());
					return GraphUtils.ToBitmap2(GraphUtils.ResizeImage(bg.Bitmap, overviewWidth, overviewHeight));
				}, main.zoomFactor);
		}

		public override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveMainView?.Invoke(e);
			ZoomButtonState newState = ZoomButtonState.Neutral;
			if (GraphUtil.HitsMinusButton(e.X, e.Y, e.Width, e.Height)){
				newState = ZoomButtonState.HighlightMinus;
			} else if (GraphUtil.HitsPlusButton(e.X, e.Y, e.Width, e.Height)){
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
			Size2 overview = GraphUtil.CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight());
			if (e.X < overview.Width && e.Y > e.Height - overview.Height){
				return;
			}
			if (GraphUtil.HitsMinusButton(e.X, e.Y, e.Width, e.Height)){
				return;
			}
			if (GraphUtil.HitsPlusButton(e.X, e.Y, e.Width, e.Height)){
				return;
			}
			main.OnMouseClickMainView?.Invoke(e);
		}

		public override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickMainView?.Invoke(e);
		}

		public override void OnMouseIsDown(BasicMouseEventArgs e){
			Size2 overview = GraphUtil.CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight());
			if (e.X < overview.Width && e.Y > e.Height - overview.Height){
				OnMouseIsDownOverview(e.X, (int) (e.Y - e.Height + overview.Height), e.Width, e.Height);
				return;
			}
			ZoomButtonState newState = ZoomButtonState.Neutral;
			bool hitsButton = false;
			if (GraphUtil.HitsMinusButton(e.X, e.Y, e.Width, e.Height)){
				newState = ZoomButtonState.PressMinus;
				hitsButton = true;
				ZoomOut();
			}
			if (GraphUtil.HitsPlusButton(e.X, e.Y, e.Width, e.Height)){
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
			Size2 overview = GraphUtil.CalcOverviewSize(width, height, main.TotalWidth(), main.TotalHeight());
			Rectangle2 win = GraphUtil.CalcWin(overview, main.TotalWidth(), main.TotalHeight(), main.VisibleX, main.VisibleY,
				main.VisibleWidth, main.VisibleHeight, main.zoomFactor);
			if (win.Contains(x, y)){
				indicatorX1 = x;
				indicatorX2 = indicatorX1;
				indicatorY1 = y;
				indicatorY2 = indicatorY1;
				visibleXStart = main.VisibleX;
				visibleYStart = main.VisibleY;
			} else{
				float x1 = x - win.Width/2;
				float y1 = y - win.Height/2;
				int newX = (int) Math.Round(x1*main.TotalWidth()/overview.Width);
				int newY = (int) Math.Round(y1*main.TotalHeight()/overview.Height);
				newX = (int) Math.Min(Math.Max(newX, 0), main.TotalWidth() - main.VisibleWidth/main.zoomFactor);
				main.VisibleX = newX;
				newY = (int) Math.Min(Math.Max(newY, 0), main.TotalHeight() - main.VisibleHeight/main.zoomFactor);
				main.VisibleY = newY;
				invalidate();
			}
		}

		private void ZoomOut(){
			main.zoomFactor /= GraphUtil.zoomStep;
			invalidate();
		}

		private void ZoomIn(){
			main.zoomFactor *= GraphUtil.zoomStep;
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
				Size2 overview = GraphUtil.CalcOverviewSize(e.Width, e.Height, main.TotalWidth(), main.TotalHeight());
				indicatorX2 = e.X;
				indicatorY2 = (int) (e.Y - e.Height + overview.Height);
				int newX = visibleXStart + (int) Math.Round((indicatorX2 - indicatorX1)*main.TotalWidth()/overview.Width);
				newX = (int) Math.Min(Math.Max(newX, 0), main.TotalWidth() - main.VisibleWidth/main.zoomFactor);
				main.VisibleX = newX;
				int newY = visibleYStart + (int) Math.Round((indicatorY2 - indicatorY1)*main.TotalHeight()/overview.Height);
				newY = (int) Math.Min(Math.Max(newY, 0), main.TotalHeight() - main.VisibleHeight/main.zoomFactor);
				main.VisibleY = newY;
				invalidate();
				return;
			}
			main.OnMouseDraggedMainView?.Invoke(e);
		}
	}
}