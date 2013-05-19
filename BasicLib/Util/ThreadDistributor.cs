using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Utils.Util{
	public class ThreadDistributor{
		protected readonly int nThreads;
		protected readonly int nTasks;
		protected Thread[] allWorkThreads;
		protected Stack<int> toBeProcessed;
		private readonly Action<int> calculation;
		private readonly object locker = new object();

		public ThreadDistributor(int nThreads, int nTasks, Action<int> calculation){
			this.nThreads = Math.Min(nThreads, nTasks);
			this.nTasks = nTasks;
			this.calculation = calculation;
		}

		public void Abort(){
			if (allWorkThreads != null){
				foreach (Thread t in allWorkThreads.Where(t => t != null)){
					t.Abort();
				}
			}
		}

		public void Start(){
			toBeProcessed = new Stack<int>();
			for (int index = nTasks - 1; index >= 0; index--){
				toBeProcessed.Push(index);
			}
			allWorkThreads = new Thread[nThreads];
			for (int i = 0; i < nThreads; i++){
				allWorkThreads[i] = new Thread(Work);
				allWorkThreads[i].Start();
			}
			for (int i = 0; i < nThreads; i++){
				allWorkThreads[i].Join();
			}
		}

		private void Work(){
			while (true){
				int x;
				lock (locker){
					if (toBeProcessed.Count == 0){
						break;
					}
					x = toBeProcessed.Pop();
				}
				calculation(x);
			}
		}
	}
}