using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting;
using BaseLibS.Api;
using BaseLibS.Num.Cluster;
using BaseLibS.Num.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumPluginBase.Distance;

namespace BaseLibS.Test
{
    [TestClass]
    public class ClusteringTest
    {
        [TestMethod]
        [DeploymentItem(@"Examples", "Examples")]
        public void TestClustering()
        {
            var hclust = new HierarchicalClustering();
            var vals = TestUtils.ReadMatrix("Examples/clustering_array_copy_error.txt.gz");
            var data = new FloatMatrixIndexer(vals);
            var distance = new EuclideanDistance();
            hclust.TreeClusterKmeans(data, MatrixAccess.Columns, distance, HierarchicalClusterLinkage.Average, false,
                false, 1, 300, 1, 10,
                (i) => { });
        }

        [TestMethod]
        public void TestClusterNodeFormat()
        {
             var hclust = new HierarchicalClustering();
            var data = new FloatMatrixIndexer(new float[,] { {1,2,3}, {2,3,4} });
            var distance = new EuclideanDistance();
            var rowTree = hclust.TreeCluster(data, MatrixAccess.Rows, distance, HierarchicalClusterLinkage.Maximum, false,
                false, 1, i => { });
            var rowTreeR = HierarchicalClusterNode.FromRFormat(new[] {-1}, new[] {-2}, new[] {1.732051});
            Assert.AreEqual(rowTreeR[0], rowTree[0]);

            var colTree = hclust.TreeCluster(data, MatrixAccess.Columns, distance, HierarchicalClusterLinkage.Maximum, false,
                false, 1, i => { });
            var colTreeR = HierarchicalClusterNode.FromRFormat(new[] {-1, -3}, new[] {-2, 1}, new[] {1.414214, 2.828428});
            CollectionAssert.AreEqual(colTree, colTreeR);
        }

    [TestMethod]
        public void TestKmedoidClustering()
        {
            var data = new FloatMatrixIndexer(new float[,]
            {
                {2, 6 }, {3, 4 }, {3, 8}, {4, 7}, {6, 2}, {6, 4}, {7, 3}, {7, 4}, {8, 5}, {7, 6}
            });
            var distance = new GenericDistanceMatrix(data, new L1Distance());
            var clustering = KmedoidClustering.GenerateClusters(data, distance, 2);
            Assert.IsTrue(new [] {0, 0, 0, 0, 7, 7, 7, 7, 7, 7}.SequenceEqual(clustering));
        }
    }
}
