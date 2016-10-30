using System;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class BoolWithSubParamsWpf : BoolWithSubParams{
		[NonSerialized] private Grid control;
		internal BoolWithSubParamsWpf(string name) : base(name){}
		internal BoolWithSubParamsWpf(string name, bool value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			CheckBox cb = (CheckBox) WpfUtils.GetGridChild(control, 0, 0);
			Value = cb.IsChecked != null && cb.IsChecked.Value;
			SubParamsFalse.SetValuesFromControl();
			SubParamsTrue.SetValuesFromControl();
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			CheckBox cb = (CheckBox) WpfUtils.GetGridChild(control, 0, 0);
			if (cb == null){
				return;
			}
			cb.IsChecked = Value;
			SubParamsFalse?.UpdateControlsFromValue();
			SubParamsTrue?.UpdateControlsFromValue();
		}

		public override object CreateControl(){
			ParameterPanelWpf panelFalse = new ParameterPanelWpf();
			ParameterPanelWpf panelTrue = new ParameterPanelWpf();
			panelFalse.Init(SubParamsFalse, ParamNameWidth, (int) TotalWidth);
			panelTrue.Init(SubParamsTrue, ParamNameWidth, (int) TotalWidth);
			CheckBox cb = new CheckBox{IsChecked = Value};
			cb.Checked += (sender, e) => ValueHasChanged();
			cb.Unchecked += (sender, e) => ValueHasChanged();
			cb.VerticalAlignment = VerticalAlignment.Center;
			Grid tlp = new Grid();
			tlp.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(100, GridUnitType.Star)});
			tlp.RowDefinitions.Add(new RowDefinition{Height = new GridLength(paramHeight, GridUnitType.Pixel)});
			tlp.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			Grid.SetRow(cb, 0);
			tlp.Children.Add(cb);
			panelFalse.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
			panelTrue.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
			Grid.SetRow(panelFalse, 1);
			tlp.Children.Add(panelFalse);
			Grid.SetRow(panelTrue, 1);
			tlp.Children.Add(panelTrue);
			cb.Checked += (sender, e) =>{
				panelFalse.Visibility = cb.IsChecked != null && !cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
				panelTrue.Visibility = cb.IsChecked != null && cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
			};
			cb.Unchecked += (sender, e) =>{
				panelFalse.Visibility = cb.IsChecked != null && !cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
				panelTrue.Visibility = cb.IsChecked != null && cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
			};
			control = tlp;
			return control;
		}
	}
}