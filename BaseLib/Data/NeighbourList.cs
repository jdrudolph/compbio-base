using System.Collections.Generic;
using System.Linq;
using BaseLib.Util;

namespace BaseLib.Data{
	public class NeighbourList{
		private readonly Dictionary<int, List<int>> neighborList = new Dictionary<int, List<int>>();

		public void Add(int i, int j){
			Add2(i, j);
			Add2(j, i);
		}

		public bool IsEmptyAt(int i){
			return !neighborList.ContainsKey(i);
		}

		public int[] GetClusterAt(int i){
			HashSet<int> cluster = new HashSet<int>();
			Stack<int> todo = new Stack<int>();
			todo.Push(i);
			while (todo.Count > 0){
				int next = todo.Pop();
				if (!cluster.Contains(next)){
					if (neighborList.ContainsKey(next)){
						cluster.Add(next);
						foreach (int x in neighborList[next]){
							todo.Push(x);
						}
					}
				}
			}
			foreach (int c in cluster.Where(c => neighborList.ContainsKey(c))){
				neighborList.Remove(c);
			}
			return ArrayUtils.ToArray(cluster);
		}

		public void RemoveCluster(int[] cluster){
			foreach (int c in cluster.Where(c => neighborList.ContainsKey(c))){
				neighborList.Remove(c);
			}
		}

		public int[] GetClusterAtNoRemove(int i){
			HashSet<int> cluster = new HashSet<int>();
			Stack<int> todo = new Stack<int>();
			todo.Push(i);
			while (todo.Count > 0){
				int next = todo.Pop();
				if (!cluster.Contains(next)){
					if (neighborList.ContainsKey(next)){
						cluster.Add(next);
						foreach (int x in neighborList[next]){
							todo.Push(x);
						}
					}
				}
			}
			return ArrayUtils.ToArray(cluster);
		}

		private void Add2(int i, int j){
			if (!neighborList.ContainsKey(i)){
				neighborList.Add(i, new List<int>());
			}
			neighborList[i].Add(j);
		}
	}
}