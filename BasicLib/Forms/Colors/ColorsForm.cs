using System.ComponentModel;
using System.Windows.Forms;

namespace BasicLib.Forms.Colors{
	public partial class ColorsForm : Form{
		public ColorsForm(){
			InitializeComponent();
			colorScale.Text = "Intensity";
		}

		public ColorScale ColorScale { get { return colorScale; } }

		protected override void OnClosing(CancelEventArgs e){
			e.Cancel = true;
			Visible = false;
		}
	}
}