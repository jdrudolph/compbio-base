using System;
using System.Windows.Forms;

namespace BasicLib.Forms.Select{
	internal partial class FolderParameterPanel : UserControl{
		public FolderParameterPanel(){
			InitializeComponent();
		}

		private void ButtonClick(object sender, EventArgs e){
			FolderBrowserDialog ofd = new FolderBrowserDialog();
			if (ofd.ShowDialog() == DialogResult.OK){
				textBox.Text = ofd.SelectedPath;
			}
		}

		public override string Text { get { return textBox.Text; } set { textBox.Text = value; } }
	}
}