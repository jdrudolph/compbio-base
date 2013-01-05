using System;

namespace BasicLib.Data{
	//TODO: use Tuple with comparator instead
	[Obsolete]
	public class PairC<T1, T2> : ICloneable, IComparable<PairC<T1, T2>> where T1 : IComparable where T2 : IComparable{
		public T1 First { get; set; }
		public T2 Second { get; set; }

		protected PairC(){
			//default constructor for serialization
		}

		public PairC(T1 first, T2 second){
			First = first;
			Second = second;
		}

		public int CompareTo(PairC<T1, T2> other){
			int c1 = First.CompareTo(other.First);
			return c1 != 0 ? c1 : Second.CompareTo(other.Second);
		}

		public override bool Equals(object obj){
			if (ReferenceEquals(null, obj)){
				return false;
			}
			if (ReferenceEquals(this, obj)){
				return true;
			}
			return obj.GetType() == typeof (PairC<T1, T2>) && Equals((PairC<T1, T2>) obj);
		}

		public bool Equals(PairC<T1, T2> other){
			if (ReferenceEquals(null, other)){
				return false;
			}
			return Equals(other.First, First) && Equals(other.Second, Second);
		}

		public object Clone(){
			return new PairC<T1, T2>(First, Second);
		}

		public override int GetHashCode(){
			unchecked{
				return (First.GetHashCode()*397) ^ Second.GetHashCode();
			}
		}
	}
}