using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BaseLib.Forms.Select;
using BaseLib.Util;

namespace BaseLib.ParamWf{
	[Serializable]
	public class DictionaryIntValueParamWf : ParameterWf{
		private string[] keys;
		public Dictionary<string, int> Value { get; set; }
		public Dictionary<string, int> Default { get; private set; }
		public int DefaultValue { get; set; }
		public string[] Keys{
			get { return keys; }
			set{
				keys = value;
				if (control != null){
					DictionaryIntValueControlWf tb = (DictionaryIntValueControlWf) control;
					tb.Keys = keys;
				}
			}
		}

		public DictionaryIntValueParamWf(string name, Dictionary<string, int> value, string[] keys) : base(name){
			Value = value;
			Default = value;
			this.keys = keys;
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
			DictionaryIntValueControlWf tb = (DictionaryIntValueControlWf) control;
			Value = tb.Value;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			DictionaryIntValueControlWf tb = (DictionaryIntValueControlWf) control;
			tb.Value = Value;
		}

		public override void Clear(){
			Value = new Dictionary<string, int>();
		}

		protected override Control Control { get { return new DictionaryIntValueControlWf{Value = Value, Keys = Keys, Default = DefaultValue}; } }

		public override object Clone(){
			return new DictionaryIntValueParamWf(Name, Value, Keys){Help = Help, Visible = Visible, Default = Default};
		}
	}
}