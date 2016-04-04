using System;
using System.Windows;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class BoolParam : BoolParamS{
		[NonSerialized] private CheckBox control;
		public BoolParam(string name) : base(name){}
		public BoolParam(string name, bool value) : base(name, value){}

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