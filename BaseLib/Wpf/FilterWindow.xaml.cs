using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BaseLib.Param;
using BaseLibS.Graph;
using BaseLibS.Param;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for FilterWindow.xaml
	/// </summary>
	public partial class FilterWindow{
		private readonly SubSelectionControlWpf subSelectionControl;

		public FilterWindow(SubSelectionControlWpf subSelectionControl){
			InitializeComponent();
			this.subSelectionControl = subSelectionControl;
			I1.Source = WpfUtils.LoadBitmap(Bitmap2.GetImage("plus1.bmp"));
			I2.Source = WpfUtils.LoadBitmap(Bitmap2.GetImage("minus1.bmp"));
			Icon = WpfUtils.LoadBitmap(Bitmap2.GetImage("Perseus.jpg"));
			RebuildGui();
		}

		private void AddButton_OnClick(object sender, RoutedEventArgs e){
			Parameters p = subSelectionControl.GetParameters();
			subSelectionControl.parameters.Add(p);
			RebuildGui();
		}

		private void RebuildGui(){
			Grid g = new Grid();
			foreach (Parameters t in subSelectionControl.parameters){
				t.SetValuesFromControl();
			}
			for (int i = 0; i < subSelectionControl.parameters.Count; i++){
				g.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Auto)});
			}
			for (int i = 0; i < subSelectionControl.parameters.Count; i++){
				ParameterPanelWpf tb = new ParameterPanelWpf{Background = new SolidColorBrush(Colors.Linen)};
				tb.Init(subSelectionControl.parameters[i]);
				tb.Margin = new Thickness(2);
				Grid.SetRow(tb, i);
				g.Children.Add(tb);
			}
			ScrollPanel1.Content = g;
		}

		private void RemoveButton_OnClick(object sender, RoutedEventArgs e){
			if (subSelectionControl.parameters.Count == 0){
				return;
			}
			subSelectionControl.parameters.RemoveAt(subSelectionControl.parameters.Count - 1);
			RebuildGui();
		}

		protected override void OnClosed(EventArgs e){
			foreach (Parameters t in subSelectionControl.parameters){
				t.SetValuesFromControl();
			}
			base.OnClosed(e);
		}
	}
}