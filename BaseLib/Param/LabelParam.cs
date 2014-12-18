using System;
using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Param{
	[Serializable]
	public class LabelParam : Parameter{
		public string Value { get; set; }
		public string Default { get; private set; }
		public LabelParam(string name) : this(name, "") { }

		public LabelParam(string name, string value) : base(name){
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
		public override bool IsModified { get { return false; } }

		public override void SetValueFromControl(){
			Label tb = (Label) control;
			Value = tb.Content.ToString();
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			Label tb = (Label) control;
			tb.Content = Value;
		}

		public override void Clear() { Value = ""; }
		protected override UIElement CreateControl() { return new Label{Content = Value}; }
		public override object Clone() { return new LabelParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}