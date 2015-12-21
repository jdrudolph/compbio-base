namespace BaseLib.Forms.Table {
	partial class SelectColumnsForm {
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
			this.listSelector1 = new ListSelector();
			this.SuspendLayout();
			// 
			// listSelector1
			// 
			this.listSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listSelector1.HasMoveButtons = false;
			this.listSelector1.Location = new System.Drawing.Point(0, 0);
			this.listSelector1.Name = "listSelector1";
			this.listSelector1.Repeats = false;
			this.listSelector1.Size = new System.Drawing.Size(319, 230);
			this.listSelector1.TabIndex = 0;
			// 
			// SelectColumnsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(319, 230);
			this.Controls.Add(this.listSelector1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectColumnsForm";
			this.ResumeLayout(false);

		}

		#endregion

		private ListSelector listSelector1;
	}
}