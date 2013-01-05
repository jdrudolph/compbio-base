using System;
using BasicLib.Forms.Base;
using BasicLib.Graphic;

namespace BasicLib.Forms.Scroll{
	internal sealed class ScrollableControlRowFooterView : ScrollComponentView{
		internal ScrollableControlRowFooterView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintRowFooterView(g, main.VisibleY, height);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveRowFooterView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveRowFooterView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickRowFooterView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickRowFooterView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownRowFooterView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpRowFooterView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverRowFooterView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedRowFooterView(e);
		}
	}
}