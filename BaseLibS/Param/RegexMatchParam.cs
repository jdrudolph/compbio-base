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
	        ReadXmlNoValue(reader);
	        reader.ReadToFollowing("Value");
	        reader.ReadToDescendant("Regex");
	        Value = new Regex(reader.ReadElementString("Regex"));
	        Previews = reader.ReadValues("Previews", "Preview");
	    }

	    public override void WriteXml(XmlWriter writer)
	    {
	        WriteXmlNoValue(writer);
            writer.WriteStartElement("Value");
            writer.WriteElementString("Regex", Value.ToString());
            writer.WriteEndElement();
            writer.WriteValues(Previews, "Previews", "Preview");
	    }
    }
}