using System.Drawing;
using System.Drawing.Drawing2D;

namespace BaseLib.Forms.Scatter{
	public class PolygonProperties{
		public Color LineColor { get; set; }
		public int LineWidth { get; set; }
		public DashStyle LineDashStyle { get; set; }
		public Color SymbolColor { get; set; }
		public int SymbolSize { get; set; }
		public int SymbolType { get; set; }
		public bool HorizErrors { get; set; }
		public bool VertErrors { get; set; }
		public int ErrorLineWidth { get; set; }
		public int ErrorSize { get; set; }

		public PolygonProperties(Color lineColor, int lineSize, DashStyle lineDashStyle, Color symbolColor, int symbolSize,
			int symbolType, bool horizErrors, bool vertErrors, int errorLineWidth, int errorSize){
			LineColor = lineColor;
			LineWidth = lineSize;
			LineDashStyle = lineDashStyle;
			SymbolColor = symbolColor;
			SymbolSize = symbolSize;
			SymbolType = symbolType;
			HorizErrors = horizErrors;
			VertErrors = vertErrors;
			ErrorLineWidth = errorLineWidth;
			ErrorSize = errorSize;
		}
	}
}