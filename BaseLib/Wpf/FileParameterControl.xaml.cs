using System;
using System.ComponentModel;
using System.Windows;
using BaseLib.Annotations;
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

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
		    var vm = (FileParamterViewModel) DataContext;
            vm.ChooseFile();
			WpfUtils.SetOkFocus(this);
		}
	}

    internal class FileParamterViewModel : ViewModelBase
    {
        internal FileParamterViewModel(string fileName, string filter, Func<string, string> processFileName, bool save)
        {
            FileName = fileName;
            _filter = filter;
            _processFileName = processFileName;
            _save = save;
            PropertyChanged += (sender, args) =>
            {
                var x = fileName;
            };
        }
        private string _fileName;
		public string FileName { get { return _fileName; } set { _fileName = value;
                OnPropertyChanged(nameof(FileName)); } }
        private string _filter;
        private bool _save;
        private Func<string, string> _processFileName;

        internal void ChooseFile()
        {
            if (_save)
            {
                SaveFileDialog ofd = new SaveFileDialog {FileName = FileName};
                if (!string.IsNullOrEmpty(_filter))
                {
                    ofd.Filter = _filter;
                }
                if (ofd.ShowDialog() == true)
                {
                    FileName = ofd.FileName;
                }
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (!string.IsNullOrEmpty(_filter))
                {
                    ofd.Filter = _filter;
                }
                if (ofd.ShowDialog() == true)
                {
                    string s = ofd.FileName;
                    if (_processFileName != null)
                    {
                        s = _processFileName(s);
                    }
                    FileName = s;
                }
            }
        }
    }
}