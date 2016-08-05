using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using BaseLibS.Param;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseLibS.Test
{
    [TestClass]
    public class SerializationTest
    {
        [TestMethod]
        public void TestBoolParam()
        {
            var sparam = new BoolParam("myname", false);
            var sparam2 = (BoolParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
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

        [TestMethod]
        public void TestDoubleParam()
        {
            var sparam = new DoubleParam("myname", 42.0);
            var sparam2 = (DoubleParam) sparam.ToXmlAndBack();
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
        public void TestFolderParam()
        {
            var sparam = new FolderParam("myname", "myvalue");
            var sparam2 = (FolderParam) sparam.ToXmlAndBack();
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
        public void TestLabelParam()
        {
            var sparam = new LabelParam("myname", "myvalue");
            var sparam2 = (LabelParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
        }

        [TestMethod]
        public void TestMs1LabelParam()
        {
            var sparam = new Ms1LabelParam("myname", new [] { new [] {1,2,3}, new [] {3,4,5} }) { Values = new [] {"a", "b"}, Multiplicity = 12};
            var sparam2 = (Ms1LabelParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Name, sparam2.Name);
            Assert.AreEqual(sparam.Multiplicity, sparam2.Multiplicity);
            Assert.IsTrue(sparam.Value.SelectMany(x => x).SequenceEqual(sparam2.Value.SelectMany(x => x)));
            Assert.IsTrue(sparam.Values.SequenceEqual(sparam2.Values));
        }

        [TestMethod]
        public void TestMultiChoiceMultiBinParam()
        {
            var sparam = new MultiChoiceMultiBinParam("myname", new [] { new [] {1,2,3}, new [] {3,4,5} }) { Values = new [] {"a", "b"}, Bins = new [] {"c", "d"}};
            var sparam2 = (MultiChoiceMultiBinParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Name, sparam2.Name);
            Assert.IsTrue(sparam.Value.SelectMany(x => x).SequenceEqual(sparam2.Value.SelectMany(x => x)));
            Assert.IsTrue(sparam.Values.SequenceEqual(sparam2.Values));
            Assert.IsTrue(sparam.Bins.SequenceEqual(sparam2.Bins));
        }

        [TestMethod]
        public void TestMultiChoiceParam()
        {
            var sparam = new MultiChoiceParam("myname", new [] {1,2,3}) { Values = new [] {"a", "b"} , Repeats = true};
            var sparam2 = (MultiChoiceParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Name, sparam2.Name);
            Assert.AreEqual(sparam.Repeats, sparam2.Repeats);
            Assert.IsTrue(sparam.Value.SequenceEqual(sparam2.Value));
            Assert.IsTrue(sparam.Values.SequenceEqual(sparam2.Values));
        }

        [TestMethod]
        public void TestMultiFileParam()
        {
            var sparam = new MultiFileParam("myname", new [] {"file1", "file2"}) { Filter = "somefilter" };
            var sparam2 = (MultiFileParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Name, sparam2.Name);
            Assert.AreEqual(sparam.Filter, sparam2.Filter);
            Assert.IsTrue(sparam.Value.SequenceEqual(sparam2.Value));
        }

        [TestMethod]
        public void TestMultiStringParam()
        {
            var sparam = new MultiStringParam("myname", new[] {"file1", "file2"});
            var sparam2 = (MultiStringParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Name, sparam2.Name);
            Assert.IsTrue(sparam.Value.SequenceEqual(sparam2.Value));
        }

        [TestMethod]
        public void TestParameterGroup()
        {
            var sparam = new ParameterGroup(new Parameter[] {new IntParam("int", 42), new StringParam("string", "42") }, "myname", false);
            var writer = new StringWriter();
            var serializer = new XmlSerializer(sparam.GetType());
            serializer.Serialize(writer, sparam);
            var writer2 = new StringReader(writer.ToString());
            var sparam2 = (ParameterGroup) serializer.Deserialize(writer2);
            Assert.AreEqual(sparam.Name, sparam2.Name);
            Assert.AreEqual(42, ((IntParam) sparam[0]).Value);
            Assert.AreEqual("42", ((StringParam) sparam[1]).Value);
        }

        [TestMethod]
        public void TestParameters()
        {
            var parameters = new Parameters(new StringParam("myname", "myvalue"), new IntParam("some int", 42));
            var parameters2 = parameters.ToXmlAndBack();
            Assert.AreEqual("myvalue", parameters2.GetParam<string>("myname").Value);
            Assert.AreEqual(42, parameters.GetParam<int>("some int").Value);
        }

        [TestMethod]
        public void TestEmptyParameters()
        {
            var parameters = new Parameters();
            var parameters2 = parameters.ToXmlAndBack();
            Assert.IsNotNull(parameters2);
        }

        [TestMethod]
        public void TestGroupedParameters()
        {
            var parameters = new Parameters();
            parameters.AddParameterGroup(new Parameter[] {new StringParam("myname", "myvalue")}, "grp1", false);
            parameters.AddParameterGroup(new Parameter[] {new IntParam("some int", 42)}, "grp2", true);
            var parameters2 = parameters.ToXmlAndBack();
            Assert.AreEqual("myvalue", parameters2.GetSubGroupAt(0).GetParam<string>("myname").Value);
            Assert.AreEqual(42, parameters.GetSubGroupAt(1).GetParam<int>("some int").Value);
            Assert.AreEqual("myvalue", parameters2.GetParam<string>("myname").Value);
        }

        [TestMethod]
        public void TestRegexMatchParam()
        {
            var parameter = new RegexMatchParam("myname", new Regex(".*"), new List<string>() {"a", "b" });
            var parameter2 = (RegexMatchParam) parameter.ToXmlAndBack();
            Assert.AreEqual(parameter.Name, parameter2.Name);
            Assert.AreEqual(parameter.Value.ToString(), parameter2.Value.ToString());
            Assert.IsTrue(parameter.Previews.SequenceEqual(parameter2.Previews));
        }

        [TestMethod]
        public void TestRegexReplaceParam()
        {
            var parameter = new RegexReplaceParam("myname", new Regex(".*"), "asdf", new List<string>() {"a", "b" });
            var parameter2 = (RegexReplaceParam) parameter.ToXmlAndBack();
            Assert.AreEqual(parameter.Name, parameter2.Name);
            Assert.AreEqual(parameter.Value.Item1.ToString(), parameter2.Value.Item1.ToString());
            Assert.AreEqual(parameter.Value.Item2, parameter2.Value.Item2);
            Assert.IsTrue(parameter.Previews.SequenceEqual(parameter2.Previews));
        }

        [TestMethod]
        public void TestRegexReplaceParamNoPreview()
        {
            var parameter = new RegexReplaceParam("myname", new Regex(".*"), "asdf", new List<string>());
            var parameter2 = (RegexReplaceParam) parameter.ToXmlAndBack();
            Assert.AreEqual(parameter.Name, parameter2.Name);
            Assert.AreEqual(parameter.Value.Item1.ToString(), parameter2.Value.Item1.ToString());
            Assert.AreEqual(parameter.Value.Item2, parameter2.Value.Item2);
            Assert.IsTrue(parameter.Previews.SequenceEqual(parameter2.Previews));
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
        public void TestSingleChoiceWithSubParamsInParameters()
        {
            var param = new SingleChoiceWithSubParams("choice", 1) { Values = new List<string> {"c1", "b2"}, SubParams = new []
            {
                new Parameters(),
                new Parameters(new IntParam("sub2", 82)) 
            }};
            var param2 = (SingleChoiceWithSubParams) param.ToXmlAndBack();
            Assert.AreEqual(param.Name, param2.Name);
            Assert.AreEqual(param.Value, param2.Value);
            Assert.IsTrue(param.Values.SequenceEqual(param2.Values));
            Assert.IsNotNull(param2.SubParams[0]);
            Assert.AreEqual(82, param2.SubParams[1].GetParam<int>("sub2").Value );
        }

        [TestMethod]
        public void TestStringParam()
        {
            var sparam = new StringParam("myname", "myvalue");
            var sparam2 = (StringParam) sparam.ToXmlAndBack();
            Assert.AreEqual(sparam.Value, sparam2.Value);
            Assert.AreEqual(sparam.Name, sparam2.Name);
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
