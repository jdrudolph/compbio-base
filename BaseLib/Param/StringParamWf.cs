using System;
using System.Windows.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class StringParamWf : StringParam{
		[NonSerialized] private TextBox control;
		internal StringParamWf(string name) : base(name){}
		internal StringParamWf(string name, string value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = control.Text;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override object CreateControl(){
			return control = new TextBox {Text = Value};
		}
	}
}