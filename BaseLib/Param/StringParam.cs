using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class StringParam : Parameter{
		public string Value { get; set; }
		public string Default { get; private set; }
		[NonSerialized] private TextBox control;
		public StringParam(string name) : this(name, "") { }

		public StringParam(string name, string value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return Value; } set { Value = value; } }

		public string Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}

		public override void ResetValue() { Value = Default; }
		public override void ResetDefault() { Default = Value; }
		public override bool IsModified { get { return !Value.Equals(Default); } }

		public override void SetValueFromControl(){
			string val = control.Text;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override void Clear() { Value = ""; }

		public override object CreateControl(){
			control = new TextBox{Text = Value};
			return control;
		}

		public override object Clone() { return new StringParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}