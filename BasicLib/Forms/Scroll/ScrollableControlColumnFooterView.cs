using System;
using BasicLib.Forms.Base;
using BasicLib.Graphic;

namespace BasicLib.Forms.Scroll{
	internal sealed class ScrollableControlColumnFooterView : ScrollComponentView{
		internal ScrollableControlColumnFooterView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintColumnFooterView(g, main.VisibleX, width);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveColumnFooterView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveColumnFooterView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickColumnFooterView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickColumnFooterView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownColumnFooterView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpColumnFooterView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverColumnFooterView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedColumnFooterView(e);
		}
	}
}