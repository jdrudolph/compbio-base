using System.Drawing;
using System.Drawing.Drawing2D;

namespace BasicLib.Graphic{
	//TODO: should not be exposed
	public abstract class WindowsBasedGraphics : IGraphics{
		protected Graphics gc;

		protected WindowsBasedGraphics(Graphics gc){
			this.gc = gc;
		}

		public SmoothingMode SmoothingMode { get { return gc.SmoothingMode; } set { gc.SmoothingMode = value; } }
		public Graphics Graphics { get { return gc; } }
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

		public void DrawRectangle(Pen pen, int x, int y, int width, int height, int radius, RectangleCorners corners){
			GraphicsPath path = GetRectangleWithRoundedCorners(x, y, width, height, radius, corners);
			gc.DrawPath(pen, path);
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height){
			gc.FillRectangle(brush, x, y, width, height);
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height, float radius,
			RectangleCorners corners){
			GraphicsPath p = GetRectangleWithRoundedCorners(x, y, width, height, radius, corners);
			gc.FillPath(brush, p);
		}

		public void DrawPolygon(Pen pen, Point[] points){
			gc.DrawPolygon(pen, points);
		}

		private static GraphicsPath GetRectangleWithRoundedCorners(float x, float y, float width, float height, float radius,
			RectangleCorners corners){
			float xw = x + width;
			float yh = y + height;
			float xwr = xw - radius;
			float yhr = yh - radius;
			float xr = x + radius;
			float yr = y + radius;
			float r2 = radius;
			float xwr2 = xw - r2;
			float yhr2 = yh - r2;
			GraphicsPath p = new GraphicsPath();
			//p.StartFigure();
			//Top Left Corner
			if ((RectangleCorners.TopLeft & corners) == RectangleCorners.TopLeft){
				p.AddArc(x, y, r2, r2, 180, 90);
			} else{
				p.AddLine(x, yr, x, y);
				p.AddLine(x, y, xr, y);
			}
			//Top Edge
			p.AddLine(xr, y, xwr, y);
			//Top Right Corner
			if ((RectangleCorners.TopRight & corners) == RectangleCorners.TopRight){
				p.AddArc(xwr2, y, r2, r2, 270, 90);
			} else{
				p.AddLine(xwr, y, xw, y);
				p.AddLine(xw, y, xw, yr);
			}
			//Right Edge
			p.AddLine(xw, yr, xw, yhr);
			//Bottom Right Corner
			if ((RectangleCorners.BottomRight & corners) == RectangleCorners.BottomRight){
				p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
			} else{
				p.AddLine(xw, yhr, xw, yh);
				p.AddLine(xw, yh, xwr, yh);
			}
			//Bottom Edge
			p.AddLine(xwr, yh, xr, yh);
			//Bottom Left Corner
			if ((RectangleCorners.BottomLeft & corners) == RectangleCorners.BottomLeft){
				p.AddArc(x, yhr2, r2, r2, 90, 90);
			} else{
				p.AddLine(xr, yh, x, yh);
				p.AddLine(x, yh, x, yhr);
			}
			//Left Edge
			p.AddLine(x, yhr, x, yr);
			p.CloseFigure();
			return p;
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