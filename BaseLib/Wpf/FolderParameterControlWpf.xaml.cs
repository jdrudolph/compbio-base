using System.Windows;
using System.Windows.Forms;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for FolderParameterControl.xaml
	/// </summary>
	public partial class FolderParameterControlWpf{
		public FolderParameterControlWpf(){
			InitializeComponent();
		}

		private void ButtonClick(object sender, RoutedEventArgs e){
			FolderBrowserDialog ofd = new FolderBrowserDialog();
			if (ofd.ShowDialog() == DialogResult.OK){
				textBox.Text = ofd.SelectedPath;
			}
		}

		public string Text { get { return textBox.Text; } set { textBox.Text = value; } }
		public void Connect(int connectionId, object target) {}
	}
}