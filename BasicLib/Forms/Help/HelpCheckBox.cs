using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BasicLib.Forms.Help{
	public partial class HelpCheckBox : UserControl{
		public HelpCheckBox(){
			InitializeComponent();
			EnabledChanged += HelpCheckBoxEnabledChanged;
		}

		private void HelpCheckBoxEnabledChanged(object sender, EventArgs e){
			checkBox1.Enabled = Enabled;
			helpLabel1.Enabled = Enabled;
			Invalidate(true);
		}

		public string HelpText { get { return helpLabel1.HelpText; } set { helpLabel1.HelpText = value; } }
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text { get { return helpLabel1.Text; } set { helpLabel1.Text = value; } }
		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool Checked { get { return checkBox1.Checked; } set { checkBox1.Checked = value; } }
		public event EventHandler CheckedChanged { add { checkBox1.CheckedChanged += value; } remove { checkBox1.CheckedChanged -= value; } }
	}
}