using System;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Util;

namespace BaseLib.Param{
	[Serializable]
	public class MultiStringParam : Parameter{
		public string[] Value { get; set; }
		public string[] Default { get; private set; }
		public MultiStringParam(string name) : this(name, new string[0]) {}

		public MultiStringParam(string name, string[] value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return StringUtils.Concat(",", Value); }
			set{
				if (value.Trim().Length == 0){
					Value = new string[0];
					return;
				}
				Value = value.Split(',');
			}
		}
		public string[] Value2{
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

		public override bool IsModified { get { return !ArrayUtils.EqualArrays(Value, Default); } }

		public override void SetValueFromControl(){
			//TODO
			//RichTextBox tb = (RichTextBox) control;
			//string text = tb.Text;
			//string[] b = text.Split('\n');
			//List<string> result = new List<string>();
			//foreach (string x in b){
			//	string y = x.Trim();
			//	if (y.Length > 0){
			//		result.Add(y);
			//	}
			//}
			//string[] val = result.ToArray();
			//Value = val;
		}

		public override void Clear(){
			Value = new string[0];
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			RichTextBox rtb = (RichTextBox)control;
			//TODO
			//if (Value.Length >= 10) {
			//	rtb.Text = StringUtils.Concat("\n", Value);
			//} else{
			//	string[] q = new string[10 - Value.Length];
			//	for (int i = 0; i < q.Length; i++){
			//		q[i] = "";
			//	}
			//	rtb.Text = StringUtils.Concat("\n", ArrayUtils.Concat(Value, q));
			//}
		}

		protected override FrameworkElement Control {
			get{
				//TODO
				//RichTextBox tb = new RichTextBox{Multiline = true};
				//if (Value.Length >= 10){
				//	tb.Text = StringUtils.Concat("\n", Value);
				//} else{
				//	string[] q = new string[10 - Value.Length];
				//	for (int i = 0; i < q.Length; i++){
				//		q[i] = "";
				//	}
				//	tb.Text = StringUtils.Concat("\n", ArrayUtils.Concat(Value, q));
				//}
				//return tb;
				return null;
			}
		}

		public override object Clone(){
			return new MultiStringParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}

		public override float Height { get { return 150f; } }
	}
}