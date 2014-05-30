using System;
using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Forms.Table{
	/// <summary>
	/// Interaction logic for PasteSelectionWindow.xaml
	/// </summary>
	public partial class PasteSelectionWindow{
		private readonly int ncols;
		public bool Ok { get; set; }
		private readonly ComboBox[] cbs;

		public PasteSelectionWindow(int ncols, string[] columnNames){
			InitializeComponent();
			this.ncols = ncols;
			Grid g = new Grid();
			for (int i = 0; i < ncols; i++){
				g.RowDefinitions.Add(new RowDefinition{Height = new GridLength(25)});
			}
			g.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			g.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(80)});
			g.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(80, GridUnitType.Star)});
			if (ncols == 1){
				TextBlock tb = new TextBlock{Text = "Column"};
				Grid.SetColumn(tb, 0);
				Grid.SetRow(tb, 0);
				g.Children.Add(tb);
			} else{
				for (int i = 0; i < ncols; i++){
					TextBlock tb = new TextBlock{Text = "Column " + (i + 1)};
					Grid.SetColumn(tb, 0);
					Grid.SetRow(tb, i);
					g.Children.Add(tb);
				}
			}
			cbs = new ComboBox[ncols];
			for (int i = 0; i < ncols; i++){
				ComboBox cb = new ComboBox();
				cbs[i] = cb;
				foreach (string t in columnNames){
					cb.Items.Add(t);
				}
				Grid.SetColumn(cb, 1);
				Grid.SetRow(cb, i);
				cb.SelectedIndex = Math.Min(columnNames.Length - 1, i);
				g.Children.Add(cb);
			}
			MainPanel.Children.Add(g);
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e){
			Ok = false;
			Close();
		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e){
			Ok = true;
			Close();
		}

		public int[] GetSelectedIndices(){
			int[] result = new int[ncols];
			for (int i = 0; i < result.Length; i++){
				result[i] = cbs[i].SelectedIndex;
			}
			return result;
		}
	}
}