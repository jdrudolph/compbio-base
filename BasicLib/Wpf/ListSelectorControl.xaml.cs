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
	/// Interaction logic for ListSelectorControl.xaml
	/// </summary>
	public partial class ListSelectorControl : UserControl {
		public ListSelectorControl() {
			InitializeComponent();
		}

		public int[] SelectedIndices { get; set; }
		public object Items { get; set; }
		public bool Repeats { get; set; }
		public bool HasMoveButtons { get; set; }
		public void Connect(int connectionId, object target){
			
		}

		public void SetDefaultSelectors(List<string> defaultSelectionNames, List<string[]> defaultSelections){
			
		}
	}
}
