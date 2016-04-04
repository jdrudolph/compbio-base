using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class DoubleParam : DoubleParamS{
		[NonSerialized] private TextBox control;

		public DoubleParam(string name, double value) : base(name, value){
			Value = value;
			Default = value;
		}

		public override void SetValueFromControl(){
			double val;
			bool success = double.TryParse(control.Text, out val);
			val = success ? val : double.NaN;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = "" + Value;
		}

		public override object CreateControl(){
			control = new TextBox{Text = "" + Value};
			control.TextChanged += (sender, e) =>{
				SetValueFromControl();
				ValueHasChanged();
			};
			return control;
		}
	}
}