namespace BasicLib.Forms.Select {
	partial class MultiFileParameterPanel {
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
			this.deleteFastaFilebutton = new System.Windows.Forms.Button();
			this.fastaFileslistBox = new System.Windows.Forms.ListBox();
			this.addFastaFileButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// deleteFastaFilebutton
			// 
			this.deleteFastaFilebutton.Location = new System.Drawing.Point(84, 3);
			this.deleteFastaFilebutton.Name = "deleteFastaFilebutton";
			this.deleteFastaFilebutton.Size = new System.Drawing.Size(74, 23);
			this.deleteFastaFilebutton.TabIndex = 46;
			this.deleteFastaFilebutton.Text = "Remove file";
			this.deleteFastaFilebutton.UseVisualStyleBackColor = true;
			this.deleteFastaFilebutton.Click += new System.EventHandler(this.DeleteFileButtonClick);
			// 
			// fastaFileslistBox
			// 
			this.fastaFileslistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fastaFileslistBox.FormattingEnabled = true;
			this.fastaFileslistBox.Location = new System.Drawing.Point(4, 30);
			this.fastaFileslistBox.Name = "fastaFileslistBox";
			this.fastaFileslistBox.Size = new System.Drawing.Size(531, 108);
			this.fastaFileslistBox.TabIndex = 45;
			// 
			// addFastaFileButton
			// 
			this.addFastaFileButton.Location = new System.Drawing.Point(4, 3);
			this.addFastaFileButton.Name = "addFastaFileButton";
			this.addFastaFileButton.Size = new System.Drawing.Size(74, 23);
			this.addFastaFileButton.TabIndex = 44;
			this.addFastaFileButton.Text = "Add file";
			this.addFastaFileButton.UseVisualStyleBackColor = true;
			this.addFastaFileButton.Click += new System.EventHandler(this.AddFilesButtonClick);
			// 
			// MultiFileParameterPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.deleteFastaFilebutton);
			this.Controls.Add(this.fastaFileslistBox);
			this.Controls.Add(this.addFastaFileButton);
			this.Name = "MultiFileParameterPanel";
			this.Size = new System.Drawing.Size(538, 141);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button deleteFastaFilebutton;
		private System.Windows.Forms.ListBox fastaFileslistBox;
		private System.Windows.Forms.Button addFastaFileButton;
	}
}
