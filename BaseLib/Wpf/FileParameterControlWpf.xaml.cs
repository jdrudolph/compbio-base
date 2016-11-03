using System;
using System.Windows;
using System.Windows.Forms;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for FileParameterControl.xaml
	/// </summary>
	public partial class FileParameterControlWpf{
		public FileParameterControlWpf(string fileName, string filter, Func<string, string> processFileName, bool save){
			InitializeComponent();
			FileParamterViewModel vm = new FileParamterViewModel(fileName, filter, processFileName, save);
			DataContext = vm;
		}

		private void ButtonClick(object sender, RoutedEventArgs e){
			FileParamterViewModel vm = (FileParamterViewModel) DataContext;
			vm.ChooseFile();
			WpfUtils.SetOkFocus(this);
		}
	}

	internal class FileParamterViewModel : ViewModelBase{
		internal FileParamterViewModel(string fileName, string filter, Func<string, string> processFileName, bool save){
			FileName = fileName;
			this.filter = filter;
			this.processFileName = processFileName;
			this.save = save;
		}

		private string fileName;

		public string FileName{
			get { return fileName; }
			set{
				fileName = value;
				OnPropertyChanged(nameof(FileName));
			}
		}

		private readonly string filter;
		private readonly bool save;
		private readonly Func<string, string> processFileName;

		internal void ChooseFile(){
			if (save){
				SaveFileDialog ofd = new SaveFileDialog{FileName = FileName};
				if (!string.IsNullOrEmpty(filter)){
					ofd.Filter = filter;
				}
				if (ofd.ShowDialog() == DialogResult.OK){
					FileName = ofd.FileName;
				}
			} else{
				OpenFileDialog ofd = new OpenFileDialog();
				if (!string.IsNullOrEmpty(filter)){
					ofd.Filter = filter;
				}
				if (ofd.ShowDialog() == DialogResult.OK){
					string s = ofd.FileName;
					if (processFileName != null){
						s = processFileName(s);
					}
					FileName = s;
				}
			}
		}
	}
}