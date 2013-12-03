using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for FileParameterControl.xaml
	/// </summary>
	public partial class FileParameterControl : System.Windows.Controls.UserControl{
		public FileParameterControl(){
			InitializeComponent();
		}

		public string Filter { get; set; }
		public string Text { get { return textBox.Text; } set { textBox.Text = value; } }
		public bool Save { get; set; }
		public void Connect(int connectionId, object target) {}

		private void ButtonClick(object sender, RoutedEventArgs e) {
			if (Save){
				Microsoft.Win32.SaveFileDialog ofd = new Microsoft.Win32.SaveFileDialog{FileName = Text};
				if (!string.IsNullOrEmpty(Filter)){
					ofd.Filter = Filter;
				}
				if (ofd.ShowDialog() == true){
					textBox.Text = ofd.FileName;
				}
			} else{
				Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
				if (!string.IsNullOrEmpty(Filter)){
					ofd.Filter = Filter;
				}
				if (ofd.ShowDialog() == true){
					textBox.Text = ofd.FileName;
				}
			}
		}
	}
}