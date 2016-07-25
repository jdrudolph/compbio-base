using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using BaseLibS.Graph;
using BaseLibS.Util;

namespace BaseLib.Graphic{
	internal class SvgGraphics : IGraphics{
		private readonly Svg svg;
		private readonly Stream stream;
		private readonly float docWidth;
		private readonly float docHeight;
		private readonly List<Line> lines = new List<Line>();
		private readonly List<Rect> rectList = new List<Rect>();
		private readonly List<Text> textList = new List<Text>();
		private readonly List<SvgImage> imageList = new List<SvgImage>();
		private readonly List<Path> pathList = new List<Path>();
		private readonly List<Circle> circleList = new List<Circle>();
		private readonly List<Ellipse> ellipseList = new List<Ellipse>();
		private readonly List<Group> groupList = new List<Group>();
		private float rotationAngle;
		private float locationX;
		private float locationY;
		private float scale;
		private Group clippingMask;

		public SvgGraphics(string filename, int width, int height)
			: this(new FileStream(filename, FileMode.Create), width, height){}

		public SvgGraphics(Stream stream, int width, int height){
			this.stream = stream;
			docWidth = width;
			docHeight = height;
			scale = 1;
			svg = new Svg{
				Width = width*scale,
				Height = height*scale,
				ViewBox = $"0 0 {width*scale} {height*scale}",
				Title = "tandem spectrum as vector graphics"
			};
		}

		public void Dispose(){
			stream?.Dispose();
		}

		private string Transform{
			get{
				string transform = "";
				if (locationX != 0 || locationY != 0){
					transform += $" translate({locationX}, {locationY})";
				}
				if (rotationAngle != 0){
					transform += $" rotate({rotationAngle})";
				}
				if (scale != 1){
					transform += $" scale({scale})";
				}
				return string.IsNullOrEmpty(transform) ? null : transform.Trim();
			}
		}

		public string GetColor(Color2 color){
			//if (color.IsNamedColor)
			//{
			//    return color.Name;
			//}
			return $"rgb({color.R},{color.G},{color.B})";
		}

		public string BrushColor(Brush2 b){
			return GetColor(b.Color);
		}

		public string PenColor(Pen2 pen){
			return GetColor(pen.Color);
		}

		/// <summary>
		/// 
		/// </summary>
		public SmoothingMode SmoothingMode { get; set; }

		/// <summary>
		/// Applies the specified rotation to the transformation matrix.
		/// </summary>
		/// <param name="angle">Angle of rotation in degrees.</param>
		public void RotateTransform(float angle){
			rotationAngle += angle;
			clippingMask = new Group{Transform = Transform};
			groupList.Add(clippingMask);
		}

		/// <summary>
		/// Sets the clipping-mask for the graphics device to draw in. This is required for applications which
		/// draw on a single canvas, where the normal grapics device draws in multiple controls. The origin (i.e.
		/// (0,0)) is moved to the given x and y position and the width and height is set to the new values. For
		/// file formats, this should have the effect that a new element is created in which all the graphics
		/// operations are located.
		/// </summary>
		/// <param name="w">The width of the clipping mask.</param>
		/// <param name="h">The height of the clipping mask.</param>
		/// <param name="x">The x-position of the clipping mask.</param>
		/// <param name="y">The y-position of the clipping mask.</param>
		public void SetClippingMask(int w, int h, int x, int y){
			locationX = x;
			locationY = y;
			float wscale = docWidth/w;
			float hscale = docHeight/h;
			if (wscale == hscale){
				scale = wscale;
			}
			clippingMask = new Group{Transform = Transform};
			groupList.Add(clippingMask);
		}

		/// <summary>
		/// Draws a line connecting the two points specified by the coordinate pairs.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the line.</param>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		public void DrawLine(Pen2 pen, float x1, float y1, float x2, float y2){
			Line line = new Line{
				X1 = x1,
				X2 = x2,
				Y1 = y1,
				Y2 = y2,
				Transform = Transform,
				Stroke = PenColor(pen),
				Strokewidth = pen.Width
			};
			lines.Add(line);
		}

