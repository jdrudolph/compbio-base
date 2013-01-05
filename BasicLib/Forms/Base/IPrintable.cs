using BasicLib.Graphic;

namespace BasicLib.Forms.Base {
	public interface IPrintable{
		void Print(IGraphics g, int width, int height);
	}
}
