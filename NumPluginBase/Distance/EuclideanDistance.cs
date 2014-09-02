using System;
using BaseLib.Api;
using BaseLib.Num;
using BaseLib.Param;

namespace NumPluginBase.Distance{
	[Serializable]
	public class EuclideanDistance : IDistance{
		public Parameters Parameters { set { } get { return new Parameters(); } }

		public double Get(float[] x, float[] y){
			return Calc(x, y);
		}

		public double Get(double[] x, double[] y){
			return Calc(x, y);
		}

		public double Get(float[,] data1, float[,] data2, int index1, int index2, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				int n = data1.GetLength(1);
				double sum = 0;
				int c = 0;
				for (int i = 0; i < n; i++){
					double d = data1[index1, i] - data2[index2, i];
					if (double.IsNaN(d)){
						continue;
					}
					sum += d*d;
					c++;
				}
				return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
			} else{
				int n = data1.GetLength(0);
				double sum = 0;
				int c = 0;
				for (int i = 0; i < n; i++){
					double d = data1[i, index1] - data2[i, index2];
					if (double.IsNaN(d)){
						continue;
					}
					sum += d*d;
					c++;
				}
				return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
			}
		}

		public double Get(double[,] data1, double[,] data2, int index1, int index2, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				int n = data1.GetLength(1);
				double sum = 0;
				int c = 0;
				for (int i = 0; i < n; i++){
					double d = data1[index1, i] - data2[index2, i];
					if (double.IsNaN(d)){
						continue;
					}
					sum += d*d;
					c++;
				}
				return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
			} else{
				int n = data1.GetLength(0);
				double sum = 0;
				int c = 0;
				for (int i = 0; i < n; i++){
					double d = data1[i, index1] - data2[i, index2];
					if (double.IsNaN(d)){
						continue;
					}
					sum += d*d;
					c++;
				}
				return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
			}
		}

		public static double Calc(float[] x, float[] y){
			int n = x.Length;
			double sum = 0;
			int c = 0;
			for (int i = 0; i < n; i++){
				double d = x[i] - y[i];
				if (double.IsNaN(d)){
					continue;
				}
				sum += d*d;
				c++;
			}
			return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
		}

		public static double Calc(double[] x, double[] y){
			int n = x.Length;
			double sum = 0;
			int c = 0;
			for (int i = 0; i < n; i++){
				double d = x[i] - y[i];
				if (double.IsNaN(d)){
					continue;
				}
				sum += d*d;
				c++;
			}
			return c == 0 ? double.NaN : Math.Sqrt(sum/c*n);
		}

		public object Clone(){
			return new EuclideanDistance();
		}

		public string Name { get { return "Euclidean"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 0; } }
		public bool IsActive { get { return true; } }
	}
}