using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class MultiFileParam : MultiFileParamS{
		[NonSerialized] private MultiFileParameterControl control;
		public MultiFileParam(string name) : base(name){}
		public MultiFileParam(string name, string[] value) : base(name, value){}

		public override void SetValueFromControl(){
			Value = control.Filenames;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Filenames = Value;
		}

		public override object CreateControl(){
			return control = new MultiFileParameterControl{Filter = Filter, Filenames = Value};
		}

		public override object Clone(){
			return new MultiFileParam(Name, Value){Help = Help, Visible = Visible, Filter = Filter, Default = Default};
		}
	}
}