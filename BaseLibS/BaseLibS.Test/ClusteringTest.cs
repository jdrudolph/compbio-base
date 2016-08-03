using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            var vals = Util.ReadMatrix("Examples/clustering_array_copy_error.txt.gz");
            var data = new FloatMatrixIndexer(vals);
            var distance = new EuclideanDistance();
            hclust.TreeClusterKmeans(data, MatrixAccess.Columns, distance, HierarchicalClusterLinkage.Average, false,
                false, 1, 300, 1, 10,
                (i) => { });
        }
    }
}
