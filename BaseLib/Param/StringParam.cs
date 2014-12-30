using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class StringParam : Parameter<string>{
		[NonSerialized] private TextBox control;
		public StringParam(string name) : this(name, "") { }

		public StringParam(string name, string value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return Value; } set { Value = value; } }

		public override void SetValueFromControl(){
			Value = control.Text;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override void Clear() { Value = ""; }

		public override object CreateControl(){
			return control = new TextBox{Text = Value};
		}

		public override object Clone() { return new StringParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}