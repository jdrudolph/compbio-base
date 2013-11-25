using System;
using System.Windows.Controls;
using BasicLib.Wpf;

namespace BasicLib.Param{
	[Serializable]
	public class FileParam : Parameter{
		public string Filter { get; set; }
		public bool Save { get; set; }
		public string Value { get; set; }
		public string Default { get; private set; }
		public FileParam(string name) : this(name, "") {}

		public FileParam(string name, string value) : base(name){
			Value = value;
			Default = value;
			Filter = null;
			Save = false;
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
			FileParameterControl tb = (FileParameterControl)control;
			string val = tb.Text;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			FileParameterControl lfp = (FileParameterControl)control;
			lfp.Text = Value;
		}

		public override void Clear(){
			Value = "";
		}

		protected override Control Control { get { return new FileParameterControl { Filter = Filter, Text = Value, Save = Save }; } }

		public override object Clone(){
			return new FileParam(Name, Value){Help = Help, Visible = Visible, Save = Save, Filter = Filter, Default = Default};
		}
	}
}