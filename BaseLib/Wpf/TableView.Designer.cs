namespace BaseLib.Wpf
{
	partial class TableView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.selectionAgentButton = new System.Windows.Forms.Button();
			this.textButton = new System.Windows.Forms.Button();
			this.itemsLabel = new System.Windows.Forms.Label();
			this.selectedLabel = new System.Windows.Forms.Label();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.mainPanel, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(523, 538);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 5;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel2.Controls.Add(this.selectionAgentButton, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.textButton, 4, 0);
			this.tableLayoutPanel2.Controls.Add(this.itemsLabel, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.selectedLabel, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 520);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(523, 18);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// selectionAgentButton
			// 
			this.selectionAgentButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.selectionAgentButton.Location = new System.Drawing.Point(487, 0);
			this.selectionAgentButton.Margin = new System.Windows.Forms.Padding(0);
			this.selectionAgentButton.Name = "selectionAgentButton";
			this.selectionAgentButton.Size = new System.Drawing.Size(18, 18);
			this.selectionAgentButton.TabIndex = 0;
			this.selectionAgentButton.UseVisualStyleBackColor = true;
			// 
			// textButton
			// 
			this.textButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textButton.Location = new System.Drawing.Point(505, 0);
			this.textButton.Margin = new System.Windows.Forms.Padding(0);
			this.textButton.Name = "textButton";
			this.textButton.Size = new System.Drawing.Size(18, 18);
			this.textButton.TabIndex = 1;
			this.textButton.UseVisualStyleBackColor = true;
			// 
			// itemsLabel
			// 
			this.itemsLabel.AutoSize = true;
			this.itemsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.itemsLabel.Location = new System.Drawing.Point(3, 0);
			this.itemsLabel.Name = "itemsLabel";
			this.itemsLabel.Size = new System.Drawing.Size(1, 18);
			this.itemsLabel.TabIndex = 2;
			// 
			// selectedLabel
			// 
			this.selectedLabel.AutoSize = true;
			this.selectedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.selectedLabel.Location = new System.Drawing.Point(9, 0);
			this.selectedLabel.Name = "selectedLabel";
			this.selectedLabel.Size = new System.Drawing.Size(1, 18);
			this.selectedLabel.TabIndex = 3;
			// 
			// mainPanel
			// 
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(523, 520);
			this.mainPanel.TabIndex = 1;
			// 
			// TableView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TableView";
			this.Size = new System.Drawing.Size(523, 538);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button selectionAgentButton;
		private System.Windows.Forms.Button textButton;
		private System.Windows.Forms.Label itemsLabel;
		private System.Windows.Forms.Label selectedLabel;
		private System.Windows.Forms.Panel mainPanel;
	}
}
