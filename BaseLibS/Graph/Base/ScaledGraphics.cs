using System;

namespace BaseLibS.Graph.Base{
	public class ScaledGraphics : IGraphics{
		private readonly IGraphics g;
		private readonly float s;

		public ScaledGraphics(IGraphics g, float s){
			this.g = g;
			this.s = s;
		}

		public SmoothingMode2 SmoothingMode{
			get { return g.SmoothingMode; }
			set { g.SmoothingMode = value; }
		}

		public void Dispose(){
			g.Dispose();
		}

		public void RotateTransform(float angle){
			g.RotateTransform(angle);
		}

		public void SetClippingMask(float width, float height, float x, float y){
			g.SetClippingMask(s*width, s*height, s*x, s*y);
		}

		public void DrawLine(Pen2 pen, float x1, float y1, float x2, float y2){
			g.DrawLine(pen.Scale(s), s*x1, s*y1, s*x2, s*y2);
		}

		public void DrawInterceptedLine(Pen2 pen, float x1, float y1, float x2, float y2, float len){
			g.DrawInterceptedLine(pen.Scale(s), s*x1, s*y1, s*x2, s*y2, s*len);
		}

		public void DrawArrow(Pen2 pen, float x1, float y1, float x2, float y2, float side){
			g.DrawArrow(pen, x1, y1, x2, y2, side);
		}

		public void DrawPath(Pen2 pen, GraphicsPath2 path){
			g.DrawPath(pen, path);
		}

		public void DrawLines(Pen2 pen, PointF2[] points){
			g.DrawLines(pen, points);
		}

		public void DrawEllipse(Pen2 pen, float x, float y, float width, float height){
			g.DrawEllipse(pen, x, y, width, height);
		}

		public void FillEllipse(Brush2 brush, float x, float y, float width, float height){
			g.FillEllipse(brush, x, y, width, height);
		}

		public void DrawRectangle(Pen2 pen, float x, float y, float width, float height){
			throw new NotImplementedException();
		}

		public void FillRectangle(Brush2 brush, float x, float y, float width, float height){
			throw new NotImplementedException();
		}

		public void DrawRoundedRectangle(Pen2 pen, float x, float y, float width, float height, int radius){
			throw new NotImplementedException();
		}

		public void FillRoundedRactangle(Brush2 brush, float x, float y, float width, float height, int radius){
			throw new NotImplementedException();
		}

		public void DrawPolygon(Pen2 pen, PointF2[] points){
			throw new NotImplementedException();
		}

		public void FillPolygon(Brush2 brush, PointF2[] points){
			throw new NotImplementedException();
		}

		public SizeF2 MeasureString(string text, Font2 font){
			throw new NotImplementedException();
		}

		public void DrawString(string s, Font2 font, Brush2 brush, float x, float y){
			throw new NotImplementedException();
		}

		public void DrawString(string s, Font2 font, Brush2 brush, RectangleF2 rectangleF, StringFormat2 format){
			throw new NotImplementedException();
		}

		public void DrawString(string s, Font2 font, Brush2 brush, PointF2 point, StringFormat2 format){
			throw new NotImplementedException();
		}

		public void DrawString(string s, Font2 font, Brush2 brush, PointF2 location){
			throw new NotImplementedException();
		}

		public void DrawString(string s, Font2 font, Brush2 brush, RectangleF2 rectangleF){
			throw new NotImplementedException();
		}

		public void DrawImage(Bitmap2 image, float x, float y, float width, float height){
			throw new NotImplementedException();
		}

		public void DrawImageUnscaled(Bitmap2 image, float x, float y){
			throw new NotImplementedException();
		}

		public SizeF2 MeasureString(string text, Font2 font, float width){
			throw new NotImplementedException();
		}

		public void FillClosedCurve(Brush2 brush, PointF2[] points){
			throw new NotImplementedException();
		}

		public void DrawCurve(Pen2 pen, PointF2[] points){
			throw new NotImplementedException();
		}

		public void TranslateTransform(float dx, float dy){
			throw new NotImplementedException();
		}

		public void ResetTransform(){
			throw new NotImplementedException();
		}

		public void ResetClip(){
			throw new NotImplementedException();
		}

		public void SetClip(RectangleF2 rectangle){
			throw new NotImplementedException();
		}

		public void Close(){
			throw new NotImplementedException();
		}
	}
}