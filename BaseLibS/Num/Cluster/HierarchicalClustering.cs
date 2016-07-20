using System;
using System.Collections.Generic;
using System.Threading;
using BaseLibS.Api;
using BaseLibS.Num.Matrix;
using BaseLibS.Num.Vector;

namespace BaseLibS.Num.Cluster{
	/// <summary>
	/// Static class containing utility routines for hierarchical clustering.
	/// </summary>
	public class HierarchicalClustering{
		/// <summary>
		/// Performs a hierarchical clustering on the the given data matrix.
		/// </summary>
		/// <param name="data">Data matrix that is going to be clustered.</param>
		/// <param name="access">Specifies whether rows or columns are to be clustered</param>
		/// <param name="distance">Defines the distance between two elements</param>
		/// <param name="linkage">Specifies the linkage for the clustering.</param>
		/// <param name="preserveOrder"></param>
		/// <param name="periodic"></param>
		/// <param name="nthreads"></param>
		/// <param name="progress"></param>
		/// <returns>An array of cluster nodes defining the resulting tree.</returns>
		public HierarchicalClusterNode[] TreeCluster(MatrixIndexer data, MatrixAccess access, IDistance distance, HierarchicalClusterLinkage linkage,
			bool preserveOrder, bool periodic, int nthreads, Action<int> progress){
			int nelements = (access == MatrixAccess.Rows) ? data.RowCount : data.ColumnCount;
			if (nelements < 2){
				return new HierarchicalClusterNode[0];
			}
			float[,] distMatrix = DistanceMatrix(data, distance, access);
			return TreeCluster(distMatrix, linkage, preserveOrder, periodic, nthreads, progress);
		}
		/// <summary>
		/// Performs hierarchical clustering based on a matrix of distances.
		/// </summary>
		/// <param name="distMatrix">The matrix of distances. It is lower triangular, excluding the diagonal.</param>
		/// <param name="linkage">Specifies the linkage for the clustering.</param>
		/// <param name="preserveOrder"></param>
		/// <param name="periodic"></param>
		/// <param name="nthreads"></param>
		/// <param name="progress"></param>
		/// <returns>An array of cluster nodes defining the resulting tree.</returns>
		public HierarchicalClusterNode[] TreeCluster(float[,] distMatrix, HierarchicalClusterLinkage linkage, bool preserveOrder, bool periodic, int nthreads,
			Action<int> progress){
			double avDist = CalcAverageDistance(distMatrix);
			switch (linkage){
				case HierarchicalClusterLinkage.Average:
					return preserveOrder
						? AverageLinkageClusterLinear(distMatrix, periodic)
						: AverageLinkageCluster(distMatrix, nthreads, avDist);
				case HierarchicalClusterLinkage.Maximum:
					return preserveOrder
						? MaximumLinkageClusterLinear(distMatrix, periodic)
						: MaximumLinkageCluster(distMatrix, nthreads, avDist);
				case HierarchicalClusterLinkage.Single:
					return preserveOrder
						? SingleLinkageClusterLinear(distMatrix, periodic)
						: SingleLinkageCluster(distMatrix, nthreads, avDist);
				default:
					throw new NotImplementedException($"Linkage method {linkage} not implemented");
			}
		}
		private static double CalcAverageDistance(float[,] distMatrix){
			double result = 0;
			double count = 0;
			for (int i = 0; i < distMatrix.GetLength(0); i++){
				for (int j = 0; j < i; j++){
					float x = distMatrix[i, j];
					if (!float.IsNaN(x) && !float.IsInfinity(x)){
						result += x;
						count++;
					}
				}
			}
			return result/count;
		}
		private static HierarchicalClusterNode[] AverageLinkageClusterLinear(float[,] matrix, bool periodic){
			int nelements = matrix.GetLength(0);
			int[] clusterid = new int[nelements];
			int[] number = new int[nelements];
			int[] position = new int[nelements];
			HierarchicalClusterNode[] result = ArrayUtils.FillArray(i => new HierarchicalClusterNode(), nelements - 1);
			for (int j = 0; j < nelements; j++){
				number[j] = 1;
				clusterid[j] = j;
				position[j] = j;
			}
			for (int n = nelements; n > 1; n--){
				int i1;
				int j1;
				bool reverse;
				bool carryOver;
				result[nelements - n].distance = FindClosestPairLinear(n, matrix, out i1, out j1, position, periodic, out reverse,
					out carryOver);
				result[nelements - n].left = reverse ? clusterid[j1] : clusterid[i1];
				result[nelements - n].right = reverse ? clusterid[i1] : clusterid[j1];
				for (int j = 0; j < j1; j++){
					matrix[j1, j] = Av(matrix[i1, j], number[i1], matrix[j1, j], number[j1]);
				}
				for (int j = j1 + 1; j < i1; j++){
					matrix[j, j1] = Av(matrix[i1, j], number[i1], matrix[j, j1], number[j1]);
				}
				for (int j = i1 + 1; j < n; j++){
					matrix[j, j1] = Av(matrix[j, i1], number[i1], matrix[j, j1], number[j1]);
				}
				for (int j = 0; j < i1; j++){
					matrix[i1, j] = matrix[n - 1, j];
				}
				for (int j = i1 + 1; j < n - 1; j++){
					matrix[j, i1] = matrix[n - 1, j];
				}
				number[j1] = number[i1] + number[j1];
				number[i1] = number[n - 1];
				clusterid[j1] = n - nelements - 1;
				clusterid[i1] = clusterid[n - 1];
				position[j1] = Math.Min(position[j1], position[i1]);
				position[i1] = position[n - 1];
				if (!carryOver){
					for (int i = 0; i < position.Length; i++){
						if (position[i] > position[j1]){
							position[i]--;
						}
					}
				}
			}
			return result;
		}
		private static HierarchicalClusterNode[] MaximumLinkageClusterLinear(float[,] matrix, bool periodic){
			int nelements = matrix.GetLength(0);
			int[] clusterid = new int[nelements];
			int[] position = new int[nelements];
			HierarchicalClusterNode[] result = ArrayUtils.FillArray(i => new HierarchicalClusterNode(), nelements - 1);
			for (int j = 0; j < nelements; j++){
				clusterid[j] = j;
				position[j] = j;
			}
			for (int n = nelements; n > 1; n--){
				int iss;
				int jss;
				bool reverse;
				bool carryOver;
				result[nelements - n].distance = FindClosestPairLinear(n, matrix, out iss, out jss, position, periodic, out reverse,
					out carryOver);
				for (int j = 0; j < jss; j++){
					matrix[jss, j] = Max(matrix[iss, j], matrix[jss, j]);
				}
				for (int j = jss + 1; j < iss; j++){
					matrix[j, jss] = Max(matrix[iss, j], matrix[j, jss]);
				}
				for (int j = iss + 1; j < n; j++){
					matrix[j, jss] = Max(matrix[j, iss], matrix[j, jss]);
				}
				for (int j = 0; j < iss; j++){
					matrix[iss, j] = matrix[n - 1, j];
				}
				for (int j = iss + 1; j < n - 1; j++){
					matrix[j, iss] = matrix[n - 1, j];
				}
				result[nelements - n].left = reverse ? clusterid[jss] : clusterid[iss];
				result[nelements - n].right = reverse ? clusterid[iss] : clusterid[jss];
				clusterid[jss] = n - nelements - 1;
				clusterid[iss] = clusterid[n - 1];
				position[jss] = Math.Min(position[jss], position[iss]);
				position[iss] = position[n - 1];
				if (!carryOver){
					for (int i = 0; i < position.Length; i++){
						if (position[i] > position[jss]){
							position[i]--;
						}
					}
				}
			}
			return result;
		}
		private static HierarchicalClusterNode[] SingleLinkageClusterLinear(float[,] matrix, bool periodic){
			int nelements = matrix.GetLength(0);
			int[] clusterid = new int[nelements];
			int[] position = new int[nelements];
			HierarchicalClusterNode[] result = ArrayUtils.FillArray(i => new HierarchicalClusterNode(), nelements - 1);
			for (int j = 0; j < nelements; j++){
				clusterid[j] = j;
				position[j] = j;
			}
			for (int n = nelements; n > 1; n--){
				int iss;
				int jss;
				bool reverse;
				bool carryOver;
				result[nelements - n].distance = FindClosestPairLinear(n, matrix, out iss, out jss, position, periodic, out reverse,
					out carryOver);
				for (int j = 0; j < jss; j++){
					matrix[jss, j] = Min(matrix[iss, j], matrix[jss, j]);
				}
				for (int j = jss + 1; j < iss; j++){
					matrix[j, jss] = Min(matrix[iss, j], matrix[j, jss]);
				}
				for (int j = iss + 1; j < n; j++){
					matrix[j, jss] = Min(matrix[j, iss], matrix[j, jss]);
				}
				for (int j = 0; j < iss; j++){
					matrix[iss, j] = matrix[n - 1, j];
				}
				for (int j = iss + 1; j < n - 1; j++){
					matrix[j, iss] = matrix[n - 1, j];
				}
				result[nelements - n].left = reverse ? clusterid[jss] : clusterid[iss];
				result[nelements - n].right = reverse ? clusterid[iss] : clusterid[jss];
				clusterid[jss] = n - nelements - 1;
				clusterid[iss] = clusterid[n - 1];
				position[jss] = Math.Min(position[jss], position[iss]);
				position[iss] = position[n - 1];
				if (!carryOver){
					for (int i = 0; i < position.Length; i++){
						if (position[i] > position[jss]){
							position[i]--;
						}
					}
				}
			}
			return result;
		}
		private HierarchicalClusterNode[] SingleLinkageCluster(float[,] distMatrix, int nthreads, double defaultDist){
			int nelements = distMatrix.GetLength(0);
			int[] clusterid = new int[nelements];
			HierarchicalClusterNode[] result = ArrayUtils.FillArray(i => new HierarchicalClusterNode(), nelements - 1);
			for (int j = 0; j < nelements; j++){
				clusterid[j] = j;
			}
			for (int n = nelements; n > 1; n--){
				int iss;
				int jss;
				result[nelements - n].distance = FindClosestPair(n, distMatrix, out iss, out jss, nthreads, defaultDist);
				for (int j = 0; j < jss; j++){
					distMatrix[jss, j] = Min(distMatrix[iss, j], distMatrix[jss, j]);
				}
				for (int j = jss + 1; j < iss; j++){
					distMatrix[j, jss] = Min(distMatrix[iss, j], distMatrix[j, jss]);
				}
				for (int j = iss + 1; j < n; j++){
					distMatrix[j, jss] = Min(distMatrix[j, iss], distMatrix[j, jss]);
				}
				for (int j = 0; j < iss; j++){
					distMatrix[iss, j] = distMatrix[n - 1, j];
				}
				for (int j = iss + 1; j < n - 1; j++){
					distMatrix[j, iss] = distMatrix[n - 1, j];
				}
				result[nelements - n].left = clusterid[iss];
				result[nelements - n].right = clusterid[jss];
				clusterid[jss] = n - nelements - 1;
				clusterid[iss] = clusterid[n - 1];
			}
			return result;
		}
		private HierarchicalClusterNode[] MaximumLinkageCluster(float[,] distMatrix, int nthreads, double defaultDist){
			int nelements = distMatrix.GetLength(0);
			int[] clusterid = new int[nelements];
			HierarchicalClusterNode[] result = ArrayUtils.FillArray(i => new HierarchicalClusterNode(), nelements - 1);
			for (int j = 0; j < nelements; j++){
				clusterid[j] = j;
			}
			for (int n = nelements; n > 1; n--){
				int iss;
				int jss;
				result[nelements - n].distance = FindClosestPair(n, distMatrix, out iss, out jss, nthreads, defaultDist);
				if (iss == -1){
					iss = 0;
					jss = 1;
				}
				for (int j = 0; j < jss; j++){
					distMatrix[jss, j] = Max(distMatrix[iss, j], distMatrix[jss, j]);
				}
				for (int j = jss + 1; j < iss; j++){
					distMatrix[j, jss] = Max(distMatrix[iss, j], distMatrix[j, jss]);
				}
				for (int j = iss + 1; j < n; j++){
					distMatrix[j, jss] = Max(distMatrix[j, iss], distMatrix[j, jss]);
				}
				for (int j = 0; j < iss; j++){
					distMatrix[iss, j] = distMatrix[n - 1, j];
				}
				for (int j = iss + 1; j < n - 1; j++){
					distMatrix[j, iss] = distMatrix[n - 1, j];
				}
				result[nelements - n].left = clusterid[iss];
				result[nelements - n].right = clusterid[jss];
				clusterid[jss] = n - nelements - 1;
				clusterid[iss] = clusterid[n - 1];
			}
			return result;
		}
		private static float Max(float m1, float m2){
			if (float.IsNaN(m1) || float.IsInfinity(m1)){
				return m2;
			}
			if (float.IsNaN(m2) || float.IsInfinity(m2)){
				return m1;
			}
			return Math.Max(m1, m2);
		}
		private static float Min(float m1, float m2){
			if (float.IsNaN(m1) || float.IsInfinity(m1)){
				return m2;
			}
			if (float.IsNaN(m2) || float.IsInfinity(m2)){
				return m1;
			}
			return Math.Min(m1, m2);
		}
		private HierarchicalClusterNode[] AverageLinkageCluster(float[,] distMatrix, int nthreads, double defaultDist){
			int nelements = distMatrix.GetLength(0);
			int[] clusterid = new int[nelements];
			int[] number = new int[nelements];
			HierarchicalClusterNode[] result = ArrayUtils.FillArray(i => new HierarchicalClusterNode(), nelements - 1);
			for (int j = 0; j < nelements; j++){
				number[j] = 1;
				clusterid[j] = j;
			}
			for (int n = nelements; n > 1; n--){
				int i1;
				int j1;
				double dist = FindClosestPair(n, distMatrix, out i1, out j1, nthreads, defaultDist);
				if (i1 != -1){
					result[nelements - n].distance = dist;
					result[nelements - n].left = clusterid[i1];
					result[nelements - n].right = clusterid[j1];
				} else{
					i1 = 1;
					j1 = 0;
					dist = nelements - n > 0 ? result[nelements - n - 1].distance*1.01 : 1;
					result[nelements - n].distance = dist;
					result[nelements - n].left = clusterid[i1];
					result[nelements - n].right = clusterid[j1];
				}
				for (int j = 0; j < j1; j++){
					distMatrix[j1, j] = Av(distMatrix[i1, j], number[i1], distMatrix[j1, j], number[j1]);
				}
				for (int j = j1 + 1; j < i1; j++){
					distMatrix[j, j1] = Av(distMatrix[i1, j], number[i1], distMatrix[j, j1], number[j1]);
				}
				for (int j = i1 + 1; j < n; j++){
					distMatrix[j, j1] = Av(distMatrix[j, i1], number[i1], distMatrix[j, j1], number[j1]);
				}
				for (int j = 0; j < i1; j++){
					distMatrix[i1, j] = distMatrix[n - 1, j];
				}
				for (int j = i1 + 1; j < n - 1; j++){
					distMatrix[j, i1] = distMatrix[n - 1, j];
				}
				number[j1] = number[i1] + number[j1];
				number[i1] = number[n - 1];
				clusterid[j1] = n - nelements - 1;
				clusterid[i1] = clusterid[n - 1];
			}
			return result;
		}
		private static float Av(float m1, int n1, float m2, int n2){
			if (float.IsNaN(m1) || float.IsInfinity(m1)){
				return m2;
			}
			if (float.IsNaN(m2) || float.IsInfinity(m2)){
				return m1;
			}
			return (m1*n1 + m2*n2)/(n1 + n2);
		}
		private int[] ips;
		private int[] jps;
		private double[] maxs;
		private double FindClosestPair(int n, float[,] distMatrix, out int ip, out int jp, int nthreads, double defaultDist){
			if (nthreads <= 1 || n <= 1000){
				return FindClosestPair(0, n, distMatrix, out ip, out jp, defaultDist);
			}
			int[] nk = new int[nthreads + 1];
			for (int k = 0; k < nthreads + 1; k++){
				nk[k] = (int) Math.Round(0.5 + Math.Sqrt(0.25 + n*(n - 1)*k/(double) nthreads));
			}
			ips = new int[nthreads];
			jps = new int[nthreads];
			maxs = new double[nthreads];
			Thread[] t = new Thread[nthreads];
			for (int i = 0; i < nthreads; i++){
				int index0 = i;
				t[i] =
					new Thread(
						new ThreadStart(
							delegate{
								maxs[index0] = FindClosestPair(nk[index0], nk[index0 + 1], distMatrix, out ips[index0], out jps[index0],
									defaultDist);
							}));
				t[i].Start();
			}
			for (int i = 0; i < nthreads; i++){
				t[i].Join();
			}
			ip = -1;
			jp = -1;
			double distance = double.MaxValue;
			for (int i = 0; i < nthreads; i++){
				if (maxs[i] < distance){
					distance = maxs[i];
					ip = ips[i];
					jp = jps[i];
				}
			}
			if (distance == double.MaxValue){
				return defaultDist;
			}
			return distance;
		}
		private static double FindClosestPair(int nmin, int nmax, float[,] matrix, out int ip, out int jp, double defaultDist){
			ip = -1;
			jp = -1;
			double distance = double.MaxValue;
			for (int i = nmin; i < nmax; i++){
				for (int j = 0; j < i; j++){
					if (matrix[i, j] < distance){
						distance = matrix[i, j];
						ip = i;
						jp = j;
					}
				}
			}
			if (distance == double.MaxValue){
				return defaultDist;
			}
			return distance;
		}
		private static double FindClosestPairLinear(int n, float[,] matrix, out int ip, out int jp, IList<int> position,
			bool periodic, out bool reverse, out bool carryOver){
			ip = -1;
			jp = -1;
			reverse = false;
			carryOver = false;
			double distance = double.MaxValue;
			for (int i = 0; i < n; i++){
				for (int j = 0; j < i; j++){
					bool p;
					bool c;
					if (ValidPositions(position[i], position[j], n, periodic, out p, out c)){
						if (matrix[i, j] < distance){
							distance = matrix[i, j];
							ip = i;
							jp = j;
							reverse = p;
							carryOver = c;
						}
					}
				}
			}
			for (int i = 0; i < n; i++){
				for (int j = 0; j < i; j++){
					if (matrix[i, j] < distance){
						matrix[i, j] = (float) distance;
					}
				}
			}
			return distance;
		}
		private static bool ValidPositions(int pos1, int pos2, int n, bool periodic, out bool reverse, out bool carryOver){
			if (Math.Abs(pos1 - pos2) == 1){
				reverse = pos1 > pos2;
				carryOver = false;
				return true;
			}
			if (periodic){
				if (pos1 == 0 && pos2 == n - 1){
					reverse = true;
					carryOver = true;
					return true;
				}
				if (pos1 == n - 1 && pos2 == 0){
					reverse = false;
					carryOver = true;
					return true;
				}
			}
			reverse = false;
			carryOver = false;
			return false;
		}
		private static float[,] DistanceMatrix(MatrixIndexer data, IDistance distance, MatrixAccess access){
			int nrows = data.RowCount;
			int ncols = data.ColumnCount;
			int nelements = (access == MatrixAccess.Rows) ? nrows : ncols;
			float[,] result = new float[nelements, nelements];
			for (int i = 0; i < nelements; i++){
				for (int j = 0; j < i; j++){
					result[i, j] = (float) distance.Get(GetVector(data, i, access), GetVector(data, j, access));
				}
			}
			return result;
		}
		private static BaseVector GetVector(MatrixIndexer data, int index, MatrixAccess access){
			if (access == MatrixAccess.Rows){
				return data.GetRow(index);
			}
			return data.GetColumn(index);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="nodes">The cluster nodes serve as input here.</param>
		/// <param name="sizes"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="itemOrder"></param>
		/// <param name="itemOrderInv"></param>
		public static void CalcTree(HierarchicalClusterNode[] nodes, out int[] sizes, out int[] start, out int[] end, out int[] itemOrder,
			out int[] itemOrderInv){
			if (nodes == null){
				sizes = null;
				start = null;
				end = null;
				itemOrder = null;
				itemOrderInv = null;
				return;
			}
			sizes = new int[nodes.Length];
			start = new int[nodes.Length];
			end = new int[nodes.Length];
			itemOrder = new int[nodes.Length + 1];
			int count = 0;
			CalcItemOrder(nodes, itemOrder, nodes.Length - 1, ref count);
			CalcSizes(nodes, sizes, nodes.Length - 1);
			CalcStartEnd(nodes, sizes, start, end, nodes.Length - 1, 0, nodes.Length + 1);
			itemOrderInv = InvertOrder(itemOrder);
		}
		private static int[] InvertOrder(IList<int> order){
			int[] inv = new int[order.Count];
			for (int i = 0; i < order.Count; i++){
				inv[order[i]] = i;
			}
			return inv;
		}

		//TODO: do this without recusions
		private static void CalcStartEnd(IList<HierarchicalClusterNode> nodes, IList<int> sizes, IList<int> start, IList<int> end, int i,
			int s, int e){
			if (i == -1){
				return;
			}
			start[i] = s;
			end[i] = e;
			HierarchicalClusterNode node = nodes[i];
			int size = sizes[i];
			int leftSize = node.left >= 0 ? 1 : sizes[-1 - node.left];
			int rightSize = size - leftSize;
			if (leftSize > 1){
				CalcStartEnd(nodes, sizes, start, end, -1 - node.left, s, s + leftSize);
			}
			if (rightSize > 1){
				CalcStartEnd(nodes, sizes, start, end, -1 - node.right, s + leftSize, e);
			}
		}

		//TODO: do this without recusions
		private static int CalcSizes(IList<HierarchicalClusterNode> nodes, IList<int> sizes, int i){
			if (i < 0 || i >= nodes.Count){
				return 0;
			}
			HierarchicalClusterNode node = nodes[i];
			int leftSize = node.left >= 0 ? 1 : CalcSizes(nodes, sizes, -node.left - 1);
			int rightSize = node.right >= 0 ? 1 : CalcSizes(nodes, sizes, -node.right - 1);
			sizes[i] = leftSize + rightSize;
			return leftSize + rightSize;
		}

		//TODO: do this without recusions
		private static void CalcItemOrder(IList<HierarchicalClusterNode> nodes, IList<int> itemOrder, int i, ref int count){
			if (i < 0 || i >= nodes.Count){
				return;
			}
			if (nodes[i].left >= 0){
				itemOrder[nodes[i].left] = count++;
			} else{
				CalcItemOrder(nodes, itemOrder, -1 - nodes[i].left, ref count);
			}
			if (nodes[i].right >= 0){
				itemOrder[nodes[i].right] = count++;
			} else{
				CalcItemOrder(nodes, itemOrder, -1 - nodes[i].right, ref count);
			}
		}
		public HierarchicalClusterNode[] TreeClusterKmeans(MatrixIndexer data, MatrixAccess access, IDistance distance, HierarchicalClusterLinkage linkage,
			bool preserveOrder, bool periodic, int nthreads, int nmeans, int restarts, int maxIter, Action<int> progress){
			int nelements = (access == MatrixAccess.Rows) ? data.RowCount : data.ColumnCount;
			if (nelements <= nmeans){
				return TreeCluster(data, access, distance, linkage, preserveOrder, periodic, nthreads, progress);
			}
			float[,] c;
			int[] inds;
			KmeansClustering.GenerateClusters(data, distance, nmeans, maxIter, restarts, progress, nthreads, out c, out inds);
			float[,] distMatrix = DistanceMatrix(new FloatMatrixIndexer(c), distance, MatrixAccess.Rows);
			HierarchicalClusterNode[] nodes = TreeCluster(distMatrix, linkage, preserveOrder, periodic, nthreads, progress);
			Dictionary<int, int[]> clusters;
			Dictionary<int, int> singletons;
			RearrangeClusters(inds, c.GetLength(0), out clusters, out singletons);
			HierarchicalClusterNode[] newNodes = new HierarchicalClusterNode[nelements - 1];
			int fill = nelements - nmeans;
			Array.Copy(nodes, 0, newNodes, fill, nodes.Length);
			int pos = 0;
			for (int i = fill; i < newNodes.Length; i++){
				HierarchicalClusterNode node = newNodes[i];
				if (node.left < 0){
					node.left -= fill;
				} else if (singletons.ContainsKey(node.left)){
					node.left = singletons[node.left];
				} else{
					if (clusters.ContainsKey(node.left)){
						HierarchicalClusterNode[] branch = FillTerminalBranch(clusters[node.left], pos);
						Array.Copy(branch, 0, newNodes, pos, branch.Length);
						pos += branch.Length;
						node.left = -pos;
					}
				}
				if (node.right < 0){
					node.right -= fill;
				} else if (singletons.ContainsKey(node.right)){
					node.right = singletons[node.right];
				} else{
					if (clusters.ContainsKey(node.right)){
						HierarchicalClusterNode[] branch = FillTerminalBranch(clusters[node.right], pos);
						Array.Copy(branch, 0, newNodes, pos, branch.Length);
						pos += branch.Length;
						node.right = -pos;
					}
				}
			}
			return newNodes;
		}
		private static HierarchicalClusterNode[] FillTerminalBranch(IList<int> inds, int firstInd){
			HierarchicalClusterNode[] result = new HierarchicalClusterNode[inds.Count - 1];
			result[0] = new HierarchicalClusterNode{left = inds[0], right = inds[1]};
			for (int i = 1; i < result.Length; i++){
				int nodeInd = firstInd + i - 1;
				result[i] = new HierarchicalClusterNode{left = inds[i + 1], right = -1 - nodeInd};
			}
			return result;
		}
		private static void RearrangeClusters(IList<int> inds, int nc, out Dictionary<int, int[]> clusters,
			out Dictionary<int, int> singletons){
			List<int>[] q = new List<int>[nc];
			for (int i = 0; i < nc; i++){
				q[i] = new List<int>();
			}
			for (int i = 0; i < inds.Count; i++){
				q[inds[i]].Add(i);
			}
			clusters = new Dictionary<int, int[]>();
			singletons = new Dictionary<int, int>();
			for (int i = 0; i < nc; i++){
				if (q[i].Count > 1){
					clusters.Add(i, q[i].ToArray());
				} else if (q[i].Count == 1){
					singletons.Add(i, q[i][0]);
				}
			}
		}
		public static int[] GetLeaves(int cl, IList<HierarchicalClusterNode> nodes){
			List<int> leaves = new List<int>();
			AddLeaves(cl, nodes, leaves);
			leaves.Sort();
			return leaves.ToArray();
		}
		private static void AddLeaves(int cl, IList<HierarchicalClusterNode> nodes, ICollection<int> leaves){
			if (cl >= 0){
				return;
			}
			if (nodes[-1 - cl].left >= 0){
				leaves.Add(nodes[-1 - cl].left);
			}
			if (nodes[-1 - cl].right >= 0){
				leaves.Add(nodes[-1 - cl].right);
			}
			AddLeaves(nodes[-1 - cl].left, nodes, leaves);
			AddLeaves(nodes[-1 - cl].right, nodes, leaves);
		}
	}
}