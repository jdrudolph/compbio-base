using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlColumnHeaderView : ScrollComponentView{
		internal ScrollableControlColumnHeaderView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintColumnHeaderView?.Invoke(g, main.VisibleX, width);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverColumnHeaderView?.Invoke(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedColumnHeaderView?.Invoke(e);
		}
	}
}