using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlRowHeaderView : ScrollComponentView{
		internal ScrollableControlRowHeaderView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintRowHeaderView(g, main.VisibleY, height);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveRowHeaderView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownRowHeaderView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpRowHeaderView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedRowHeaderView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveRowHeaderView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickRowHeaderView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickRowHeaderView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverRowHeaderView(e);
		}
	}
}