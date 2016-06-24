using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseLibS.Param{
	[Serializable]
	public class RegexParam : Parameter<Tuple<Regex, string, List<string>>>
    {
        public RegexParam(string name) : this(name, new Regex("Column (.*)"), "$1", new List<string>() { "Column 1" }) { }

        public RegexParam(string name, List<string> items) : this(name, Tuple.Create(new Regex("Column (.*)"), "$1", items)) { }

        public RegexParam(string name, Regex pattern, string replacement, List<string> items) : this(name, Tuple.Create(pattern, replacement, items)) { }

        public RegexParam(string name, Tuple<Regex, string, List<string>> value) : base(name)
	    {
			Value = value;
		    Default = value;
	    }

	    public override void Clear(){
            Value = Tuple.Create(new Regex("Column (.*)"), "$1", new List<string>() {"Column 1"});
		}
        public override ParamType Type => ParamType.Server;

        public override string StringValue
        {
            get { return Value.ToString(); }
            set { throw new NotImplementedException("Setting string value for RegexParam not implemented"); }
        }
    }
}