using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using BaseLib.Forms.Help;
using BaseLibS.Num;

namespace BaseLib.Forms.Select{
	internal partial class MultiListSelectorSubSelection : UserControl{
		private Thread downThread;
		private Thread upThread;
		internal MultiListSelector MultiListSelector { get; set; }
		private TableLayoutPanel tableLayoutPanel1;
		private ListBox selectedListBox;
		private Button deselectButton;
		private Button selectButton;
		private Panel panel2;
		private Button bottomButton;
		private Button downButton;
		private Button upButton;
		private Button topButton;
		private TableLayoutPanel tableLayoutPanel2;
		private Panel panel3;
		private HelpLabel helpLabel7;

		internal MultiListSelectorSubSelection(){
			InitializeComponent();
			InitializeComponent2();
			downButton.MouseDown += DownButtonMouseDown;
			downButton.MouseUp += DownButtonMouseUp;
			upButton.MouseDown += UpButtonMouseDown;
			upButton.MouseUp += UpButtonMouseUp;
		}

		private void InitializeComponent2(){
			tableLayoutPanel1 = new TableLayoutPanel();
			selectedListBox = new ListBox();
			Panel panel1 = new Panel();
			HelpLabel helpLabel6 = new HelpLabel();
			HelpLabel helpLabel5 = new HelpLabel();
			deselectButton = new Button();
			selectButton = new Button();
			panel2 = new Panel();
			HelpLabel helpLabel4 = new HelpLabel();
			HelpLabel helpLabel3 = new HelpLabel();
			HelpLabel helpLabel2 = new HelpLabel();
			HelpLabel helpLabel1 = new HelpLabel();
			bottomButton = new Button();
			downButton = new Button();
			upButton = new Button();
			topButton = new Button();
			tableLayoutPanel2 = new TableLayoutPanel();
			panel3 = new Panel();
			helpLabel7 = new HelpLabel();
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			panel3.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tableLayoutPanel1.Controls.Add(selectedListBox, 1, 0);
			tableLayoutPanel1.Controls.Add(panel1, 0, 0);
			tableLayoutPanel1.Controls.Add(panel2, 2, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 20);
			tableLayoutPanel1.Margin = new Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(280, 326);
			tableLayoutPanel1.TabIndex = 1;
			// 
			// selectedListBox
			// 
			selectedListBox.Dock = DockStyle.Fill;
			selectedListBox.FormattingEnabled = true;
			selectedListBox.Location = new System.Drawing.Point(33, 3);
			selectedListBox.Name = "selectedListBox";
			selectedListBox.SelectionMode = SelectionMode.MultiExtended;
			selectedListBox.Size = new System.Drawing.Size(214, 316);
			selectedListBox.TabIndex = 1;
			// 
			// panel1
			// 
			panel1.Controls.Add(helpLabel6);
			panel1.Controls.Add(helpLabel5);
			panel1.Controls.Add(deselectButton);
			panel1.Controls.Add(selectButton);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(3, 3);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(24, 320);
			panel1.TabIndex = 2;
			// 
			// helpLabel6
			// 
			helpLabel6.BackColor = System.Drawing.Color.Transparent;
			helpLabel6.HelpText = "Deselect.";
			helpLabel6.HelpTitle = "<";
			helpLabel6.Location = new System.Drawing.Point(1, 59);
			helpLabel6.Name = "helpLabel6";
			helpLabel6.Size = new System.Drawing.Size(23, 13);
			helpLabel6.TabIndex = 11;
			// 
			// helpLabel5
			// 
			helpLabel5.BackColor = System.Drawing.Color.Transparent;
			helpLabel5.HelpText = "Select.";
			helpLabel5.HelpTitle = ">";
			helpLabel5.Location = new System.Drawing.Point(2, 23);
			helpLabel5.Name = "helpLabel5";
			helpLabel5.Size = new System.Drawing.Size(23, 13);
			helpLabel5.TabIndex = 10;
			// 
			// deselectButton
			// 
			deselectButton.Location = new System.Drawing.Point(0, 36);
			deselectButton.Name = "deselectButton";
			deselectButton.Size = new System.Drawing.Size(24, 23);
			deselectButton.TabIndex = 1;
			deselectButton.Text = "<";
			deselectButton.UseVisualStyleBackColor = true;
			deselectButton.Click += DeselectButtonClick;
			// 
			// selectButton
			// 
			selectButton.Location = new System.Drawing.Point(0, 0);
			selectButton.Name = "selectButton";
			selectButton.Size = new System.Drawing.Size(24, 23);
			selectButton.TabIndex = 0;
			selectButton.Text = ">";
			selectButton.UseVisualStyleBackColor = true;
			selectButton.Click += SelectButtonClick;
			// 
			// panel2
			// 
			panel2.Controls.Add(helpLabel4);
			panel2.Controls.Add(helpLabel3);
			panel2.Controls.Add(helpLabel2);
			panel2.Controls.Add(helpLabel1);
			panel2.Controls.Add(bottomButton);
			panel2.Controls.Add(downButton);
			panel2.Controls.Add(upButton);
			panel2.Controls.Add(topButton);
			panel2.Dock = DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(253, 3);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(24, 320);
			panel2.TabIndex = 3;
			// 
			// helpLabel4
			// 
			helpLabel4.BackColor = System.Drawing.Color.Transparent;
			helpLabel4.HelpText = "Move selection to the bottom.";
			helpLabel4.HelpTitle = "b";
			helpLabel4.Location = new System.Drawing.Point(1, 113);
			helpLabel4.Name = "helpLabel4";
			helpLabel4.Size = new System.Drawing.Size(23, 13);
			helpLabel4.TabIndex = 9;
			// 
			// helpLabel3
			// 
			helpLabel3.BackColor = System.Drawing.Color.Transparent;
			helpLabel3.HelpText = "Move selection down.";
			helpLabel3.HelpTitle = "d";
			helpLabel3.Location = new System.Drawing.Point(1, 82);
			helpLabel3.Name = "helpLabel3";
			helpLabel3.Size = new System.Drawing.Size(23, 13);
			helpLabel3.TabIndex = 8;
			// 
			// helpLabel2
			// 
			helpLabel2.BackColor = System.Drawing.Color.Transparent;
			helpLabel2.HelpText = "Move selection up.";
			helpLabel2.HelpTitle = "u";
			helpLabel2.Location = new System.Drawing.Point(1, 50);
			helpLabel2.Name = "helpLabel2";
			helpLabel2.Size = new System.Drawing.Size(23, 13);
			helpLabel2.TabIndex = 7;
			// 
			// helpLabel1
			// 
			helpLabel1.BackColor = System.Drawing.Color.Transparent;
			helpLabel1.HelpText = "Move selection to the top.";
			helpLabel1.HelpTitle = "t";
			helpLabel1.Location = new System.Drawing.Point(1, 19);
			helpLabel1.Name = "helpLabel1";
			helpLabel1.Size = new System.Drawing.Size(23, 13);
			helpLabel1.TabIndex = 6;
			// 
			// bottomButton
			// 
			bottomButton.Location = new System.Drawing.Point(0, 94);
			bottomButton.Name = "bottomButton";
			bottomButton.Size = new System.Drawing.Size(24, 20);
			bottomButton.TabIndex = 5;
			bottomButton.Text = "b";
			bottomButton.UseVisualStyleBackColor = true;
			bottomButton.Click += BottomButtonClick;
			// 
			// downButton
			// 
			downButton.Location = new System.Drawing.Point(0, 62);
			downButton.Name = "downButton";
			downButton.Size = new System.Drawing.Size(24, 20);
			downButton.TabIndex = 4;
			downButton.Text = "d";
			downButton.UseVisualStyleBackColor = true;
			downButton.Click += DownButtonClick;
			// 
			// upButton
			// 
			upButton.Location = new System.Drawing.Point(0, 31);
			upButton.Name = "upButton";
			upButton.Size = new System.Drawing.Size(24, 20);
			upButton.TabIndex = 3;
			upButton.Text = "u";
			upButton.UseVisualStyleBackColor = true;
			upButton.Click += UpButtonClick;
			// 
			// topButton
			// 
			topButton.Location = new System.Drawing.Point(0, 0);
			topButton.Name = "topButton";
			topButton.Size = new System.Drawing.Size(24, 20);
			topButton.TabIndex = 2;
			topButton.Text = "t";
			topButton.UseVisualStyleBackColor = true;
			topButton.Click += TopButtonClick;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 1;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 1);
			tableLayoutPanel2.Controls.Add(panel3, 0, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel2.Margin = new Padding(0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new System.Drawing.Size(280, 346);
			tableLayoutPanel2.TabIndex = 2;
			// 
			// panel3
			// 
			panel3.Controls.Add(helpLabel7);
			panel3.Dock = DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(0, 0);
			panel3.Margin = new Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(280, 20);
			panel3.TabIndex = 2;
			// 
			// helpLabel7
			// 
			helpLabel7.BackColor = System.Drawing.Color.Transparent;
			helpLabel7.Dock = DockStyle.Fill;
			helpLabel7.HelpText = null;
			helpLabel7.HelpTitle = null;
			helpLabel7.Location = new System.Drawing.Point(0, 0);
			helpLabel7.Name = "helpLabel7";
			helpLabel7.Size = new System.Drawing.Size(280, 20);
			helpLabel7.TabIndex = 0;
			// 
			// MultiListSelectorSubSelection
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel2);
			Name = "MultiListSelectorSubSelection";
			Size = new System.Drawing.Size(280, 346);
			tableLayoutPanel1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel2.ResumeLayout(false);
			tableLayoutPanel2.ResumeLayout(false);
			panel3.ResumeLayout(false);
			ResumeLayout(false);
		}

		public override string Text{
			get { return helpLabel7.Text; }
			set { helpLabel7.Text = value; }
		}

		internal ListBox.ObjectCollection SelectedItems => selectedListBox.Items;

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

		public ListBox SelectedListBox => selectedListBox;

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