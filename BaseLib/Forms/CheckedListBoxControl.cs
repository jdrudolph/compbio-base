using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseLib.Forms{
	public partial class CheckedListBoxControl : UserControl{
		public CheckedListBoxControl(){
			InitializeComponent();
			listView1.ItemSelectionChanged +=
				(sender, args) =>{
					ItemCheck?.Invoke(this, new ItemCheckEventArgs(args.ItemIndex, CheckState.Checked, CheckState.Unchecked));
				};
		}

		public event EventHandler<ItemCheckEventArgs> ItemCheck;

		public void Add(string text){
			listView1.Items.Add(text);
		}

		public ICollection CheckedIndices => listView1.CheckedIndices;

		public IEnumerable<string> CheckedItems{
			get{
				List<string> result = new List<string>();
				foreach (var cb in listView1.CheckedItems){
					result.Add(cb.ToString());
				}
				return result;
			}
		}

		public int Count => listView1.Items.Count;

		public void SetItemChecked(int i, bool b){
			listView1.Items[i].Checked = b;
		}
	}
}