using System;

namespace BaseLibS.Param{
	[Serializable]
	public class LabelParamS : Parameter<string>{
		public LabelParamS(string name) : this(name, "") { }

		public LabelParamS(string name, string value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return Value; } set { Value = value; } }

		public override void Clear() { Value = ""; }
	}
}