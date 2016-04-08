using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class FileParamWpf : FileParam{
		[NonSerialized] private FileParameterControl control;
		public FileParamWpf(string name) : base(name){}
		public FileParamWpf(string name, string value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			Value = control.Text;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override object CreateControl(){
			return
				control = new FileParameterControl{Filter = Filter, ProcessFileName = ProcessFileName, Text = Value, Save = Save};
		}
	}
}