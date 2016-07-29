using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace BaseLibS.Param{
	[Serializable]
	public class RegexReplaceParam : Parameter<Tuple<Regex, string>>
    {
        public List<string> Previews { get; set; }

        /// <summary>
        /// for xml serialization only
        /// </summary>
	    private RegexReplaceParam() : this("", Tuple.Create(new Regex("(.*)"), "$1"), new List<string>()) { }

	    public RegexReplaceParam(string name, Regex regex, string replace, List<string> previews) : this(name, Tuple.Create(regex, replace), previews) { }

	    public RegexReplaceParam(string name, Tuple<Regex, string> value, List<string> previews) : base(name)
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
	        var replace = reader.GetAttribute("Replace");
	        reader.ReadToFollowing("Value");
	        reader.ReadToDescendant("Regex");
	        var regex = new Regex(reader.ReadElementString("Regex"));
	        Value = Tuple.Create(regex, replace);
	        Previews = reader.ReadValues("Previews", "Preview");
	    }

	    public override void WriteXml(XmlWriter writer)
	    {
            WriteXmlNoValue(writer);
            writer.WriteAttributeString("Replace", Value.Item2);
            writer.WriteStartElement("Value");
            writer.WriteElementString("Regex", Value.Item1.ToString());
            writer.WriteEndElement();
            writer.WriteValues(Previews, "Previews", "Preview");
	    }
    }
}