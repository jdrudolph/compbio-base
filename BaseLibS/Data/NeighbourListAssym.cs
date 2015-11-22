using System.Collections.Generic;
using System.Linq;
using BaseLibS.Num;

namespace BaseLibS.Data{
	public class NeighbourListAssym{
		private readonly Dictionary<int, HashSet<int>> neighborList1 = new Dictionary<int, HashSet<int>>();
		private readonly Dictionary<int, HashSet<int>> neighborList2 = new Dictionary<int, HashSet<int>>();

		public void Add(int i, int j){
			if (!neighborList1.ContainsKey(i)){
				neighborList1.Add(i, new HashSet<int>());
			}
			neighborList1[i].Add(j);
			if (!neighborList2.ContainsKey(j)){
				neighborList2.Add(j, new HashSet<int>());
			}
			neighborList2[j].Add(i);
		}

		//public bool IsEmpty1At(int i){
		//	return !neighborList1.ContainsKey(i);
		//}

		//public bool IsEmpty2At(int j){
		//	return !neighborList2.ContainsKey(j);
		//}

		//public int[] GetCluster1At(int i){
		//	int[] cluster = GetCluster1AtNoRemove(i);
		//	RemoveCluster(cluster);
		//	return cluster;
		//}

		//public int[][] GetAllClusters(){
		//	List<int[]> result = new List<int[]>();
		//	while (neighborList.Count > 0){
		//		result.Add(GetClusterAt(neighborList.Keys.First()));
		//	}
		//	return result.ToArray();
		//}

		//public void RemoveCluster(int[] cluster){
		//	foreach (int c in cluster.Where(c => neighborList.ContainsKey(c))){
		//		neighborList.Remove(c);
		//	}
		//}

		//public int[] GetClusterAtNoRemove(int i){
		//	HashSet<int> cluster = new HashSet<int>();
		//	Stack<int> todo = new Stack<int>();
		//	todo.Push(i);
		//	while (todo.Count > 0){
		//		int next = todo.Pop();
		//		if (!cluster.Contains(next)){
		//			if (neighborList.ContainsKey(next)){
		//				cluster.Add(next);
		//				foreach (int x in neighborList[next]){
		//					todo.Push(x);
		//				}
		//			}
		//		}
		//	}
		//	return ArrayUtils.ToArray(cluster);
		//}
	}
}