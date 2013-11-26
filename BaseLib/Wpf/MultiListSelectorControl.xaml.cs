using System.Collections.Generic;
using System.Windows.Controls;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for MultiListSelectorControl.xaml
	/// </summary>
	public partial class MultiListSelectorControl : UserControl {
		public MultiListSelectorControl() {
			InitializeComponent();
		}

		public int[][] SelectedIndices { get; set; }

		public void Connect(int connectionId, object target){
			
		}

		public void Init(IList<string> values, IList<string> bins){
			
		}
	}
}
