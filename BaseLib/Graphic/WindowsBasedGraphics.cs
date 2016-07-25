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

		public void DrawPath(Pen2 pen, GraphicsPath2 path){
			gc.DrawPath(GetPen(pen), GetGraphicsPath(path));
		}

		public void DrawLines(Pen2 pen, PointF2[] points){
			gc.DrawLines(GetPen(pen), ToPointsF(points));
		}

		public void DrawLines(Pen2 pen, Point2[] points){
			gc.DrawLines(GetPen(pen), ToPoints(points));
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

		public void DrawPolygon(Pen2 pen, Point2[] points){
			gc.DrawPolygon(GetPen(pen), ToPoints(points));
		}

		public void FillPolygon(Brush2 brush, Point2[] points){
			gc.FillPolygon(GetBrush(brush), ToPoints(points));
		}

		public SizeF2 MeasureString(string text, Font font){
			return GraphUtils.ToSizeF2(gc.MeasureString(text, font));
		}

		public void DrawString(string s, Font font, Brush2 brush, float x, float y){
			gc.DrawString(s, font, GetBrush(brush), x, y);
		}

		public void DrawString(string s, Font font, Brush2 brush, RectangleF2 rectangleF, StringFormat format){
			gc.DrawString(s, font, GetBrush(brush), ToRectangleF(rectangleF), format);
		}

		public void DrawString(string s, Font font, Brush2 brush, Point2 location){
			gc.DrawString(s, font, GetBrush(brush), GraphUtils.ToPoint(location));
		}

		public void DrawString(string s, Font font, Brush2 brush, RectangleF2 rectangleF){
			gc.DrawString(s, font, GetBrush(brush), ToRectangleF(rectangleF));
		}

		public void DrawString(string s, Font font, Brush2 brush, Point2 point, StringFormat format){
			gc.DrawString(s, font, GetBrush(brush), GraphUtils.ToPoint(point), format);
		}

		public void DrawImage(Image image, int x, int y, int width, int height){
			gc.DrawImage(image, x, y, width, height);
		}

		public void DrawImage(Image image, Rectangle2 rectangle){
			gc.DrawImage(image, ToRectangle(rectangle));
		}

		public void DrawImageUnscaled(Image image, int x, int y){
			gc.DrawImageUnscaled(image, x, y);
		}

		public SizeF2 MeasureString(string text, Font font, int width){
			return GraphUtils.ToSizeF2(gc.MeasureString(text, font, width));
		}

		public void FillClosedCurve(Brush2 brush, Point2[] points){
			gc.FillClosedCurve(GetBrush(brush), ToPoints(points));
		}

		public void DrawCurve(Pen2 pen, Point2[] points){
			gc.DrawCurve(GetPen(pen), ToPoints(points));
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

		public void SetClip(Rectangle2 rectangle){
			gc.SetClip(ToRectangle(rectangle));
		}

		public abstract void Close();

		public static Pen GetPen(Pen2 p){
			return new Pen(Color.FromArgb(p.Color.A, p.Color.R, p.Color.G, p.Color.B), p.Width);
		}

		private static Brush GetBrush(Brush2 b){
			return new SolidBrush(Color.FromArgb(b.Color.A, b.Color.R, b.Color.G, b.Color.B));
		}

		private static Point[] ToPoints(Point2[] p){
			Point[] result = new Point[p.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = GraphUtils.ToPoint(p[i]);
			}
			return result;
		}

		private static PointF[] ToPointsF(PointF2[] p){
			PointF[] result = new PointF[p.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = GraphUtils.ToPointF(p[i]);
			}
			return result;
		}

		private static GraphicsPath GetGraphicsPath(GraphicsPath2 path){
			byte[] types = new byte[path.PathPoints.Length];
			for (int i = 1; i < types.Length; i++){
				types[i] = 1;
			}
			return new GraphicsPath(ToPointsF(path.PathPoints), new byte[path.PathPoints.Length]);
		}

		private static RectangleF ToRectangleF(RectangleF2 rectangle){
			return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		private static RectangleF ToRectangle(Rectangle2 rectangle){
			return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}
	}
}