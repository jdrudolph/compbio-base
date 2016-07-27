using System;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlMainView : ScrollComponentView{
		internal ScrollableControlMainView(CompoundScrollableControl main) : base(main) {}

		protected internal override void OnPaint(IGraphics g, int width, int height){
			main.OnPaintMainView?.Invoke(g, main.VisibleX, main.VisibleY, width, height);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveMainView?.Invoke(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverMainView?.Invoke(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveMainView?.Invoke(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickMainView?.Invoke(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickMainView?.Invoke(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownMainView?.Invoke(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpMainView?.Invoke(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedMainView?.Invoke(e);
		}
	}
}