		/// <summary>
		/// Draws a GraphicsPath.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the path.</param>
		/// <param name="path">GraphicsPath to draw.</param>
		public void DrawPath(Pen2 pen, GraphicsPath path){
			DrawLines(pen, path.PathPoints);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pen"></param>
		/// <param name="points"></param>
		public void DrawLines(Pen2 pen, PointF[] points){
			Path path = new Path{D = "", Transform = Transform};
			for (int i = 0; i < points.Length; i++){
				if (i == 0){
					path.D += $"M{points[i].X} {points[i].Y} ";
				} else{
					path.D += $"L{points[i].X} {points[i].Y} ";
				}
			}
			path.Stroke = PenColor(pen);
			path.StrokeWidth = pen.Width;
			path.Fill = "none";
			if (pen.DashStyle != DashStyle2.Solid){
				path.StrokeDashArray = "1, 1";
			}
			//path.D = path.D + " Z";
			pathList.Add(path);
		}

		public void DrawLines(Pen2 pen, Point[] points){
			Path path = new Path{D = "", Transform = Transform};
			for (int i = 0; i < points.Length; i++){
				if (i == 0){
					path.D += $"M{points[i].X} {points[i].Y} ";
				} else{
					path.D += $"L{points[i].X} {points[i].Y} ";
				}
			}
			path.Stroke = PenColor(pen);
			path.StrokeWidth = pen.Width;
			path.Fill = "none";
			if (pen.DashStyle != DashStyle2.Solid){
				path.StrokeDashArray = "1, 1";
			}
			//path.D = path.D + " Z";
			pathList.Add(path);
		}

		/// <summary>
		/// Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of
		/// the rectangle, a height, and a width.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the ellipse.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		public void DrawEllipse(Pen2 pen, float x, float y, float width, float height){
			if (width == height){
				circleList.Add(new Circle{
					X = x + width/2f,
					Y = y + height/2f,
					R = width,
					Fill = "none",
					Stroke = pen.Color.Name,
					StrokeWidth = pen.Width,
					Transform = Transform
				});
			} else{
				ellipseList.Add(new Ellipse{
					Cx = x,
					Cy = y,
					Rx = width,
					Ry = height,
					Fill = "none",
					Stroke = pen.Color.Name,
					StrokeWidth = pen.Width,
					Transform = Transform
				});
			}
		}

