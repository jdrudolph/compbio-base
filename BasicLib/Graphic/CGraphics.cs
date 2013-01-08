using System.Drawing;

namespace BasicLib.Graphic{
	//TODO: should not be exposed
	public class CGraphics : WindowsBasedGraphics {
		public CGraphics(Graphics g) : base(g) { }
		public override void Close() {}
	}
}