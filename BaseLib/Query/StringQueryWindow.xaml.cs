using System.Windows;

namespace BaseLib.Query{
	/// <summary>
	/// Interaction logic for StringQueryWindow.xaml
	/// </summary>
	public partial class StringQueryWindow : Window{
		public StringQueryWindow(string value){
			InitializeComponent();
			TextBox.Text = value;
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
	}
}