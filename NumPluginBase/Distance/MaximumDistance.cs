using System;
using BaseLib.Api;
using BaseLib.Num;
using BaseLib.Param;

namespace NumPluginBase.Distance{
	[Serializable]
	public class MaximumDistance : IDistance{
		public Parameters Parameters { set { } get { return new Parameters(); } }
		public double Get(float[] x, float[] y)
		{
			return Calc(x, y);
		}

		public double Get(double[] x, double[] y){
			return Calc(x, y);
		}

		public double Get(float[,] data1, float[,] data2, int index1, int index2, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				int n = data1.GetLength(1);
				float max = float.MinValue;
				for (int i = 0; i < n; i++){
					float d = data1[index1, i] - data2[index2, i];
					if (float.IsNaN(d) || float.IsInfinity(d)){
						continue;
					}
					float dist = Math.Abs(d);
					if (dist > max){
						max = dist;
					}
				}
				return max == float.MinValue ? double.NaN : max;
			} else{
				int n = data1.GetLength(0);
				float max = float.MinValue;
				for (int i = 0; i < n; i++){
					float d = data1[i, index1] - data2[i, index2];
					if (float.IsNaN(d) || float.IsInfinity(d)){
						continue;
					}
					float dist = Math.Abs(d);
					if (dist > max){
						max = dist;
					}
				}
				return max == float.MinValue ? double.NaN : max;
			}
		}

		public double Get(double[,] data1, double[,] data2, int index1, int index2, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				int n = data1.GetLength(1);
				double max = double.MinValue;
				for (int i = 0; i < n; i++){
					double d = data1[index1, i] - data2[index2, i];
					if (double.IsNaN(d) || double.IsInfinity(d)){
						continue;
					}
					double dist = Math.Abs(d);
					if (dist > max){
						max = dist;
					}
				}
				return max == double.MinValue ? double.NaN : max;
			} else{
				int n = data1.GetLength(0);
				double max = double.MinValue;
				for (int i = 0; i < n; i++){
					double d = data1[i, index1] - data2[i, index2];
					if (double.IsNaN(d) || double.IsInfinity(d)){
						continue;
					}
					double dist = Math.Abs(d);
					if (dist > max){
						max = dist;
					}
				}
				return max == double.MinValue ? double.NaN : max;
			}
		}

		public static double Calc(float[] x, float[] y){
			int n = x.Length;
			float max = float.MinValue;
			for (int i = 0; i < n; i++){
				float d = x[i] - y[i];
				if (float.IsNaN(d) || float.IsInfinity(d)){
					continue;
				}
				float dist = Math.Abs(d);
				if (dist > max){
					max = dist;
				}
			}
			return max == float.MinValue ? double.NaN : max;
		}

		public static double Calc(double[] x, double[] y){
			int n = x.Length;
			double max = double.MinValue;
			for (int i = 0; i < n; i++){
				double d = x[i] - y[i];
				if (double.IsNaN(d) || double.IsInfinity(d)){
					continue;
				}
				double dist = Math.Abs(d);
				if (dist > max){
					max = dist;
				}
			}
			return max == double.MinValue ? double.NaN : max;
		}

		public object Clone(){
			return new MaximumDistance();
		}

		public string Name { get { return "Maximum"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 2; } }
		public bool IsActive { get { return true; } }
	}
}