using System.Drawing;
using System.Drawing.Drawing2D;

namespace BaseLib.Graphic{
	//TODO: should not be exposed
	public abstract class WindowsBasedGraphics : IGraphics{
		protected Graphics gc;

		protected WindowsBasedGraphics(Graphics gc){
			this.gc = gc;
		}

		public SmoothingMode SmoothingMode { get { return gc.SmoothingMode; } set { gc.SmoothingMode = value; } }
		public Graphics Graphics => gc;
		//TODO?
		public void SetClippingMask(int width, int height, int x, int y) {}

		public void Dispose(){
			gc.Dispose();
		}

		public void RotateTransform(float angle){
			gc.RotateTransform(angle);
		}

		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2){
			gc.DrawLine(pen, x1, y1, x2, y2);
		}

		public void DrawPath(Pen pen, GraphicsPath path){
			gc.DrawPath(pen, path);
		}

		public void DrawLines(Pen pen, PointF[] points){
			gc.DrawLines(pen, points);
		}

		public void DrawLines(Pen pen, Point[] points){
			gc.DrawLines(pen, points);
		}

		public void DrawEllipse(Pen pen, float x, float y, float width, float height){
			gc.DrawEllipse(pen, x, y, width, height);
		}

		public void FillEllipse(Brush brush, float x, float y, float width, float height){
			gc.FillEllipse(brush, x, y, width, height);
		}

		public void DrawRectangle(Pen pen, float x, float y, float width, float height){
			gc.DrawRectangle(pen, x, y, width, height);
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height){
			gc.FillRectangle(brush, x, y, width, height);
		}

		public void DrawPolygon(Pen pen, Point[] points){
			gc.DrawPolygon(pen, points);
		}

		public void FillPolygon(Brush brush, Point[] points){
			gc.FillPolygon(brush, points);
		}

		public SizeF MeasureString(string text, Font font){
			return gc.MeasureString(text, font);
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y){
			gc.DrawString(s, font, brush, x, y);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF rectangleF, StringFormat format){
			gc.DrawString(s, font, brush, rectangleF, format);
		}

		public void DrawString(string s, Font font, Brush brush, Point location){
			gc.DrawString(s, font, brush, location);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF rectangleF){
			gc.DrawString(s, font, brush, rectangleF);
		}

		public void DrawString(string s, Font font, Brush brush, Point point, StringFormat format){
			gc.DrawString(s, font, brush, point, format);
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

		public void FillClosedCurve(Brush brush, Point[] points){
			gc.FillClosedCurve(brush, points);
		}

		public void DrawCurve(Pen pen, Point[] points){
			gc.DrawCurve(pen, points);
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
	}
}