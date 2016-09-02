using System;
using BaseLibS.Num.Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumPluginBase.Distance;

namespace NumPluginBase.Test
{
    [TestClass]
    public class DistanceMatrixTest
    {
        [TestMethod]
        public void TestGeneralDistanceMatrix()
        {
            var distance = new EuclideanDistance();
            var distanceMatrix = new GenericDistanceMatrix(new FloatMatrixIndexer(new float[,] { {0,0}, {1,0}, {3,0} }), distance);
            Assert.AreEqual(0, distanceMatrix[0,0]);
            Assert.AreEqual(0, distanceMatrix[2,2]);
            Assert.AreEqual(distanceMatrix[1,2], distanceMatrix[2,1]);
            Assert.AreEqual(1, distanceMatrix[1,0]);
            Assert.AreEqual(2, distanceMatrix[1,2]);
        }
    }
}
