using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class Ms1LabelParamWpf : Ms1LabelParam{
		[NonSerialized] private Ms1LabelPanelWpf control;
		internal Ms1LabelParamWpf(string name, int[][] value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = control.SelectedIndices;
		}

		public override void UpdateControlFromValue(){
			control.SelectedIndices = Value;
		}

		public override object CreateControl(){
			return control = new Ms1LabelPanelWpf(Multiplicity, Values){SelectedIndices = Value};
		}
	}
}