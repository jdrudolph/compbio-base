using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BaseLib.Query {
	/// <summary>
	/// Interaction logic for SingleChoiceQueryWindow.xaml
	/// </summary>
	public partial class SingleChoiceQueryWindow : Window {
		public SingleChoiceQueryWindow(IEnumerable<string> choice) {
			InitializeComponent();
			foreach (string s in choice){
				ComboBox.Items.Add(s);
			}
			ComboBox.SelectedIndex = 0;
			ComboBox.Focus();
		}

		public string SelectedText { get { return ComboBox.Text; } }
		public string Label { set { Label1.Text = value; } }

		private void CancelButton_OnClick(object sender, RoutedEventArgs e){
			DialogResult = false;
			Close();
		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e){
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
