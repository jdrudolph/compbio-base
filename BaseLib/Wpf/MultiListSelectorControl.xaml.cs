using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Util;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for MultiListSelectorControl.xaml
	/// </summary>
	public partial class MultiListSelectorControl : UserControl{
		public event EventHandler SelectionChanged;
		private MultiListSelectorSubSelectionControl[] subSelection;
		public IList<string> items;

		public MultiListSelectorControl(){
			InitializeComponent();
			allListBox = new ListBox();
			tableLayoutPanel1 = new Grid();
			//allListBox.Width = 182;
			//allListBox.Height = 368;
			tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(44, GridUnitType.Star)});
			tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(56, GridUnitType.Star)});
			Grid.SetRow(allListBox, 0);
			Grid.SetColumn(allListBox, 0);
			tableLayoutPanel1.Children.Add(allListBox);
			tableLayoutPanel1.Margin = new Thickness(0);
			tableLayoutPanel1.RowDefinitions.Add(new RowDefinition());
			//tableLayoutPanel1.Width = 428;
			//tableLayoutPanel1.Height = 379;
			Content = tableLayoutPanel1;
			//Width = 428;
			//Height = 379;
		}

		private readonly ListBox allListBox;
		private readonly Grid tableLayoutPanel1;

		public void Init(IList<string> items1){
			items = items1;
			allListBox.Items.Clear();
			foreach (string s in items1){
				allListBox.Items.Add(s);
			}
		}

		public int[][] SelectedIndices { get; set; }
		public void Connect(int connectionId, object target) {}

		public void Init(IList<string> items1, IList<string> selectorNames){
			items = items1;
			foreach (string s in items1){
				allListBox.Items.Add(s);
			}
			int n = selectorNames.Count;
			Grid tableLayoutPanel2 = new Grid();
			subSelection = new MultiListSelectorSubSelectionControl[n];
			for (int i = 0; i < n; i++){
				subSelection[i] = new MultiListSelectorSubSelectionControl
				{MultiListSelectorControl = this};
				//Content = selectorNames[i]
			}
			tableLayoutPanel2.Margin = new Thickness(0);
			tableLayoutPanel2.ColumnDefinitions.Add(new ColumnDefinition());
			for (int i = 0; i < n; i++){
				tableLayoutPanel2.RowDefinitions.Add(new RowDefinition{Height = new GridLength(10, GridUnitType.Star)});
			}
			for (int i = 0; i < n; i++){
				Grid.SetRow(subSelection[i], i);
				Grid.SetColumn(subSelection[i], 0);
				tableLayoutPanel2.Children.Add(subSelection[i]);
			}
			//tableLayoutPanel2.Width = 240;
			//tableLayoutPanel2.Height = 379;
			Grid.SetRow(tableLayoutPanel2, 0);
			Grid.SetColumn(tableLayoutPanel2, 1);
			tableLayoutPanel1.Children.Add(tableLayoutPanel2);
		}

		internal void SelectionHasChanged(MultiListSelectorSubSelectionControl sender, EventArgs e){
			if (SelectionChanged != null){
				SelectionChanged(this, e);
			}
		}

		public void SetSelected(int selectorInd, int itemInd, bool b){
			//TODO
		}

		internal ListBox AllListBox { get { return allListBox; } }

		public int[] GetSelectedIndices(int selectorInd){
			string[] sel = subSelection[selectorInd].SelectedStrings;
			int[] result = new int[sel.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = ArrayUtils.IndexOf(items, sel[i]);
			}
			return result;
		}
	}
}