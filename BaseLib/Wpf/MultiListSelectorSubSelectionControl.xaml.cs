using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Forms.Select;
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
			//tableLayoutPanel1 = new Grid();
			//selectedListBox = new System.Windows.Controls.ListBox();
			//panel1 = new Canvas();
			//helpLabel6 = new TextBlock();
			//helpLabel5 = new TextBlock();
			//deselectButton = new System.Windows.Controls.Button();
			//selectButton = new System.Windows.Controls.Button();
			//panel2 = new Canvas();
			//helpLabel4 = new TextBlock();
			//helpLabel3 = new TextBlock();
			//helpLabel2 = new TextBlock();
			//helpLabel1 = new TextBlock();
			//bottomButton = new System.Windows.Controls.Button();
			//downButton = new System.Windows.Controls.Button();
			//upButton = new System.Windows.Controls.Button();
			//topButton = new System.Windows.Controls.Button();
			//tableLayoutPanel2 = new Grid();
			//panel3 = new Canvas();
			//tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Pixel) });
			//tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });
			//tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Pixel) });
			//tableLayoutPanel1.RowDefinitions.Add(new RowDefinition());
			//tableLayoutPanel1.Width = 280;
			//tableLayoutPanel1.Height = 326;
			//Grid.SetRow(selectedListBox, 0);
			//Grid.SetRow(panel1, 0);
			//Grid.SetRow(panel2, 0);
			//Grid.SetColumn(selectedListBox, 1);
			//Grid.SetColumn(panel1, 0);
			//Grid.SetColumn(panel2, 2);
			//tableLayoutPanel1.Children.Add(selectedListBox);
			//tableLayoutPanel1.Children.Add(panel1);
			//tableLayoutPanel1.Children.Add(panel2);
			//tableLayoutPanel1.Margin = new Thickness(0);
			//// 
			//// selectedListBox
			//// 
			//selectedListBox.Width = 214;
			//selectedListBox.Height = 316;
			//// 
			//// panel1
			//// 
			//panel1.Children.Add(helpLabel6);
			//panel1.Children.Add(helpLabel5);
			//panel1.Children.Add(deselectButton);
			//panel1.Children.Add(selectButton);
			//panel1.Width = 24;
			//panel1.Height = 320;
			//// 
			//// helpLabel6
			//// 
			//helpLabel6.ToolTip = "Deselect.";
			//helpLabel6.Text = "<";
			////helpLabel6.Location = new System.Drawing.Point(1, 59);
			//helpLabel6.Width = 23;
			//helpLabel6.Height = 13;
			//// 
			//// helpLabel5
			//// 
			//helpLabel5.ToolTip = "Select.";
			//helpLabel5.Text = ">";
			////helpLabel5.Location = new System.Drawing.Point(2, 23);
			//helpLabel5.Width = 23;
			//helpLabel5.Height = 13;
			//// 
			//// deselectButton
			//// 
			////deselectButton.Location = new System.Drawing.Point(0, 36);
			//deselectButton.Width = 24;
			//deselectButton.Height = 23;
			//deselectButton.Content = "<";
			//deselectButton.Click += DeselectButtonClick;
			//// 
			//// selectButton
			//// 
			////selectButton.Location = new System.Drawing.Point(0, 0);
			//selectButton.Width = 24;
			//selectButton.Height = 23;
			//selectButton.Content = ">";
			//selectButton.Click += SelectButtonClick;
			//// 
			//// panel2
			//// 
			//panel2.Children.Add(helpLabel4);
			//panel2.Children.Add(helpLabel3);
			//panel2.Children.Add(helpLabel2);
			//panel2.Children.Add(helpLabel1);
			//panel2.Children.Add(bottomButton);
			//panel2.Children.Add(downButton);
			//panel2.Children.Add(upButton);
			//panel2.Children.Add(topButton);
			//panel2.Width = 24;
			//panel2.Height = 320;
			//// 
			//// helpLabel4
			//// 
			//helpLabel4.ToolTip = "Move selection to the bottom.";
			//helpLabel4.Text = "b";
			////helpLabel4.Location = new System.Drawing.Point(1, 113);
			//helpLabel4.Width = 23;
			//helpLabel4.Height = 13;
			//// 
			//// helpLabel3
			//// 
			//helpLabel3.ToolTip = "Move selection down.";
			//helpLabel3.Text = "d";
			////helpLabel3.Location = new System.Drawing.Point(1, 82);
			//helpLabel3.Width = 23;
			//helpLabel3.Height = 13;
			//// 
			//// helpLabel2
			//// 
			//helpLabel2.ToolTip = "Move selection up.";
			//helpLabel2.Text = "u";
			////helpLabel2.Location = new System.Drawing.Point(1, 50);
			//helpLabel2.Width = 23;
			//helpLabel2.Height = 13;
			//// 
			//// helpLabel1
			//// 
			//helpLabel1.ToolTip = "Move selection to the top.";
			//helpLabel1.Text = "t";
			////helpLabel1.Location = new System.Drawing.Point(1, 19);
			//helpLabel1.Width = 23;
			//helpLabel1.Height = 13;
			//// 
			//// bottomButton
			//// 
			////bottomButton.Location = new System.Drawing.Point(0, 94);
			//bottomButton.Width = 24;
			//bottomButton.Height = 20;
			//bottomButton.Content = "b";
			//bottomButton.Click += BottomButtonClick;
			//// 
			//// downButton
			//// 
			////downButton.Location = new System.Drawing.Point(0, 62);
			//downButton.Width = 24;
			//downButton.Height = 20;
			//downButton.Content = "d";
			//downButton.Click += DownButtonClick;
			//// 
			//// upButton
			//// 
			////upButton.Location = new System.Drawing.Point(0, 31);
			//upButton.Width = 24;
			//upButton.Height = 20;
			//upButton.Content = "u";
			//upButton.Click += UpButtonClick;
			//// 
			//// topButton
			//// 
			////topButton.Location = new System.Drawing.Point(0, 0);
			//topButton.Width = 24;
			//topButton.Height = 20;
			//topButton.Content = "t";
			//topButton.Click += TopButtonClick;
			//// 
			//// tableLayoutPanel2
			//// 
			//tableLayoutPanel2.ColumnDefinitions.Add(new ColumnDefinition());
			//tableLayoutPanel2.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(20, GridUnitType.Pixel) });
			//tableLayoutPanel2.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(100, GridUnitType.Star) });
			//Grid.SetRow(tableLayoutPanel1, 1);
			//Grid.SetRow(panel3, 0);
			//Grid.SetColumn(tableLayoutPanel1, 0);
			//Grid.SetColumn(panel3, 0);		
	
			
			//tableLayoutPanel2.Children.Add(tableLayoutPanel1);
			//tableLayoutPanel2.Children.Add(panel3);
			//tableLayoutPanel2.Margin =new Thickness(0);
			//tableLayoutPanel2.Width = 280;
			//tableLayoutPanel2.Height = 346;
			//// 
			//// panel3
			//// 
			//panel3.Margin = new Thickness(0);
			//panel3.Width = 280;
			//panel3.Height = 20;
			//// 
			//// MultiListSelectorSubSelection
			//// 
			//Content=tableLayoutPanel2;
			//Width = 280;
			//Height = 346;

		}

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
			//selectedListBox.ClearSelected();
			//items = ArrayUtils.SubArray(items, order);
			//selectedListBox.Items.AddRange(items);
			//foreach (int i in selection) {
			//	selectedListBox.SetSelected(i, true);
			//}
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

		private void SelectButtonClick(object sender, EventArgs e) {
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

		private void DeselectButtonClick(object sender, EventArgs e) {
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

		private Grid tableLayoutPanel1;
		private System.Windows.Controls.ListBox selectedListBox;
		private Canvas panel1;
		private TextBlock helpLabel6;
		private TextBlock helpLabel5;
		private System.Windows.Controls.Button deselectButton;
		private System.Windows.Controls.Button selectButton;
		private Canvas panel2;
		private TextBlock helpLabel4;
		private TextBlock helpLabel3;
		private TextBlock helpLabel2;
		private TextBlock helpLabel1;
		private System.Windows.Controls.Button bottomButton;
		private System.Windows.Controls.Button downButton;
		private System.Windows.Controls.Button upButton;
		private System.Windows.Controls.Button topButton;
		private Grid tableLayoutPanel2;
		private Canvas panel3;

		//TODO
		internal string[] SelectedStrings {
			get {
				return new string[0];
			}
		}

	}
}
