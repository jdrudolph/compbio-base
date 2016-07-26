using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseLibS.Param{
	[Serializable]
	public class RegexReplaceParam : Parameter<Tuple<Regex, string, List<string>>>
    {
        public RegexReplaceParam(string name) : this(name, new Regex("Column (.*)"), "$1", new List<string>() { "Column 1" }) { }

        public RegexReplaceParam(string name, List<string> items) : this(name, Tuple.Create(new Regex("Column (.*)"), "$1", items)) { }

        public RegexReplaceParam(string name, Regex pattern, string replacement, List<string> items) : this(name, Tuple.Create(pattern, replacement, items)) { }

        public RegexReplaceParam(string name, Tuple<Regex, string, List<string>> value) : base(name)
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
            set { throw new NotImplementedException($"Setting string value for {typeof(RegexReplaceParam)} not implemented"); }
        }
    }
}