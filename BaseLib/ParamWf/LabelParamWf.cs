using System;
using System.Windows.Forms;

namespace BaseLib.ParamWf{
	[Serializable]
	public class LabelParamWf : ParameterWf{
		public string Value { get; set; }
		public string Default { get; private set; }
		public LabelParamWf(string name) : this(name, "") {}

		public LabelParamWf(string name, string value) : base(name){
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

		public override bool IsModified { get { return false; } }

		public override void SetValueFromControl(){
			Label tb = (Label) control;
			Value = tb.Text;
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			Label tb = (Label)control;
			tb.Text = Value;
		}

		public override void Clear(){
			Value = "";
		}

		protected override Control Control { get { return new Label{Text = Value}; } }

		public override object Clone(){
			return new LabelParamWf(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}