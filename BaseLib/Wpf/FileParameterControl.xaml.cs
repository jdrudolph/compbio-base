using System.Windows.Controls;

namespace BaseLib.Wpf {
	/// <summary>
	/// Interaction logic for FileParameterControl.xaml
	/// </summary>
	public partial class FileParameterControl : UserControl {
		public FileParameterControl() {
			InitializeComponent();
		}
		public string Filter { get; set; }
		public string Text { get; set; }
		public bool Save { get; set; }
		public void Connect(int connectionId, object target){
			
		}
	}
}
