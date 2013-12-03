using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using BaseLib.Util;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for MultiListSelectorSubSelectionControl.xaml
	/// </summary>
	public partial class MultiListSelectorSubSelectionControl : System.Windows.Controls.UserControl {
		private Thread downThread;
		private Thread upThread;
		internal MultiListSelectorControl MultiListSelectorControl { get; set; }

		public MultiListSelectorSubSelectionControl() {
			InitializeComponent();
			downButton.MouseDown += DownButtonMouseDown;
			downButton.MouseUp += DownButtonMouseUp;
			upButton.MouseDown += UpButtonMouseDown;
			upButton.MouseUp += UpButtonMouseUp;
		}

		public string Text { set { titleBlock.Text = value; } }
		internal System.Windows.Controls.ItemCollection SelectedItems { get { return selectedListBox.Items; } }
		internal string[] SelectedStrings {
			get {
				System.Windows.Controls.ItemCollection sel = SelectedItems;
				string[] result = new string[sel.Count];
				for (int i = 0; i < result.Length; i++) {
					result[i] = sel[i].ToString();
				}
				return result;
			}
		}
		public System.Windows.Controls.ListBox SelectedListBox { get { return selectedListBox; } }

		private static int[] GetSelectedIndices(System.Windows.Controls.ListBox box) {
			int[] result = new int[box.SelectedItems.Count];
			for (int i = 0; i < result.Length; i++) {
				result[i] = box.Items.IndexOf(box.SelectedItems[i]);
			}
			return result;
		}

		private void SetOrder(IList<int> order, IEnumerable<int> selection) {
			object[] items = new object[selectedListBox.Items.Count];
			for (int i = 0; i < items.Length; i++) {
				items[i] = selectedListBox.Items[i];
			}
			selectedListBox.Items.Clear();
			//TODO
			selectedListBox.UnselectAll();
			items = ArrayUtils.SubArray(items, order);
			foreach (object item in items){
				selectedListBox.Items.Add(item);
			}
			foreach (int i in selection){
				selectedListBox.SelectedIndex = i;
				//selectedListBox.SetSelected(i, true);
			}
		}


		private void DownButtonMouseUp(object sender, MouseButtonEventArgs e) {
			downThread.Abort();
			downThread = null;
		}

		private void UpButtonMouseUp(object sender, MouseButtonEventArgs e) {
			upThread.Abort();
			upThread = null;
		}

		private void DownButtonMouseDown(object sender, MouseButtonEventArgs e) {
			downThread = new Thread(WalkDown);
			downThread.Start();
		}

		private void UpButtonMouseDown(object sender, MouseButtonEventArgs e) {
			upThread = new Thread(WalkUp);
			upThread.Start();
		}

		private void WalkDown() {
			Thread.Sleep(400);
			while (true) {
				Dispatcher.Invoke(() => DownButtonClick(null, null));
				Thread.Sleep(150);
			}
		}

		private void WalkUp() {
			Thread.Sleep(400);
			while (true) {
				Dispatcher.Invoke(() => UpButtonClick(null, null));
				Thread.Sleep(150);
			}
		}


		private void TopButtonClick(object sender, EventArgs e) {
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0) {
				return;
			}
			int n = selectedListBox.Items.Count;
			int[] unselectedIndices = ArrayUtils.Complement(selectedIndices, n);
			int[] order = ArrayUtils.Concat(selectedIndices, unselectedIndices);
			int[] selection = ArrayUtils.ConsecutiveInts(selectedIndices.Length);
			SetOrder(order, selection);
		}

		private void UpButtonClick(object sender, EventArgs e) {
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0) {
				return;
			}
			Array.Sort(selectedIndices);
			int index = -1;
			for (int i = selectedIndices.Length - 1; i >= 0; i--) {
				if (i == 0 || selectedIndices[i - 1] < selectedIndices[i] - 1) {
					index = i;
					break;
				}
			}
			int q = selectedIndices[index];
			if (q == 0) {
				return;
			}
			int m = selectedIndices.Length - index;
			int n = selectedListBox.Items.Count;
			int[] order = new int[n];
			for (int i = 0; i < q - 1; i++) {
				order[i] = i;
			}
			for (int i = 0; i < m; i++) {
				order[q - 1 + i] = q + i;
			}
			order[q + m - 1] = q - 1;
			for (int i = q + m; i < n; i++) {
				order[i] = i;
			}
			int[] selection = selectedIndices;
			for (int i = index; i < selection.Length; i++) {
				selection[i]--;
			}
			SetOrder(order, selection);
		}

		private void DownButtonClick(object sender, EventArgs e) {
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0) {
				return;
			}
			Array.Sort(selectedIndices);
			int index = -1;
			for (int i = 0; i < selectedIndices.Length; i++) {
				if (i == selectedIndices.Length - 1 || selectedIndices[i + 1] > selectedIndices[i] + 1) {
					index = i;
					break;
				}
			}
			int q = selectedIndices[0];
			int n = selectedListBox.Items.Count;
			if (selectedIndices[index] == n - 1) {
				return;
			}
			int m = index + 1;
			int[] order = new int[n];
			for (int i = 0; i < q; i++) {
				order[i] = i;
			}
			order[q] = q + m;
			for (int i = 0; i < m; i++) {
				order[q + 1 + i] = q + i;
			}
			for (int i = q + m + 1; i < n; i++) {
				order[i] = i;
			}
			int[] selection = selectedIndices;
			for (int i = 0; i <= index; i++) {
				selection[i]++;
			}
			SetOrder(order, selection);
		}

		private void BottomButtonClick(object sender, EventArgs e) {
			int[] selectedIndices = GetSelectedIndices(selectedListBox);
			if (selectedIndices.Length == 0) {
				return;
			}
			int n = selectedListBox.Items.Count;
			int[] unselectedIndices = ArrayUtils.Complement(selectedIndices, n);
			int[] order = ArrayUtils.Concat(unselectedIndices, selectedIndices);
			int[] selection = ArrayUtils.ConsecutiveInts(n - selectedIndices.Length, n);
			SetOrder(order, selection);
		}

		//TODO
		//protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
		//	switch (keyData) {
		//		case Keys.Control | Keys.A:
		//			System.Windows.Controls.Control c = GetChildAtPoint(System.Windows.Forms.Cursor.Position);
		//			if (c != null) {
		//				if (c.Equals(selectedListBox)) {
		//					MultiListSelector.SelectAll(selectedListBox);
		//				}
		//			}
		//			Invalidate(true);
		//			break;
		//	}
		//	return base.ProcessCmdKey(ref msg, keyData);
		//}

		private void Select_OnClick(object sender, RoutedEventArgs e){
			object[] os = new object[MultiListSelectorControl.AllListBox.SelectedItems.Count];
			MultiListSelectorControl.AllListBox.SelectedItems.CopyTo(os, 0);
			foreach (object o in os) {
				if (!selectedListBox.Items.Contains(o)) {
					selectedListBox.Items.Add(o);
				}
			}
			foreach (object o in os) {
				MultiListSelectorControl.AllListBox.Items.Remove(o);
			}
			MultiListSelectorControl.SelectionHasChanged(this, e);
		}

		private void Deselect_OnClick(object sender, RoutedEventArgs e){
			object[] os = new object[selectedListBox.SelectedItems.Count];
			selectedListBox.SelectedItems.CopyTo(os, 0);
			foreach (object o in os) {
				selectedListBox.Items.Remove(o);
			}
			foreach (object o in os) {
				MultiListSelectorControl.AllListBox.Items.Add(o);
			}
			MultiListSelectorControl.SelectionHasChanged(this, e);
		}
	}
}
