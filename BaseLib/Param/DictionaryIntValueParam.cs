using System;
using System.Collections.Generic;
using System.Windows;
using BaseLib.Wpf;
using BaseLibS.Util;

namespace BaseLib.Param{
	[Serializable]
	public class DictionaryIntValueParam : Parameter{
		private string[] keys;
		public Dictionary<string, int> Value { get; set; }
		public Dictionary<string, int> Default { get; private set; }
		public int DefaultValue { get; set; }

		public string[] Keys{
			get { return keys; }
			set{
				keys = value;
				if (control != null){
					DictionaryIntValueControl tb = (DictionaryIntValueControl) control;
					tb.Keys = keys;
				}
			}
		}

		public DictionaryIntValueParam(string name, Dictionary<string, int> value, string[] keys) : base(name){
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

		public override void ResetValue() { Value = Default; }
		public override void ResetDefault() { Default = Value; }
		public override bool IsModified { get { return Value != Default; } }

		public override void SetValueFromControl(){
			DictionaryIntValueControl tb = (DictionaryIntValueControl) control;
			Value = tb.Value;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			DictionaryIntValueControl tb = (DictionaryIntValueControl) control;
			tb.Value = Value;
		}

		public override void Clear() { Value = new Dictionary<string, int>(); }
		protected override UIElement Control { get { return new DictionaryIntValueControl{Value = Value, Keys = Keys, Default = DefaultValue}; } }
		public override object Clone() { return new DictionaryIntValueParam(Name, Value, Keys){Help = Help, Visible = Visible, Default = Default}; }
	}
}