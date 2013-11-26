using BaseLib.Forms.Axis;

namespace BaseLib.Forms.Colors{
	partial class ColorScale {
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
			this.colorStrip = new ColorStrip();
			this.axis = new NumericAxis();
			this.SuspendLayout();
			// 
			// colorStrip
			// 
			this.colorStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.colorStrip.Location = new System.Drawing.Point(0, 59);
			this.colorStrip.Margin = new System.Windows.Forms.Padding(0);
			this.colorStrip.Name = "colorStrip";
			this.colorStrip.Size = new System.Drawing.Size(749, 31);
			this.colorStrip.TabIndex = 1;
			// 
			// axis
			// 
			this.axis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.axis.ForeColor = System.Drawing.Color.Black;
			this.axis.Location = new System.Drawing.Point(0, 0);
			this.axis.Margin = new System.Windows.Forms.Padding(0);
			this.axis.Name = "axis";
			this.axis.Size = new System.Drawing.Size(749, 59);
			this.axis.TabIndex = 0;
			// 
			// ColorScale
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.colorStrip);
			this.Controls.Add(this.axis);
			this.Name = "ColorScale";
			this.Size = new System.Drawing.Size(749, 90);
			this.ResumeLayout(false);

		}

		#endregion

		private NumericAxis axis;
		private ColorStrip colorStrip;
	}
}