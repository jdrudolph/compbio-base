
using BasicLib.Forms.Base;

namespace BasicLib.Forms.Help {
	partial class HelpCheckBox {
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
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.helpLabel1 = new HelpLabel();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(3, 3);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(15, 14);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// helpLabel1
			// 
			this.helpLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.helpLabel1.HelpText = null;
			this.helpLabel1.Location = new System.Drawing.Point(19, 3);
			this.helpLabel1.Name = "helpLabel1";
			this.helpLabel1.Size = new System.Drawing.Size(102, 18);
			this.helpLabel1.TabIndex = 1;
			this.helpLabel1.Text = "helpLabel1";
			// 
			// HelpCheckBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.helpLabel1);
			this.Controls.Add(this.checkBox1);
			this.Name = "HelpCheckBox";
			this.Size = new System.Drawing.Size(126, 21);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBox1;
		private HelpLabel helpLabel1;
	}
}
