using System;
using System.Collections;
using System.Collections.Generic;

namespace BaseLibS.Api{
	[Serializable]
	public abstract class BaseVector : ICloneable, IDisposable, IEnumerable<double>{
		public abstract double Dot(BaseVector svmVector);
		public abstract BaseVector Copy();
		public abstract int Length { get; }
		public abstract double this[int index] { get; set; }
		public abstract double SumSquaredDiffs(BaseVector y1);
		public abstract BaseVector SubArray(IList<int> inds);
		public object Clone() { return Copy(); }
		public abstract IEnumerator<double> GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
		public abstract bool ContainsNaNOrInfinity();
		public abstract void Dispose();
	}
}