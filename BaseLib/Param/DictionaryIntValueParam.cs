using System;
using System.Collections.Generic;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class DictionaryIntValueParam : DictionaryIntValueParamS{
		[NonSerialized] private DictionaryIntValueControl control;
		public DictionaryIntValueParam(string name, Dictionary<string, int> value, string[] keys) : base(name, value, keys){}

		public override string[] Keys{
			get { return keys; }
			set{
				keys = value;
				if (control != null){
					control.Keys = keys;
				}
			}
		}

		public override void SetValueFromControl(){
			Value = control.Value;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Value = Value;
		}

		public override object CreateControl(){
			return control = new DictionaryIntValueControl{Value = Value, Keys = Keys, Default = DefaultValue};
		}
	}
}