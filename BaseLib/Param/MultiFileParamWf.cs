using System;
using BaseLib.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class MultiFileParamWf : MultiFileParam{
		[NonSerialized] private MultiFileParameterControl control;
		internal MultiFileParamWf(string name) : base(name){}
		internal MultiFileParamWf(string name, string[] value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

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
	}
}