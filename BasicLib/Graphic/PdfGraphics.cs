using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BasicLib.Graphic{
	internal class PdfGraphics : IGraphics{
		private float currentWidth;
		private float currentHeight;
		private readonly float originalWidth;
		private readonly float originalHeight;
		private readonly Document document;
		private readonly PdfWriter writer;
		private readonly PdfContentByte content;
		private readonly PdfTemplate topTemplate;
		private PdfTemplate template;

		internal PdfGraphics(string filename, int width, int height){
			originalWidth = currentWidth = width;
			originalHeight = currentHeight = height;
			document = new Document(new iTextSharp.text.Rectangle(width, height), 50, 50, 50, 50);
			document.AddAuthor("");
			document.AddSubject("");
			try{
				writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
				document.Open();
				content = writer.DirectContent;
				template = topTemplate = content.CreateTemplate(width, height);
				content.AddTemplate(template, 0, 0);
			} catch (DocumentException de){
				throw new IOException(de.Message);
			}
		}

		public void Dispose(){
			if (document != null){
				document.Dispose();
			}
			if (writer != null){
				writer.Dispose();
			}
		}

		public SmoothingMode SmoothingMode { get; set; }

		public void SetClippingMask(int width, int height, int x, int y){
			template = topTemplate.CreateTemplate(width, height);
			topTemplate.AddTemplate(template, x, topTemplate.Height - height - y);
			currentWidth = width;
			currentHeight = height;
		}

		public void RotateTransform(float angle){
			System.Diagnostics.Debug.Assert(angle == 90 || angle == -90,
				"PdfGraphics.RotateTransform(float) currently only supports -90 and 90 degrees");
			Matrix m = new Matrix();
			if (angle == 90){
				m.Translate(-(currentHeight + 2), (currentHeight - 4));
				m.Rotate(-angle);
			} else if (angle == -90){
				m.Rotate(-angle);
				m.Translate((currentHeight + 2), -(currentHeight - 4));
			}
			template.Transform(m);
		}

		public void Clear(Color color){
			template.SetRGBColorFill(color.R, color.G, color.B);
			template.Rectangle(0, 0, template.Width, template.Height);
			template.FillStroke();
		}

		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2){
			SetPen(pen);
			template.MoveTo(x1, currentHeight - y1);
			template.LineTo(x2, currentHeight - y2);
			template.Stroke();
		}

		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2){
			SetPen(pen);
			template.MoveTo(x1, currentHeight - y1);
			template.LineTo(x2, currentHeight - y2);
			template.Stroke();
		}

		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2, string title, string description){
			DrawLine(pen, x1, y1, x2, y2);
		}

		public void DrawPath(Pen pen, GraphicsPath path){
			SetPen(pen);
			for (int index = 0; index < path.PathData.Points.Length; index++){
				PointF point = path.PathData.Points[index];
				if (index == 0){
					template.MoveTo(point.X, currentHeight - point.Y);
				} else{
					template.LineTo(point.X, currentHeight - point.Y);
					template.Stroke();
					template.MoveTo(point.X, currentHeight - point.Y);
				}
			}
			template.Stroke();
		}

		public void DrawLines(Pen pen, PointF[] points){
			SetPen(pen);
			for (int index = 0; index < points.Length; index++){
				PointF point = points[index];
				if (index == 0){
					template.MoveTo(point.X, currentHeight - point.Y);
				} else{
					template.LineTo(point.X, currentHeight - point.Y);
					template.Stroke();
					template.MoveTo(point.X, currentHeight - point.Y);
				}
			}
			template.Stroke();
		}

		public void DrawLines(Pen pen, Point[] points){
			SetPen(pen);
			for (int index = 0; index < points.Length; index++){
				PointF point = points[index];
				if (index == 0){
					template.MoveTo(point.X, currentHeight - point.Y);
				} else{
					template.LineTo(point.X, currentHeight - point.Y);
					template.Stroke();
					template.MoveTo(point.X, currentHeight - point.Y);
				}
			}
			template.Stroke();
		}

		public void DrawEllipse(Pen pen, int x, int y, int width, int height){
			SetPen(pen);
			template.Ellipse(x, currentHeight - y, x + width, currentHeight - (y + height));
			template.Stroke();
		}

		public void DrawEllipse(Pen pen, float x, float y, float width, float height){
			SetPen(pen);
			template.Ellipse(x, currentHeight - y, x + width, currentHeight - (y + height));
			template.Stroke();
		}

		public void FillEllipse(Brush brush, int x, int y, int width, int height){
			SetBrush(brush);
			template.Ellipse(x, currentHeight - y, x + width, currentHeight - (y + height));
			template.Fill();
		}

		public void FillEllipse(Brush brush, float x, float y, float width, float height){
			SetBrush(brush);
			template.Ellipse(x, currentHeight - y, x + width, currentHeight - (y + height));
			template.Fill();
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height){
			SetPen(pen);
			template.Rectangle(x, currentHeight - y, width, -height);
			template.Stroke();
		}

		public void DrawRectangle(Pen pen, float x, float y, float width, float height){
			SetPen(pen);
			template.Rectangle(x, currentHeight - y, width, -height);
			template.Stroke();
		}

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="pen">Pen  that determines the color, width, and style of the rectangle.</param>
		/// <param name="rectangle">The rectangle to draw.</param>
		public void DrawRectangle(Pen pen, System.Drawing.Rectangle rectangle){
			DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public void DrawRectangle(Pen pen, RectangleF rectangle){
			DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height, int radius, RectangleCorners corners){
			SetPen(pen);
			template.RoundRectangle(x, y, width, height, radius);
			template.Stroke();
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height){
			if (brush is SolidBrush){
				if ((brush as SolidBrush).Color.IsEmpty){
					return;
				}
			}
			SetBrush(brush);
			template.Rectangle(x, currentHeight - y, width, -height);
			template.Fill();
		}

		public void FillRectangle(Brush brush, int x, int y, int width, int height){
			FillRectangle(brush, x, y, width, (float) height);
		}

		public void FillRectangle(Brush brush, System.Drawing.Rectangle rectangle){
			FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public void FillRectangle(Brush brush, RectangleF rectangle){
			FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		/// <summary>
		/// Fills the interior of a rectangle with rounded corners specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		/// <param name="radius"></param>
		/// <param name="corners"></param>
		public void FillRectangle(Brush brush, float x, float y, float width, float height, float radius,
			RectangleCorners corners){
			SetBrush(brush);
			template.RoundRectangle(x, y, width, height, radius);
			template.Fill();
		}

		public void DrawPolygon(Pen pen, Point[] points){
			SetPen(pen);
			for (int index = 0; index < points.Length; index++){
				PointF point = points[index];
				if (index == 0){
					template.MoveTo(point.X, currentHeight - point.Y);
				} else{
					template.LineTo(point.X, currentHeight - point.Y);
					template.Stroke();
					template.MoveTo(point.X, currentHeight - point.Y);
				}
			}
			template.Stroke();
		}

		public void FillPolygon(Brush brush, Point[] points){
			SetBrush(brush);
			for (int index = 0; index < points.Length; index++){
				PointF point = points[index];
				if (index == 0){
					template.MoveTo(point.X, currentHeight - point.Y);
				} else{
					template.LineTo(point.X, currentHeight - point.Y);
					template.Stroke();
					template.MoveTo(point.X, currentHeight - point.Y);
				}
			}
			template.Fill();
		}

		public SizeF MeasureString(string text, System.Drawing.Font font){
			SetFont(font);
			Chunk chunk = new Chunk(text, GetFont(font));
			return new SizeF(chunk.GetWidthPoint(), font.Height*0.5f);
		}

		public SizeF MeasureString(string text, System.Drawing.Font font, int width){
			return MeasureString(text, font);
		}

		public void FillClosedCurve(Brush brush, Point[] points){
			FillPolygon(brush, points);
		}

		public void DrawCurve(Pen pen, Point[] points){
			DrawPolygon(pen, points);
		}

		public void TranslateTransform(float dx, float dy){
			Matrix m = new Matrix();
			m.Translate(dx, -dy);
		}

		//TODO: is the transform additive?
		public void ResetTransform(){
			template.Transform(new Matrix());
		}

		public void ResetClip(){
			//TODO
		}

		public void SetClip(System.Drawing.Rectangle rectangle){
			template = topTemplate.CreateTemplate(rectangle.Width, rectangle.Height);
			topTemplate.AddTemplate(template, rectangle.X, topTemplate.Height - rectangle.Height - rectangle.Y);
			currentWidth = rectangle.Width;
			currentHeight = rectangle.Height;
		}

		public void Close(){
			document.Close();
			writer.Close();
		}

		public SizeF MeasureString(string text, System.Drawing.Font font, int width, StringFormat format){
			return MeasureString(text, font);
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, float x, float y){
			StringFormat format = new StringFormat{Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near};
			DrawString(s, font, brush, new RectangleF(new PointF(x, y), MeasureString(s, font)), format);
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, Point point, StringFormat format){
			DrawString(s, font, brush, new RectangleF(point, MeasureString(s, font)), format);
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, RectangleF rectangleF, StringFormat format){
			template.BeginText();
			SetFont(font);
			SetBrush(brush);
			float x = rectangleF.X;
			float y = rectangleF.Y;
			IEnumerable<string> items = GetItems(font, rectangleF, s);
			foreach (string t in items){
				SizeF size = MeasureString(t, font);
				bool vertical = false;
				if (format != null){
					switch (format.Alignment){
						case StringAlignment.Center:
							x = x + (rectangleF.Width - size.Width)/2f;
							break;
						case StringAlignment.Near:
							x = x + 2;
							break;
						case StringAlignment.Far:
							x = x + (rectangleF.Width - size.Width) - 2;
							break;
					}
					switch (format.LineAlignment){
						case StringAlignment.Center:
							y = y + font.Height/2f;
							break;
						case StringAlignment.Near:
							y = y + 2;
							break;
						case StringAlignment.Far:
							y = y + font.Height;
							break;
					}
					switch (format.FormatFlags){
						case StringFormatFlags.DirectionVertical:
							vertical = true;
							break;
					}
				}
				if (vertical){
					Matrix m = new Matrix();
					m.Rotate(-90);
					m.Translate(y - currentHeight, x + (font.Height*0.5f));
					template.SetTextMatrix(m);
				} else{
					template.SetTextMatrix(x, currentHeight - y - (font.Height*0.5f));
				}
				template.ShowText(t.TrimStart().TrimEnd());
				y += (font.Height*0.5f);
			}
			template.EndText();
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, RectangleF rectangleF, StringFormat format,
			string title, string description){
			DrawString(s, font, brush, rectangleF, format);
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, RectangleF rectangleF, string title,
			string decription){
			DrawString(s, font, brush, rectangleF);
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, Point location){
			DrawString(s, font, brush, new RectangleF(location, new SizeF(0, 0)), new StringFormat());
		}

		public void DrawString(string s, System.Drawing.Font font, Brush brush, RectangleF rectangleF){
			DrawString(s, font, brush, rectangleF, new StringFormat());
		}

		private IEnumerable<string> GetItems(System.Drawing.Font font, RectangleF rectangleF, string s){
			string[] lines = s.Split(new[]{'\n'});
			List<string> items = new List<string>();
			foreach (string line in lines){
				if (MeasureString(line, font).Width > rectangleF.Width){
					List<string> words = new List<string>(line.Split(new[]{'\t', ' '}));
					string word = "";
					int c = 0;
					while (words.Count > 0){
						if (c == 100){
							throw new Exception("endless loop");
						}
						if (words.Count == 1){
							items.Add(words[0].Trim());
							break;
						}
						if (word == words[0]){
							items.Add(words[0].Trim());
							words.RemoveAt(0);
						}
						if (MeasureString(words[0], font).Width > rectangleF.Width){
							items.Add(words[0].Trim());
							words.RemoveAt(0);
							continue;
						}
						string l = "";
						while (MeasureString(l, font).Width < rectangleF.Width && words.Count > 0){
							word = words[0].Trim();
							words.RemoveAt(0);
							l += word + " ";
						}
						if (MeasureString(l, font).Width > rectangleF.Width){
							items.Add(l.Remove(l.Length - word.Length - 1).Trim());
							words.Insert(0, word);
						} else{
							items.Add(l.Trim());
						}
						c++;
					}
				} else{
					items.Add(line);
				}
			}
			return items;
		}

		public void DrawImage(System.Drawing.Image image, int x, int y, int width, int height){
			iTextSharp.text.Image img = null;
			try{
				img = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Tiff);
			} catch (Exception ex){
				Console.Error.WriteLine(ex.Message);
			}
			if (img != null){
				img.ScaleAbsolute(width, height);
				img.SetAbsolutePosition(x, (currentHeight - img.ScaledHeight) - y);
				template.AddImage(img);
			}
		}

		public void DrawImage(System.Drawing.Image image, System.Drawing.Rectangle rectangle){
			DrawImage(image, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public void DrawImageUnscaled(System.Drawing.Image image, int x, int y){
			// TODO reduce the resolution to fit
			try{
				iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Tiff);
				img.SetAbsolutePosition(x, (currentHeight - img.ScaledHeight) - y);
				template.AddImage(img);
			} catch{}
		}

		private void SetFont(System.Drawing.Font font){
			iTextSharp.text.Font f = GetFont(font);
			template.SetFontAndSize(f.BaseFont, font.SizeInPoints);
		}

		private iTextSharp.text.Font GetFont(System.Drawing.Font font){
			iTextSharp.text.Font f = FontFactory.GetFont("c:/windows/fonts/arial.ttf", BaseFont.CP1252, BaseFont.EMBEDDED,
				font.Size, font.Bold ? 1 : 0);
			try{
				string file;
				if (font.Name == "Lucida Sans Unicode"){
					file = "c:/windows/fonts/L_10646.TTF";
					f = FontFactory.GetFont(file, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, font.Size, font.Bold ? 1 : 0);
				} else if (font.Name == "Arial Unicode MS"){
					file = "c:/windows/fonts/ARIALUNI.TTF";
					f = FontFactory.GetFont(file, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, font.Size, font.Bold ? 1 : 0);
				} else{
					file = string.Format("c:/windows/fonts/{0}.ttf", font.Name);
					if (File.Exists(file)){
						f = FontFactory.GetFont(file, BaseFont.CP1252, BaseFont.EMBEDDED, font.Size, font.Bold ? 1 : 0);
					}
				}
			} catch (Exception){
				// do nothing
			}
			return f;
		}

		private void SetPen(Pen pen){
			template.SetRGBColorStroke(pen.Color.R, pen.Color.G, pen.Color.B);
			template.SetLineWidth(pen.Width);
			switch (pen.DashCap){
				case DashCap.Round:
					template.SetLineCap(PdfContentByte.LINE_CAP_ROUND);
					break;
			}
			switch (pen.DashStyle){
				case DashStyle.Solid:
					template.SetLineDash(0f);
					break;
				case DashStyle.Dash:
					template.SetLineDash(pen.DashPattern, pen.DashOffset);
					break;
				case DashStyle.Custom:
					template.SetLineDash(pen.DashPattern, pen.DashOffset);
					break;
			}
		}

		private void SetBrush(Brush brush){
			if (brush is SolidBrush){
				SolidBrush b = (SolidBrush) brush;
				template.SetRGBColorFill(b.Color.R, b.Color.G, b.Color.B);
			}
		}
	}
}