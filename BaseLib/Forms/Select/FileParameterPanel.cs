using System;
using System.Windows.Forms;

namespace BaseLib.Forms.Select{
	internal partial class FileParameterPanel : UserControl{
		public string Filter { get; set; }
		public bool Save { get; set; }

		public FileParameterPanel(){
			InitializeComponent();
		}

		private void ButtonClick(object sender, EventArgs e){
			if (Save){
				SaveFileDialog ofd = new SaveFileDialog{FileName = Text};
				if (!string.IsNullOrEmpty(Filter)){
					ofd.Filter = Filter;
				}
				if (ofd.ShowDialog() == DialogResult.OK){
					textBox.Text = ofd.FileName;
				}
			} else{
				OpenFileDialog ofd = new OpenFileDialog();
				if (!string.IsNullOrEmpty(Filter)){
					ofd.Filter = Filter;
				}
				if (ofd.ShowDialog() == DialogResult.OK){
					textBox.Text = ofd.FileName;
				}
			}
		}

		public override string Text { get { return textBox.Text; } set { textBox.Text = value; } }
	}
}