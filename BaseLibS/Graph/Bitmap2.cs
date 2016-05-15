using System;
using System.IO;

namespace BaseLibS.Graph{
	[Serializable]
	public class Bitmap2{
		private int[,] data;

		/// <summary>
		/// Initializes a new instance of the <code>Bitmap2</code> class with the specified size.
		/// </summary>
		/// <param name="width">The width in pixels.</param>
		/// <param name="height">The height in pixels.</param>
		public Bitmap2(int width, int height){
			data = new int[width, height];
		}

		/// <summary>
		/// Initializes a new instance of the <code>Bitmap2</code> class from the specified file.
		/// </summary>
		/// <param name="filename">The bitmap file name and path.</param>
		public Bitmap2(string filename){
			throw new NotImplementedException();
		}

		/// <summary>
		/// Initializes a new instance of the <code>Bitmap2</code> class from the specified data stream.
		/// </summary>
		/// <param name="stream">The data stream used to load the image.</param>
		public Bitmap2(Stream stream){
			throw new NotImplementedException();
		}
	}
}