using System;
using BaseLib.Wpf;
using BaseLibS.Param;
using BaseLibS.Util;

namespace BaseLib.Param{
	[Serializable]
	public class MultiFileParam : Parameter<string[]>{
		public string Filter { get; set; }
		[NonSerialized] private MultiFileParameterControl control;
		public MultiFileParam(string name) : this(name, new string[0]) { }

		public MultiFileParam(string name, string[] value) : base(name){
			Value = value;
			Default = new string[Value.Length];
			for (int i = 0; i < Value.Length; i++){
				Default[i] = Value[i];
			}
			Filter = null;
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

		public override bool IsModified { get { return !ArrayUtils.EqualArrays(Default, Value); } }
		public override void SetValueFromControl() { Value = control.Filenames; }
		public override void Clear() { Value = new string[0]; }

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Filenames = Value;
		}

		public override object CreateControl() { return control = new MultiFileParameterControl{Filter = Filter, Filenames = Value}; }
		public override object Clone() { return new MultiFileParam(Name, Value){Help = Help, Visible = Visible, Filter = Filter, Default = Default}; }
		public override float Height { get { return 120; } }
	}
}