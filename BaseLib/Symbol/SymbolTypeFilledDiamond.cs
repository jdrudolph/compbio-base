using System.Collections.Generic;
using System.Drawing;
using BaseLib.Graphic;

namespace BaseLib.Symbol{
	public class SymbolTypeFilledDiamond : SymbolType{
		public SymbolTypeFilledDiamond(int index) : base(index) {}
		public override string Name => "Filled diamond";

		public override void GetPath(int size, out int[] pathX, out int[] pathY){
			int s2 = size/2;
			List<int> x = new List<int>();
			List<int> y = new List<int>();
			for (int i = s2; i >= 0; i--){
				for (int j = i - s2; j <= s2 - i; j++){
					x.Add(i);
					y.Add(j);
				}
			}
			for (int i = s2; i > 0; i--){
				for (int j = i - s2; j <= s2 - i; j++){
					x.Add(-i);
					y.Add(j);
				}
			}
			pathX = x.ToArray();
			pathY = y.ToArray();
		}

		public override void Draw(int size, int x, int y, IGraphics g, Pen pen, Brush brush){
			int s2 = size/2;
			Point[] points = new[]{new Point(x - s2, y), new Point(x, y - s2), new Point(x + s2, y), new Point(x, y + s2)};
			g.FillPolygon(brush, points);
			g.DrawPolygon(pen, points);
		}
	}
}