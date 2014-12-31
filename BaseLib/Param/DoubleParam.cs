using System;
using System.Globalization;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class DoubleParam : Parameter<double>{
		[NonSerialized] private TextBox control;


		public DoubleParam(string name, double value) : base(name){
			Value = value;
			Default = value;
		}

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

		public override object CreateControl(){
			control = new TextBox{Text = "" + Value};
			control.TextChanged += (sender, e) =>{
				SetValueFromControl();
				ValueHasChanged();
			};
			return control;
		}

		public override string StringValue { get { return Value.ToString(CultureInfo.InvariantCulture); } set { Value = double.Parse(value); } }
		public override void Clear() { Value = 0; }
		public override object Clone() { return new DoubleParam(Name, Value) { Help = Help, Visible = Visible, Default = Default }; }
	}
}