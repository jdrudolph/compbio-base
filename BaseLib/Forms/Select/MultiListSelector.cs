using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaseLib.Util;

namespace BaseLib.Forms.Select{
	public partial class MultiListSelector : UserControl{
		public event EventHandler SelectionChanged;
		private MultiListSelectorSubSelection[] subSelection;
		public IList<string> items;

		public MultiListSelector(){
			InitializeComponent();
			ResizeRedraw = true;
		}

		public void Init(IList<string> items1){
			items = items1;
			allListBox.Items.Clear();
			foreach (string s in items1){
				allListBox.Items.Add(s);
			}
		}

		public void Init(IList<string> items1, IList<string> selectorNames){
			items = items1;
			foreach (string s in items1){
				allListBox.Items.Add(s);
			}
			int n = selectorNames.Count;
			TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
			subSelection = new MultiListSelectorSubSelection[n];
			for (int i = 0; i < n; i++){
				subSelection[i] = new MultiListSelectorSubSelection{
					Dock = DockStyle.Fill, Location = new System.Drawing.Point(3, 3), MultiListSelector = null,
					Name = "multiListSelectorSubSelection1", Size = new System.Drawing.Size(234, 183), TabIndex = 1,
					Text = selectorNames[i]
				};
				subSelection[i].MultiListSelector = this;
			}
			tableLayoutPanel2.ColumnCount = 1;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new System.Drawing.Point(188, 0);
			tableLayoutPanel2.Margin = new Padding(0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = n;
			for (int i = 0; i < n; i++){
				tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
				tableLayoutPanel2.Controls.Add(subSelection[i], 0, i);
			}
			tableLayoutPanel2.Size = new System.Drawing.Size(240, 379);
			tableLayoutPanel2.TabIndex = 2;
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
		}

		internal ListBox AllListBox { get { return allListBox; } }
		public int[][] SelectedIndices{
			get{
				int[][] result = new int[subSelection.Length][];
				for (int i = 0; i < result.Length; i++){
					result[i] = GetSelectedIndices(i);
				}
				return result;
			}
			set{
				ClearSelection();
				for (int i = 0; i < value.Length; i++){
					foreach (int x in value[i]){
						SetSelected(i, x, true);
					}
				}
			}
		}

		private void ClearSelection(){
			foreach (MultiListSelectorSubSelection t in subSelection){
				AllListBox.Items.AddRange(t.SelectedListBox.Items);
				t.SelectedListBox.Items.Clear();
			}
		}

		internal void SelectionHasChanged(MultiListSelectorSubSelection sender, EventArgs e){
			if (SelectionChanged != null){
				SelectionChanged(this, e);
			}
		}

		public static void TrySelect(ListSelector selector, IEnumerable<string> strings){
			if (!strings.Any()){
				return;
			}
			int index = -1;
			for (int i = 0; i < selector.Items.Count; i++){
				string x = (string) selector.Items[i];
				if (StringUtils.ContainsAll(x, strings)){
					index = i;
					break;
				}
			}
			if (index != -1){
				selector.SelectedItems.Add(selector.Items[index]);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
			switch (keyData){
				case Keys.Control | Keys.A:
					Control c = GetChildAtPoint(Cursor.Position);
					if (c != null){
						if (c.Equals(allListBox)){
							SelectAll(allListBox);
						}
					}
					Invalidate(true);
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		public static void SelectAll(ListBox p0){
			for (int i = 0; i < p0.Items.Count; i++){
				p0.SetSelected(i, true);
			}
		}

		private HashSet<string> GetSubSelection(int selectorInd){
			return new HashSet<string>(subSelection[selectorInd].SelectedStrings);
		}

		public void SetSelected(int selectorInd, int itemInd, bool b){
			HashSet<string> x = GetSubSelection(selectorInd);
			if (x.Contains(items[itemInd])){
				return;
			}
			if (!AllListBox.Items.Contains(items[itemInd])){
				for (int i = 0; i < subSelection.Length; i++){
					if (i == selectorInd){
						continue;
					}
					if (subSelection[i].SelectedListBox.Items.Contains(items[itemInd])){
						subSelection[i].SelectedListBox.Items.Remove(items[itemInd]);
						AllListBox.Items.Add(items[itemInd]);
						break;
					}
				}
			}
			AllListBox.Items.Remove(items[itemInd]);
			subSelection[selectorInd].SelectedListBox.Items.Add(items[itemInd]);
		}

		public int[] GetSelectedIndices(int selectorInd){
			string[] sel = subSelection[selectorInd].SelectedStrings;
			int[] result = new int[sel.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = ArrayUtils.IndexOf(items, sel[i]);
			}
			return result;
		}
	}
}