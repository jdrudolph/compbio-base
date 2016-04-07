using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class MultiChoiceMultiBinParam : MultiChoiceMultiBinParamS{
		[NonSerialized] private MultiListSelectorControl control;
		public MultiChoiceMultiBinParam(string name) : base(name){}
		public MultiChoiceMultiBinParam(string name, int[][] value) : base(name, value){}
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
			control = new MultiListSelectorControl();
			control.Init(Values, Bins);
			control.SelectedIndices = Value;
			return control;
		}
	}
}