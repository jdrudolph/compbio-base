using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace BaseLibS.Util{
    public interface IThreadDistributor {
        void Start();
        void Abort();
    }


    public class ThreadDistributor : IThreadDistributor {
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
			reportProgress?.Invoke(0);
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
					reportProgress?.Invoke(tasksDone/(double) nTasks);
				}
			}
		}
	}

    public class BinaryTreeThreadDistributor<T> : IThreadDistributor {
        protected Thread[] AllWorkThreads;
        private readonly int _nThreads;
        private readonly Action<T, T> _action;
        private readonly Node[] _nodes;
        private readonly object _locker = new object();

        public BinaryTreeThreadDistributor(T[] objects, Action<T, T> action, int nThreads) {
            _action = action;
            _nThreads = nThreads;
            _nodes = new Node[2 * objects.Length - 1];
            Node n1, n2;
            Queue<Node> nodeQueue = new Queue<Node>();
            int j = 0;
            for (; j < objects.Length; j++) {
                _nodes[j] = new Node(null, null, objects[j], true, true);
                nodeQueue.Enqueue(_nodes[j]);
            }
            for (; j < _nodes.Length; j++) {
                n1 = nodeQueue.Dequeue();
                n2 = nodeQueue.Dequeue();
                _nodes[j] = new Node(n1, n2, n1.Data);
                nodeQueue.Enqueue(_nodes[j]);
            }
        }

        public void Start() {
            
            AllWorkThreads = new Thread[_nThreads];
            for (int i = 0; i < _nThreads; i++) {
                AllWorkThreads[i] = new Thread(Work);
                AllWorkThreads[i].Start(i);
            }
            for (int i = 0; i < _nThreads; i++) {
                AllWorkThreads[i].Join();
            }
        }

        private void Work() {
            int k;
            while (true) {
                lock (_locker) {
                    k = 0;
                    while (k < _nodes.Length && !_nodes[k].Started && !_nodes[k].Finished && _nodes[k].Left.Finished && _nodes[k].Right.Finished) k++;
                    if (k != _nodes.Length) {
                        _nodes[k].Started = true;
                    } else {
                        break;
                    }
                }
                _action(_nodes[k].Left.Data, _nodes[k].Right.Data);
                lock (_locker) {
                    _nodes[k].Finished = true;
                }
            }
        }

        public void Abort() {
            if (AllWorkThreads == null) return;
            foreach (Thread t in AllWorkThreads.Where(t => t != null)) {
                t.Abort();
            }
        }

        private class Node {
            public Node Left { get; }
            public Node Right { get; }
            public T Data { get; }
            public bool Started { get; set; }
            public bool Finished { get; set; }
            public Node(Node left, Node right, T data, bool started = false, bool finished = false) {
                Left = left;
                Right = right;
                Data = data;
                Started = started;
                Finished = finished;
            }
        }
    }
}