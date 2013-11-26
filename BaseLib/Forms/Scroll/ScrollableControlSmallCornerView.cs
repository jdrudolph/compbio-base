using System.Drawing;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	internal sealed class ScrollableControlSmallCornerView : BasicView{
		protected internal override void OnPaintBackground(IGraphics g, int width, int height) {
			Brush b = new SolidBrush(Color.FromArgb(236, 233, 216));
			g.FillRectangle(b, 0, 0, width, height);
			Pen p = new Pen(Color.FromArgb(172, 168, 153));
			g.DrawLine(p, 0, height - 1, width, height - 1);
			g.DrawLine(p, width - 1, 0, width - 1, height);
		}
	}
}