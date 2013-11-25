using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicLib.Wpf {
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
