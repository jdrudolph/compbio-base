using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BasicLib.Forms.Select;
using BasicLib.Util;

namespace BasicLib.Param{
	[Serializable]
	public class DictionaryIntValueParam : Parameter{
		public Dictionary<string, int> Value { get; set; }
		public Dictionary<string, int> Default { get; private set; }

		public DictionaryIntValueParam(string name, Dictionary<string, int> value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue { get { return StringUtils.ToString(Value); } set { Value = DictionaryFromString(value); } }
		public Dictionary<string, int> Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}

		public static Dictionary<string, int> DictionaryFromString(string s){
			Dictionary<string, int> result = new Dictionary<string, int>();
			foreach (string s1 in s.Split('\r')){
				string[] w = s1.Trim().Split('\t');
				result.Add(w[0], int.Parse(w[1]));
			}
			return result;
		}

		public override void ResetValue(){
			Value = Default;
		}

		public override void ResetDefault(){
			Default = Value;
		}

		public override bool IsModified { get { return Value != Default; } }

		public override void SetValueFromControl(){
			DictionaryIntValueControl tb = (DictionaryIntValueControl) control;
			Value = tb.Value;
		}

		public override void UpdateControlFromValue(){
			DictionaryIntValueControl tb = (DictionaryIntValueControl) control;
			tb.Value = Value;
		}

		public override void Clear(){
			Value = new Dictionary<string, int>();
		}

		protected override Control Control{
			get{
				DictionaryIntValueControl tb = new DictionaryIntValueControl{Value = Value};
				return tb;
			}
		}

		public override object Clone(){
			return new DictionaryIntValueParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}