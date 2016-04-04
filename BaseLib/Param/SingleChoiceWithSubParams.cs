using System;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class SingleChoiceWithSubParams : SingleChoiceWithSubParamsS{
		[NonSerialized] private Grid control;
		public SingleChoiceWithSubParams(string name) : base(name){}
		public SingleChoiceWithSubParams(string name, int value) : base(name, value){}

		public override void SetValueFromControl(){
			if (control == null){
				return;
			}
			ComboBox cb = (ComboBox) WpfUtils.GetGridChild(control, 0, 0);
			if (cb != null){
				Value = cb.SelectedIndex;
			}
			foreach (Parameters p in SubParams){
				p.SetValuesFromControl();
			}
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			ComboBox cb = (ComboBox) WpfUtils.GetGridChild(control, 0, 0);
			if (Value >= 0 && Value < Values.Count){
				cb.SelectedIndex = Value;
			}
			foreach (Parameters p in SubParams){
				p.UpdateControlsFromValue();
			}
		}

		public override object CreateControl(){
			ParameterPanel[] panels = new ParameterPanel[SubParams.Count];
			for (int i = 0; i < panels.Length; i++){
				panels[i] = new ParameterPanel();
				panels[i].Init(SubParams[i], ParamNameWidth, (int) TotalWidth);
			}
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
			Grid grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(paramHeight, GridUnitType.Pixel)});
			grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			Grid.SetRow(cb, 0);
			grid.Children.Add(cb);
			for (int i = 0; i < panels.Length; i++){
				panels[i].Visibility = (i == Value) ? Visibility.Visible : Visibility.Hidden;
				panels[i].VerticalAlignment = VerticalAlignment.Top;
				Grid.SetRow(panels[i], 1);
				grid.Children.Add(panels[i]);
			}
			cb.SelectionChanged += (sender, e) =>{
				for (int i = 0; i < panels.Length; i++){
					panels[i].Visibility = (i == cb.SelectedIndex) ? Visibility.Visible : Visibility.Hidden;
				}
			};
			grid.Width = TotalWidth;
			control = grid;
			return control;
		}

		public override object Clone(){
			SingleChoiceWithSubParams s = new SingleChoiceWithSubParams(Name, Value){
				Help = Help,
				Visible = Visible,
				Values = Values,
				Default = Default,
				SubParams = new Parameters[SubParams.Count]
			};
			for (int i = 0; i < SubParams.Count; i++){
				s.SubParams[i] = (Parameters) SubParams[i].Clone();
			}
			return s;
		}
	}
}