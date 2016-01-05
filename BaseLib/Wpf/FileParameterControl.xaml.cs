using System;
using System.Windows;
using Microsoft.Win32;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for FileParameterControl.xaml
	/// </summary>
	public partial class FileParameterControl{
		public FileParameterControl(){
			InitializeComponent();
		}

		public string Filter { get; set; }
		public Func<string, string> ProcessFileName { get; set; }
		public string Text { get { return TextBox.Text; } set { TextBox.Text = value; } }
		public bool Save { get; set; }
		public void Connect(int connectionId, object target) {}

		private void ButtonClick(object sender, RoutedEventArgs e){
			if (Save){
				SaveFileDialog ofd = new SaveFileDialog{FileName = Text};
				if (!string.IsNullOrEmpty(Filter)){
					ofd.Filter = Filter;
				}
				if (ofd.ShowDialog() == true){
					TextBox.Text = ofd.FileName;
				}
			} else{
				OpenFileDialog ofd = new OpenFileDialog();
				if (!string.IsNullOrEmpty(Filter)){
					ofd.Filter = Filter;
				}
				if (ofd.ShowDialog() == true){
					string s = ofd.FileName;
					if (ProcessFileName != null){
						s = ProcessFileName(s);
					}
					TextBox.Text = s;
				}
			}
			WpfUtils.SetOkFocus(this);
		}
	}
}