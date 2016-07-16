using BaseLib.Forms.Scroll;

namespace BaseLib.Forms.Table {
	partial class FindForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.columnSelectButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.wildcardsComboBox = new System.Windows.Forms.ComboBox();
			this.lookInComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.useCheckBox = new System.Windows.Forms.CheckBox();
			this.searchUpCheckBox = new System.Windows.Forms.CheckBox();
			this.matchWholeWordCheckBox = new System.Windows.Forms.CheckBox();
			this.findAllButton = new System.Windows.Forms.Button();
			this.findNextButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.matchCaseCheckBox = new System.Windows.Forms.CheckBox();
			this.expressionTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableView1 = new CompoundScrollableControl();
			this.statusStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 196);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(336, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableView1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(336, 196);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.columnSelectButton);
			this.panel1.Controls.Add(this.helpButton);
			this.panel1.Controls.Add(this.wildcardsComboBox);
			this.panel1.Controls.Add(this.lookInComboBox);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.useCheckBox);
			this.panel1.Controls.Add(this.searchUpCheckBox);
			this.panel1.Controls.Add(this.matchWholeWordCheckBox);
			this.panel1.Controls.Add(this.findAllButton);
			this.panel1.Controls.Add(this.findNextButton);
			this.panel1.Controls.Add(this.cancelButton);
			this.panel1.Controls.Add(this.matchCaseCheckBox);
			this.panel1.Controls.Add(this.expressionTextBox);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(330, 190);
			this.panel1.TabIndex = 0;
			// 
			// columnSelectButton
			// 
			this.columnSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.columnSelectButton.Location = new System.Drawing.Point(299, 36);
			this.columnSelectButton.Name = "columnSelectButton";
			this.columnSelectButton.Size = new System.Drawing.Size(22, 21);
			this.columnSelectButton.TabIndex = 14;
			this.columnSelectButton.Text = ">";
			this.columnSelectButton.UseVisualStyleBackColor = true;
			this.columnSelectButton.Click += new System.EventHandler(this.ColumnSelectButtonClick);
			// 
			// helpButton
			// 
			this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.helpButton.Location = new System.Drawing.Point(299, 6);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(22, 21);
			this.helpButton.TabIndex = 13;
			this.helpButton.Text = ">";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// wildcardsComboBox
			// 
			this.wildcardsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.wildcardsComboBox.FormattingEnabled = true;
			this.wildcardsComboBox.Items.AddRange(new object[] {
            "Regular expressions",
            "Wildcards"});
			this.wildcardsComboBox.Location = new System.Drawing.Point(72, 134);
			this.wildcardsComboBox.Name = "wildcardsComboBox";
			this.wildcardsComboBox.Size = new System.Drawing.Size(249, 21);
			this.wildcardsComboBox.TabIndex = 12;
			// 
			// lookInComboBox
			// 
			this.lookInComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lookInComboBox.FormattingEnabled = true;
			this.lookInComboBox.Location = new System.Drawing.Point(72, 36);
			this.lookInComboBox.Name = "lookInComboBox";
			this.lookInComboBox.Size = new System.Drawing.Size(221, 21);
			this.lookInComboBox.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Look in:";
			// 
			// useCheckBox
			// 
			this.useCheckBox.AutoSize = true;
			this.useCheckBox.Location = new System.Drawing.Point(15, 136);
			this.useCheckBox.Name = "useCheckBox";
			this.useCheckBox.Size = new System.Drawing.Size(45, 17);
			this.useCheckBox.TabIndex = 9;
			this.useCheckBox.Text = "Use";
			this.useCheckBox.UseVisualStyleBackColor = true;
			this.useCheckBox.CheckedChanged += new System.EventHandler(this.UseCheckBoxCheckedChanged);
			// 
			// searchUpCheckBox
			// 
			this.searchUpCheckBox.AutoSize = true;
			this.searchUpCheckBox.Location = new System.Drawing.Point(15, 113);
			this.searchUpCheckBox.Name = "searchUpCheckBox";
			this.searchUpCheckBox.Size = new System.Drawing.Size(75, 17);
			this.searchUpCheckBox.TabIndex = 8;
			this.searchUpCheckBox.Text = "Search up";
			this.searchUpCheckBox.UseVisualStyleBackColor = true;
			// 
			// matchWholeWordCheckBox
			// 
			this.matchWholeWordCheckBox.AutoSize = true;
			this.matchWholeWordCheckBox.Location = new System.Drawing.Point(15, 90);
			this.matchWholeWordCheckBox.Name = "matchWholeWordCheckBox";
			this.matchWholeWordCheckBox.Size = new System.Drawing.Size(113, 17);
			this.matchWholeWordCheckBox.TabIndex = 7;
			this.matchWholeWordCheckBox.Text = "Match whole word";
			this.matchWholeWordCheckBox.UseVisualStyleBackColor = true;
			// 
			// findAllButton
			// 
			this.findAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.findAllButton.Location = new System.Drawing.Point(165, 161);
			this.findAllButton.Name = "findAllButton";
			this.findAllButton.Size = new System.Drawing.Size(75, 23);
			this.findAllButton.TabIndex = 5;
			this.findAllButton.Text = "Find all";
			this.findAllButton.UseVisualStyleBackColor = true;
			this.findAllButton.Click += new System.EventHandler(this.FindAllButtonClick);
			// 
			// findNextButton
			// 
			this.findNextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.findNextButton.Location = new System.Drawing.Point(84, 161);
			this.findNextButton.Name = "findNextButton";
			this.findNextButton.Size = new System.Drawing.Size(75, 23);
			this.findNextButton.TabIndex = 4;
			this.findNextButton.Text = "Find next";
			this.findNextButton.UseVisualStyleBackColor = true;
			this.findNextButton.Click += new System.EventHandler(this.FindNextButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(246, 161);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// matchCaseCheckBox
			// 
			this.matchCaseCheckBox.AutoSize = true;
			this.matchCaseCheckBox.Location = new System.Drawing.Point(15, 67);
			this.matchCaseCheckBox.Name = "matchCaseCheckBox";
			this.matchCaseCheckBox.Size = new System.Drawing.Size(82, 17);
			this.matchCaseCheckBox.TabIndex = 2;
			this.matchCaseCheckBox.Text = "Match case";
			this.matchCaseCheckBox.UseVisualStyleBackColor = true;
			// 
			// expressionTextBox
			// 
			this.expressionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.expressionTextBox.Location = new System.Drawing.Point(72, 7);
			this.expressionTextBox.Name = "expressionTextBox";
			this.expressionTextBox.Size = new System.Drawing.Size(221, 20);
			this.expressionTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Find what:";
			// 
			// tableView1
			// 
			this.tableView1.ColumnHeaderHeight=(26);
			this.tableView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableView1.Location = new System.Drawing.Point(3, 199);
			this.tableView1.Name = "tableView1";
			this.tableView1.RowHeaderWidth = 70;
			this.tableView1.Size = new System.Drawing.Size(330, 1);
			this.tableView1.TabIndex = 1;
			this.tableView1.VisibleX = 0;
			this.tableView1.VisibleY = 0;
			// 
			// FindForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(336, 218);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(276, 256);
			this.Name = "FindForm";
			this.Text = "Find";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox matchCaseCheckBox;
		private System.Windows.Forms.TextBox expressionTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button findAllButton;
		private System.Windows.Forms.Button findNextButton;
		private CompoundScrollableControl tableView1;
		private System.Windows.Forms.CheckBox matchWholeWordCheckBox;
		private System.Windows.Forms.ComboBox wildcardsComboBox;
		private System.Windows.Forms.ComboBox lookInComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox useCheckBox;
		private System.Windows.Forms.CheckBox searchUpCheckBox;
		private System.Windows.Forms.Button columnSelectButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
	}
}