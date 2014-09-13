using System;

namespace BaseLibS.Num.Test{
	public class FisherExactTest{
		public static double Test(int q00, int q01, int q10, int q11){
			return Math.Exp(GetLogFisherP(q00, q01, q10, q11));
		}

		public static double Test(bool[] x, bool[] y){
			int q00;
			int q01;
			int q10;
			int q11;
			CalcContingency(x, y, out q00, out q01, out q10, out q11);
			return Test(q00, q01, q10, q11);
		}

		public static double GetLogFisherP(int q00, int q01, int q10, int q11){
			int rowSum0 = q00 + q01;
			int rowSum1 = q10 + q11;
			int colSum0 = q00 + q10;
			int colSum1 = q01 + q11;
			int total = rowSum0 + rowSum1;
			return NumUtils.Factln(rowSum0) + NumUtils.Factln(rowSum1) + NumUtils.Factln(colSum0) + NumUtils.Factln(colSum1) -
				NumUtils.Factln(q00) - NumUtils.Factln(q01) - NumUtils.Factln(q10) - NumUtils.Factln(q11) - NumUtils.Factln(total);
		}

		public static void CalcContingency(bool[] x, bool[] y, out int r00, out int r01, out int r10, out int r11){
			r00 = 0;
			r01 = 0;
			r10 = 0;
			r11 = 0;
			for (int i = 0; i < x.Length; i++){
				if (x[i]){
					if (y[i]){
						r00++;
					} else{
						r01++;
					}
				} else{
					if (y[i]){
						r10++;
					} else{
						r11++;
					}
				}
			}
		}
	}
}