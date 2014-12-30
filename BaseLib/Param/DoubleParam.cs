using System;
using System.Globalization;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class DoubleParam : Parameter{
		public double Value { get; set; }
		public double Default { get; private set; }
		[NonSerialized] private TextBox control;

		public DoubleParam(string name, double value) : base(name){
			Value = value;
			Default = value;
		}

		public double Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}

		public override string StringValue { get { return Value.ToString(CultureInfo.InvariantCulture); } set { Value = double.Parse(value); } }
		public override void ResetValue() { Value = Default; }
		public override void ResetDefault() { Default = Value; }
		public override bool IsModified { get { return Value != Default; } }

		public override void SetValueFromControl(){
			double val;
			bool success = double.TryParse(control.Text, out val);
			val = success ? val : double.NaN;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = "" + Value;
		}

		public override void Clear() { Value = 0; }

		public override object CreateControl(){
			TextBox tb = new TextBox{Text = "" + Value};
			tb.TextChanged += (sender, e) =>{
				SetValueFromControl();
				ValueHasChanged();
			};
			control = tb;
			return control;
		}

		public override object Clone() { return new DoubleParam(Name, Value){Help = Help, Visible = Visible, Default = Default}; }
	}
}