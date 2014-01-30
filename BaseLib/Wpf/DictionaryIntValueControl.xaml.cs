using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Util;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for DictionaryIntValueControl.xaml
	/// </summary>
	public partial class DictionaryIntValueControl : UserControl {
		public DictionaryIntValueControl() {
			InitializeComponent();
		}
		public Dictionary<string, int> Value { get; set; }
		public string[] Keys { get; set; }
		public int Default { get; set; }
		public void Connect(int connectionId, object target){
			
		}

		private void EditButton_OnClick(object sender, RoutedEventArgs e){
			DictionaryIntValuePopup p = new DictionaryIntValuePopup();
			p.SetData(Value, Keys, Default);
			if (p.ShowDialog() == true) {
				Value = p.GetData(Keys);
				TextBox1.Text = StringVal;
			}
		}

		private string StringVal {
			get {
				List<string> result = new List<string>();
				foreach (KeyValuePair<string, int> pair in Value) {
					result.Add("[" + pair.Key + "," + pair.Value + "]");
				}
				return StringUtils.Concat(",", result);
			}
		}

	}
}
