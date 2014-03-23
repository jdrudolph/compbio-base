using System.Windows.Input;

namespace BaseLib.Query {
	/// <summary>
	/// Interaction logic for IntQueryWindow.xaml
	/// </summary>
	public partial class IntQueryWindow {
		public IntQueryWindow(int value, int min, int max) {
			InitializeComponent();
			NumUpDown.Minimum = min;
			NumUpDown.Maximum = max;
			NumUpDown.Value = value;
			NumUpDown.Focus();
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

		private void OnKeyDownHandler(object sender, KeyEventArgs e) {
			if (e.Key == Key.Return) {
				DialogResult = true;
				Close();
			}
		}
	}
}
