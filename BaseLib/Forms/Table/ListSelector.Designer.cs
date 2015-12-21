namespace BaseLib.Forms.Table{
	partial class ListSelector {
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
			this.selectedListBox = new System.Windows.Forms.ListBox();
			this.allListBox = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
			this.defaultButtonPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// selectedListBox
			// 
			this.selectedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.selectedListBox.FormattingEnabled = true;
			this.selectedListBox.Location = new System.Drawing.Point(217, 3);
			this.selectedListBox.Name = "selectedListBox";
			this.selectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.selectedListBox.Size = new System.Drawing.Size(178, 368);
			this.selectedListBox.TabIndex = 1;
			// 
			// allListBox
			// 
			this.allListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.allListBox.FormattingEnabled = true;
			this.allListBox.Location = new System.Drawing.Point(3, 3);
			this.allListBox.Name = "allListBox";
			this.allListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.allListBox.Size = new System.Drawing.Size(178, 368);
			this.allListBox.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Controls.Add(this.allListBox, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.selectedListBox, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(428, 379);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.helpLabel6);
			this.panel1.Controls.Add(this.helpLabel5);
			this.panel1.Controls.Add(this.deselectButton);
			this.panel1.Controls.Add(this.selectButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(187, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(24, 373);
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
			this.panel2.Location = new System.Drawing.Point(401, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(24, 373);
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
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.defaultButtonPanel, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(428, 379);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// defaultButtonPanel
			// 
			this.defaultButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.defaultButtonPanel.Location = new System.Drawing.Point(3, 382);
			this.defaultButtonPanel.Name = "defaultButtonPanel";
			this.defaultButtonPanel.Size = new System.Drawing.Size(422, 1);
			this.defaultButtonPanel.TabIndex = 1;
			// 
			// ListSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "ListSelector";
			this.Size = new System.Drawing.Size(428, 379);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox selectedListBox;
		private System.Windows.Forms.ListBox allListBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button deselectButton;
		private System.Windows.Forms.Button selectButton;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button bottomButton;
		private System.Windows.Forms.Button downButton;
		private System.Windows.Forms.Button upButton;
		private System.Windows.Forms.Button topButton;
		private HelpLabel helpLabel1;
		private HelpLabel helpLabel4;
		private HelpLabel helpLabel3;
		private HelpLabel helpLabel2;
		private HelpLabel helpLabel6;
		private HelpLabel helpLabel5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Panel defaultButtonPanel;

	}
}