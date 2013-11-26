using System.Collections.Generic;
using System.Drawing;
using BaseLib.Graphic;

namespace BaseLib.Symbol{
	public class SymbolTypeDiagonalCross : SymbolType{
		public SymbolTypeDiagonalCross(int index) : base(index) {}
		public override string Name { get { return "Diagonal cross"; } }

		public override void GetPath(int size, out int[] pathX, out int[] pathY){
			int s2 = size/2;
			List<int> x = new List<int>();
			List<int> y = new List<int>();
			for (int i = -s2; i <= s2; i++){
				x.Add(i);
				y.Add(i);
			}
			for (int i = -s2; i <= s2; i++){
				if (i == 0){
					continue;
				}
				x.Add(-i);
				y.Add(i);
			}
			pathX = x.ToArray();
			pathY = y.ToArray();
		}

		public override void Draw(int size, int x, int y, IGraphics g, Pen pen, Brush brush){
			int s2 = size/2;
			g.DrawLine(pen, x - s2, y - s2, x + s2, y + s2);
			g.DrawLine(pen, x + s2, y - s2, x - s2, y + s2);
		}
	}
}