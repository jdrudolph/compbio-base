using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlMiddleCornerView : ScrollComponentView{
		public ScrollableControlMiddleCornerView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintMiddleCornerView(g);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveMiddleCornerView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickMiddleCornerView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickMiddleCornerView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedMiddleCornerView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverMiddleCornerView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownMiddleCornerView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpMiddleCornerView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveMiddleCornerView(e);
		}
	}
}