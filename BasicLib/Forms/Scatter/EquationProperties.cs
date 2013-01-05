using System.Drawing;
using System.Drawing.Drawing2D;

namespace BasicLib.Forms.Scatter{
	public class EquationProperties{
		public Color LineColor { get; set; }
		public int LineWidth { get; set; }
		public DashStyle LineDashStyle { get; set; }
		public double MinX { get; set; }
		public double MaxX { get; set; }

		public EquationProperties(Color lineColor, int lineWidth, DashStyle lineDashStyle, double minX, double maxX){
			LineColor = lineColor;
			LineWidth = lineWidth;
			LineDashStyle = lineDashStyle;
			MinX = minX;
			MaxX = maxX;
		}
	}
}