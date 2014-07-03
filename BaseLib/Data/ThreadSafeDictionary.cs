using System;
using System.Collections.Generic;

namespace BaseLib.Data{
	[Serializable]
	public class ThreadSafeDictionary<Tk, Tv>{
		private readonly Dictionary<Tk, Tv> hashSet;
		private readonly object locker = new object();
		public ThreadSafeDictionary() { hashSet = new Dictionary<Tk, Tv>(); }

		public void Add(Tk key, Tv value){
			lock (locker){
				hashSet.Add(key, value);
			}
		}

		public bool Remove(Tk item){
			bool foundAndRemoved;
			lock (locker){
				foundAndRemoved = hashSet.Remove(item);
			}
			return foundAndRemoved;
		}

		public bool ContainsKey(Tk key){
			lock (locker){
				return hashSet.ContainsKey(key);
			}
		}

		public Tv this[Tk key]{
			get{
				lock (locker){
					return hashSet[key];
				}
			}
			set{
				lock (locker){
					hashSet[key] = value;
				}
			}
		}
	}
}