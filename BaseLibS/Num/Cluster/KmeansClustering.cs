using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLibS.Api;
using BaseLibS.Data;
using BaseLibS.Num.Matrix;

namespace BaseLibS.Num.Cluster{
	public class KmeansClustering{
		private static readonly Random2 randy = new Random2(7);
		/// <summary>
		/// Run k-means clustering
		/// </summary>
		/// <param name="data"></param>
		/// <param name="k">number of clusters</param>
		/// <param name="maxIter">maximal number of iterations, if not converging</param>
		/// <param name="restarts"></param>
		/// <param name="nThreads"></param>
		/// <param name="clusterCenters"></param>
		/// <param name="clusterIndices"></param>
		public static void GenerateClusters(MatrixIndexer data, IDistance distance, int k, int maxIter, int restarts,
			Action<int> progress, int nThreads, out float[,] clusterCenters, out int[] clusterIndices){
			Dictionary<EquatableArray<double>, List<int>> rowIndexMap;
			double[][] reducedData;
			ExtractUniqueRows(data, out rowIndexMap, out reducedData);
			int[] uniqueClusterIndices;
			GenerateClustersImpl(reducedData, distance, k, maxIter, restarts, progress, nThreads, out clusterCenters,
				out uniqueClusterIndices);
			clusterIndices = new int[data.RowCount];
			RestoreRowIndices(rowIndexMap, uniqueClusterIndices, clusterIndices);
		}
		private static void RestoreRowIndices(Dictionary<EquatableArray<double>, List<int>> rowIndexMap,
			int[] uniqueClusterIndices, int[] clusterIndices){
			List<int>[] duplicates = rowIndexMap.Values.ToArray();
			for (int newIndex = 0; newIndex < uniqueClusterIndices.Length; newIndex++){
				List<int> duplicate = duplicates[newIndex];
				foreach (int oldIndex in duplicate){
					clusterIndices[oldIndex] = uniqueClusterIndices[newIndex];
				}
			}
		}
		private static void ExtractUniqueRows(MatrixIndexer data,
			out Dictionary<EquatableArray<double>, List<int>> rowIndexMap, out double[][] reducedData){
			List<double[]> uniqueRows = new List<double[]>();
			rowIndexMap = new Dictionary<EquatableArray<double>, List<int>>();
			for (int row = 0; row < data.RowCount; row++){
				var rowArray = data.GetRow(row).ToArray();
				var rowEqArray = new EquatableArray<double>(rowArray);
				if (!rowIndexMap.ContainsKey(rowEqArray)){
					rowIndexMap.Add(rowEqArray, new List<int>());
					uniqueRows.Add(rowArray);
				}
				rowIndexMap[rowEqArray].Add(row);
			}
			reducedData = uniqueRows.ToArray();
		}
		public static void GenerateClustersImpl(double[][] data, IDistance distance, int k, int maxIter, int restarts,
			Action<int> progress, int nThreads, out float[,] clusterCenters, out int[] clusterIndices){
			int npoints = data.Length;
			int nvars = data.First().Length;
			if ((k < 1) || restarts < 1){
				throw new Exception("Invalid cluster parameters.");
			}
			if (npoints <= k){
				clusterCenters = new float[npoints, nvars];
				for (int i = 0; i < npoints; i++){
					var rowData = data[i];
					for (int j = 0; j < nvars; j++){
						clusterCenters[i, j] = (float) rowData[j];
					}
				}
				clusterIndices = ArrayUtils.ConsecutiveInts(npoints);
				return;
			}
			if (nvars < 1){
				clusterCenters = new float[k, 0];
				clusterIndices = ArrayUtils.ConsecutiveInts(k);
				return;
			}
			// Multiple passes of k-means algorithm
			var bestPass = Enumerable.Range(0, restarts).AsParallel().Select(pass =>{
				double[][] ct = SelectInitialCenters(data, npoints, nvars, k);
				int[] localClusterIndices = UpdateCenterPositions(data, distance, k, maxIter, restarts, progress, nThreads, npoints,
					pass, nvars, ct);
				double e = CalculateE(data, distance, localClusterIndices, npoints, ct);
				return new{ct, localClusterIndices, e};
			}).Aggregate((best, current) => best.e < current.e ? best : current);
			clusterCenters = new float[k, nvars];
			for (int i = 0; i < k; i++){
				for (int j = 0; j < nvars; j++){
					clusterCenters[i, j] = (float) bestPass.ct[i][j];
				}
			}
			clusterIndices = bestPass.localClusterIndices;
			progress(100);
		}
		private static double CalculateE(double[][] data, IDistance distance, int[] clusterIndices, int npoints, double[][] ct){
			return Enumerable.Range(0, npoints).Select(i => distance.Get(data[i], ct[clusterIndices[i]])).Sum();
		}
		private static int[] UpdateCenterPositions(double[][] data, IDistance distance, int k, int maxIter, int restarts,
			Action<int> progress, int nThreads, int npoints, int pass, int nvars, double[][] ct){
			bool[] cbusy = Enumerable.Repeat(false, k).ToArray();
			int[] clusterIndices = Enumerable.Repeat(-1, npoints).ToArray();
			for (int iter = 0; iter < maxIter; iter++){
				progress(100*pass/restarts*(1 + iter/maxIter));
				// assign items to clusters
				bool wereChanges = false;
				Parallel.For(0, npoints, new ParallelOptions(){MaxDegreeOfParallelism = nThreads}, i =>{
					var rowData = data[i];
					var dMin = double.MaxValue;
					var cclosest = -1;
					for (int j = 0; j < k; j++){
						var cluster = ct[j];
						var d = distance.Get(cluster, rowData);
						if (d < dMin){
							dMin = d;
							cclosest = j;
							clusterIndices[i] = cclosest;
						}
					}
					if (clusterIndices[i] != cclosest){
						wereChanges = true;
					}
					clusterIndices[i] = cclosest;
				});
				// Update centers
				int[] csizes = new int[k];
				for (int i = 0; i < k; i++){
					var cRow = new double[nvars];
					for (int j = 0; j < nvars; j++){
						cRow[j] = 0;
					}
					ct[i] = cRow;
				}
				int[,] counts = new int[k, nvars];
				for (int i = 0; i < npoints; i++){
					var rowData = data[i];
					int cind = clusterIndices[i];
					if (cind < 0){
						continue;
					}
					csizes[cind] = csizes[cind] + 1;
					for (int l = 0; l < nvars; l++){
						if (!double.IsNaN(rowData[l])){
							ct[cind][l] = ct[cind][l] + rowData[l];
							counts[cind, l]++;
						}
					}
				}
				for (int j = 0; j < k; j++){
					for (int l = 0; l < nvars; l++){
						ct[j][l] = counts[j, l] > 0 ? ct[j][l]/counts[j, l] : double.NaN;
					}
				}
				bool zerosizeclusters = false;
				for (int i = 0; i < k; i++){
					cbusy[i] = csizes[i] != 0;
					zerosizeclusters = zerosizeclusters || csizes[i] == 0;
				}
				if (zerosizeclusters){
					// Some clusters may have zero size.
					// New centers are chosen for such clusters and restart algorithm is restarted.
					SelectCenter(data, npoints, nvars, ct, cbusy, k);
					continue;
				}
				// if nothing has changed during iteration
				if (!wereChanges){
					break;
				}
			}
			return clusterIndices;
		}
		private static double[][] SelectInitialCenters(double[][] xy, int npoints, int nvars, int k){
			double[][] centers = Enumerable.Range(0, k).Select(i => new double[nvars]).ToArray();
			bool[] busycenters = Enumerable.Repeat(false, k).ToArray();
			return SelectCenter(xy, npoints, nvars, centers, busycenters, k);
		}
		private static double[][] SelectCenter(double[][] xy, int npoints, int nvars, double[][] centers, bool[] busycenters,
			int k){
			int[] perm = randy.NextPermutation(npoints);
			for (int cc = 0; cc < k; cc++){
				double[] row = xy[perm[cc]];
				if (!busycenters[cc]){
					for (int i = 0; i < nvars; i++){
						centers[cc][i] = row[i];
					}
				}
			}
			return centers;
		}
	}
}