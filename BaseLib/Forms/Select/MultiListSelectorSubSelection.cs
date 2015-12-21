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
		private ListBox selectedListBox;
		private HelpLabel mainLabel;

		internal MultiListSelectorSubSelection(){
			InitializeComponent();
			InitializeComponent2();
		}

		private void InitializeComponent2(){
			TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
			selectedListBox = new ListBox();
			TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
			mainLabel = new HelpLabel();
			tableLayoutPanel1.SuspendLayout();
			Panel panel1 = new Panel();
			panel1.SuspendLayout();
			Panel panel2 = new Panel();
			panel2.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			Panel panel3 = new Panel();
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
			// deselectButton
			// 
			Button deselectButton = new Button{
				Size = new System.Drawing.Size(24, 23),
				TabIndex = 1,
				Text = "<",
				UseVisualStyleBackColor = true
			};
			deselectButton.Click += DeselectButtonClick;
			// 
			// selectButton
			// 
			Button selectButton = new Button{
				Size = new System.Drawing.Size(24, 23),
				TabIndex = 0,
				Text = ">",
				UseVisualStyleBackColor = true
			};
			selectButton.Click += SelectButtonClick;
			// 
			// bottomButton
			// 
			Button bottomButton = new Button{
				Size = new System.Drawing.Size(24, 20),
				TabIndex = 5,
				Text = "b",
				UseVisualStyleBackColor = true
			};
			bottomButton.Click += BottomButtonClick;
			// 
			// downButton
			// 
			Button downButton = new Button{
				Size = new System.Drawing.Size(24, 20),
				TabIndex = 4,
				Text = "d",
				UseVisualStyleBackColor = true
			};
			downButton.Click += DownButtonClick;
			// 
			// upButton
			// 
			Button upButton = new Button{
				Size = new System.Drawing.Size(24, 20),
				TabIndex = 3,
				Text = "u",
				UseVisualStyleBackColor = true
			};
			upButton.Click += UpButtonClick;
			// 
			// topButton
			// 
			Button topButton = new Button{
				Size = new System.Drawing.Size(24, 20),
				TabIndex = 2,
				Text = "t",
				UseVisualStyleBackColor = true
			};
			topButton.Click += TopButtonClick;
			// 
			// panel1
			// 
			panel1.Controls.Add(new HelpLabel{
				BackColor = System.Drawing.Color.Transparent,
				HelpText = "Deselect.",
				HelpTitle = "<",
				Size = new System.Drawing.Size(23, 13),
				TabIndex = 11
			});
			panel1.Controls.Add(new HelpLabel{
				BackColor = System.Drawing.Color.Transparent,
				HelpText = "Select.",
				HelpTitle = ">",
				Size = new System.Drawing.Size(23, 13),
				TabIndex = 10
			});
			panel1.Controls.Add(deselectButton);
			panel1.Controls.Add(selectButton);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(3, 3);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(24, 320);
			panel1.TabIndex = 2;
			// 
			// panel2
			// 
			panel2.Controls.Add(new HelpLabel{
				BackColor = System.Drawing.Color.Transparent,
				HelpText = "Move selection to the bottom.",
				HelpTitle = "b",
				Size = new System.Drawing.Size(23, 13),
				TabIndex = 9
			});
			panel2.Controls.Add(new HelpLabel{
				BackColor = System.Drawing.Color.Transparent,
				HelpText = "Move selection down.",
				HelpTitle = "d",
				Size = new System.Drawing.Size(23, 13),
				TabIndex = 8
			});
			panel2.Controls.Add(new HelpLabel{
				BackColor = System.Drawing.Color.Transparent,
				HelpText = "Move selection up.",
				HelpTitle = "u",
				Size = new System.Drawing.Size(23, 13),
				TabIndex = 7
			});
			panel2.Controls.Add(new HelpLabel{
				BackColor = System.Drawing.Color.Transparent,
				HelpText = "Move selection to the top.",
				HelpTitle = "t",
				Size = new System.Drawing.Size(23, 13),
				TabIndex = 6
			});
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
			panel3.Controls.Add(mainLabel);
			panel3.Dock = DockStyle.Fill;
			panel3.Location = new System.Drawing.Point(0, 0);
			panel3.Margin = new Padding(0);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(280, 20);
			panel3.TabIndex = 2;
			// 
			// mainLabel
			// 
			mainLabel.BackColor = System.Drawing.Color.Transparent;
			mainLabel.Dock = DockStyle.Fill;
			mainLabel.HelpText = null;
			mainLabel.HelpTitle = null;
			mainLabel.Location = new System.Drawing.Point(0, 0);
			mainLabel.Name = "mainLabel";
			mainLabel.Size = new System.Drawing.Size(280, 20);
			mainLabel.TabIndex = 0;
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
			downButton.MouseDown += DownButtonMouseDown;
			downButton.MouseUp += DownButtonMouseUp;
			upButton.MouseDown += UpButtonMouseDown;
			upButton.MouseUp += UpButtonMouseUp;
		}

		public override string Text{
			get { return mainLabel.Text; }
			set { mainLabel.Text = value; }
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

		internal ListBox SelectedListBox => selectedListBox;

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