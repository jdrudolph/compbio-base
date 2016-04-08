using System;

namespace BaseLibS.Param{
	[Serializable]
	public class LabelParam : Parameter<string>{
		public LabelParam(string name) : this(name, "") { }

		public LabelParam(string name, string value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return Value; } set { Value = value; } }

		public override void Clear() { Value = ""; }
		public override ParamType Type => ParamType.Server;
	}
}