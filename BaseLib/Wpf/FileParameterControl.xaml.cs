using System;
using System.Windows;
using Microsoft.Win32;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for FileParameterControl.xaml
	/// </summary>
	public partial class FileParameterControl{
		public FileParameterControl(string fileName, string filter, Func<string, string> processFileName, bool save){
			InitializeComponent();
			var vm = new FileParamterViewModel(fileName, filter, processFileName, save);
			DataContext = vm;
		}

		private void ButtonClick(object sender, RoutedEventArgs e){
			var vm = (FileParamterViewModel) DataContext;
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
			PropertyChanged += (sender, args) => { var x = fileName; };
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
				if (ofd.ShowDialog() == true){
					FileName = ofd.FileName;
				}
			} else{
				OpenFileDialog ofd = new OpenFileDialog();
				if (!string.IsNullOrEmpty(filter)){
					ofd.Filter = filter;
				}
				if (ofd.ShowDialog() == true){
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