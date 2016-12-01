using System;
using System.Windows.Forms;

namespace BaseLib.Forms{
	public partial class FileParameterControl : UserControl{
		private readonly string filter;
		private readonly bool save;
		private readonly Func<string, string> processFileName;
		public string FileName { get; set; }

		public FileParameterControl(string fileName, string filter, Func<string, string> processFileName, bool save){
			InitializeComponent();
			FileName = fileName;
			this.filter = filter;
			this.processFileName = processFileName;
			this.save = save;
			button1.Click += ButtonClick;
		}

		internal void ChooseFile(){
			if (save){
				SaveFileDialog ofd = new SaveFileDialog{FileName = FileName};
				if (!string.IsNullOrEmpty(filter)){
					ofd.Filter = filter;
				}
				if (ofd.ShowDialog() == DialogResult.OK){
					FileName = ofd.FileName;
					textBox1.Text = ofd.FileName;
				}
			} else{
				OpenFileDialog ofd = new OpenFileDialog();
				if (!string.IsNullOrEmpty(filter)){
					ofd.Filter = filter;
				}
				if (ofd.ShowDialog() == DialogResult.OK){
					string s = ofd.FileName;
					if (processFileName != null){
						s = processFileName(s);
					}
					FileName = s;
					textBox1.Text = s;
				}
			}
		}

		private void ButtonClick(object sender, EventArgs e){
			ChooseFile();
		}
	}
}