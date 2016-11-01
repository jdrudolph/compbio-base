using System;
using System.Windows.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class IntParamWf : IntParam{
		[NonSerialized] private TextBox control;
		internal IntParamWf(string name, int value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			int val;
			bool s = int.TryParse(control.Text, out val);
			if (s){
				Value = val;
			}
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