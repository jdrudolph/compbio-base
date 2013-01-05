using System;
using BasicLib.Forms.Base;
using BasicLib.Graphic;

namespace BasicLib.Forms.Scroll{
	internal sealed class SimpleScrollableControlMainView : BasicView{
		private readonly SimpleScrollableControl main;

		internal SimpleScrollableControlMainView(SimpleScrollableControl main){
			this.main = main;
		}

		protected internal override void OnPaint(IGraphics g, int width, int height) {
			main.OnPaintMainView(g, main.VisibleX, main.VisibleY, width, height);
		}

		protected internal override void OnMouseMoved(BasicMouseEventArgs e){
			main.OnMouseMoveMainView(e);
		}

		protected internal override void OnMouseHover(EventArgs e){
			main.OnMouseHoverMainView(e);
		}

		protected internal override void OnMouseLeave(EventArgs e){
			main.OnMouseLeaveMainView(e);
		}

		protected internal override void OnMouseClick(BasicMouseEventArgs e){
			main.OnMouseClickMainView(e);
		}

		protected internal override void OnMouseDoubleClick(BasicMouseEventArgs e){
			main.OnMouseDoubleClickMainView(e);
		}

		protected internal override void OnMouseIsDown(BasicMouseEventArgs e){
			main.OnMouseIsDownMainView(e);
		}

		protected internal override void OnMouseIsUp(BasicMouseEventArgs e){
			main.OnMouseIsUpMainView(e);
		}

		protected internal override void OnMouseDragged(BasicMouseEventArgs e){
			main.OnMouseDraggedMainView(e);
		}
	}
}