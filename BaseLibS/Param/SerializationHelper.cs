using System.Collections.Generic;
using System.Xml;

namespace BaseLibS.Param
{
    public static class SerializationHelper
    {
        public static void WriteValues(this XmlWriter writer, IList<string> values, string rootTag = "Values", string childTag = "Value")
        {
            writer.WriteStartElement(rootTag);
	        foreach (var value in values)
	        {
	            writer.WriteElementString(childTag, value);
	        }
            writer.WriteEndElement();
        }

	    public static List<string> ReadValues(this XmlReader reader, string rootTag = "Values", string childTag = "Value")
	    {
	        var values = new List<string>();
	        reader.ReadToFollowing(rootTag); // <Values>
	        reader.ReadToDescendant(childTag);
	        while (true)
	        {
	            if (reader.NodeType == XmlNodeType.EndElement)
	            {
	                break;
	            }
	            values.Add(reader.ReadElementContentAsString());
	        }
	        return values;
	    }

    }
}