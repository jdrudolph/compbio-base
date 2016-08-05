using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BaseLibS.Param{
	[Serializable]
	public class RegexMatchParam : Parameter<Regex>
    {
        /// <summary>
        /// for xml serialization only
        /// </summary>
	    private RegexMatchParam() : this("", new Regex(".*"), new List<string>()) { }

	    public List<string> Previews { get; set; }

	    public RegexMatchParam(string name, Regex value, List<string> previews ) : base(name)
	    {
			Value = value;
		    Default = value;
	        Previews = previews;
	    }

	    public override void Clear()
	    {
	        Value = Default;
            Previews = new List<string>();
	    }
        public override ParamType Type => ParamType.Server;

        public override string StringValue
        {
            get { return Value.ToString(); }
            set { throw new NotImplementedException($"Setting string value for {typeof(RegexReplaceParam)} not implemented"); }
        }

	    public override void ReadXml(XmlReader reader)
	    {
            ReadBasicAttributes(reader);
            reader.ReadStartElement();
            Value = new Regex(reader.ReadElementContentAsString());
	        reader.ReadInto(Previews);
            reader.ReadEndElement();
	    }

	    public override void WriteXml(XmlWriter writer)
	    {
	        WriteBasicAttributes(writer);
            writer.WriteStartElement("Value");
            writer.WriteValue(Value.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Previews");
	        foreach (var preview in Previews)
	        {
	            writer.WriteElementString("Preview", preview);
	        }
            writer.WriteEndElement();
	    }
    }
}