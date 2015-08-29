using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for MultiFileParameterControl.xaml
	/// </summary>
	public partial class MultiFileParameterControl {
		public MultiFileParameterControl(){
			InitializeComponent();
		}

		public void Connect(int connectionId, object target) {}

		private void AddButton_OnClick(object sender, RoutedEventArgs e){
			OpenFileDialog ofd = new OpenFileDialog{Multiselect = true, Filter = Filter};
			if (ofd.ShowDialog() == true){
				AddFastaFiles(ofd.FileNames, ListBox1);
			}
		}

		private void RemoveButton_OnClick(object sender, RoutedEventArgs e){
			if (ListBox1.SelectedIndex >= 0){
				ListBox1.Items.RemoveAt(ListBox1.SelectedIndex);
			}
		}

		public string[] Filenames{
			get { return ToStrings(ListBox1.Items); }
			set{
				ListBox1.Items.Clear();
				foreach (string s in value){
					ListBox1.Items.Add(s);
				}
			}
		}
		public string Filter { get; set; }

		private static void AddFastaFiles(IEnumerable<string> filenames, ItemsControl listbox){
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
	}
}