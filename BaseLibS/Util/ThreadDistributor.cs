using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BaseLibS.Util{
	public class ThreadDistributor{
		protected readonly int nThreads;
		protected readonly int nTasks;
		protected Thread[] allWorkThreads;
		protected Stack<int> toBeProcessed;
		private readonly Action<int, int> calculation;
		private readonly object locker = new object();
		private readonly Action<double> reportProgress;
		private int tasksDone;

		public ThreadDistributor(int nThreads, int nTasks, Action<int> calculation, Action<double> reportProgress)
			: this(nThreads, nTasks, (itask, ithread) => calculation(itask), reportProgress){}

		public ThreadDistributor(int nThreads, int nTasks, Action<int> calculation)
			: this(nThreads, nTasks, (itask, ithread) => calculation(itask), null){}

		public ThreadDistributor(int nThreads, int nTasks, Action<int, int> calculation)
			: this(nThreads, nTasks, calculation, null){}

		public ThreadDistributor(int nThreads, int nTasks, Action<int, int> calculation, Action<double> reportProgress){
			this.nThreads = Math.Min(nThreads, nTasks);
			this.nTasks = nTasks;
			this.calculation = calculation;
			this.reportProgress = reportProgress;
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
				allWorkThreads[i].Start(i);
			}
			for (int i = 0; i < nThreads; i++){
				allWorkThreads[i].Join();
			}
		}

		private void Work(object ithread){
			if (reportProgress != null){
				reportProgress(0);
			}
			while (true){
				int x;
				lock (locker){
					if (toBeProcessed.Count == 0){
						break;
					}
					x = toBeProcessed.Pop();
				}
				calculation(x, (int) ithread);
				lock (locker){
					tasksDone++;
					if (reportProgress != null){
						reportProgress(tasksDone/(double) nTasks);
					}
				}
			}
		}
	}
}