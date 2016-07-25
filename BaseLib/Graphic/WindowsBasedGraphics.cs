using System.Drawing;
using System.Drawing.Drawing2D;
using BaseLibS.Graph;

namespace BaseLib.Graphic{
	//TODO: should not be exposed
	public abstract class WindowsBasedGraphics : IGraphics{
		protected Graphics gc;

		protected WindowsBasedGraphics(Graphics gc){
			this.gc = gc;
		}

		public SmoothingMode SmoothingMode{
			get { return gc.SmoothingMode; }
			set { gc.SmoothingMode = value; }
		}

		public Graphics Graphics => gc;
		//TODO?
		public void SetClippingMask(int width, int height, int x, int y){}

		public void Dispose(){
			gc.Dispose();
		}

		public void RotateTransform(float angle){
			gc.RotateTransform(angle);
		}

		public void DrawLine(Pen2 pen, float x1, float y1, float x2, float y2){
			gc.DrawLine(GetPen(pen), x1, y1, x2, y2);
		}

		public void DrawPath(Pen2 pen, GraphicsPath path){
			gc.DrawPath(GetPen(pen), path);
		}

		public void DrawLines(Pen2 pen, PointF[] points){
			gc.DrawLines(GetPen(pen), points);
		}

		public void DrawLines(Pen2 pen, Point[] points){
			gc.DrawLines(GetPen(pen), points);
		}

		public void DrawEllipse(Pen2 pen, float x, float y, float width, float height){
			gc.DrawEllipse(GetPen(pen), x, y, width, height);
		}

		public void FillEllipse(Brush2 brush, float x, float y, float width, float height){
			gc.FillEllipse(GetBrush(brush), x, y, width, height);
		}

		public void DrawRectangle(Pen2 pen, float x, float y, float width, float height){
			gc.DrawRectangle(GetPen(pen), x, y, width, height);
		}

		public void FillRectangle(Brush2 brush, float x, float y, float width, float height){
			gc.FillRectangle(GetBrush(brush), x, y, width, height);
		}

		public void DrawPolygon(Pen2 pen, Point[] points){
			gc.DrawPolygon(GetPen(pen), points);
		}

		public void FillPolygon(Brush2 brush, Point[] points){
			gc.FillPolygon(GetBrush(brush), points);
		}

		public SizeF MeasureString(string text, Font font){
			return gc.MeasureString(text, font);
		}

		public void DrawString(string s, Font font, Brush2 brush, float x, float y){
			gc.DrawString(s, font, GetBrush(brush), x, y);
		}

		public void DrawString(string s, Font font, Brush2 brush, RectangleF rectangleF, StringFormat format){
			gc.DrawString(s, font, GetBrush(brush), rectangleF, format);
		}

		public void DrawString(string s, Font font, Brush2 brush, Point location){
			gc.DrawString(s, font, GetBrush(brush), location);
		}

		public void DrawString(string s, Font font, Brush2 brush, RectangleF rectangleF){
			gc.DrawString(s, font, GetBrush(brush), rectangleF);
		}

		public void DrawString(string s, Font font, Brush2 brush, Point point, StringFormat format){
			gc.DrawString(s, font, GetBrush(brush), point, format);
		}

		public void DrawImage(Image image, int x, int y, int width, int height){
			gc.DrawImage(image, x, y, width, height);
		}

		public void DrawImage(Image image, Rectangle rectangle){
			gc.DrawImage(image, rectangle);
		}

		public void DrawImageUnscaled(Image image, int x, int y){
			gc.DrawImageUnscaled(image, x, y);
		}

		public SizeF MeasureString(string text, Font font, int width){
			return gc.MeasureString(text, font, width);
		}

		public void FillClosedCurve(Brush2 brush, Point[] points){
			gc.FillClosedCurve(GetBrush(brush), points);
		}

		public void DrawCurve(Pen2 pen, Point[] points){
			gc.DrawCurve(GetPen(pen), points);
		}

		public void TranslateTransform(float dx, float dy){
			gc.TranslateTransform(dx, dy);
		}

		public void ResetTransform(){
			gc.ResetTransform();
		}

		public void ResetClip(){
			gc.ResetClip();
		}

		public void SetClip(Rectangle rectangle){
			gc.SetClip(rectangle);
		}

		public abstract void Close();

		public static Pen GetPen(Pen2 p){
			return new Pen(Color.FromArgb(p.Color.A, p.Color.R, p.Color.G, p.Color.B), p.Width);
		}

		private static Brush GetBrush(Brush2 b){
			return new SolidBrush(Color.FromArgb(b.Color.A, b.Color.R, b.Color.G, b.Color.B));
		}
	}
}