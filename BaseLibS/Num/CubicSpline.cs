using System;
using System.IO;
using BaseLibS.Util;

namespace BaseLibS.Num{
	[Serializable]
	public class CubicSpline{
		public readonly double[] x;
		public double[] Y { get; }
		private double[] y2;
		public double Yp1 { get; }
		public double Ypn { get; }

		public CubicSpline(double val){
			x = new double[]{0, 1};
			Y = new[]{val, val};
			y2 = new double[]{0, 0};
			Yp1 = 0;
			Ypn = 0;
		}

		/// <param name="x">x values</param>
		/// <param name="y">y values</param>
		/// <param name="yp1">derivative at <code>x[0]</code> </param>
		/// <param name="ypn">derivative at <code>x[x.Length - 1]</code> </param>
		public CubicSpline(double[] x, double[] y, double yp1, double ypn){
			this.x = x;
			Y = y;
			Yp1 = yp1;
			Ypn = ypn;
		}

		public CubicSpline(BinaryReader reader){
			x = FileUtils.ReadDoubleArray(reader);
			Y = FileUtils.ReadDoubleArray(reader);
			Yp1 = reader.ReadDouble();
			Ypn = reader.ReadDouble();
			y2 = FileUtils.ReadDoubleArray(reader);
		}

		public int Length => x.Length;

		public void Write(BinaryWriter writer){
			FileUtils.Write(x, writer);
			FileUtils.Write(Y, writer);
			writer.Write(Yp1);
			writer.Write(Ypn);
			FileUtils.Write(y2, writer);
		}

		public void CalcSpline(){
			int n = x.Length;
			y2 = new double[n];
			double[] u = new double[n - 1];
			if (Yp1 > 1e30){
				y2[0] = u[0] = 0.0;
			} else{
				y2[0] = -0.5;
				u[0] = (3.0/(x[1] - x[0]))*((Y[1] - Y[0])/(x[1] - x[0]) - Yp1);
			}
			for (int i = 1; i < n - 1; i++){
				double sig = (x[i] - x[i - 1])/(x[i + 1] - x[i - 1]);
				double p = sig*y2[i - 1] + 2.0;
				y2[i] = (sig - 1.0)/p;
				u[i] = (Y[i + 1] - Y[i])/(x[i + 1] - x[i]) - (Y[i] - Y[i - 1])/(x[i] - x[i - 1]);
				u[i] = (6.0*u[i]/(x[i + 1] - x[i - 1]) - sig*u[i - 1])/p;
			}
			double qn;
			double un;
			if (Ypn > 1e30){
				qn = un = 0.0;
			} else{
				qn = 0.5;
				un = (3.0/(x[n - 1] - x[n - 2]))*(Ypn - (Y[n - 1] - Y[n - 2])/(x[n - 1] - x[n - 2]));
			}
			y2[n - 1] = (un - qn*u[n - 2])/(qn*y2[n - 2] + 1.0);
			for (int k = n - 2; k >= 0; k--){
				y2[k] = y2[k]*y2[k + 1] + u[k];
			}
		}

		public double Interpolate(double xval){
			int n = x.Length;
			if (xval > x[n - 1]){
				return Y[n - 1];
			}
			if (xval < x[0]){
				return Y[0];
			}
			int klo = 1;
			int khi = n;
			while (khi - klo > 1){
				int k = (khi + klo) >> 1;
				if (x[k - 1] > xval){
					khi = k;
				} else{
					klo = k;
				}
			}
			khi--;
			klo--;
			double h = x[khi] - x[klo];
			if (h == 0.0){
				throw new Exception("x values are identical.");
			}
			double a = (x[khi] - xval)/h;
			double b = (xval - x[klo])/h;
			return a*Y[klo] + b*Y[khi] + ((a*a*a - a)*y2[klo] + (b*b*b - b)*y2[khi])*(h*h)/6.0;
		}
	}
}