using System;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class DoubleParam : Parameter<double>{
		public DoubleParam(string name, double value) : base(name){
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
		public override ParamType Type => ParamType.Server;
	}
}