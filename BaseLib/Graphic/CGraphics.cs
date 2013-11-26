using System.Drawing;

namespace BaseLib.Graphic{
	//TODO: should not be exposed
	public class CGraphics : WindowsBasedGraphics {
		public CGraphics(Graphics g) : base(g) { }
		public override void Close() {}
	}
}