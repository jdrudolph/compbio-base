using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class LabelParamWpf : LabelParam{
		[NonSerialized] private Label control;
		internal LabelParamWpf(string name) : base(name){}
		internal LabelParamWpf(string name, string value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			Value = control.Content.ToString();
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Content = Value;
		}

		public override object CreateControl(){
			return control = new Label{Content = Value};
		}
	}
}