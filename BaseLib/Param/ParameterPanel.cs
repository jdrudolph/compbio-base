using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Param{
	public class ParameterPanel : UserControl{
		public Parameters Parameters { get; private set; }
		private Grid grid;
		public bool Collapsible { get; set; }
		public bool CollapsedDefault { get; set; }
		private ParameterGroupPanel[] parameterGroupPanels;

		public ParameterPanel(){
			Collapsible = true;
		}

		public float Init(Parameters parameters1){
			return Init(parameters1, 250F, 1050);
		}

		public float Init(Parameters parameters1, float paramNameWidth, int totalWidth){
			Parameters = parameters1;
			int nrows = Parameters.GroupCount;
			parameterGroupPanels = new ParameterGroupPanel[nrows];
			grid = new Grid();
			grid.ColumnDefinitions.Clear();
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.Name = "tableLayoutPanel";
			grid.RowDefinitions.Clear();
			float totalHeight = 0;
			for (int i = 0; i < nrows; i++){
				float h = parameters1.GetGroup(i).Height + 26;
				grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(h, GridUnitType.Pixel)});
				totalHeight += h + 6;
			}
			grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			grid.Width = totalWidth;
			grid.Height = (int) totalHeight;
			for (int i = 0; i < nrows; i++){
				AddParameterGroup(parameters1.GetGroup(i), i, paramNameWidth, totalWidth);
			}
			Content = grid;
			Name = "ParameterPanel";
			Width = totalWidth;
			Height = totalHeight;
			return totalHeight;
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
				grid.Children.Add(pgp);
			} else{
				GroupBox gb = new GroupBox{Header = p.Name, Margin = new Thickness(3), Padding = new Thickness(3), Content = pgp};
				Grid.SetColumn(gb, 0);
				Grid.SetRow(gb, i);
				grid.Children.Add(gb);
			}
		}

		public void RegisterScrollViewer(ScrollViewer scrollViewer){
			foreach (ParameterGroupPanel panel in parameterGroupPanels){
				panel.RegisterScrollViewer(scrollViewer);
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