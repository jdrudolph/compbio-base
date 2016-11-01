using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class MultiChoiceParamWpf : MultiChoiceParam{
		[NonSerialized] private ListSelectorControlWpf control;
		internal MultiChoiceParamWpf(string name) : base(name){}
		internal MultiChoiceParamWpf(string name, int[] value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

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
			control = new ListSelectorControlWpf{HasMoveButtons = true};
			foreach (string value in Values){
				control.Items.Add(value);
			}
			control.Repeats = Repeats;
			control.SelectedIndices = Value;
			control.SetDefaultSelectors(DefaultSelectionNames, DefaultSelections);
			return control;
		}
	}
}