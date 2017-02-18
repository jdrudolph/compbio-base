using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Graphic;
using BaseLibS.Graph;
using BaseLibS.Param;

namespace BaseLib.Param{
	public partial class ParameterForm : Form{
		private readonly Action helpAction;

		public ParameterForm(Parameters parameters, string title, string helpDescription, string helpOutput,
			IList<string> helpSuppls) : this(parameters, title, helpDescription, helpOutput, helpSuppls, null){}

		public ParameterForm(Parameters parameters, string title, string helpDescription, string helpOutput,
			IList<string> helpSuppls, Action helpAction){
			InitializeComponent();
			this.helpAction = helpAction;
			if (helpAction == null){
				helpButton.Width = 0;
			}
			okButton.Click += OkButtonClick;
			cancelButton.Click += CancelButtonClick;
			helpButton.Click += HelpButtonClick;
			helpButton.Image = new Bitmap(GraphUtils.ToBitmap(Bitmap2.GetImage("help.png")), 18, 18);
			KeyDown += OnKeyDownHandler;
			Height = (int) (parameterPanel1.Init(parameters) + 65);
			helpPanel.Controls.Clear();
			List<string> titles = new List<string>();
			List<string> description = new List<string>();
			if (!string.IsNullOrEmpty(helpDescription)){
				titles.Add("Description");
				description.Add(helpDescription);
			}
			if (!string.IsNullOrEmpty(helpOutput)){
				titles.Add(" - ");
				description.Add(null);
				titles.Add("Output");
				description.Add(helpOutput);
			}
			if (helpSuppls != null){
				for (int i = 0; i < helpSuppls.Count; i++){
					if (!string.IsNullOrEmpty(helpSuppls[i])){
						titles.Add(" - ");
						description.Add(null);
						titles.Add("Suppl. table " + (i + 1));
						description.Add(helpSuppls[i]);
					}
				}
			}
			TableLayoutPanel g = new TableLayoutPanel();
			g.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			for (int i = 0; i < titles.Count; i++){
				g.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 50));
			}
			g.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
			for (int i = 0; i < titles.Count; i++){
				Label tb = new Label{Text = titles[i]};
				//ToolTipService.SetShowDuration(tb, 400000);
				//if (description[i] != null){
				//	tb.ToolTip = new ToolTip{
				//		Content = StringUtils.Concat("\n", StringUtils.Wrap(description[i], 75))
				//	};
				//}
				g.Controls.Add(tb, i + 1, 0);
			}
			helpPanel.Controls.Add(g);
			Text = title;
		}

		public sealed override string Text{
			get { return base.Text; }
			set { base.Text = value; }
		}

		public void FocusOkButton(){
			//Invoke(new Action(delegate { okButton.Focus(); }));
			okButton.Focus();
		}

		private void OkButtonClick(object sender, EventArgs e){
			DialogResult = DialogResult.OK;
			parameterPanel1.SetParameters();
			Close();
		}

		private void CancelButtonClick(object sender, EventArgs e){
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void OnKeyDownHandler(object sender, KeyEventArgs e){
			if ((e.KeyData & Keys.Return) != Keys.Return){
				DialogResult = DialogResult.OK;
				parameterPanel1.SetParameters();
				Close();
			}
		}

		private void HelpButtonClick(object sender, EventArgs e){
			helpAction?.Invoke();
		}
	}
}