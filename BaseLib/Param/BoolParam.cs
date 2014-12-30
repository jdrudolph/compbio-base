using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class BoolParam : Parameter{
		public bool Value { get; set; }
		public bool Default { get; private set; }
		[NonSerialized] private CheckBox control;
		public BoolParam(string name) : this(name, false) { }

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
		public override void ResetValue() { Value = Default; }
		public override void ResetDefault() { Default = Value; }
		public override bool IsModified { get { return Value != Default; } }
		public override void SetValueFromControl() { Value = control.IsChecked != null && control.IsChecked.Value; }

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.IsChecked = Value;
		}

		public override void Clear() { Value = false; }

		public override object CreateControl(){
			control = new CheckBox{IsChecked = Value, VerticalAlignment = VerticalAlignment.Center};
			return control;
		}

		public override object Clone() { return new BoolParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}