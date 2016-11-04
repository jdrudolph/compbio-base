using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BaseLib.Wpf{
	public partial class CheckedListBoxControl : UserControl{
		public CheckedListBoxControl(){
			InitializeComponent();
		}

		public event EventHandler<ItemCheckEventArgs> ItemCheck;

		public void Add(string text){
			CheckBox cb = new CheckBox{Text = text, Dock = DockStyle.Fill};
			int index = listBox1.Items.Count;
			cb.CheckedChanged +=
				(sender, e) => { ItemCheck?.Invoke(this, new ItemCheckEventArgs(index, CheckState.Checked, CheckState.Unchecked)); };
			listBox1.Items.Add(cb);
		}

		public ICollection CheckedIndices{
			get{
				List<int> result = new List<int>();
				for (int i = 0; i < listBox1.Items.Count; i++){
					CheckBox lbi = (CheckBox) listBox1.Items[i];
					CheckBox cb = lbi;
					if (cb.Checked){
						result.Add(i);
					}
				}
				return result;
			}
		}

		public IEnumerable<string> CheckedItems{
			get{
				List<string> result = new List<string>();
				foreach (CheckBox cb in listBox1.Items){
					if (cb.Checked){
						result.Add(cb.Text);
					}
				}
				return result;
			}
		}

		public int Count => listBox1.Items.Count;

		public void SetItemChecked(int i, bool b){
			CheckBox cb = (CheckBox)listBox1.Items[i];
			cb.Checked = b;
		}
	}
}