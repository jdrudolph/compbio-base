using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class MultiFileParamWpf : MultiFileParam{
		[NonSerialized] private MultiFileParameterControlWpf control;
		internal MultiFileParamWpf(string name) : base(name){}
		internal MultiFileParamWpf(string name, string[] value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

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
			return control = new MultiFileParameterControlWpf{Filter = Filter, Filenames = Value};
		}
	}
}