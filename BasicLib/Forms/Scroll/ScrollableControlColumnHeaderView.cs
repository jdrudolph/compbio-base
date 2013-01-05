using System;
using BasicLib.Forms.Base;
using BasicLib.Graphic;

namespace BasicLib.Forms.Scroll{
	internal sealed class ScrollableControlColumnHeaderView : ScrollComponentView{
		internal ScrollableControlColumnHeaderView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintColumnHeaderView(g, main.VisibleX, width);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveColumnHeaderView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveColumnHeaderView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickColumnHeaderView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickColumnHeaderView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownColumnHeaderView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpColumnHeaderView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverColumnHeaderView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedColumnHeaderView(e);
		}
	}
}