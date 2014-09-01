using System;
using System.Collections.Generic;
using BaseLib.Num.Api;
using BaseLib.Util;
using NumPluginBase.Classification;

namespace NumPluginBase.Regression{
	[Serializable]
	public class KnnRegressionModel : RegressionModel{
		private readonly float[][] x;
		private readonly float[] y;
		private readonly int k;

		public KnnRegressionModel(IList<float[]> x, IList<float> y, int k) {
			List<int> v = new List<int>();
			for (int i = 0; i < y.Count; i++){
				if (!double.IsNaN(y[i]) && !double.IsInfinity(y[i])){
					v.Add(i);
				}
			}
			this.x = ArrayUtils.SubArray(x, v);
			this.y = ArrayUtils.SubArray(y, v);
			this.k = k;
		}

		public override float Predict(float[] xTest) {
			int[] inds = KnnClassificationModel.GetNeighborInds(x, xTest, k);
			float result = 0;
			foreach (int ind in inds){
				result += y[ind];
			}
			result /= inds.Length;
			return result;
		}
	}
}