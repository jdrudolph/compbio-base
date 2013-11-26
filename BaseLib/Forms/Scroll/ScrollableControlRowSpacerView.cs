using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlRowSpacerView : ScrollComponentView{
		internal ScrollableControlRowSpacerView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintRowSpacerView(g);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveRowSpacerView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveRowSpacerView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickRowSpacerView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickRowSpacerView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownRowSpacerView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpRowSpacerView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverRowSpacerView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedRowSpacerView(e);
		}
	}
}