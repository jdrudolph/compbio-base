using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BaseLib.Forms.Select{
	internal partial class MultiFileParameterPanel : UserControl{
		public MultiFileParameterPanel(){
			InitializeComponent();
		}

		public string[] Filenames{
			get { return ToStrings(fastaFileslistBox.Items); }
			set{
				fastaFileslistBox.Items.Clear();
				foreach (string s in value){
					fastaFileslistBox.Items.Add(s);
				}
			}
		}
		public string Filter { get; set; }

		private void AddFilesButtonClick(object sender, EventArgs e){
			OpenFileDialog ofd = new OpenFileDialog{Multiselect = true, Filter = Filter};
			if (ofd.ShowDialog() == DialogResult.OK){
				AddFastaFiles(ofd.FileNames, fastaFileslistBox);
			}
		}

		private static void AddFastaFiles(IEnumerable<string> filenames, ListBox listbox){
			string[] names = ToStrings(listbox.Items);
			foreach (string file in filenames.Where(file => !names.Contains(file))){
				listbox.Items.Add(file);
			}
		}

		private static string[] ToStrings(IList collection){
			string[] result = new string[collection.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = collection[i].ToString();
			}
			return result;
		}

		private void DeleteFileButtonClick(object sender, EventArgs e){
			if (fastaFileslistBox.SelectedIndex >= 0){
				fastaFileslistBox.Items.RemoveAt(fastaFileslistBox.SelectedIndex);
			}
		}
	}
}