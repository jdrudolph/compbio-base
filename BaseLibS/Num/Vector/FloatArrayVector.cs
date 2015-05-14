using System;
using System.Collections.Generic;
using BaseLibS.Api;
using BaseLibS.Util;

namespace BaseLibS.Num.Vector{
	[Serializable]
	public class FloatArrayVector : BaseVector{
		internal float[] values;

		public FloatArrayVector(float[] values){
			this.values = values;
		}

		public override int Length{
			get { return values.Length; }
		}

		public override BaseVector Copy(){
			float[] newValues = new float[Length];
			Array.Copy(values, newValues, Length);
			return new FloatArrayVector(newValues);
		}

		public override double this[int i]{
			get { return values[i]; }
			set { values[i] = (float) value; }
		}

		public override double Dot(BaseVector y){
			if (y is SparseFloatVector){
				return SparseFloatVector.Dot(this, (SparseFloatVector) y);
			}
			if (y is SparseBoolVector){
				return SparseBoolVector.Dot(this, (SparseBoolVector) y);
			}
			if (y is DoubleArrayVector){
				return Dot(this, (DoubleArrayVector) y);
			}
			if (y is BoolArrayVector){
				return BoolArrayVector.Dot((BoolArrayVector) y, this);
			}
			return Dot(this, (FloatArrayVector) y);
		}

		public override double SumSquaredDiffs(BaseVector y){
			if (y is SparseFloatVector){
				return SparseFloatVector.SumSquaredDiffs(this, (SparseFloatVector) y);
			}
			if (y is DoubleArrayVector){
				return SumSquaredDiffs(this, (DoubleArrayVector) y);
			}
			if (y is BoolArrayVector){
				return BoolArrayVector.SumSquaredDiffs((BoolArrayVector) y, this);
			}
			return SumSquaredDiffs(this, (FloatArrayVector) y);
		}

		public override BaseVector SubArray(IList<int> inds){
			return new FloatArrayVector(ArrayUtils.SubArray(values, inds));
		}

		public override IEnumerator<double> GetEnumerator(){
			foreach (float foo in values){
				yield return foo;
			}
		}

		internal static double Dot(FloatArrayVector x, FloatArrayVector y){
			double sum = 0;
			for (int i = 0; i < x.Length; i++){
				sum += x.values[i]*y.values[i];
			}
			return sum;
		}

		internal static double Dot(FloatArrayVector x, DoubleArrayVector y){
			double sum = 0;
			for (int i = 0; i < x.Length; i++){
				sum += x.values[i]*y.values[i];
			}
			return sum;
		}

		internal static double SumSquaredDiffs(FloatArrayVector x, FloatArrayVector y){
			double sum = 0;
			for (int i = 0; i < x.Length; i++){
				double d = x.values[i] - y.values[i];
				sum += d*d;
			}
			return sum;
		}

		internal static double SumSquaredDiffs(FloatArrayVector x, DoubleArrayVector y){
			double sum = 0;
			for (int i = 0; i < x.Length; i++){
				double d = x.values[i] - y.values[i];
				sum += d*d;
			}
			return sum;
		}

		public override bool ContainsNaNOrInf(){
			foreach (float value in values){
				if (float.IsNaN(value) || float.IsInfinity(value)){
					return true;
				}
			}
			return false;
		}

		public override void Dispose(){
			values = null;
		}

		public override bool IsNanOrInf(){
			foreach (float value in values){
				if (!float.IsNaN(value) && !float.IsInfinity(value)){
					return false;
				}
			}
			return true;
		}

		public override float[] Unpack(){
			return values;
		}
	}
}