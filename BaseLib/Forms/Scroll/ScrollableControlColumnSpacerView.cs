using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlColumnSpacerView : ScrollComponentView{
		internal ScrollableControlColumnSpacerView(CompoundScrollableControl main) : base(main) { }

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintColumnSpacerView(g);
		}


		protected internal override void OnMouseMoved(BasicMouseEventArgs e) {
			main.OnMouseMoveColumnSpacerView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e) {
			main.OnMouseLeaveColumnSpacerView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e) {
			main.OnMouseClickColumnSpacerView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e) {
			main.OnMouseDoubleClickColumnSpacerView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e) {
			main.OnMouseIsDownColumnSpacerView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e) {
			main.OnMouseIsUpColumnSpacerView(e);
		}

		protected internal override void OnMouseHover(EventArgs e) {
			main.OnMouseHoverColumnSpacerView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e) {
			main.OnMouseDraggedColumnSpacerView(e);
		}
	}
}