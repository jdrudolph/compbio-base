using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for ListSelectorControl.xaml
	/// </summary>
	public partial class ListSelectorControl : UserControl{
		public ListSelectorControl(){
			InitializeComponent();
		}

		public int[] SelectedIndices { get; set; }
		public Collection<string> Items { get; set; }
		public bool Repeats { get; set; }
		public bool HasMoveButtons { get; set; }
		public void Connect(int connectionId, object target) {}
		public void SetDefaultSelectors(List<string> defaultSelectionNames, List<string[]> defaultSelections) {}
	}
}