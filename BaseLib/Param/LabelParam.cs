using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class LabelParam : Parameter<string>{
		[NonSerialized] private Label control;
		public LabelParam(string name) : this(name, "") { }

		public LabelParam(string name, string value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return Value; } set { Value = value; } }
		public override void SetValueFromControl() { Value = control.Content.ToString(); }

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Content = Value;
		}

		public override void Clear() { Value = ""; }

		public override object CreateControl(){
			return control = new Label{Content = Value};
		}

		public override object Clone() { return new LabelParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}