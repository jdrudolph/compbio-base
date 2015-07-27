using System;
using System.Collections.Generic;
using BaseLibS.Api;
using BaseLibS.Num;
using BaseLibS.Num.Vector;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.RegressionRank{
	public class CorrelationFeatureRanking : IRegressionFeatureRankingMethod{
		public int[] Rank(BaseVector[] x, float[] y, Parameters param, IGroupDataProvider data, int nthreads){
			int nfeatures = x[0].Length;
			double[] s = new double[nfeatures];
			for (int i = 0; i < nfeatures; i++){
				float[] xx = new float[x.Length];
				for (int j = 0; j < xx.Length; j++){
					xx[j] = (float)x[j][i];
				}
				s[i] = CalcScore(xx, y);
			}
			return ArrayUtils.Order(s);
		}

		private static double CalcScore(IList<float> xx, IList<float> yy) { return 1 - Math.Abs(ArrayUtils.Correlation(xx, yy)); }
		public Parameters GetParameters(IGroupDataProvider data) { return new Parameters(); }
		public string Name { get { return "Abs(Pearson correlation)"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 0; } }
		public bool IsActive { get { return true; } }
	}
}