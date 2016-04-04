using System;

namespace BaseLibS.Param{
	[Serializable]
	public class StringParamS : Parameter<string>{
		public StringParamS(string name) : this(name, ""){}

		public StringParamS(string name, string value) : base(name){
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
	}
}