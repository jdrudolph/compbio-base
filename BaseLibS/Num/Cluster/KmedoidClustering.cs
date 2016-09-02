using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using BaseLibS.Api;
using BaseLibS.Num.Matrix;

namespace BaseLibS.Num.Cluster
{
    public static class KmedoidClustering
    {
        /// <summary>
        /// Run K-medoid clustering.
        /// </summary>
        /// <param name="data">data matrix with n rows</param>
        /// <param name="distance"></param>
        /// <param name="k">number of clusters k &lt; n</param>
        /// <returns>Array of length n. <code>assignment[i]</code> returns the index of the cluster medoid in the data matrix.</returns>
        public static int[] GenerateClusters(MatrixIndexer data, IDistance distance, int k)
        {
            // return GenerateClusters(data, new GenericDistanceMatrix(data, distance), k); // TODO allow calling GenericDistanceMatrix without circular dependancy
            throw new NotImplementedException("Use GenericDistanceMatrix to convert IDistance to IDistanceMatrix");
        }

        /// <summary>
        /// Run K-medoid clustering.
        /// </summary>
        /// <param name="data">data matrix with n rows</param>
        /// <param name="distance"></param>
        /// <param name="k">number of clusters k &lt; n</param>
        /// <returns>Array of length n. <code>assignment[i]</code> returns the index of the cluster medoid in the data matrix.</returns>
        public static int[] GenerateClusters(MatrixIndexer data, IDistanceMatrix distance, int k)
        {
            var n = data.RowCount;
            var medoids = SelectInitialMedoids(distance, k, n);
            var assignments = AssignClusters(distance, medoids, n);
            var cost = CalculateCost(distance, assignments);
            while (true)
            {
                medoids = SelectMedoids(distance, assignments);
                assignments = AssignClusters(distance, medoids, n);
                var newCost = CalculateCost(distance, assignments);
                if (Math.Abs(newCost - cost) < 1E-10)
                {
                    break;
                }
                cost = newCost;
            }
            return assignments;
        }

        internal static int[] SelectMedoids(IDistanceMatrix distance, int[] assignments)
        {
            var medoids = new List<int>();
            var clusters = assignments.Select((x, i) => new {Value = x, Index = i})
                .GroupBy(obj => obj.Value)
                .Select(grp => grp.Select(obj => obj.Index).ToArray()).ToArray();
            foreach (var cluster in clusters)
            {
                var withinClusterDistance = double.PositiveInfinity;
                var medoid = 0;
                foreach (var newMedoid in cluster)
                {
                    var newWithinClusterDistance = 0.0;
                    foreach (var point in cluster)
                    {
                        newWithinClusterDistance += distance[newMedoid, point];
                    }
                    if (newWithinClusterDistance < withinClusterDistance)
                    {
                        withinClusterDistance = newWithinClusterDistance;
                        medoid = newMedoid;
                    }
                }
                medoids.Add(medoid);
            }
            return medoids.ToArray();
        }

        internal static double CalculateCost(IDistanceMatrix distance, int[] assignments)
        {
            var cost = 0.0;
            for (int i = 0; i < assignments.Length; i++)
            {
                cost += distance[i, assignments[i]];
            }
            return cost;
        }

        internal static int[] AssignClusters(IDistanceMatrix distance, int[] medoids, int n)
        {
            var assignments = new int[n];
            for (int point = 0; point < n; point++)
            {
                var d = double.PositiveInfinity;
                var assigned = 0;
                foreach (var medoid in medoids)
                {
                    var dNew = distance[point, medoid];
                    if (dNew < d)
                    {
                        d = dNew;
                        assigned = medoid;
                    }
                }
                assignments[point] = assigned;
            }
            return assignments;
        }

        internal static int[] SelectInitialMedoids(IDistanceMatrix distance, int k, int n)
        {
            var sumD = new double[n];
            for (int i = 0; i < n; i++)
            {
                var sumDil = 0.0;
                for (int l = 0; l < n; l++)
                {
                    sumDil += distance[i, l];
                }
                sumD[i] = sumDil;
            }
            var v = new double[n];
            for (int j = 0; j < n; j++)
            {
                var vj = 0.0;
                for (int i = 0; i < n; i++)
                {
                    vj += distance[i, j]/sumD[i];
                }
                v[j] = vj;
            }
            var idx = new int[n];
            for (int i = 0; i < n; i++)
            {
                idx[i] = i;
            }
            Array.Sort(v, idx);
            var medoids = new int[k];
            Array.Copy(idx, medoids, k);
            return medoids;
        }
    }
}
