using System;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class IntParamS : Parameter<int>{
		public IntParamS(string name, int value) : base(name){
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

		public override object Clone(){
			return new IntParamS(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}