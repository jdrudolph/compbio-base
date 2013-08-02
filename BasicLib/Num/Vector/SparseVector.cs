using System;
using System.Collections.Generic;

namespace BasicLib.Num.Vector{
	[Serializable]
	public class SparseVector : BaseVector{
		internal readonly int[] indices;
		internal readonly float[] values;
		internal readonly int length;

		public SparseVector(IList<float> values){
			List<int> newIndices = new List<int>();
			List<float> newValues = new List<float>();
			for (int i = 0; i < values.Count; i++){
				if (values[i] != 0){
					newValues.Add(values[i]);
					newIndices.Add(i);
				}
			}
			indices = newIndices.ToArray();
			this.values = newValues.ToArray();
			length = values.Count;
		}

		public SparseVector(int[] indices, float[] values, int length){
			this.indices = indices;
			this.values = values;
			this.length = length;
		}

		public override int Length { get { return length; } }

		public override BaseVector Copy(){
			int[] newIndices = new int[indices.Length];
			Array.Copy(indices, newIndices, indices.Length);
			float[] newValues = new float[values.Length];
			Array.Copy(values, newValues, values.Length);
			return new SparseVector(newIndices, newValues, length);
		}

		public override BaseVector Extract(int[] inds){
			List<int> newIndices = new List<int>();
			List<float> newValues = new List<float>();
			for (int i = 0; i < inds.Length; i++){
				int x = Array.BinarySearch(indices, inds[i]);
				if (x >= 0){
					newIndices.Add(i);
					newValues.Add(values[x]);
				}
			}
			return new SparseVector(newIndices.ToArray(), newValues.ToArray(), inds.Length);
		}

		public override double this[int i]{
			get{
				int ind = Array.BinarySearch(indices, i);
				return ind < 0 ? 0 : values[ind];
			}
		}

		public override double Dot(BaseVector y){
			if (y is FloatArrayVector){
				return Dot((FloatArrayVector) y, this);
			}
			if (y is DoubleArrayVector){
				return Dot((DoubleArrayVector) y, this);
			}
			return Dot(this, (SparseVector) y);
		}

		public override double SumSquaredDiffs(BaseVector y){
			if (y is FloatArrayVector){
				return SumSquaredDiffs((FloatArrayVector) y, this);
			}
			if (y is DoubleArrayVector){
				return SumSquaredDiffs((DoubleArrayVector) y, this);
			}
			return SumSquaredDiffs(this, (SparseVector) y);
		}

		internal static double Dot(SparseVector x, SparseVector y){
			double sum = 0;
			int xlen = x.Length;
			int ylen = y.Length;
			int i = 0;
			int j = 0;
			while (i < xlen && j < ylen){
				if (x.indices[i] == y.indices[j]){
					sum += x.values[i++]*y.values[j++];
				} else{
					if (x.indices[i] > y.indices[j]){
						++j;
					} else{
						++i;
					}
				}
			}
			return sum;
		}

		internal static double Dot(FloatArrayVector x, SparseVector y){
			double sum = 0;
			int xlen = x.Length;
			int ylen = y.Length;
			int i = 0;
			int j = 0;
			while (i < xlen && j < ylen){
				if (i == y.indices[j]){
					sum += x.values[i++]*y.values[j++];
				} else{
					if (i > y.indices[j]){
						++j;
					} else{
						++i;
					}
				}
			}
			return sum;
		}

		internal static double Dot(DoubleArrayVector x, SparseVector y){
			double sum = 0;
			int xlen = x.Length;
			int ylen = y.Length;
			int i = 0;
			int j = 0;
			while (i < xlen && j < ylen){
				if (i == y.indices[j]){
					sum += x.values[i++]*y.values[j++];
				} else{
					if (i > y.indices[j]){
						++j;
					} else{
						++i;
					}
				}
			}
			return sum;
		}

		internal static double SumSquaredDiffs(SparseVector x, SparseVector y){
			double sum = 0;
			int xlen = x.length;
			int ylen = y.length;
			int i = 0;
			int j = 0;
			while (i < xlen && j < ylen){
				if (x.indices[i] == y.indices[j]){
					double d = x.values[i++] - y.values[j++];
					sum += d*d;
				} else if (x.indices[i] > y.indices[j]){
					sum += y.values[j]*y.values[j];
					++j;
				} else{
					sum += x.values[i]*x.values[i];
					++i;
				}
			}
			while (i < xlen){
				sum += x.values[i]*x.values[i];
				++i;
			}
			while (j < ylen){
				sum += y.values[j]*y.values[j];
				++j;
			}
			return sum;
		}

		internal static double SumSquaredDiffs(FloatArrayVector x, SparseVector y){
			double sum = 0;
			int xlen = x.Length;
			int ylen = y.Length;
			int i = 0;
			int j = 0;
			while (i < xlen && j < ylen){
				if (i == y.indices[j]){
					double d = x.values[i++] - y.values[j++];
					sum += d*d;
				} else if (i > y.indices[j]){
					sum += y.values[j]*y.values[j];
					++j;
				} else{
					sum += x.values[i]*x.values[i];
					++i;
				}
			}
			while (i < xlen){
				sum += x.values[i]*x.values[i];
				++i;
			}
			while (j < ylen){
				sum += y.values[j]*y.values[j];
				++j;
			}
			return sum;
		}

		internal static double SumSquaredDiffs(DoubleArrayVector x, SparseVector y){
			double sum = 0;
			int xlen = x.Length;
			int ylen = y.Length;
			int i = 0;
			int j = 0;
			while (i < xlen && j < ylen){
				if (i == y.indices[j]){
					double d = x.values[i++] - y.values[j++];
					sum += d*d;
				} else if (i > y.indices[j]){
					sum += y.values[j]*y.values[j];
					++j;
				} else{
					sum += x.values[i]*x.values[i];
					++i;
				}
			}
			while (i < xlen){
				sum += x.values[i]*x.values[i];
				++i;
			}
			while (j < ylen){
				sum += y.values[j]*y.values[j];
				++j;
			}
			return sum;
		}
	}
}