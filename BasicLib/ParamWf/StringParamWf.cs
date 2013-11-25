using System;
using System.Windows.Forms;

namespace BasicLib.ParamWf{
	[Serializable]
	public class StringParamWf : ParameterWf{
		public string Value { get; set; }
		public string Default { get; private set; }
		public StringParamWf(string name) : this(name, "") {}

		public StringParamWf(string name, string value) : base(name){
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

		public override void ResetValue(){
			Value = Default;
		}

		public override void ResetDefault(){
			Default = Value;
		}

		public override bool IsModified { get { return !Value.Equals(Default); } }

		public override void SetValueFromControl(){
			TextBox tb = (TextBox) control;
			string val = tb.Text;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			TextBox tb = (TextBox)control;
			tb.Text = Value;
		}

		public override void Clear(){
			Value = "";
		}

		protected override Control Control { get { return new TextBox{Text = Value}; } }

		public override object Clone(){
			return new StringParamWf(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}