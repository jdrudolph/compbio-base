using System.Collections.Generic;

namespace BasicLib.Util {
	public class ComparableList<T>{
		private readonly IList<T> list;

		public ComparableList(IList<T> list){
			this.list = list;
		}

		public override bool Equals(object obj) {
			if(obj == null){
				return false;
			}
			if(!(obj is ComparableList<T>)){
				return false;
			}
			return Equals((ComparableList<T>) obj);
		}

		public bool Equals(ComparableList<T> other){
			if (ReferenceEquals(null, other)) return false;
			return ReferenceEquals(this, other) || ArrayUtils.EqualArrays(other.list, list);
		}

		public override int GetHashCode(){
			int result = 0;
			foreach (T t in list){
				result += 29*result + t.GetHashCode();
			}
			return result;
		}

		public IList<T> GetList(){
			return list;
		}
	}
}
