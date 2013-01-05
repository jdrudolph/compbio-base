using System.Drawing;

namespace BasicLib.Graphic{
	public class CGraphics : WindowsBasedGraphics{
		public CGraphics(Graphics g) : base(g) {}
		public override void Close() {}
	}
}