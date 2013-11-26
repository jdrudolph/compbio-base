using System;
using BaseLib.Util;

namespace BaseLib.Num.Vector{
	[Serializable]
	public class DoubleArrayVector : BaseVector{
		internal readonly double[] values;

		public DoubleArrayVector(double[] values){
			this.values = values;
		}

		public override int Length { get { return values.Length; } }

		public override BaseVector Copy(){
			float[] newValues = new float[Length];
			Array.Copy(values, newValues, Length);
			return new FloatArrayVector(newValues);
		}

		public override BaseVector Extract(int[] indices){
			return new DoubleArrayVector(ArrayUtils.SubArray(values, indices));
		}

		public override double this[int i] { get { return values[i]; } }

		public override double Dot(BaseVector y){
			if ((y is SparseVector)){
				return SparseVector.Dot(this, (SparseVector) y);
			}
			if ((y is FloatArrayVector)){
				return FloatArrayVector.Dot((FloatArrayVector) y, this);
			}
			return Dot(this, (DoubleArrayVector) y);
		}

		public override double SumSquaredDiffs(BaseVector y){
			if ((y is SparseVector)){
				return SparseVector.SumSquaredDiffs(this, (SparseVector) y);
			}
			if ((y is FloatArrayVector)){
				return FloatArrayVector.SumSquaredDiffs((FloatArrayVector) y, this);
			}
			return SumSquaredDiffs(this, (DoubleArrayVector) y);
		}

		internal static double Dot(DoubleArrayVector x, DoubleArrayVector y){
			double sum = 0;
			for (int i = 0; i < x.Length; i++){
				sum += x.values[i]*y.values[i];
			}
			return sum;
		}

		internal static double SumSquaredDiffs(DoubleArrayVector x, DoubleArrayVector y){
			double sum = 0;
			for (int i = 0; i < x.Length; i++){
				double d = x.values[i] - y.values[i];
				sum += d*d;
			}
			return sum;
		}
	}
}