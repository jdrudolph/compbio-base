using System.Collections.Generic;
using System.Windows;

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
	}
}
