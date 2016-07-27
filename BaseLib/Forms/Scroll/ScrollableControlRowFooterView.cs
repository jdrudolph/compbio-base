using System;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlRowFooterView : ScrollComponentView{
		internal ScrollableControlRowFooterView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintRowFooterView?.Invoke(g, main.VisibleY, height);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverRowFooterView?.Invoke(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedRowFooterView?.Invoke(e);
		}
	}
}