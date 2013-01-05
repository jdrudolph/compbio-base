using System;
using System.Globalization;
using System.Windows.Forms;

namespace BasicLib.Param{
	[Serializable]
	public class IntParam : Parameter{
		public int Value { get; set; }
		public int Default { get; private set; }

		public IntParam(string name, int value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return Value.ToString(CultureInfo.InvariantCulture); } set { Value = int.Parse(value); } }
		public int Value2{
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

		public override bool IsModified { get { return Value != Default; } }

		public override void SetValueFromControl(){
			TextBox tb = (TextBox) control;
			int val;
			bool s = int.TryParse(tb.Text, out val);
			if (s){
				Value = val;
			}
		}

		public override void UpdateControlFromValue(){
			TextBox tb = (TextBox) control;
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
			return new IntParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}