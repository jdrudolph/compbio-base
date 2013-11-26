using BaseLib.Graphic;

namespace BaseLib.Forms.Base {
	public interface IPrintable{
		void Print(IGraphics g, int width, int height);
	}
}