		/// <summary>
		/// Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		public void FillEllipse(Brush2 brush, float x, float y, float width, float height){
			if (width == height){
				circleList.Add(new Circle{
					X = x + width/2f,
					Y = y + height/2f,
					R = width,
					Fill = BrushColor(brush),
					Transform = Transform
				});
			} else{
				ellipseList.Add(new Ellipse{
					Cx = x,
					Cy = y,
					Rx = width,
					Ry = height,
					Fill = BrushColor(brush),
					Transform = Transform
				});
			}
		}

		/// <summary>
		/// Draws a rectangle specified by a coordinate pair, a width, and a height.
		/// </summary>
		/// <param name="pen">Pen  that determines the color, width, and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">Width of the rectangle to draw.</param>
		/// <param name="height">Height of the rectangle to draw.</param>
		public void DrawRectangle(Pen2 pen, float x, float y, float width, float height){
			Rect rect = new Rect{
				X = x,
				Y = y,
				Width = width,
				Height = height,
				Fill = "none",
				Transform = Transform,
				Stroke = pen.Color.Name,
				StrokeWidth = pen.Width
			};
			rectList.Add(rect);
		}

		/// <summary>
		/// Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		public void FillRectangle(Brush2 brush, float x, float y, float width, float height){
			rectList.Add(new Rect{X = x, Y = y, Width = width, Height = height, Fill = BrushColor(brush), Transform = Transform});
		}

		public void DrawPolygon(Pen2 pen, Point[] points){
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="brush"></param>
		/// <param name="points"></param>
		public void FillPolygon(Brush2 brush, Point[] points){
			throw new NotImplementedException();
		}

		/// <summary>
		/// Measures the specified string when drawn with the specified Font.
		/// </summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <returns></returns>
		public SizeF MeasureString(string text, Font font){
			return TextRenderer.MeasureText(text, font);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="font"></param>
		/// <param name="width"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		public SizeF MeasureString(string text, Font font, int width, StringFormat format){
			TextFormatFlags flags = new TextFormatFlags();
			if (format != null){
				switch (format.Alignment){
					case StringAlignment.Center:
						flags = TextFormatFlags.HorizontalCenter;
						break;
					case StringAlignment.Near:
						flags = TextFormatFlags.Left | TextFormatFlags.Top;
						break;
					case StringAlignment.Far:
						flags = TextFormatFlags.Right | TextFormatFlags.Bottom;
						break;
				}
			}
			return TextRenderer.MeasureText(text, font, new Size(width, font.Height), flags);
		}

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
		public void DrawString(string s, Font font, Brush2 brush, float x, float y){
			DrawString(s, font, brush, new RectangleF(x, y, 0, 0), null);
		}

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="rectangleF">System.Drawing.RectangleF structure that specifies the location of the drawn text.</param>
		/// <param name="format">System.Drawing.StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
		public void DrawString(string s, Font font, Brush2 brush, RectangleF rectangleF, StringFormat format){
			if (format != null && rectangleF.Width > 0){
				switch (format.Alignment){
					case StringAlignment.Center:
						rectangleF.X += (rectangleF.Width - MeasureString(s, font, (int) rectangleF.Width, format).Width)*0.5f;
						break;
					case StringAlignment.Far:
						rectangleF.X += rectangleF.Width - MeasureString(s, font, (int) rectangleF.Width, format).Width;
						break;
				}
			}
			textList.Add(new Text{
				X = rectangleF.X,
				Y = rectangleF.Y + (font.Height*0.5f),
				FontFamily = font.Name,
				FontSize = font.Size*1.2f,
				FontWeight = font.Bold ? "bold" : "normal",
				Value = s,
				Fill = BrushColor(brush),
				Transform = Transform
			});
		}

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="location">The location of the upper-left corner of the drawn text.</param>
		public void DrawString(string s, Font font, Brush2 brush, Point location){
			DrawString(s, font, brush, new RectangleF(location, SizeF.Empty), null);
		}

		public void DrawString(string s, Font font, Brush2 brush, Point point, StringFormat format){
			DrawString(s, font, brush, new RectangleF(point, SizeF.Empty), format);
		}

		public void DrawString(string s, Font font, Brush2 brush, RectangleF rectangleF){
			DrawString(s, font, brush, rectangleF, null);
		}

		/// <summary>
		/// Draws the specified Image at the specified location and with the specified size.
		/// </summary>
		/// <param name="image">Image to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="width">Width of the drawn image.</param>
		/// <param name="height">Height of the drawn image.</param>
		public void DrawImage(Image image, int x, int y, int width, int height){
			imageList.Add(new SvgImage{X = x, Y = y, Transform = Transform});
		}

		/// <summary>
		/// Draws the specified Image at the specified location and with the specified size.
		/// </summary>
		/// <param name="image">Image to draw.</param>
		/// <param name="rectangle">Dimensions of the drawn image.</param>
		public void DrawImage(Image image, Rectangle rectangle){
			imageList.Add(new SvgImage{X = rectangle.X, Y = rectangle.Y, Transform = Transform});
		}

		/// <summary>
		/// Draws the specified image using its original physical size at the location specified by a coordinate pair.
		/// </summary>
		/// <param name="image">Image to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		public void DrawImageUnscaled(Image image, int x, int y){
			imageList.Add(new SvgImage{X = x, Y = y, Transform = Transform});
		}

		public SizeF MeasureString(string text, Font font, int width){
			return TextRenderer.MeasureText(text, font);
		}

		public void FillClosedCurve(Brush2 brush, Point[] points){
			FillPolygon(brush, points);
		}

		public void DrawCurve(Pen2 pen, Point[] points){
			DrawPolygon(pen, points);
		}

		public void TranslateTransform(float dx, float dy){
			//TODO
		}

		public void ResetTransform(){
			//TODO
		}

		public void ResetClip(){
			//TODO
		}

		public void SetClip(Rectangle rectangle){
			//TODO
		}

		public void Close(){
			svg.Line = lines.ToArray();
			svg.Rect = rectList.ToArray();
			svg.Text = textList.ToArray();
			svg.SvgImage = imageList.ToArray();
			svg.Path = pathList.ToArray();
			svg.Circle = circleList.ToArray();
			svg.Ellipse = ellipseList.ToArray();
			XmlSerialization.WriteToStream(stream, svg);
			stream.Close();
		}
	}
}