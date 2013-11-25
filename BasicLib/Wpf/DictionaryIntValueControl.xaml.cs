using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicLib.Wpf {
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
