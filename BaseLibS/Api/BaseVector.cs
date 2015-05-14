using System;
using System.Collections;
using System.Collections.Generic;

namespace BaseLibS.Api{
	[Serializable]
	public abstract class BaseVector : ICloneable, IDisposable, IEnumerable<double>{
		/// <summary>
		/// Determines the scalar product of this vector with another one passed as the argument. 
		/// </summary>
		public abstract double Dot(BaseVector vec);

		/// <summary>
		/// Produces a deep copy of this vector.
		/// </summary>
		public abstract BaseVector Copy();

		/// <summary>
		/// Number of elements in this vector.
		/// </summary>
		public abstract int Length { get; }

		/// <summary>
		/// Indexer to the elements of this vector.
		/// </summary>
		public abstract double this[int index] { get; set; }

		public abstract double SumSquaredDiffs(BaseVector y1);
		public abstract BaseVector SubArray(IList<int> inds);

		public object Clone(){
			return Copy();
		}

		public abstract IEnumerator<double> GetEnumerator();

		/// <summary>
		/// True if at least one entry is NaN or Infinity.
		/// </summary>
		public abstract bool ContainsNaNOrInf();

		/// <summary>
		/// True if all entries are NaN or Infinity.
		/// </summary>
		public abstract bool IsNanOrInf();

		public abstract void Dispose();
		public abstract float[] Unpack();

		IEnumerator IEnumerable.GetEnumerator(){
			return GetEnumerator();
		}
	}
}