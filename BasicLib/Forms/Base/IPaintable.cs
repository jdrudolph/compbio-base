using System;
using System.Windows.Forms;
using BasicLib.Graphic;

namespace BasicLib.Forms.Base{
	[Obsolete]
	public interface IPaintable {
		void DoPaint2(IGraphics g, Control container, int addx, int addy);
	}
}