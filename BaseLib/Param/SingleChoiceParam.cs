using System;
using System.Windows.Controls;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class SingleChoiceParam : SingleChoiceParamS{
		[NonSerialized] private ComboBox control;
		public SingleChoiceParam(string name) : base(name){}
		public SingleChoiceParam(string name, int value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			if (control == null){
				return;
			}
			int val = control.SelectedIndex;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control != null && Value >= 0 && Value < Values.Count){
				control.SelectedIndex = Value;
			}
		}

		public void UpdateControlFromValue2(){
			if (control != null && Values != null){
				control.Items.Clear();
				foreach (string value in Values){
					control.Items.Add(value);
				}
				if (Value >= 0 && Value < Values.Count){
					control.SelectedIndex = Value;
				}
			}
			if (control != null && Value >= 0 && Value < Values.Count){
				control.SelectedIndex = Value;
			}
		}

		public override object CreateControl(){
			ComboBox cb = new ComboBox();
			cb.SelectionChanged += (sender, e) =>{
				SetValueFromControl();
				ValueHasChanged();
			};
			if (Values != null){
				foreach (string value in Values){
					cb.Items.Add(value);
				}
				if (Value >= 0 && Value < Values.Count){
					cb.SelectedIndex = Value;
				}
			}
			control = cb;
			return control;
		}
	}
}