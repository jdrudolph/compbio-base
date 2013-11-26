using System;

namespace BaseLib.Num.Vector{
	[Serializable]
	public abstract class BaseVector : ICloneable{
		public abstract double Dot(BaseVector svmVector);
		public abstract BaseVector Copy();
		public abstract BaseVector Extract(int[] indices);
		public abstract int Length { get; }
		public abstract double this[int index] { get; }
		public abstract double SumSquaredDiffs(BaseVector y1);

		public object Clone(){
			return Copy();
		}
	}
}