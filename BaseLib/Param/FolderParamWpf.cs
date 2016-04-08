using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class FolderParamWpf : FolderParam{
		[NonSerialized] private FolderParameterControl control;
		public FolderParamWpf(string name) : base(name){}
		public FolderParamWpf(string name, string value) : base(name, value){}
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
			return control = new FolderParameterControl{Text = Value};
		}
	}
}