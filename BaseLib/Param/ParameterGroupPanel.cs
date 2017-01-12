using System.Windows.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	public class ParameterGroupPanel : UserControl{
		public ParameterGroup ParameterGroup { get; private set; }
		private TableLayoutPanel grid;

		public void Init(ParameterGroup parameters1){
			Init(parameters1, 200F, 1050);
		}

		public void Init(ParameterGroup parameters1, float paramNameWidth, int totalWidth){
			ParameterGroup = parameters1;
			int nrows = ParameterGroup.Count;
			grid = new TableLayoutPanel();
			//{ HorizontalAlignment = System.Windows.HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
			grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, paramNameWidth));
			grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, totalWidth - paramNameWidth));
			grid.Margin = new Padding(0);
			for (int i = 0; i < nrows; i++){
				float h = ParameterGroup[i].Visible ? ParameterGroup[i].Height : 0;
				grid.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
			}
			for (int i = 0; i < nrows; i++){
				AddParameter(ParameterGroup[i], i);
			}
			Controls.Add(grid);
			Name = "ParameterPanel";
			Margin = new Padding(0, 3, 0, 3);
			grid.Dock = DockStyle.Fill;
			Dock = DockStyle.Fill;
		}

		public void SetParameters(){
			ParameterGroup p1 = ParameterGroup;
			for (int i = 0; i < p1.Count; i++){
				p1[i].SetValueFromControl();
			}
		}

		private void AddParameter(Parameter p, int i){
			Label txt1 = new Label{Text = p.Name};
			//ToolTipService.SetShowDuration(txt1, 400000);
			//if (!string.IsNullOrEmpty(p.Help)){
			//txt1.ToolTip = StringUtils.ReturnAtWhitespace(p.Help);
			//}
			object o = p.CreateControl();
			if (o is Control){
				Control c = (Control)o;
				c.Dock = DockStyle.Fill;
				c.Margin = new Padding(0);
				c.Visible = p.Visible;
				grid.Controls.Add(c, 1, i);
			}
			txt1.Dock = DockStyle.Fill;
			txt1.Visible = p.Visible;
			grid.Controls.Add(txt1, 0, i);
		}

		//public void RegisterScrollViewer(System.Windows.Controls.ScrollViewer scrollViewer){
		//	foreach (var child in grid.Controls){
		//		(child as IScrollRegistrationTarget)?.RegisterScrollViewer(scrollViewer);
		//	}
		//}

		public void Enable(){
			grid.Enabled = true;
		}

		public void Disable(){
			grid.Enabled = false;
		}
	}
}