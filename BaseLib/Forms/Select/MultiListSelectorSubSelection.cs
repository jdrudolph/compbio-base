using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLib.Forms.Select{
	internal partial class MultiListSelectorSubSelection : UserControl{
		private Thread downThread;
		private Thread upThread;
		internal MultiListSelector MultiListSelector { get; set; }

		internal MultiListSelectorSubSelection(){
			InitializeComponent();
			downButton.MouseDown += DownButtonMouseDown;
			downButton.MouseUp += DownButtonMouseUp;
			upButton.MouseDown += UpButtonMouseDown;
			upButton.MouseUp += UpButtonMouseUp;
		}

		public override string Text { get { return helpLabel7.Text; } set { helpLabel7.Text = value; } }
		internal ListBox.ObjectCollection SelectedItems { get { return selectedListBox.Items; } }
		internal string[] SelectedStrings{
			get{
				ListBox.ObjectCollection sel = SelectedItems;
				string[] result = new string[sel.Count];
				for (int i = 0; i < result.Length; i++){
					result[i] = sel[i].ToString();
				}
				return result;
			}
		}
		public ListBox SelectedListBox { get { return selectedListBox; } }

		private void DownButtonMouseUp(object sender, MouseEventArgs e){
			downThread.Abort();
			downThread = null;
		}

		private void UpButtonMouseUp(object sender, MouseEventArgs e){
			upThread.Abort();
			upThread = null;
		}

		private void DownButtonMouseDown(object sender, MouseEventArgs e){
			downThread = new Thread(WalkDown);
			downThread.Start();
		}

		private void UpButtonMouseDown(object sender, MouseEventArgs e){
			upThread = new Thread(WalkUp);
			upThread.Start();
		}

		private void WalkDown(){
			Thread.Sleep(400);
			while (true){
				Invoke((Action) (() => DownButtonClick(null, null)));
				Thread.Sleep(150);
			}
		}

		private void WalkUp(){
			Thread.Sleep(400);
			while (true){
				Invoke((Action) (() => UpButtonClick(null, null)));
				Thread.Sleep(150);
			}
		}

		private static int[] GetSelectedIndices(ListBox box){
			int[] result = new int[box.SelectedIndices.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = box.SelectedIndices[i];
			}
			return result;
		}

		private void SetOrder(IList<int> order, IEnumerable<int> selection){
			object[] items = new object[selectedListBox.Items.Count];
			for (int i = 0; i < items.Length; i++){
				items[i] = selectedListBox.Items[i];
			}
			selectedListBox.Items.Clear();
			selectedListBox.ClearSelected();
			items = ArrayUtils.SubArray(items, order);
			selectedListBox.Items.AddRange(items);
			foreach (int i in selection){
				selectedListBox.SetSelected(i, true);
			}
		}

		private void TopButtonClick(object sender, EventArgs e){
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			int n = selectedListBox.Items.Count;
			int[] unselectedIndices = ArrayUtils.Complement(selectedIndices, n);
			int[] order = ArrayUtils.Concat(selectedIndices, unselectedIndices);
			int[] selection = ArrayUtils.ConsecutiveInts(selectedIndices.Length);
			SetOrder(order, selection);
		}

		private void UpButtonClick(object sender, EventArgs e){
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			Array.Sort(selectedIndices);
			int index = -1;
			for (int i = selectedIndices.Length - 1; i >= 0; i--){
				if (i == 0 || selectedIndices[i - 1] < selectedIndices[i] - 1){
					index = i;
					break;
				}
			}
			int q = selectedIndices[index];
			if (q == 0){
				return;
			}
			int m = selectedIndices.Length - index;
			int n = selectedListBox.Items.Count;
			int[] order = new int[n];
			for (int i = 0; i < q - 1; i++){
				order[i] = i;
			}
			for (int i = 0; i < m; i++){
				order[q - 1 + i] = q + i;
			}
			order[q + m - 1] = q - 1;
			for (int i = q + m; i < n; i++){
				order[i] = i;
			}
			int[] selection = selectedIndices;
			for (int i = index; i < selection.Length; i++){
				selection[i]--;
			}
			SetOrder(order, selection);
		}

		private void DownButtonClick(object sender, EventArgs e){
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			Array.Sort(selectedIndices);
			int index = -1;
			for (int i = 0; i < selectedIndices.Length; i++){
				if (i == selectedIndices.Length - 1 || selectedIndices[i + 1] > selectedIndices[i] + 1){
					index = i;
					break;
				}
			}
			int q = selectedIndices[0];
			int n = selectedListBox.Items.Count;
			if (selectedIndices[index] == n - 1){
				return;
			}
			int m = index + 1;
			int[] order = new int[n];
			for (int i = 0; i < q; i++){
				order[i] = i;
			}
			order[q] = q + m;
			for (int i = 0; i < m; i++){
				order[q + 1 + i] = q + i;
			}
			for (int i = q + m + 1; i < n; i++){
				order[i] = i;
			}
			int[] selection = selectedIndices;
			for (int i = 0; i <= index; i++){
				selection[i]++;
			}
			SetOrder(order, selection);
		}

		private void BottomButtonClick(object sender, EventArgs e){
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0){
				return;
			}
			int n = selectedListBox.Items.Count;
			int[] unselectedIndices = ArrayUtils.Complement(selectedIndices, n);
			int[] order = ArrayUtils.Concat(unselectedIndices, selectedIndices);
			int[] selection = ArrayUtils.ConsecutiveInts(n - selectedIndices.Length, n);
			SetOrder(order, selection);
		}

		private void SelectButtonClick(object sender, EventArgs e){
			object[] os = new object[MultiListSelector.AllListBox.SelectedItems.Count];
			MultiListSelector.AllListBox.SelectedItems.CopyTo(os, 0);
			foreach (object o in os){
				if (!selectedListBox.Items.Contains(o)){
					selectedListBox.Items.Add(o);
				}
			}
			foreach (object o in os){
				MultiListSelector.AllListBox.Items.Remove(o);
			}
			MultiListSelector.SelectionHasChanged(this, e);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
			switch (keyData){
				case Keys.Control | Keys.A:
					Control c = GetChildAtPoint(Cursor.Position);
					if (c != null){
						if (c.Equals(selectedListBox)){
							MultiListSelector.SelectAll(selectedListBox);
						}
					}
					Invalidate(true);
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void DeselectButtonClick(object sender, EventArgs e){
			object[] os = new object[selectedListBox.SelectedItems.Count];
			selectedListBox.SelectedItems.CopyTo(os, 0);
			foreach (object o in os){
				selectedListBox.Items.Remove(o);
			}
			foreach (object o in os){
				MultiListSelector.AllListBox.Items.Add(o);
			}
			MultiListSelector.SelectionHasChanged(this, e);
		}
	}
}