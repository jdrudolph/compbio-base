using System.Windows.Forms;

namespace BasicLib.Forms.Axis{
	public partial class NumericAxisPropertiesForm : Form{
		public bool Ok { get; set; }

		public NumericAxisPropertiesForm(string title, double minValue, double maxValue){
			InitializeComponent();
			MinValue = minValue;
			MaxValue = maxValue;
			Title = title;
		}

		private void CancelButtonClick(object sender, System.EventArgs e){
			Ok = false;
			Close();
		}

		private void OkButtonClick(object sender, System.EventArgs e){
			Ok = true;
			Close();
		}

		public string Title { get { return titleTextBox.Text; } set { titleTextBox.Text = value; } }
		public double MinValue{
			get{
				double x;
				bool s = double.TryParse(minValueTextBox.Text, out x);
				if (!s){
					return double.NaN;
				}
				return double.IsInfinity(x) ? double.NaN : x;
			}
			set { minValueTextBox.Text = "" + value; }
		}
		public double MaxValue{
			get{
				double x;
				bool s = double.TryParse(maxValueTextBox.Text, out x);
				if (!s){
					return double.NaN;
				}
				return double.IsInfinity(x) ? double.NaN : x;
			}
			set { maxValueTextBox.Text = "" + value; }
		}
	}
}