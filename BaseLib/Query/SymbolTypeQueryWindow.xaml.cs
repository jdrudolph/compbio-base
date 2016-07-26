using System.Windows;
using System.Windows.Input;
using BaseLibS.Symbol;

namespace BaseLib.Query {
	/// <summary>
	/// Interaction logic for SymbolTypeQueryWindow.xaml
	/// </summary>
	public partial class SymbolTypeQueryWindow {
		private readonly bool hasNoSymbol;
		public SymbolTypeQueryWindow() : this(false) { }
		public SymbolTypeQueryWindow(bool hasNoSymbol) {
			InitializeComponent();
			this.hasNoSymbol = hasNoSymbol;
			if (hasNoSymbol) {
				ComboBox.Items.Add("<None>");
			}
			foreach (string name in SymbolType.allNames){
				ComboBox.Items.Add(name);
			}
			ComboBox.SelectedIndex = 0;
		}

		public int SymbolTypeIndex {
			get {
				int selInd = ComboBox.SelectedIndex;
				return hasNoSymbol ? selInd - 1 : selInd;
			}
		}
		private void CancelButton_OnClick(object sender, RoutedEventArgs e) {
			DialogResult = false;
			Close();
		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e) {
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
