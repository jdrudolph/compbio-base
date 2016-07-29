using System;
using System.Globalization;
using System.Xml;

namespace BaseLibS.Param{
	[Serializable]
	public class IntParam : Parameter<int>{
        /// <summary>
        /// only for xml serialization
        /// </summary>
	    private IntParam() : this("", 0) { }

	    public IntParam(string name, int value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = int.Parse(value); }
		}

		public override void Clear(){
			Value = 0;
		}
		public override ParamType Type => ParamType.Server;

	}
}