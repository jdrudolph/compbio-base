using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseLib.Forms.Table{
	public partial class SelectColumnsForm : Form{
		public SelectColumnsForm(){
			InitializeComponent();
		}

		public SelectColumnsForm(string[] names, IEnumerable<int> inds){
			InitializeComponent();
			listSelector1.Items.AddRange(names);
			foreach (int ind in inds){
				listSelector1.SetSelected(ind, true);
			}
		}

		public int[] SelectedIndices => listSelector1.SelectedIndices;
	}
}