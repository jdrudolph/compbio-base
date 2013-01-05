using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms;
using BasicLib.Forms.Base;
using BasicLib.Forms.Help;

namespace BasicLib.Param{
	public partial class ParameterGroupPanel : UserControl{
		public ParameterGroup ParameterGroup { get; private set; }
		private TableLayoutPanel tableLayoutPanel;

		public void Init(ParameterGroup parameters1){
			Init(parameters1, 250F, 1000);
		}

		public void Init(ParameterGroup parameters1, float paramNameWidth, int totalWidth){
			ParameterGroup = parameters1;
			int nrows = ParameterGroup.Count;
			tableLayoutPanel = new TableLayoutPanel();
			SuspendLayout();
			tableLayoutPanel.ColumnCount = 2;
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, paramNameWidth));
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel.Dock = DockStyle.Fill;
			tableLayoutPanel.Padding = new Padding(0);
			tableLayoutPanel.Margin = new Padding(0);
			tableLayoutPanel.Location = new Point(0, 0);
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowCount = nrows;
			float totalHeight = 0;
			for (int i = 0; i < nrows; i++){
				float h = ParameterGroup[i].Visible ? ParameterGroup[i].Height : 0;
				tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
				totalHeight += h;
			}
			tableLayoutPanel.Size = new Size(totalWidth, (int) totalHeight);
			tableLayoutPanel.TabIndex = 0;
			for (int i = 0; i < nrows; i++){
				AddParameter(ParameterGroup[i], i);
			}
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel);
			Name = "ParameterPanel";
			Size = new Size(totalWidth, (int) totalHeight);
			Dock = DockStyle.Fill;
			Margin = new Padding(3);
			Padding = new Padding(3);
			ResumeLayout(true);
		}

		public void SetParameters(){
			ParameterGroup p1 = ParameterGroup;
			for (int i = 0; i < p1.Count; i++){
				p1[i].SetValueFromControl();
			}
		}

		private void AddParameter(Parameter p, int i){
			if (!string.IsNullOrEmpty(p.Help)){
				Panel pa = new Panel{Dock = DockStyle.Fill, Width = 245, Margin = new Padding(0), Padding = new Padding(0)};
				pa.Controls.Add(CreateLabel(p.Name, p.Help));
				tableLayoutPanel.Controls.Add(pa, 0, i);
			} else{
				Panel pa = new Panel{Dock = DockStyle.Fill};
				Label l = new Label
				{Width = 245, Text = p.Name, Location = new Point(2, 3), Anchor = AnchorStyles.Top | AnchorStyles.Left};
				pa.Controls.Add(l);
				tableLayoutPanel.Controls.Add(pa, 0, i);
			}
			Control c = p.GetControl();
			c.Width = 500;
			c.Dock = DockStyle.Fill;
			tableLayoutPanel.Controls.Add(c, 1, i);
		}

		private static Control CreateLabel(string name, string help){
			return new HelpLabel{
				Text = name, HelpText = help, BackColor = Color.Empty, Width = 245, Location = new Point(2, 10),
				Anchor = AnchorStyles.Top | AnchorStyles.Left
			};
		}

		public void Enable(){
			tableLayoutPanel.Enabled = true;
		}

		public void Disable(){
			tableLayoutPanel.Enabled = false;
		}
	}
}