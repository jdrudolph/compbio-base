using System.Windows;
using System.Windows.Controls;
using BaseLib.Wpf;
using BaseLibS.Util;

namespace BaseLib.Param{
	public partial class ParameterGroupPanel : UserControl{
		public ParameterGroup ParameterGroup { get; private set; }
		private Grid grid;
		public void Init(ParameterGroup parameters1) { Init(parameters1, 200F, 1050); }

		public void Init(ParameterGroup parameters1, float paramNameWidth, int totalWidth){
			ParameterGroup = parameters1;
			int nrows = ParameterGroup.Count;
			grid = new Grid{
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			grid.ColumnDefinitions.Add(new ColumnDefinition{
				Width = new GridLength(paramNameWidth, GridUnitType.Pixel)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition{
				Width = new GridLength(totalWidth - paramNameWidth, GridUnitType.Pixel)
			});
			grid.Margin = new Thickness(0);
			for (int i = 0; i < nrows; i++){
				float h = ParameterGroup[i].Visible ? ParameterGroup[i].Height : 0;
				grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(h, GridUnitType.Pixel)});
			}
			for (int i = 0; i < nrows; i++){
				AddParameter(ParameterGroup[i], i);
			}
			AddChild(grid);
			Name = "ParameterPanel";
			Margin = new Thickness(0, 3, 0, 3);
			VerticalAlignment = VerticalAlignment.Top;
			HorizontalAlignment = HorizontalAlignment.Left;
		}

		public void SetParameters(){
			ParameterGroup p1 = ParameterGroup;
			for (int i = 0; i < p1.Count; i++){
				p1[i].SetValueFromControl();
			}
		}

		private void AddParameter(Parameter p, int i){
			TextBlock txt1 = new TextBlock{
				Text = p.Name,
				FontSize = 12,
				FontWeight = FontWeights.Regular,
				VerticalAlignment = VerticalAlignment.Top
			};
			ToolTipService.SetShowDuration(txt1, 400000);
			if (!string.IsNullOrEmpty(p.Help)){
				txt1.ToolTip = StringUtils.ReturnAtWhitespace(p.Help);
			}
			Grid.SetColumn(txt1, 0);
			Grid.SetRow(txt1, i);
			UIElement c = p.GetControl() ?? new Control();
			Grid.SetColumn(c, 1);
			Grid.SetRow(c, i);
			grid.Children.Add(txt1);
			grid.Children.Add(c);
		}
		public void RegisterScrollViewer(ScrollViewer scrollViewer){
			foreach (var child in grid.Children){
				if (child is IScrollRegistrationTarget){
					((IScrollRegistrationTarget)child).RegisterScrollViewer(scrollViewer);
				}
			}
		}

		public void Enable() { grid.IsEnabled = true; }
		public void Disable() { grid.IsEnabled = false; }
	}
}