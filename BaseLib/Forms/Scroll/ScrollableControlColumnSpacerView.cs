using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlColumnSpacerView : ScrollComponentView{
		internal ScrollableControlColumnSpacerView(CompoundScrollableControl main) : base(main){}

		protected internal override void OnPaint(IGraphics g, int width, int height){
			main.OnPaintColumnSpacerView?.Invoke(g);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverColumnSpacerView?.Invoke(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedColumnSpacerView?.Invoke(e);
		}
	}
}