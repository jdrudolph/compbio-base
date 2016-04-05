using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class Ms1LabelParam : Ms1LabelParamS{
		[NonSerialized] private Ms1LabelPanel control;

		public Ms1LabelParam(string name, int[][] value) : base(name, value){
			Value = value;
			Default = (int[][]) value.Clone();
		}

		public override void SetValueFromControl(){
			Value = control.SelectedIndices;
		}

		public override void UpdateControlFromValue(){
			control.SelectedIndices = Value;
		}

		public override object CreateControl(){
			return control = new Ms1LabelPanel(Multiplicity, Values){SelectedIndices = Value};
		}
	}
}