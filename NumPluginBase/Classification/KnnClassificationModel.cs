﻿using System;
using System.Collections.Generic;
using BaseLib.Num.Api;
using BaseLib.Util;

namespace NumPluginBase.Classification{
	[Serializable]
	public class KnnClassificationModel : ClassificationModel{
		private readonly float[][] x;
		private readonly int[][] y;
		private readonly int ngroups;
		private readonly int k;

		public KnnClassificationModel(float[][] x, int[][] y, int ngroups, int k){
			this.x = x;
			this.y = y;
			this.ngroups = ngroups;
			this.k = k;
		}

		public override float[] PredictStrength(float[] xTest){
			int[] inds = GetNeighborInds(x, xTest, k);
			float[] result = new float[ngroups];
			foreach (int ind in inds){
				foreach (int i in y[ind]){
					result[i]++;
				}
			}
			for (int i = 0; i < ngroups; i++) {
				result[i] /= k;
			}
			return result;
		}

		public static int[] GetNeighborInds(IList<float[]> x, float[] xTest, int k){
			double[] d = CalcDistances(x, xTest);
			int[] o = ArrayUtils.Order(d);
			List<int> result = new List<int>();
			for (int i = 0; i < d.Length; i++){
				if (!double.IsNaN(d[o[i]]) && !double.IsInfinity(d[o[i]])){
					result.Add(o[i]);
				}
				if (result.Count >= k){
					break;
				}
			}
			return result.ToArray();
		}

		private static double[] CalcDistances(IList<float[]> x, IList<float> xTest){
			double[] result = new double[x.Count];
			for (int i = 0; i < x.Count; i++){
				result[i] = CalcDistance(x[i], xTest);
			}
			return result;
		}

		private static double CalcDistance(IList<float> x1, IList<float> x2){
			int n = x1.Count;
			double sum = 0;
			int c = 0;
			for (int i = 0; i < n; i++){
				double d = x1[i] - x2[i];
				if (double.IsNaN(d)){
					continue;
				}
				sum += d*d;
				c++;
			}
			return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
		}
	}
}