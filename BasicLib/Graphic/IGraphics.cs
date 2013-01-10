using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BasicLib.Graphic{
	[Flags]
	public enum RectangleCorners{
		None = 0,
		TopLeft = 1,
		TopRight = 2,
		BottomLeft = 4,
		BottomRight = 8,
		All = TopLeft | TopRight | BottomLeft | BottomRight
	}

	/// <summary>
	/// This interface provides the abstract basis for graphics routines for different devices (window, pdf,
	/// etc). As such, graphics routines can be fully generalized and can be reused when exporting graphics
	/// to a file. The routines defined here are fully based on those from the C# class Graphics, in order
	/// to keep the routines legible.
	/// <p/>
	/// It is not intended to be complete
	/// </summary>
	public interface IGraphics{
		/// <summary>
		/// 
		/// </summary>
		SmoothingMode SmoothingMode { get; set; }
		void Dispose();

		/// <summary>
		/// Applies the specified rotation to the transformation matrix.
		/// </summary>
		/// <param name="angle">Angle of rotation in degrees.</param>
		void RotateTransform(float angle);

		/// <summary>
		/// Sets the clipping-mask for the graphics device to draw in. This is required for applications which
		/// draw on a single canvas, where the normal grapics device draws in multiple controls. The origin (i.e.
		/// (0,0)) is moved to the given x and y position and the width and height is set to the new values. For
		/// file formats, this should have the effect that a new element is created in which all the graphics
		/// operations are located.
		/// </summary>
		/// <param name="width">The width of the clipping mask.</param>
		/// <param name="height">The height of the clipping mask.</param>
		/// <param name="x">The x-position of the clipping mask.</param>
		/// <param name="y">The y-position of the clipping mask.</param>
		//TODO:somehow clipping and transforming is mixed up here.
		[Obsolete]
		void SetClippingMask(int width, int height, int x, int y);

		/// <summary>
		/// Draws a line connecting the two points specified by the coordinate pairs.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the line.</param>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		void DrawLine(Pen pen, int x1, int y1, int x2, int y2);

		/// <summary>
		/// Draws a line connecting the two points specified by the coordinate pairs.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the line.</param>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		void DrawLine(Pen pen, float x1, float y1, float x2, float y2);

		/// <summary>
		/// Draws a line connecting the two points specified by the coordinate pairs.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the line.</param>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		/// <param name="title">Title which will be shown by Tooltip for example in SVG.</param>
		/// <param name="description">Description which will be shown by Tooltip for example in SVG.</param>
		void DrawLine(Pen pen, float x1, float y1, float x2, float y2, string title, string description);

		/// <summary>
		/// Draws a GraphicsPath.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the path.</param>
		/// <param name="path">GraphicsPath to draw.</param>
		void DrawPath(Pen pen, GraphicsPath path);

		void DrawLines(Pen pen, PointF[] points);
		void DrawLines(Pen pen, Point[] points);

		/// <summary>
		/// Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of
		/// the rectangle, a height, and a width.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the ellipse.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		void DrawEllipse(Pen pen, int x, int y, int width, int height);

		/// <summary>
		/// Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of
		/// the rectangle, a height, and a width.
		/// </summary>
		/// <param name="pen">Pen that determines the color, width, and style of the ellipse.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		void DrawEllipse(Pen pen, float x, float y, float width, float height);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		void FillEllipse(Brush brush, int x, int y, int width, int height);

		/// <summary>
		/// Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		void FillEllipse(Brush brush, float x, float y, float width, float height);

		/// <summary>
		/// Draws a rectangle specified by a coordinate pair, a width, and a height.
		/// </summary>
		/// <param name="pen">Pen  that determines the color, width, and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">Width of the rectangle to draw.</param>
		/// <param name="height">Height of the rectangle to draw.</param>
		void DrawRectangle(Pen pen, int x, int y, int width, int height);

		/// <summary>
		/// Draws a rectangle specified by a coordinate pair, a width, and a height.
		/// </summary>
		/// <param name="pen">Pen  that determines the color, width, and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">Width of the rectangle to draw.</param>
		/// <param name="height">Height of the rectangle to draw.</param>
		void DrawRectangle(Pen pen, float x, float y, float width, float height);

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="pen">Pen  that determines the color, width, and style of the rectangle.</param>
		/// <param name="rectangle">The rectangle to draw.</param>
		void DrawRectangle(Pen pen, Rectangle rectangle);

		/// <summary>
		/// Draws a rectangle
		/// </summary>
		/// <param name="pen">Pen  that determines the color, width, and style of the rectangle.</param>
		/// <param name="rectangle">The rectangle to draw.</param>
		void DrawRectangle(Pen pen, RectangleF rectangle);

		void DrawRectangle(Pen pen, int x, int y, int width, int height, int radius, RectangleCorners corners);

		/// <summary>
		/// Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		void FillRectangle(Brush brush, int x, int y, int width, int height);

		/// <summary>
		/// Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">Brush that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		void FillRectangle(Brush brush, float x, float y, float width, float height);

		/// <summary>
		/// Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">Brush that determines the characteristics of the fill.</param>
		/// <param name="rectangle">The rectangle to fill.</param>
		void FillRectangle(Brush brush, Rectangle rectangle);

		/// <summary>
		/// Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.
		/// </summary>
		/// <param name="brush">Brush that determines the characteristics of the fill.</param>
		/// <param name="rectangle">The rectangle to fill.</param>
		void FillRectangle(Brush brush, RectangleF rectangle);

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
		void FillRectangle(Brush brush, float x, float y, float width, float height, float radius, RectangleCorners corners);

		void DrawPolygon(Pen pen, Point[] points);
		void FillPolygon(Brush brush, Point[] points);

		/// <summary>
		/// Measures the specified string when drawn with the specified Font.
		/// </summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <returns></returns>
		SizeF MeasureString(string text, Font font);

		SizeF MeasureString(string text, Font font, int width, StringFormat format);

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
		void DrawString(string s, Font font, Brush brush, float x, float y);

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="rectangleF">System.Drawing.RectangleF structure that specifies the location of the drawn text.</param>
		/// <param name="format">System.Drawing.StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
		void DrawString(string s, Font font, Brush brush, RectangleF rectangleF, StringFormat format);

		void DrawString(string s, Font font, Brush brush, Point point, StringFormat format);

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="rectangleF">System.Drawing.RectangleF structure that specifies the location of the drawn text.</param>
		/// <param name="format">System.Drawing.StringFormat that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
		/// <param name="title">Title which will be shown by Tooltip for example in SVG.</param>
		/// <param name="decription">Description which will be shown by Tooltip for example in SVG.</param>
		void DrawString(string s, Font font, Brush brush, RectangleF rectangleF, StringFormat format, string title,
			string decription);

		/// <summary>
		/// Draws the specified text string at the specified location with the specified Brush and Font objects.
		/// </summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">Font that defines the text format of the string.</param>
		/// <param name="brush">Brush that determines the color and texture of the drawn text.</param>
		/// <param name="rectangleF">System.Drawing.RectangleF structure that specifies the location of the drawn text.</param>
		/// <param name="title">Title which will be shown by Tooltip for example in SVG.</param>
		/// <param name="decription">Description which will be shown by Tooltip for example in SVG.</param>
		void DrawString(string s, Font font, Brush brush, RectangleF rectangleF, string title, string decription);

		void DrawString(string s, Font font, Brush brush, Point location);
		void DrawString(string s, Font font, Brush brush, RectangleF rectangleF);

		/// <summary>
		/// Draws the specified Image at the specified location and with the specified size.
		/// </summary>
		/// <param name="image">Image to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="width">Width of the drawn image.</param>
		/// <param name="height">Height of the drawn image.</param>
		void DrawImage(Image image, int x, int y, int width, int height);

		void DrawImage(Image image, Rectangle rectangle);

		/// <summary>
		/// Draws the specified image using its original physical size at the location specified by a coordinate pair.
		/// </summary>
		/// <param name="image">Image to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		void DrawImageUnscaled(Image image, int x, int y);

		SizeF MeasureString(string text, Font font, int width);
		void FillClosedCurve(Brush brush, Point[] points);
		void DrawCurve(Pen pen, Point[] points);
		void TranslateTransform(float dx, float dy);
		void ResetTransform();
		void ResetClip();
		void SetClip(Rectangle rectangle);
		void Close();
	}
}