using System;
using System.Collections.Generic;
using System.Globalization;

namespace BaseLibS.Param{
	[Serializable]
	public class SingleChoiceWithSubParamsS : ParameterWithSubParams<int>{
		public IList<string> Values { get; set; }
		public IList<Parameters> SubParams { get; set; }
		public SingleChoiceWithSubParamsS(string name) : this(name, 0){}

		public SingleChoiceWithSubParamsS(string name, int value) : base(name){
			TotalWidth = 1000F;
			ParamNameWidth = 250F;
			Value = value;
			Default = value;
			Values = new List<string>(new[] { "" });
			SubParams = new List<Parameters>(new[] { new Parameters() });
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = int.Parse(value); }
		}

		public override void ResetSubParamValues(){
			Value = Default;
			foreach (Parameters p in SubParams){
				p.ResetValues();
			}
		}

		public override void ResetSubParamDefaults(){
			Default = Value;
			foreach (Parameters p in SubParams){
				p.ResetDefaults();
			}
		}

		public override bool IsModified{
			get{
				if (Value != Default){
					return true;
				}
				foreach (Parameters p in SubParams){
					if (p.IsModified){
						return true;
					}
				}
				return false;
			}
		}

		public string SelectedValue => Value < 0 || Value >= Values.Count ? null : Values[Value];

		public override Parameters GetSubParameters(){
			return Value < 0 || Value >= Values.Count ? null : SubParams[Value];
		}

		public override void Clear(){
			Value = 0;
			foreach (Parameters parameters in SubParams){
				parameters.Clear();
			}
		}

		public override float Height{
			get{
				float max = 0;
				foreach (Parameters param in SubParams){
					max = Math.Max(max, param.Height + 6);
				}
				return 44 + max;
			}
		}

		public void SetValueChangedHandlerForSubParams(ValueChangedHandler action){
			ValueChanged += action;
			foreach (Parameter p in GetSubParameters().GetAllParameters()){
				if (p is IntParamS || p is DoubleParamS){
					p.ValueChanged += action;
				} else{
					(p as SingleChoiceWithSubParamsS)?.SetValueChangedHandlerForSubParams(action);
				}
			}
		}
		public override ParamType Type => ParamType.Server;
	}
}