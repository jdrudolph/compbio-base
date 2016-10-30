using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class MultiChoiceMultiBinParamWpf : MultiChoiceMultiBinParam{
		[NonSerialized] private MultiListSelectorControlWpf control;
		internal MultiChoiceMultiBinParamWpf(string name) : base(name){}
		internal MultiChoiceMultiBinParamWpf(string name, int[][] value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			Value = control.SelectedIndices;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.SelectedIndices = Value;
		}

		public override object CreateControl(){
			control = new MultiListSelectorControlWpf();
			control.Init(Values, Bins);
			control.SelectedIndices = Value;
			return control;
		}
	}
}