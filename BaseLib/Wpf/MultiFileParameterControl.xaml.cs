using System.Windows.Controls;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for MultiFileParameterControl.xaml
	/// </summary>
	public partial class MultiFileParameterControl : UserControl {
		public MultiFileParameterControl() {
			InitializeComponent();
		}
		public string[] Filenames{
			get; set;
		}
		public string Filter { get; set; }

		public void Connect(int connectionId, object target){
			
		}
	}
}
