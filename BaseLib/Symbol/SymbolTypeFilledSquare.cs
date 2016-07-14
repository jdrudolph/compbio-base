using System.Collections.Generic;
using System.Drawing;
using BaseLib.Graphic;

namespace BaseLib.Symbol{
	public class SymbolTypeFilledSquare : SymbolType{
		public SymbolTypeFilledSquare(int index) : base(index) {}
		public override string Name => "Filled square";

		public override void GetPath(int size, out int[] pathX, out int[] pathY){
			int s2 = size/2;
			List<int> x = new List<int>();
			List<int> y = new List<int>();
			for (int i = -s2; i <= s2; i++){
				for (int j = -s2; j <= s2; j++){
					x.Add(i);
					y.Add(j);
				}
			}
			pathX = x.ToArray();
			pathY = y.ToArray();
		}

		public override void Draw(int size, int x, int y, IGraphics g, Pen pen, Brush brush){
			int s2 = size/2;
			g.FillRectangle(brush, x - s2, y - s2, size, size);
			g.DrawRectangle(pen, x - s2, y - s2, size, size);
		}
	}
}