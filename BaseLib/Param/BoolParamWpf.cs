using System;
using System.Windows;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class BoolParamWpf : BoolParam{
		[NonSerialized] private CheckBox control;
		internal BoolParamWpf(string name) : base(name){}
		internal BoolParamWpf(string name, bool value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			Value = control.IsChecked != null && control.IsChecked.Value;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.IsChecked = Value;
		}

		public override object CreateControl(){
			return control = new CheckBox{IsChecked = Value, VerticalAlignment = VerticalAlignment.Center};
		}
	}
}