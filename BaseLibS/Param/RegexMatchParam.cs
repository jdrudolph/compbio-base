using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseLibS.Param{
	[Serializable]
	public class RegexMatchParam : Parameter<Tuple<Regex, List<string>>>
    {
        public RegexMatchParam(string name, Tuple<Regex, List<string>> value) : base(name)
	    {
			Value = value;
		    Default = value;
	    }

	    public override void Clear(){
            Value = Tuple.Create(new Regex("Column (.*)"), new List<string>() {"Column 1"});
		}
        public override ParamType Type => ParamType.Server;

        public override string StringValue
        {
            get { return Value.ToString(); }
            set { throw new NotImplementedException($"Setting string value for {typeof(RegexReplaceParam)} not implemented"); }
        }
    }
}