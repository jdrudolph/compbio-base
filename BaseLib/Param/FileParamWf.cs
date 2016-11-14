using System;
using BaseLib.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	//TODO should be internal
	[Serializable]
	public class FileParamWf : FileParam{
		[NonSerialized] private FileParameterControl control;
		public FileParamWf(string name) : base(name){}
		public FileParamWf(string name, string value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			FileParameterControl vm = control;
			Value = vm.FileName;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			FileParameterControl vm = control;
			vm.FileName = Value;
		}

		public override object CreateControl(){
			control = new FileParameterControl(Value, Filter, ProcessFileName, Save);
			return control;
		}
	}
}