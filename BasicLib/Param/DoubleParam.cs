using System;
using System.Globalization;
using System.Windows.Forms;

namespace BasicLib.Param{
	[Serializable]
	public class DoubleParam : Parameter{
		public double Value { get; set; }
		public double Default { get; private set; }

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

		public override void ResetValue(){
			Value = Default;
		}

		public override void ResetDefault(){
			Default = Value;
		}

		public override bool IsModified { get { return Value != Default; } }

		public override void SetValueFromControl(){
			TextBox tb = (TextBox) control;
			double val;
			bool success = double.TryParse(tb.Text, out val);
			val = success ? val : double.NaN;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			TextBox tb = (TextBox)control;
			tb.Text = "" + Value;
		}

		public override void Clear(){
			Value = 0;
		}

		protected override Control Control{
			get{
				TextBox tb = new TextBox{Text = "" + Value};
				tb.TextChanged += (sender, e) =>{
					SetValueFromControl();
					ValueHasChanged();
				};
				return tb;
			}
		}

		public override object Clone(){
			return new DoubleParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}