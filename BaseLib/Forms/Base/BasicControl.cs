using System;
using System.Windows.Forms;
using BaseLib.Graphic;

namespace BaseLib.Forms.Base{
	public class BasicControl : ScrollableControl{
		private ToolTip tip;
		private bool mouseDown;
		public BasicView view;

		public BasicControl(){
			//view = new BasicView();
			DoubleBuffered = true;
			ResizeRedraw = true;
			Margin = new Padding(0);
			Dock = DockStyle.Fill;
		}

		public void Print(IGraphics g) {
			if (view != null) {
				view.OnPaint(g, Width, Height);
			}
		}

		protected override sealed void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			if (view != null) {
				try{
					view.OnPaint(new CGraphics(e.Graphics), Width, Height);
				} catch (Exception e1){
					MessageBox.Show(e1.Message + "\n" + e1.StackTrace);
				}
			}
		}

		protected override sealed void OnPaintBackground(PaintEventArgs e){
			base.OnPaintBackground(e);
			if (view != null){
				view.OnPaintBackground(new CGraphics(e.Graphics), Width, Height);
			}
		}

		protected override sealed void OnMouseDown(MouseEventArgs e){
			base.OnMouseDown(e);
			mouseDown = true;
			view.OnMouseIsDown(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
		}

		protected override sealed void OnMouseUp(MouseEventArgs e){
			base.OnMouseUp(e);
			mouseDown = false;
			view.OnMouseIsUp(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
		}

		protected override sealed void OnMouseMove(MouseEventArgs e){
			SetStyle(ControlStyles.Selectable, true);
			Focus();
			base.OnMouseMove(e);
			if (mouseDown){
				view.OnMouseDragged(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
			} else{
				view.OnMouseMoved(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
			}
		}

		protected override sealed void OnMouseLeave(EventArgs e){
			base.OnMouseLeave(e);
			if (view != null){
				view.OnMouseLeave(e);
			}
		}

		protected override sealed void OnMouseClick(MouseEventArgs e){
			base.OnMouseClick(e);
			if (view != null){
				view.OnMouseClick(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
			}
		}

		protected override sealed void OnMouseDoubleClick(MouseEventArgs e){
			base.OnMouseDoubleClick(e);
			if (view != null){
				view.OnMouseDoubleClick(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
			}
		}

		protected override sealed void OnMouseHover(EventArgs e){
			base.OnMouseHover(e);
			if (view != null){
				view.OnMouseHover(e);
			}
		}

		protected override sealed void OnMouseCaptureChanged(EventArgs e){
			base.OnMouseCaptureChanged(e);
			if (view != null){
				view.OnMouseCaptureChanged(e);
			}
		}

		protected override sealed void OnMouseEnter(EventArgs e){
			base.OnMouseEnter(e);
			if (view != null){
				view.OnMouseEnter(e);
			}
		}

		protected override sealed void OnMouseWheel(MouseEventArgs e){
			base.OnMouseWheel(e);
			if (view != null){
				view.OnMouseWheel(new BasicMouseEventArgs(e, Width, Height, () => ModifierKeys, ViewToolTip));
			}
		}

		protected override sealed void OnResize(EventArgs e){
			base.OnResize(e);
			if (view != null){
				view.OnResize(e, Width, Height);
			}
		}

		protected sealed override void Dispose(bool disposing) {
			base.Dispose(disposing);
			if (view != null){
				view.Dispose(disposing);
			}
		}

		private void ViewToolTip(string text, int x, int y, int duration){
			if (tip == null){
				tip = new ToolTip();
			}
			tip.Show(text, this, x, y, duration);
		}
	}
}