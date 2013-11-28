using System.Collections.Generic;
using System.Windows.Controls;
using BaseLib.Util;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for MultiListSelectorControl.xaml
	/// </summary>
	public partial class MultiListSelectorControl : UserControl {
		private MultiListSelectorSubSelectionControl[] subSelection;
		public IList<string> items;

		public MultiListSelectorControl() {
			InitializeComponent();
		}

		public void Init(IList<string> items1) {
			//TODO
		}


		public int[][] SelectedIndices { get; set; }

		public void Connect(int connectionId, object target){
			
		}

		public void Init(IList<string> values, IList<string> bins){
			
		}

		public void SetSelected(int selectorInd, int itemInd, bool b) {
			//TODO
		}

		public int[] GetSelectedIndices(int selectorInd) {
			string[] sel = subSelection[selectorInd].SelectedStrings;
			int[] result = new int[sel.Length];
			for (int i = 0; i < result.Length; i++) {
				result[i] = ArrayUtils.IndexOf(items, sel[i]);
			}
			return result;
		}

	}
}
