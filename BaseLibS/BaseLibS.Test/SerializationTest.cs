using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Xml.Serialization;
using BaseLibS.Param;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseLibS.Test
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void TestLabelParam()
        {
            var sparam = new LabelParam("myname", "myvalue");
            var sparam2 = (LabelParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestStringParam()
        {
            var sparam = new StringParam("myname", "myvalue");
            var sparam2 = (StringParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestFileParam()
        {
            var sparam = new FileParam("myname", "myvalue");
            var sparam2 = (FileParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestIntParam()
        {
            var sparam = new IntParam("myname", 42);
            var sparam2 = (IntParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestDoubleParam()
        {
            var sparam = new DoubleParam("myname", 42.0);
            var sparam2 = (DoubleParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestBoolParam()
        {
            var sparam = new BoolParam("myname", false);
            var sparam2 = (BoolParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestSingleChoiceParam()
        {
            var param = new SingleChoiceParam("choice", 1) { Values = new List<string> {"c1", "b2"} };
            var param2 = (SingleChoiceParam) param.ToXmlAndBack();
            Assert.AreEqual(param.Name, param2.Name);
            Assert.AreEqual(param.Value, param2.Value);
            Assert.IsTrue(param.Values.SequenceEqual(param2.Values));
        }

        [TestMethod]
        public void TestSingleChoiceWithSubParams()
        {
            var param = new SingleChoiceWithSubParams("choice", 1) { Values = new List<string> {"c1", "b2"}, SubParams = new []
            {
                new Parameters(new IntParam("sub1", 42)),
                new Parameters(new IntParam("sub2", 82)) 
            }};
            var param2 = (SingleChoiceWithSubParams) param.ToXmlAndBack();
            Assert.AreEqual(param.Name, param2.Name);
            Assert.AreEqual(param.Value, param2.Value);
            Assert.IsTrue(param.Values.SequenceEqual(param2.Values));
            Assert.AreEqual(42, param2.SubParams[0].GetParam<int>("sub1").Value );
            Assert.AreEqual(82, param2.SubParams[1].GetParam<int>("sub2").Value );
        }

        [TestMethod]
        public void ParametersTest()
        {
            var parameters = new Parameters(new StringParam("myname", "myvalue"), new IntParam("some int", 42));
            var parameters2 = parameters.ToXmlAndBack();
            Assert.AreEqual("myvalue", parameters2.GetParam<string>("myname").Value);
            Assert.AreEqual(42, parameters.GetParam<int>("some int").Value);
        }

        [TestMethod]
        public void TestBoolWithSubParams()
        {
            var param = new BoolWithSubParams("bws", true)
            {
                SubParamsFalse = new Parameters(new IntParam("false", 42)),
                SubParamsTrue = new Parameters(new BoolParam("true", false))
            };
            var param2 = (BoolWithSubParams) param.ToXmlAndBack();
            Assert.AreEqual(param.Name, param2.Name);
            Assert.AreEqual(param.Value, param2.Value);
            Assert.AreEqual(42, param2.SubParamsFalse.GetParam<int>("false").Value);
            Assert.AreEqual(false, param2.SubParamsTrue.GetParam<bool>("true").Value);

        }

        [TestMethod]
        public void TestDictionaryIntValueParam()
        {
            var param = new DictionaryIntValueParam("dict", new Dictionary<string, int>() { {"a", 1}, {"b", 2} }, new []{"a", "b"});
            var param2 = (DictionaryIntValueParam) param.ToXmlAndBack();
            Assert.AreEqual(param.Name, param2.Name);
            Assert.IsTrue(param.Value.Keys.SequenceEqual(param2.Value.Keys));
            Assert.IsTrue(param.Value.Values.SequenceEqual(param2.Value.Values));
            Assert.IsTrue(param.Keys.SequenceEqual(param2.Keys));
        }
    }
    public static class ParameterExtensions
    {
        public static Parameters ToXmlAndBack(this Parameters p)
        {
            var writer = new StringWriter();
            var serializer = new XmlSerializer(p.GetType());
            serializer.Serialize(writer, p);
            var writer2 = new StringReader(writer.ToString());
            return (Parameters) serializer.Deserialize(writer2);
        }
        public static Parameter ToXmlAndBack(this Parameter p)
        {
            var writer = new StringWriter();
            var serializer = new XmlSerializer(p.GetType());
            serializer.Serialize(writer, p);
            var writer2 = new StringReader(writer.ToString());
            return (Parameter) serializer.Deserialize(writer2);
        }
        public static string ToXml(this Parameters p)
        {
            var writer = new StringWriter();
            var serializer = new XmlSerializer(p.GetType());
            serializer.Serialize(writer, p);
            return writer.ToString();
        }
        public static string ToXml(this Parameter p)
        {
            var writer = new StringWriter();
            var serializer = new XmlSerializer(p.GetType());
            serializer.Serialize(writer, p);
            return writer.ToString();
        }

    }
}
