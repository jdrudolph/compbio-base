using System.Windows;

namespace BaseLib.Query{
	/// <summary>
	/// Interaction logic for StringQueryWindow.xaml
	/// </summary>
	public partial class DoubleQueryWindow : Window{
		public DoubleQueryWindow(double value){
			InitializeComponent();
			TextBox.Text = "" + value;
		}

		public double Value { get { return double.Parse(TextBox.Text); } }

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