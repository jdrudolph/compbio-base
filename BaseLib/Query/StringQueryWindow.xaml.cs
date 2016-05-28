using System.Windows;
using System.Windows.Input;

namespace BaseLib.Query{
	/// <summary>
	/// Interaction logic for StringQueryWindow.xaml
	/// </summary>
	public partial class StringQueryWindow{
		public StringQueryWindow(string value){
			InitializeComponent();
			TextBox.Text = value;
			TextBox.SelectAll();
			TextBox.Focus();
		}

		public string Value => TextBox.Text;

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
	}
}