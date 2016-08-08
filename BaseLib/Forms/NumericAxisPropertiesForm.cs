using System.Windows.Forms;

namespace BaseLib.Forms{
	internal partial class NumericAxisPropertiesForm : Form{
		internal bool Ok { get; set; }

		internal NumericAxisPropertiesForm(string title, double minValue, double maxValue){
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

		internal string Title { get { return titleTextBox.Text; } set { titleTextBox.Text = value; } }
		internal double MinValue{
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
		internal double MaxValue{
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