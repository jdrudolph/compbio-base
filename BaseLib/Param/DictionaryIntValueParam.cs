using System;
using System.Collections.Generic;
using BaseLib.Wpf;
using BaseLibS.Param;
using BaseLibS.Util;

namespace BaseLib.Param{
	[Serializable]
	public class DictionaryIntValueParam : Parameter<Dictionary<string, int>>{
		private string[] keys;
		public int DefaultValue { get; set; }
		[NonSerialized] private DictionaryIntValueControl control;

		public string[] Keys{
			get { return keys; }
			set{
				keys = value;
				if (control != null){
					control.Keys = keys;
				}
			}
		}

		public DictionaryIntValueParam(string name, Dictionary<string, int> value, string[] keys) : base(name){
			Value = value;
			Default = new Dictionary<string, int>();
			foreach (KeyValuePair<string, int> pair in value){
				Default.Add(pair.Key, pair.Value);
			}
			this.keys = keys;
		}

		public override string StringValue { get { return StringUtils.ToString(Value); } set { Value = DictionaryFromString(value); } }

		public static Dictionary<string, int> DictionaryFromString(string s){
			Dictionary<string, int> result = new Dictionary<string, int>();
			foreach (string s1 in s.Split('\r')){
				string[] w = s1.Trim().Split('\t');
				result.Add(w[0], int.Parse(w[1]));
			}
			return result;
		}

		public override bool IsModified{
			get{
				if (Value.Count != Default.Count){
					return true;
				}
				foreach (string k in Value.Keys){
					if (!Default.ContainsKey(k)){
						return true;
					}
					if (Default[k] != Value[k]){
						return true;
					}
				}
				return false;
			}
		}

		public override void SetValueFromControl() { Value = control.Value; }

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Value = Value;
		}

		public override void Clear() { Value = new Dictionary<string, int>(); }
		public override object CreateControl() { return control = new DictionaryIntValueControl{Value = Value, Keys = Keys, Default = DefaultValue}; }
		public override object Clone() { return new DictionaryIntValueParam(Name, Value, Keys){Help = Help, Visible = Visible, Default = Default}; }
	}
}