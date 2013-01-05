using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BasicLib.Param{
	public sealed partial class ParameterForm : Form{
		public ParameterForm(Parameters parameters, string title, string helpDescription, string helpOutput,
			IList<string> helpSuppls){
			InitializeComponent();
			parameterPanel1.Init(parameters);
			string text = "";
			if (!string.IsNullOrEmpty(helpDescription)){
				text += "\n\nDescription:\n " + helpDescription;
			}
			if (!string.IsNullOrEmpty(helpOutput)){
				text += "\n\nOutput:\n " + helpOutput;
			}
			if (helpSuppls != null){
				for (int i = 0; i < helpSuppls.Count; i++){
					if (!string.IsNullOrEmpty(helpSuppls[i])){
						text += "\n\nSuppl. table " + (i + 1) + ":\n " + helpSuppls[i];
					}
				}
			}
			helpTextBox.Text = text;
			Text = title;
		}

		public bool Ok { get; private set; }

		private void CancelButtonClick(object sender, EventArgs e){
			Close();
		}

		private void OkButtonClick(object sender, EventArgs e){
			Ok = true;
			parameterPanel1.SetParameters();
			Close();
		}
	}
}