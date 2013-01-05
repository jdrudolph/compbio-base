using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms;
using BasicLib.Forms.Select;

namespace BasicLib.Param{
	public partial class ParameterPanel : UserControl{
		public Parameters Parameters { get; private set; }
		private TableLayoutPanel tableLayoutPanel;
		public bool Collapsible { get; set; }
		public bool CollapsedDefault { get; set; }
		private ParameterGroupPanel[] parameterGroupPanels;

		public ParameterPanel(){
			InitializeComponent();
			Collapsible = true;
		}

		public void Init(Parameters parameters1){
			Init(parameters1, 250F, 1000);
		}

		public void Init(Parameters parameters1, float paramNameWidth, int totalWidth){
			Parameters = parameters1;
			int nrows = Parameters.GroupCount;
			parameterGroupPanels = new ParameterGroupPanel[nrows];
			tableLayoutPanel = new TableLayoutPanel();
			SuspendLayout();
			tableLayoutPanel.ColumnCount = 1;
			tableLayoutPanel.ColumnStyles.Clear();
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel.Dock = DockStyle.None;
			tableLayoutPanel.Location = new Point(0, 0);
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowCount = nrows + 1;
			tableLayoutPanel.RowStyles.Clear();
			float totalHeight = 0;
			for (int i = 0; i < nrows; i++){
				float h = parameters1.GetGroup(i).Height + 26;
				tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, h));
				totalHeight += h + 6;
			}
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
			tableLayoutPanel.Size = new Size(totalWidth, (int) totalHeight);
			tableLayoutPanel.TabIndex = 0;
			for (int i = 0; i < nrows; i++){
				AddParameterGroup(parameters1.GetGroup(i), i, paramNameWidth, totalWidth);
			}
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Clear();
			Controls.Add(tableLayoutPanel);
			Name = "ParameterPanel";
			Size = new Size(totalWidth, (int) totalHeight);
			ResumeLayout(true);
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
				tableLayoutPanel.Controls.Add(pgp, 0, i);
			} else{
				Size s = new Size(pgp.Size.Width, pgp.Size.Height + 25);
				Control gb = Collapsible
					? new CollapsibleGroupBox{
						Dock = DockStyle.Fill, Text = p.Name, Margin = new Padding(3), Padding = new Padding(3), Size = s, FullSize = s,
						IsCollapsed = p.CollapsedDefault
					}
					: new GroupBox{Dock = DockStyle.Fill, Text = p.Name, Margin = new Padding(3), Padding = new Padding(3)};
				gb.Controls.Add(pgp);
				tableLayoutPanel.Controls.Add(gb, 0, i);
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