using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class FileParam : Parameter{
		public string Filter { get; set; }
		public Func<string, string> ProcessFileName { get; set; }
		public bool Save { get; set; }
		public string Value { get; set; }
		public string Default { get; private set; }
		[NonSerialized] private FileParameterControl control;
		public FileParam(string name) : this(name, "") { }

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

		public override void ResetValue() { Value = Default; }
		public override void ResetDefault() { Default = Value; }
		public override bool IsModified { get { return !Value.Equals(Default); } }
		public override void SetValueFromControl() { Value = control.Text; }

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override void Clear() { Value = ""; }

		public override object CreateControl(){
			control = new FileParameterControl{Filter = Filter, ProcessFileName = ProcessFileName, Text = Value, Save = Save};
			return control;
		}

		public override object Clone(){
			return new FileParam(Name, Value){
				Help = Help,
				Visible = Visible,
				Save = Save,
				Filter = Filter,
				Default = Default,
				ProcessFileName = ProcessFileName
			};
		}
	}
}