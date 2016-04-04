using System;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class BoolParamS : Parameter<bool>{
		public BoolParamS(string name) : this(name, false){}

		public BoolParamS(string name, bool value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = bool.Parse(value); }
		}

		public override void Clear(){
			Value = false;
		}
		public override ParamType Type => ParamType.Server;
	}
}