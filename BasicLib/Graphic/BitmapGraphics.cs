using System.Drawing;
using System.Drawing.Imaging;

namespace BasicLib.Graphic{
	public sealed class BitmapGraphics : WindowsBasedGraphics{
		private readonly Bitmap bmap;
		private readonly string filename;
		private readonly ImageFormat imageFormat;

		public BitmapGraphics(string filename, int width, int height, ImageFormat imageFormat) : base(null){
			bmap = new Bitmap(width, height);
			gc = Graphics.FromImage(bmap);
			this.filename = filename;
			this.imageFormat = imageFormat;
		}

		public override void Close(){
			bmap.Save(filename, imageFormat);
		}
	}
}