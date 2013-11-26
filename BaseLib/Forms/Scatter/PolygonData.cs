using System;

namespace BaseLib.Forms.Scatter {
	[Serializable]
	public class PolygonData{
		public double[] x;
		public double[] xErrUp;
		public double[] xErrDown;
		public double[] y;
		public double[] yErrUp;
		public double[] yErrDown;
	}
}
