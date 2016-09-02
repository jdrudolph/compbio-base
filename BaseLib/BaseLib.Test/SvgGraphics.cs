using System;
using System.IO;
using System.Text;
using BaseLibS.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseLib.Test
{
    [TestClass]
    public class SvgGraphics
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stream = new MemoryStream();
            var svg = new BaseLib.Graphic.SvgGraphics("D:\\test.svg", 100, 100);
            svg.DrawLine(new Pen2(Color2.Aqua), 0f, 0f, 1f, 1f);
            svg.Close();
            var x = stream.ToArray();
            var y = Encoding.UTF8.GetString(x);
        }
    }
}
