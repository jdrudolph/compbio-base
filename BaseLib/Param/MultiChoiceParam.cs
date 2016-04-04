using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class MultiChoiceParam : MultiChoiceParamS{
		[NonSerialized] private ListSelectorControl control;
		public MultiChoiceParam(string name) : base(name){}
		public MultiChoiceParam(string name, int[] value) : base(name, value){}

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
			control = new ListSelectorControl{HasMoveButtons = true};
			foreach (string value in Values){
				control.Items.Add(value);
			}
			control.Repeats = Repeats;
			control.SelectedIndices = Value;
			control.SetDefaultSelectors(defaultSelectionNames, defaultSelections);
			return control;
		}
	}
}