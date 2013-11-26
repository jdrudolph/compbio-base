using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Param{
	public class ParameterPanel : UserControl{
		public Parameters Parameters { get; private set; }
		private Grid tableLayoutPanel;
		public bool Collapsible { get; set; }
		public bool CollapsedDefault { get; set; }
		private ParameterGroupPanel[] parameterGroupPanels;

		public ParameterPanel(){
			Collapsible = true;
		}

		public void Init(Parameters parameters1){
			Init(parameters1, 250F, 1000);
		}

		public void Init(Parameters parameters1, float paramNameWidth, int totalWidth){
			Parameters = parameters1;
			int nrows = Parameters.GroupCount;
			parameterGroupPanels = new ParameterGroupPanel[nrows];
			tableLayoutPanel = new Grid();
			tableLayoutPanel.ColumnDefinitions.Clear();
			tableLayoutPanel.ColumnDefinitions.Add(new ColumnDefinition());
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowDefinitions.Clear();
			float totalHeight = 0;
			for (int i = 0; i < nrows; i++){
				float h = parameters1.GetGroup(i).Height + 26;
				tableLayoutPanel.RowDefinitions.Add(new RowDefinition{Height = new GridLength(h, GridUnitType.Pixel)});
				totalHeight += h + 6;
			}
			tableLayoutPanel.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			tableLayoutPanel.Width = totalWidth;
			tableLayoutPanel.Height = (int) totalHeight;
			for (int i = 0; i < nrows; i++){
				AddParameterGroup(parameters1.GetGroup(i), i, paramNameWidth, totalWidth);
			}
			//TODO
			//Controls.Clear();
			AddChild(tableLayoutPanel);
			Name = "ParameterPanel";
			Width = totalWidth;
			Height = (int) totalHeight;
		}

		public void SetParameters(){
			Parameters p1 = Parameters;
			for (int i = 0; i < p1.GroupCount; i++){
				p1.GetGroup(i).SetParametersFromConrtol();
			}
		}

		private void AddParameterGroup(ParameterGroup p, int i, float paramNameWidth, int totalWidth){
			ParameterGroupPanel pgp = new ParameterGroupPanel();
			parameterGroupPanels[i] = pgp;
			pgp.Init(p, paramNameWidth, totalWidth);
			if (p.Name == null){
				Grid.SetColumn(pgp, 0);
				Grid.SetRow(pgp, i);
				tableLayoutPanel.Children.Add(pgp);
			} else{
				GroupBox gb = new GroupBox{Header = p.Name, Margin = new Thickness(3), Padding = new Thickness(3), Content = pgp};
				Grid.SetColumn(gb, 0);
				Grid.SetRow(gb, i);
				tableLayoutPanel.Children.Add(gb);
			}
		}

		public void Disable(){
			foreach (ParameterGroupPanel parameterGroupPanel in parameterGroupPanels){
				parameterGroupPanel.Disable();
			}
		}

		public void Enable(){
			foreach (ParameterGroupPanel parameterGroupPanel in parameterGroupPanels){
				parameterGroupPanel.Enable();
			}
		}
	}
}