using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class StringParam : StringParamS{
		[NonSerialized] private TextBox control;
		public StringParam(string name) : base(name){}
		public StringParam(string name, string value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

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
			return control = new TextBox{Text = Value};
		}
	}
}