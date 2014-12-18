using System;
using System.Windows;
using BaseLib.Wpf;

namespace BaseLib.Param{
	[Serializable]
	public class FolderParam : Parameter{
		public string Value { get; set; }
		public string Default { get; private set; }
		public FolderParam(string name) : this(name, "") { }

		public FolderParam(string name, string value) : base(name){
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
			FolderParameterControl tb = (FolderParameterControl) control;
			string val = tb.Text;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			FolderParameterControl lfp = (FolderParameterControl) control;
			lfp.Text = Value;
		}

		public override void Clear() { Value = ""; }

		protected override UIElement CreateControl(){
			FolderParameterControl tb = new FolderParameterControl{Text = Value};
			return tb;
		}

		public override object Clone() { return new FolderParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}