using System.Windows;

namespace BaseLib.Query {
	/// <summary>
	/// Interaction logic for IntQueryWindow.xaml
	/// </summary>
	public partial class IntQueryWindow : Window {
		public IntQueryWindow(int value, int min, int max) {
			InitializeComponent();
			NumUpDown.Minimum = min;
			NumUpDown.Maximum = max;
			NumUpDown.Value = value;
		}

		public int Value {
			get { return (int)NumUpDown.Value; }
		}

		private void CancelButtonClick(object sender, System.EventArgs e) {
			DialogResult = false;
			Close();
		}

		private void OkButtonClick(object sender, System.EventArgs e) {
			DialogResult = true;
			Close();
		}
	}
}
