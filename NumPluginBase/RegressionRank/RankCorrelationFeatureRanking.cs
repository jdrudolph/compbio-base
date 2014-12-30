using System;
using System.Collections.Generic;
using BaseLib.Param;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.RegressionRank{
	public class RankCorrelationFeatureRanking : IRegressionFeatureRankingMethod{
		public int[] Rank(BaseVector[] x, float[] y, Parameters param, IGroupDataProvider data, int nthreads){
			int nfeatures = x[0].Length;
			float[] yr = ArrayUtils.RankF(y);
			double[] s = new double[nfeatures];
			for (int i = 0; i < nfeatures; i++){
				float[] xx = new float[x.Length];
				for (int j = 0; j < xx.Length; j++){
					xx[j] = (float) x[j][i];
				}
				float[] xxr = ArrayUtils.RankF(xx);
				s[i] = CalcScore(xxr, yr);
			}
			return ArrayUtils.Order(s);
		}

		private static double CalcScore(IList<float> xx, IList<float> yy) { return 1 - Math.Abs(ArrayUtils.Correlation(xx, yy)); }
		public Parameters GetParameters(IGroupDataProvider data) { return new Parameters(); }
		public string Name { get { return "Abs(Spearman correlation)"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 1; } }
		public bool IsActive { get { return true; } }
	}
}