using System;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Graphic;

namespace BasicLib.Forms.Base{
	public class BasicView : IPrintable{
		public Color BackColor { get; set; }
		public Color ForeColor { get; set; }
		public bool Visible { get; set; }
		public Font Font { get; set; }
		public bool Enabled { get; set; }
		protected Action invalidate;
		protected Action resetCursor;
		protected Action<Cursor> setCursor;

		public BasicView(){
			BackColor = Color.White;
			ForeColor = Color.Black;
			Visible = true;
			Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
		}

		public void Activate(BasicControl control){
			invalidate = control.Invalidate;
			resetCursor = control.ResetCursor;
			setCursor = c => control.Cursor = c;
			control.view = this;
		}

		public void Activate(BasicView view){
			invalidate = view.Invalidate;
			resetCursor = view.ResetCursor;
			setCursor = c => view.Cursor = c;
		}

		public void Invalidate(){
			if (invalidate != null){
				invalidate();
			}
		}

		public void ResetCursor(){
			if (resetCursor != null){
				resetCursor();
			}
		}

		public Cursor Cursor{
			set{
				if (setCursor != null){
					setCursor(value);
				}
			}
		}
		protected internal virtual void OnPaint(IGraphics g, int width, int height) {}

		protected internal virtual void OnPaintBackground(IGraphics g, int width, int height){
			g.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);
		}

		protected internal virtual void OnMouseDragged(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseMoved(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUp(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsDown(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeave(EventArgs e) {}
		protected internal virtual void OnMouseClick(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClick(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHover(EventArgs e) {}
		protected internal virtual void OnMouseCaptureChanged(EventArgs e) {}
		protected internal virtual void OnMouseEnter(EventArgs e) {}
		protected internal virtual void OnMouseWheel(BasicMouseEventArgs e) {}
		protected internal virtual void OnResize(EventArgs e, int width, int height) {}
		protected internal virtual void Dispose(bool disposing) {}

		public BasicControl CreateControl(){
			BasicControl c = new BasicControl();
			Activate(c);
			return c;
		}

		public void Print(IGraphics g, int width, int height){
			OnPaintBackground(g, width, height);
			OnPaint(g, width, height);
		}
	}
}