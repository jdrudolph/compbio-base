using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class LabelParam : LabelParamS{
		[NonSerialized] private Label control;
		public LabelParam(string name) : base(name){}
		public LabelParam(string name, string value) : base(name, value){}

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

		public override object Clone() { return new LabelParam(Name, Value) { Help = Help, Visible = Visible, Default = Default }; }
	}
}