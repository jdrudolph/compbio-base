using System;
using System.Globalization;
using System.Windows.Forms;

namespace BasicLib.Param{
	[Serializable]
	public class BoolParam : Parameter{
		public bool Value { get; set; }
		public bool Default { get; private set; }
		public BoolParam(string name) : this(name, false) {}

		public BoolParam(string name, bool value) : base(name){
			Value = value;
			Default = value;
		}

		public bool Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}
		public override string StringValue { get { return Value.ToString(CultureInfo.InvariantCulture); } set { Value = bool.Parse(value); } }

		public override void ResetValue(){
			Value = Default;
		}

		public override void ResetDefault(){
			Default = Value;
		}

		public override bool IsModified { get { return Value != Default; } }

		public override void SetValueFromControl(){
			CheckBox tb = (CheckBox) control;
			Value = tb.Checked;
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			CheckBox cb = (CheckBox)control;
			cb.Checked = Value;
		}

		public override void Clear(){
			Value = false;
		}

		protected override Control Control { get { return new CheckBox{Checked = Value}; } }

		public override object Clone(){
			return new BoolParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}