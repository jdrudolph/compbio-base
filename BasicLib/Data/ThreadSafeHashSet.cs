using System;
using System.Collections.Generic;
using BasicLib.Util;

namespace BasicLib.Data{
	[Serializable]
	public class ThreadSafeHashSet<T> : ISet<T>{
		private readonly HashSet<T> hashSet;
		private readonly object mutex = new object();

		public ThreadSafeHashSet(){
			hashSet = new HashSet<T>();
		}

		public ThreadSafeHashSet(IEnumerable<T> collection){
			hashSet = new HashSet<T>(collection);
		}

		public IEnumerator<T> GetEnumerator(){
			HashSet<T> clone;
			lock (mutex){
				clone = new HashSet<T>(hashSet);
			}
			return clone.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
			return GetEnumerator();
		}

		public bool Add(T item){
			bool elementWasAdded;
			lock (mutex){
				elementWasAdded = hashSet.Add(item);
			}
			return elementWasAdded;
		}

		public bool Remove(T item){
			bool foundAndRemoved;
			lock (mutex){
				foundAndRemoved = hashSet.Remove(item);
			}
			return foundAndRemoved;
		}

		public void ExceptWith(IEnumerable<T> other){
			lock (mutex){
				hashSet.ExceptWith(other);
			}
		}

		public void IntersectWith(IEnumerable<T> other){
			lock (mutex){
				hashSet.IntersectWith(other);
			}
		}

		public bool IsProperSubsetOf(IEnumerable<T> other){
			bool isProperSubsetOf = hashSet.IsProperSubsetOf(other);
			return isProperSubsetOf;
		}

		public bool IsProperSupersetOf(IEnumerable<T> other){
			bool isProperSupersetOf = hashSet.IsProperSupersetOf(other);
			return isProperSupersetOf;
		}

		public bool IsSubsetOf(IEnumerable<T> other){
			bool isSubsetOf = hashSet.IsSubsetOf(other);
			return isSubsetOf;
		}

		public bool IsSupersetOf(IEnumerable<T> other){
			bool isSupersetOf = hashSet.IsSupersetOf(other);
			return isSupersetOf;
		}

		public bool Overlaps(IEnumerable<T> other){
			bool overlaps = hashSet.Overlaps(other);
			return overlaps;
		}

		public bool SetEquals(IEnumerable<T> other){
			bool setsAreEqual = hashSet.Overlaps(other);
			return setsAreEqual;
		}

		public void SymmetricExceptWith(IEnumerable<T> other){
			lock (mutex){
				hashSet.SymmetricExceptWith(other);
			}
		}

		public void UnionWith(IEnumerable<T> other){
			lock (mutex){
				hashSet.UnionWith(other);
			}
		}

		void ICollection<T>.Add(T item){
			Add(item);
		}

		public void Clear(){
			lock (mutex){
				hashSet.Clear();
			}
		}

		public bool Contains(T item){
			bool contains = hashSet.Contains(item);
			return contains;
		}

		public void CopyTo(T[] array, int arrayIndex){
			int i = 0;
			foreach (T item in this){
				array[arrayIndex + i] = item;
				i++;
			}
		}

		public int Count { get { return hashSet.Count; } }
		public bool IsReadOnly { get { return ((ISet<T>) (hashSet)).IsReadOnly; } }

		public int RemoveWhere(Predicate<T> predicate){
			HashSet<T> toBeRemoved = new HashSet<T>();
			foreach (T item in this){ // GetEnumerator() is thread safe {
				if (predicate(item)){
					toBeRemoved.Add(item);
				}
			}
			int foundAndRemovedCount = 0;
			lock (mutex){
				foreach (T item in toBeRemoved){
					bool foundAndRemoved = hashSet.Remove(item);
					if (foundAndRemoved){
						foundAndRemovedCount++;
					}
				}
			}
			return foundAndRemovedCount;
		}

		public T[] ToArray(){
			T[] result;
			lock (mutex){
				result = ArrayUtils.ToArray(hashSet);
			}
			return result;
		}
	}
}