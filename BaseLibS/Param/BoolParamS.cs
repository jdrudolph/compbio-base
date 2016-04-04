using System;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class BoolParamS : Parameter<bool>{
		protected BoolParamS(string name) : this(name, false){}

		protected BoolParamS(string name, bool value) : base(name){
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