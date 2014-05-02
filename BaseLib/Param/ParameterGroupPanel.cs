using System.Windows;
using System.Windows.Controls;
using BaseLib.Util;

namespace BaseLib.Param{
	public partial class ParameterGroupPanel : UserControl{
		public ParameterGroup ParameterGroup { get; private set; }
		private Grid tableLayoutPanel;
		public void Init(ParameterGroup parameters1) { Init(parameters1, 200F, 1050); }

		public void Init(ParameterGroup parameters1, float paramNameWidth, int totalWidth){
			ParameterGroup = parameters1;
			int nrows = ParameterGroup.Count;
			tableLayoutPanel = new Grid{
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top
			};
			tableLayoutPanel.ColumnDefinitions.Add(new ColumnDefinition{
				Width = new GridLength(paramNameWidth, GridUnitType.Pixel)
			});
			tableLayoutPanel.ColumnDefinitions.Add(new ColumnDefinition{
				Width = new GridLength(totalWidth - paramNameWidth, GridUnitType.Pixel)
			});
			tableLayoutPanel.Margin = new Thickness(0);
			for (int i = 0; i < nrows; i++){
				float h = ParameterGroup[i].Visible ? ParameterGroup[i].Height : 0;
				tableLayoutPanel.RowDefinitions.Add(new RowDefinition{Height = new GridLength(h, GridUnitType.Pixel)});
			}
			for (int i = 0; i < nrows; i++){
				AddParameter(ParameterGroup[i], i);
			}
			AddChild(tableLayoutPanel);
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
			if (p.Help != null){
				txt1.ToolTip = StringUtils.ReturnAtWhitespace(p.Help);
			}
			Grid.SetColumn(txt1, 0);
			Grid.SetRow(txt1, i);
			UIElement c = p.GetControl() ?? new Control();
			Grid.SetColumn(c, 1);
			Grid.SetRow(c, i);
			tableLayoutPanel.Children.Add(txt1);
			tableLayoutPanel.Children.Add(c);
		}

		public void Enable() { tableLayoutPanel.IsEnabled = true; }
		public void Disable() { tableLayoutPanel.IsEnabled = false; }
	}
}