using System;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class DoubleParamS : Parameter<double>{
		public DoubleParamS(string name, double value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = double.Parse(value); }
		}

		public override void Clear(){
			Value = 0;
		}
	}
}