using System;
using System.Globalization;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class IntParam : Parameter<int>{
		[NonSerialized] private TextBox control;

		public IntParam(string name, int value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = int.Parse(value); }
		}

		public override void SetValueFromControl(){
			int val;
			bool s = int.TryParse(control.Text, out val);
			if (s){
				Value = val;
			}
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = "" + Value;
		}

		public override void Clear(){
			Value = 0;
		}

		public override object CreateControl(){
			control = new TextBox{Text = "" + Value};
			control.TextChanged += (sender, e) =>{
				SetValueFromControl();
				ValueHasChanged();
			};
			return control;
		}

		public override object Clone(){
			return new IntParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}