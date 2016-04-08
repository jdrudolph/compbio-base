using System;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class BoolWithSubParams : ParameterWithSubParams<bool>{
		public Parameters SubParamsFalse { get; set; }
		public Parameters SubParamsTrue { get; set; }
		public BoolWithSubParams(string name) : this(name, false){}

		public BoolWithSubParams(string name, bool value) : base(name){
			TotalWidth = 1000F;
			ParamNameWidth = 250F;
			Value = value;
			Default = value;
			SubParamsFalse = new Parameters();
			SubParamsTrue = new Parameters();
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = bool.Parse(value); }
		}

		public override void ResetSubParamValues(){
			SubParamsTrue.ResetValues();
			SubParamsFalse.ResetValues();
		}

		public override void ResetSubParamDefaults(){
			SubParamsTrue.ResetDefaults();
			SubParamsFalse.ResetDefaults();
		}

		public override bool IsModified{
			get{
				if (Value != Default){
					return true;
				}
				return SubParamsTrue.IsModified || SubParamsFalse.IsModified;
			}
		}

		public override Parameters GetSubParameters(){
			return Value ? SubParamsTrue : SubParamsFalse;
		}

		public override void Clear(){
			Value = false;
			SubParamsTrue.Clear();
			SubParamsFalse.Clear();
		}

		public override float Height => 50 + Math.Max(SubParamsFalse.Height, SubParamsTrue.Height);
		public override ParamType Type => ParamType.Server;
	}
}