using BasicLib.Util;

namespace BasicLib.Forms.Scatter{
	public class ScatterPlotValues{
		public double[] SingleValues { get; private set; }
		public double[][] MultiValues { get; private set; }
		public bool IsMulti { get; private set; }

		public ScatterPlotValues(double[] vals){
			SingleValues = vals;
			MultiValues = null;
			IsMulti = false;
		}

		public ScatterPlotValues(double[][] vals){
			SingleValues = null;
			MultiValues = vals;
			IsMulti = true;
		}

		public int Length { get { return IsMulti ? MultiValues.Length : SingleValues.Length; } }

		public ScatterPlotValues Rank(){
			return IsMulti ? null : new ScatterPlotValues(Rank(SingleValues));
		}

		private static double[] Rank(double[] x){
			int[] v = ArrayUtils.GetValidInds(x);
			double[] r = ArrayUtils.Rank(ArrayUtils.SubArray(x, v));
			double[] result = (double[]) x.Clone();
			for (int i = 0; i < v.Length; i++){
				result[v[i]] = r[i];
			}
			return result;
		}
	}
}