using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlColumnFooterView : ScrollComponentView{
		internal ScrollableControlColumnFooterView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintColumnFooterView?.Invoke(g, main.VisibleX, width);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverColumnFooterView?.Invoke(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedColumnFooterView?.Invoke(e);
		}
	}
}