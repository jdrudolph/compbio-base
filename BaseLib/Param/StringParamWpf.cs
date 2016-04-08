using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class StringParamWpf : StringParam{
		[NonSerialized] private TextBox control;
		internal StringParamWpf(string name) : base(name){}
		internal StringParamWpf(string name, string value) : base(name, value){}
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