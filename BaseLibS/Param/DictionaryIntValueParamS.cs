using System;
using System.Collections.Generic;
using BaseLibS.Util;

namespace BaseLibS.Param{
	[Serializable]
	public class DictionaryIntValueParamS : Parameter<Dictionary<string, int>>{
		protected string[] keys;
		public int DefaultValue { get; set; }

		public virtual string[] Keys{
			get { return keys; }
			set { keys = value; }
		}

		public DictionaryIntValueParamS(string name, Dictionary<string, int> value, string[] keys) : base(name){
			Value = value;
			Default = new Dictionary<string, int>();
			foreach (KeyValuePair<string, int> pair in value){
				Default.Add(pair.Key, pair.Value);
			}
			this.keys = keys;
		}

		public override string StringValue{
			get { return StringUtils.ToString(Value); }
			set { Value = DictionaryFromString(value); }
		}

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

		public override void Clear(){
			Value = new Dictionary<string, int>();
		}

		public override object Clone(){
			return new DictionaryIntValueParamS(Name, Value, Keys){Help = Help, Visible = Visible, Default = Default};
		}
	}
}