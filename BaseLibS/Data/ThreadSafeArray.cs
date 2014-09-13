using System;

namespace BaseLibS.Data{
	[Serializable]
	public class ThreadSafeArray<T>{
		private readonly T[] array;
		private readonly object locker = new object();

		public ThreadSafeArray(int len){
			array = new T[len];
		}

		public T this[int i]{
			get{
				lock (locker){
					return array[i];
				}
			}
			set{
				lock (locker){
					array[i] = value;
				}
			}
		}

		public int Length { get { return array.Length; } }
		public T[] ToArray { get { return array; } }
	}
}