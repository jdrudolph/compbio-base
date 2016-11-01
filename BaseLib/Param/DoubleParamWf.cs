using System;
using System.Windows.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class DoubleParamWf : DoubleParam{
		[NonSerialized] private TextBox control;
		internal DoubleParamWf(string name, double value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

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