using System.Collections.Generic;
using System.Drawing;
using BaseLib.Graphic;

namespace BaseLib.Symbol{
	public class SymbolTypeDiamond : SymbolType{
		public SymbolTypeDiamond(int index) : base(index) {}
		public override string Name => "Diamond";

		public override void GetPath(int size, out int[] pathX, out int[] pathY){
			int s2 = size/2;
			List<int> x = new List<int>();
			List<int> y = new List<int>();
			for (int i = 0; i < s2; i++){
				x.Add(i);
				y.Add(s2 - i);
			}
			for (int i = 0; i < s2; i++){
				x.Add(s2 - i);
				y.Add(-i);
			}
			for (int i = 0; i < s2; i++){
				x.Add(-i);
				y.Add(-s2 + i);
			}
			for (int i = 0; i < s2; i++){
				x.Add(-s2 + i);
				y.Add(i);
			}
			pathX = x.ToArray();
			pathY = y.ToArray();
		}

		public override void Draw(int size, int x, int y, IGraphics g, Pen pen, Brush brush){
			int s2 = size/2;
			Point[] points = new[]{new Point(x - s2, y), new Point(x, y - s2), new Point(x + s2, y), new Point(x, y + s2)};
			g.DrawPolygon(pen, points);
		}
	}
}