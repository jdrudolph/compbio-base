using System;
using BaseLibS.Param;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var f = new FileParam("Just a test");
            Assert.IsNotNull(f);
        }
    }
}
