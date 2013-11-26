namespace BaseLib.Forms.Select {
	partial class DictionaryIntValueControlWf {
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
			this.button = new System.Windows.Forms.Button();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button
			// 
			this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button.Location = new System.Drawing.Point(264, 0);
			this.button.Name = "button";
			this.button.Size = new System.Drawing.Size(35, 21);
			this.button.TabIndex = 3;
			this.button.Text = "Edit";
			this.button.UseVisualStyleBackColor = true;
			this.button.Click += new System.EventHandler(this.ButtonClick);
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.Location = new System.Drawing.Point(3, 1);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(255, 20);
			this.textBox.TabIndex = 2;
			// 
			// DictionaryIntValueControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button);
			this.Controls.Add(this.textBox);
			this.Name = "DictionaryIntValueControl";
			this.Size = new System.Drawing.Size(302, 26);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button;
		private System.Windows.Forms.TextBox textBox;
	}
}
