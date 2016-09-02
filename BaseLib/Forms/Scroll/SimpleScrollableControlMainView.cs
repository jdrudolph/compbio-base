using System;
using BaseLibS.Graph;
using BaseLibS.Graph.Base;

namespace BaseLib.Forms.Scroll{
	internal sealed class SimpleScrollableControlMainView : BasicView{
		private const int buttonSize = 14;
		private ZoomButtonState state = ZoomButtonState.Neutral;
		private readonly SimpleScrollableControl main;
		private static readonly Color2 zoomColor = Color2.CornflowerBlue;
		private static readonly Brush2 zoomBrush = new Brush2(zoomColor);
		private static readonly Brush2 zoomBrushHighlight = new Brush2(Color2.Lighter(zoomColor, 30));
		private static readonly Brush2 zoomBrushPress = new Brush2(Color2.Darker(zoomColor, 30));

		internal SimpleScrollableControlMainView(SimpleScrollableControl main){
			this.main = main;
		}

		public void PaintZoomButtons(IGraphics g, int width, int height){
			g.SmoothingMode = SmoothingMode2.AntiAlias;
			Brush2 b = zoomBrush;
			switch (state){
				case ZoomButtonState.HighlightPlus:
					b = zoomBrushHighlight;
					break;
				case ZoomButtonState.PressPlus:
					b = zoomBrushPress;
					break;
			}
			PaintPlusZoomButton(g, b, width, height);
			b = zoomBrush;
			switch (state){
				case ZoomButtonState.HighlightMinus:
					b = zoomBrushHighlight;
					break;
				case ZoomButtonState.PressMinus:
					b = zoomBrushPress;
					break;
			}
			PaintMinusZoomButton(g, b, width, height);
			g.SmoothingMode = SmoothingMode2.Default;
		}

		public static void PaintPlusZoomButton(IGraphics g, Brush2 b, int width, int height){
			Pen2 w = new Pen2(Color2.White, 2);
			PaintRoundButton(g, b, w, width - buttonSize - 4, height - 2*buttonSize - 8, buttonSize);
			g.DrawLine(w, width - buttonSize, height - 8 - 3*buttonSize/2, width - 8, height - 8 - 3*buttonSize/2);
			g.DrawLine(w, width - 4 - buttonSize/2, height - 2*buttonSize - 4, width - 4 - buttonSize/2, height - buttonSize - 12);
		}

		public static void PaintMinusZoomButton(IGraphics g, Brush2 b, int width, int height){
			Pen2 w = new Pen2(Color2.White, 2);
			PaintRoundButton(g, b, w, width - buttonSize - 4, height - buttonSize - 4, buttonSize);
			g.DrawLine(w, width - buttonSize, height - 4 - buttonSize/2, width - 8, height - 4 - buttonSize/2);
		}

		public static void PaintRoundButton(IGraphics g, Brush2 b, Pen2 w, int x, int y, int size){
			g.FillEllipse(b, x, y, size, size);
			g.DrawEllipse(w, x + 2, y + 2, size - 4, size - 4);
		}

		public override void OnPaint(IGraphics g, int width, int height){
			main.OnPaintMainView?.Invoke(g, main.VisibleX, main.VisibleY, width, height);
			PaintZoomButtons(g, width, height);
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

		private bool HitsPlusButton(int x, int y, int width, int height){
			if (x < width - buttonSize - 4){
				return false;
			}
			if (x > width - 4){
				return false;
			}
			if (y < height - 2*buttonSize - 8){
				return false;
			}
			if (y > height - buttonSize - 8){
				return false;
			}
			return true;
		}

		private bool HitsMinusButton(int x, int y, int width, int height){
			if (x < width - buttonSize - 4){
				return false;
			}
			if (x > width - 4){
				return false;
			}
			if (y < height - buttonSize - 4){
				return false;
			}
			if (y > height - 4){
				return false;
			}
			return true;
		}

		public override void OnMouseHover(EventArgs e){
			main.OnMouseHoverMainView?.Invoke(e);
		}

		public override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveMainView?.Invoke(e);
		}

		public override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickMainView?.Invoke(e);
		}

		public override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickMainView?.Invoke(e);
		}

		public override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownMainView?.Invoke(e);
		}

		public override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpMainView?.Invoke(e);
		}

		public override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedMainView?.Invoke(e);
		}
	}
}