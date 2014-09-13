using System;
using System.Collections.Generic;

namespace BaseLibS.Num.Vector{
	[Serializable]
	public abstract class BaseVector : ICloneable{
		public abstract double Dot(BaseVector svmVector);
		public abstract BaseVector Copy();
		public abstract int Length { get; }
		public abstract double this[int index] { get; }
		public abstract double SumSquaredDiffs(BaseVector y1);
		public abstract BaseVector SubArray(IList<int> inds);

		public object Clone(){
			return Copy();
		}
	}
}