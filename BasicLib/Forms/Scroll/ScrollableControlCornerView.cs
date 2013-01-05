using System;
using BasicLib.Forms.Base;
using BasicLib.Graphic;

namespace BasicLib.Forms.Scroll{
	internal sealed class ScrollableControlCornerView : ScrollComponentView{
		internal ScrollableControlCornerView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintCornerView(g);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveCornerView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickCornerView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickCornerView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedCornerView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverCornerView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownCornerView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpCornerView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveCornerView(e);
		}
	}
}