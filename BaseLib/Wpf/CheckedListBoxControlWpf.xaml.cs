using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Forms;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for CheckedListBoxControl.xaml
	/// </summary>
	public partial class CheckedListBoxControlWpf{
		public CheckedListBoxControlWpf() { InitializeComponent(); }
		public event EventHandler<ItemCheckEventArgs> ItemCheck;

		public void Add(string text){
			System.Windows.Controls.CheckBox cb = new System.Windows.Controls.CheckBox{Content = text};
			int index = ListBox1.Items.Count;
			cb.Checked += (sender, e) =>{
				ItemCheck?.Invoke(this, new ItemCheckEventArgs(index, CheckState.Checked, CheckState.Unchecked));
			};
			ListBox1.Items.Add(new ListBoxItem{Content = cb});
		}

		public ICollection CheckedIndices{
			get{
				List<int> result = new List<int>();
				for (int i = 0; i < ListBox1.Items.Count; i++){
					ListBoxItem lbi = (ListBoxItem) ListBox1.Items[i];
					System.Windows.Controls.CheckBox cb = (System.Windows.Controls.CheckBox) lbi.Content;
					if (cb.IsChecked == true){
						result.Add(i);
					}
				}
				return result;
			}
		}

		public IEnumerable<string> CheckedItems{
			get{
				List<string> result = new List<string>();
				foreach (System.Windows.Controls.CheckBox cb in ListBox1.Items){
					if (cb.IsChecked == true){
						result.Add(cb.Content.ToString());
					}
				}
				return result;
			}
		}

		public int Count => ListBox1.Items.Count;

		public void SetItemChecked(int i, bool b){
			ListBoxItem lbi = (ListBoxItem) ListBox1.Items[i];
			System.Windows.Controls.CheckBox cb = (System.Windows.Controls.CheckBox) lbi.Content;
			cb.IsChecked = b;
		}
	}
}