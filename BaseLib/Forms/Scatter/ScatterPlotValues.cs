using System.Collections.Generic;
using BaseLib.Util;

namespace BaseLib.Forms.Scatter{
	public class ScatterPlotValues{
		public List<double> SingleValues { get; private set; }
		public List<double[]> MultiValues { get; private set; }
		public bool IsMulti { get; private set; }
		public ScatterPlotValues(IEnumerable<double> vals) : this(new List<double>(vals)) {}
		public ScatterPlotValues(IEnumerable<double[]> vals) : this(new List<double[]>(vals)) {}

		public ScatterPlotValues(List<double> vals){
			SingleValues = vals;
			MultiValues = null;
			IsMulti = false;
		}

		public ScatterPlotValues(List<double[]> vals){
			SingleValues = null;
			MultiValues = vals;
			IsMulti = true;
		}

		public void AddValue(double v) {
			SingleValues.Add(v);
		}

		public void AddValue(double[] v) {
			MultiValues.Add(v);
		}

		public int Length { get { return IsMulti ? MultiValues.Count : SingleValues.Count; } }

		public ScatterPlotValues Rank(){
			return IsMulti ? null : new ScatterPlotValues(Rank(SingleValues));
		}

		private static List<double> Rank(IList<double> x){
			int[] v = ArrayUtils.GetValidInds(x);
			double[] r = ArrayUtils.Rank(ArrayUtils.SubArray(x, v));
			List<double> result = new List<double>(x);
			for (int i = 0; i < v.Length; i++){
				result[v[i]] = r[i];
			}
			return result;
		}
	}
}