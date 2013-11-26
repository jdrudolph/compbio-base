using System.Collections.Generic;
using System.Windows.Controls;

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
	}
}
