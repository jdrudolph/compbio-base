using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for FolderParameterControl.xaml
	/// </summary>
	public partial class FolderParameterControl : UserControl{
		public FolderParameterControl(){
			InitializeComponent();
		}

		private void ButtonClick(object sender, RoutedEventArgs e) {
			//TODO
			//FolderBrowserDialog ofd = new FolderBrowserDialog();
			//if (ofd.ShowDialog() == true) {
			//	textBox.Text = ofd.SelectedPath;
			//}
		}

		public string Text { get { return textBox.Text; } set { textBox.Text = value; } }
		public void Connect(int connectionId, object target) {}
	}
}
