using System;
using BasicLib.Util;

namespace BasicLib.Num.Vector{
	[Serializable]
	public class FloatArrayVector : BaseVector{
		internal readonly float[] values;

		public FloatArrayVector(float[] values){
			this.values = values;
		}

		public override int Length { get { return values.Length; } }

		public override BaseVector Copy(){
			float[] newValues = new float[Length];
			Array.Copy(values, newValues, Length);
			return new FloatArrayVector(newValues);
		}

		public override BaseVector Extract(int[] indices) {
			return new FloatArrayVector(ArrayUtils.SubArray(values, indices));
		}

		public override double this[int i] { get { return values[i]; } }

		public override double Dot(BaseVector y){
			if (y is SparseVector) {
				return SparseVector.Dot(this, (SparseVector)y);
			}
			if (y is DoubleArrayVector) {
				return Dot(this, (DoubleArrayVector)y);
			}
			return Dot(this, (FloatArrayVector)y);
		}

		public override double SumSquaredDiffs(BaseVector y) {
			if (y is SparseVector) {
				return SparseVector.SumSquaredDiffs(this, (SparseVector)y);
			}
			if (y is DoubleArrayVector) {
				return SumSquaredDiffs(this, (DoubleArrayVector)y);
			}
			return SumSquaredDiffs(this, (FloatArrayVector)y);
		}

		internal static double Dot(FloatArrayVector x, FloatArrayVector y) {
			double sum = 0;
			for (int i = 0; i < x.Length; i++) {
				sum += x.values[i] * y.values[i];
			}
			return sum;
		}

		internal static double Dot(FloatArrayVector x, DoubleArrayVector y) {
			double sum = 0;
			for (int i = 0; i < x.Length; i++) {
				sum += x.values[i] * y.values[i];
			}
			return sum;
		}

		internal static double SumSquaredDiffs(FloatArrayVector x, FloatArrayVector y) {
			double sum = 0;
			for (int i = 0; i < x.Length; i++) {
				double d = x.values[i] - y.values[i];
				sum += d * d;
			}
			return sum;
		}

		internal static double SumSquaredDiffs(FloatArrayVector x, DoubleArrayVector y) {
			double sum = 0;
			for (int i = 0; i < x.Length; i++) {
				double d = x.values[i] - y.values[i];
				sum += d * d;
			}
			return sum;
		}
	}
}