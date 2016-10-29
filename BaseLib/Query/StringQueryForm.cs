using System;
using System.Windows.Forms;

namespace BaseLib.Query{
	public partial class StringQueryForm : Form{
		public StringQueryForm(string value){
			InitializeComponent();
			okButton.Click += OkButtonOnClick;
			cancelButton.Click += CancelButtonOnClick;
			textBox1.Text = value;
			textBox1.KeyDown += TextBox1OnKeyDown;
			ActiveControl = textBox1;
		}

		public string Value => textBox1.Text;

		private void TextBox1OnKeyDown(object sender, KeyEventArgs keyEventArgs){
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelButtonOnClick(object sender, EventArgs eventArgs){
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void OkButtonOnClick(object sender, EventArgs eventArgs){
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}