using System;
using System.Collections.Generic;
using BaseLibS.Api;
using BaseLibS.Num;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.Distance{
	[Serializable]
	public class SpearmanCorrelationDistance : IDistance{
		public Parameters Parameters { set { } get { return new Parameters(); } }
		public double Get(IList<float> x, IList<float> y) { return Calc(x, y); }
		public double Get(IList<double> x, IList<double> y) { return Calc(x, y); }
		public double Get(BaseVector x, BaseVector y) { return Calc(x, y); }
		//TODO
		public double Get(float[,] data1, float[,] data2, int index1, int index2, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				int n = data1.GetLength(1);
				float[] x = new float[n];
				float[] y = new float[n];
				for (int i = 0; i < n; i++){
					x[i] = data1[index1, i];
					y[i] = data2[index2, i];
				}
				return Get(x, y);
			} else{
				int n = data1.GetLength(0);
				float[] x = new float[n];
				float[] y = new float[n];
				for (int i = 0; i < n; i++){
					x[i] = data1[i, index1];
					y[i] = data2[i, index2];
				}
				return Get(x, y);
			}
		}

		//TODO
		public double Get(double[,] data1, double[,] data2, int index1, int index2, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				int n = data1.GetLength(1);
				double[] x = new double[n];
				double[] y = new double[n];
				for (int i = 0; i < n; i++){
					x[i] = data1[index1, i];
					y[i] = data2[index2, i];
				}
				return Get(x, y);
			} else{
				int n = data1.GetLength(0);
				double[] x = new double[n];
				double[] y = new double[n];
				for (int i = 0; i < n; i++){
					x[i] = data1[i, index1];
					y[i] = data2[i, index2];
				}
				return Get(x, y);
			}
		}

		public static double Calc(IList<double> x, IList<double> y){
			int n = x.Count;
			List<int> valids = new List<int>();
			for (int i = 0; i < n; i++){
				double xx = x[i];
				double yy = y[i];
				if (double.IsNaN(xx) || double.IsNaN(yy) || double.IsInfinity(xx) || double.IsInfinity(yy)){
					continue;
				}
				valids.Add(i);
			}
			if (valids.Count < 3){
				return double.NaN;
			}
			return PearsonCorrelationDistance.Calc(ArrayUtils.Rank(ArrayUtils.SubArray(x, valids)),
				ArrayUtils.Rank(ArrayUtils.SubArray(y, valids)));
		}

		//TODO
		public static double Calc(BaseVector x, BaseVector y){
			int n = x.Length;
			List<int> valids = new List<int>();
			for (int i = 0; i < n; i++){
				double xx = x[i];
				double yy = y[i];
				if (double.IsNaN(xx) || double.IsNaN(yy) || double.IsInfinity(xx) || double.IsInfinity(yy)){
					continue;
				}
				valids.Add(i);
			}
			if (valids.Count < 3){
				return double.NaN;
			}
			return PearsonCorrelationDistance.Calc(ArrayUtils.Rank(x.SubArray(valids)), ArrayUtils.Rank(y.SubArray(valids)));
		}

		public static double Calc(IList<float> x, IList<float> y){
			int n = x.Count;
			List<int> valids = new List<int>();
			for (int i = 0; i < n; i++){
				double xx = x[i];
				double yy = y[i];
				if (double.IsNaN(xx) || double.IsNaN(yy) || double.IsInfinity(xx) || double.IsInfinity(yy)){
					continue;
				}
				valids.Add(i);
			}
			if (valids.Count < 3){
				return double.NaN;
			}
			return PearsonCorrelationDistance.Calc(ArrayUtils.RankF(ArrayUtils.SubArray(x, valids)),
				ArrayUtils.RankF(ArrayUtils.SubArray(y, valids)));
		}

		public object Clone() { return new SpearmanCorrelationDistance(); }
		public string Name { get { return "Spearman correlation"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 5; } }
		public bool IsActive { get { return true; } }
	}
}