using System.Windows;
using System.Windows.Input;

namespace BaseLib.Query{
	/// <summary>
	/// Interaction logic for StringQueryWindow.xaml
	/// </summary>
	public partial class FolderQueryWindow{
		public FolderQueryWindow(string value){
			InitializeComponent();
			TextBox.Text = value;
			TextBox.SelectAll();
			TextBox.Focus();
		}

		public string Value { get { return TextBox.Text; } }

		private void CancelButton_OnClick(object sender, RoutedEventArgs e){
			DialogResult = false;
			Close();
		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e){
			DialogResult = true;
			Close();
		}

		private void OnKeyDownHandler(object sender, KeyEventArgs e){
			if (e.Key == Key.Return){
				DialogResult = true;
				Close();
			}
		}

		private void BrowseButton_OnClick(object sender, RoutedEventArgs e){
			System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
			if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK){
				TextBox.Text = fbd.SelectedPath;
			}
		}
	}
}