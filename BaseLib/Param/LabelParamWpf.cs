using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class LabelParamWpf : LabelParam{
		[NonSerialized] private Label control;
		public LabelParamWpf(string name) : base(name){}
		public LabelParamWpf(string name, string value) : base(name, value){}
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