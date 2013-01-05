using System;
using BasicLib.Graphic;

namespace BasicLib.Forms.Base{
	[Obsolete]
	public interface IGridView {
		void DoPaint(IGraphics graphics);
	}
}