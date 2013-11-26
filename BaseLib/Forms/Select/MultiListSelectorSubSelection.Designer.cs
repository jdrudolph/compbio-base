using BaseLib.Forms.Help;

namespace BaseLib.Forms.Select {
	partial class MultiListSelectorSubSelection {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.selectedListBox = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.helpLabel6 = new HelpLabel();
			this.helpLabel5 = new HelpLabel();
			this.deselectButton = new System.Windows.Forms.Button();
			this.selectButton = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.helpLabel4 = new HelpLabel();
			this.helpLabel3 = new HelpLabel();
			this.helpLabel2 = new HelpLabel();
			this.helpLabel1 = new HelpLabel();
			this.bottomButton = new System.Windows.Forms.Button();
			this.downButton = new System.Windows.Forms.Button();
			this.upButton = new System.Windows.Forms.Button();
			this.topButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.helpLabel7 = new HelpLabel();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.selectedListBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 20);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(280, 326);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// selectedListBox
			// 
			this.selectedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.selectedListBox.FormattingEnabled = true;
			this.selectedListBox.Location = new System.Drawing.Point(33, 3);
			this.selectedListBox.Name = "selectedListBox";
			this.selectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.selectedListBox.Size = new System.Drawing.Size(214, 316);
			this.selectedListBox.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.helpLabel6);
			this.panel1.Controls.Add(this.helpLabel5);
			this.panel1.Controls.Add(this.deselectButton);
			this.panel1.Controls.Add(this.selectButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(24, 320);
			this.panel1.TabIndex = 2;
			// 
			// helpLabel6
			// 
			this.helpLabel6.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel6.HelpText = "Deselect.";
			this.helpLabel6.HelpTitle = "<";
			this.helpLabel6.Location = new System.Drawing.Point(1, 59);
			this.helpLabel6.Name = "helpLabel6";
			this.helpLabel6.Size = new System.Drawing.Size(23, 13);
			this.helpLabel6.TabIndex = 11;
			// 
			// helpLabel5
			// 
			this.helpLabel5.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel5.HelpText = "Select.";
			this.helpLabel5.HelpTitle = ">";
			this.helpLabel5.Location = new System.Drawing.Point(2, 23);
			this.helpLabel5.Name = "helpLabel5";
			this.helpLabel5.Size = new System.Drawing.Size(23, 13);
			this.helpLabel5.TabIndex = 10;
			// 
			// deselectButton
			// 
			this.deselectButton.Location = new System.Drawing.Point(0, 36);
			this.deselectButton.Name = "deselectButton";
			this.deselectButton.Size = new System.Drawing.Size(24, 23);
			this.deselectButton.TabIndex = 1;
			this.deselectButton.Text = "<";
			this.deselectButton.UseVisualStyleBackColor = true;
			this.deselectButton.Click += new System.EventHandler(this.DeselectButtonClick);
			// 
			// selectButton
			// 
			this.selectButton.Location = new System.Drawing.Point(0, 0);
			this.selectButton.Name = "selectButton";
			this.selectButton.Size = new System.Drawing.Size(24, 23);
			this.selectButton.TabIndex = 0;
			this.selectButton.Text = ">";
			this.selectButton.UseVisualStyleBackColor = true;
			this.selectButton.Click += new System.EventHandler(this.SelectButtonClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.helpLabel4);
			this.panel2.Controls.Add(this.helpLabel3);
			this.panel2.Controls.Add(this.helpLabel2);
			this.panel2.Controls.Add(this.helpLabel1);
			this.panel2.Controls.Add(this.bottomButton);
			this.panel2.Controls.Add(this.downButton);
			this.panel2.Controls.Add(this.upButton);
			this.panel2.Controls.Add(this.topButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(253, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(24, 320);
			this.panel2.TabIndex = 3;
			// 
			// helpLabel4
			// 
			this.helpLabel4.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel4.HelpText = "Move selection to the bottom.";
			this.helpLabel4.HelpTitle = "b";
			this.helpLabel4.Location = new System.Drawing.Point(1, 113);
			this.helpLabel4.Name = "helpLabel4";
			this.helpLabel4.Size = new System.Drawing.Size(23, 13);
			this.helpLabel4.TabIndex = 9;
			// 
			// helpLabel3
			// 
			this.helpLabel3.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel3.HelpText = "Move selection down.";
			this.helpLabel3.HelpTitle = "d";
			this.helpLabel3.Location = new System.Drawing.Point(1, 82);
			this.helpLabel3.Name = "helpLabel3";
			this.helpLabel3.Size = new System.Drawing.Size(23, 13);
			this.helpLabel3.TabIndex = 8;
			// 
			// helpLabel2
			// 
			this.helpLabel2.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel2.HelpText = "Move selection up.";
			this.helpLabel2.HelpTitle = "u";
			this.helpLabel2.Location = new System.Drawing.Point(1, 50);
			this.helpLabel2.Name = "helpLabel2";
			this.helpLabel2.Size = new System.Drawing.Size(23, 13);
			this.helpLabel2.TabIndex = 7;
			// 
			// helpLabel1
			// 
			this.helpLabel1.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel1.HelpText = "Move selection to the top.";
			this.helpLabel1.HelpTitle = "t";
			this.helpLabel1.Location = new System.Drawing.Point(1, 19);
			this.helpLabel1.Name = "helpLabel1";
			this.helpLabel1.Size = new System.Drawing.Size(23, 13);
			this.helpLabel1.TabIndex = 6;
			// 
			// bottomButton
			// 
			this.bottomButton.Location = new System.Drawing.Point(0, 94);
			this.bottomButton.Name = "bottomButton";
			this.bottomButton.Size = new System.Drawing.Size(24, 20);
			this.bottomButton.TabIndex = 5;
			this.bottomButton.Text = "b";
			this.bottomButton.UseVisualStyleBackColor = true;
			this.bottomButton.Click += new System.EventHandler(this.BottomButtonClick);
			// 
			// downButton
			// 
			this.downButton.Location = new System.Drawing.Point(0, 62);
			this.downButton.Name = "downButton";
			this.downButton.Size = new System.Drawing.Size(24, 20);
			this.downButton.TabIndex = 4;
			this.downButton.Text = "d";
			this.downButton.UseVisualStyleBackColor = true;
			this.downButton.Click += new System.EventHandler(this.DownButtonClick);
			// 
			// upButton
			// 
			this.upButton.Location = new System.Drawing.Point(0, 31);
			this.upButton.Name = "upButton";
			this.upButton.Size = new System.Drawing.Size(24, 20);
			this.upButton.TabIndex = 3;
			this.upButton.Text = "u";
			this.upButton.UseVisualStyleBackColor = true;
			this.upButton.Click += new System.EventHandler(this.UpButtonClick);
			// 
			// topButton
			// 
			this.topButton.Location = new System.Drawing.Point(0, 0);
			this.topButton.Name = "topButton";
			this.topButton.Size = new System.Drawing.Size(24, 20);
			this.topButton.TabIndex = 2;
			this.topButton.Text = "t";
			this.topButton.UseVisualStyleBackColor = true;
			this.topButton.Click += new System.EventHandler(this.TopButtonClick);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.panel3, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(280, 346);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.helpLabel7);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Margin = new System.Windows.Forms.Padding(0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(280, 20);
			this.panel3.TabIndex = 2;
			// 
			// helpLabel7
			// 
			this.helpLabel7.BackColor = System.Drawing.Color.Transparent;
			this.helpLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpLabel7.HelpText = null;
			this.helpLabel7.HelpTitle = null;
			this.helpLabel7.Location = new System.Drawing.Point(0, 0);
			this.helpLabel7.Name = "helpLabel7";
			this.helpLabel7.Size = new System.Drawing.Size(280, 20);
			this.helpLabel7.TabIndex = 0;
			// 
			// MultiListSelectorSubSelection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "MultiListSelectorSubSelection";
			this.Size = new System.Drawing.Size(280, 346);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ListBox selectedListBox;
		private System.Windows.Forms.Panel panel1;
		private HelpLabel helpLabel6;
		private HelpLabel helpLabel5;
		private System.Windows.Forms.Button deselectButton;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Panel panel2;
		private HelpLabel helpLabel4;
		private HelpLabel helpLabel3;
		private HelpLabel helpLabel2;
		private HelpLabel helpLabel1;
		private System.Windows.Forms.Button bottomButton;
		private System.Windows.Forms.Button downButton;
		private System.Windows.Forms.Button upButton;
		private System.Windows.Forms.Button topButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Panel panel3;
		private HelpLabel helpLabel7;
	}
}
