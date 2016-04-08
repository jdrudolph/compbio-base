using System;

namespace BaseLibS.Param{
	[Serializable]
	public class StringParam : Parameter<string>{
		public StringParam(string name) : this(name, ""){}

		public StringParam(string name, string value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return Value; }
			set { Value = value; }
		}

		public override void Clear(){
			Value = "";
		}
		public override ParamType Type => ParamType.Server;
	}
}