using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BaseLibS.Util;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for MultiListSelectorControl.xaml
	/// </summary>
	public partial class MultiListSelectorControl{
		public event EventHandler SelectionChanged;
		private MultiListSelectorSubSelectionControl[] subSelection;
		public IList<string> items;

		public MultiListSelectorControl(){
			InitializeComponent();
			allListBox = new ListBox{SelectionMode = SelectionMode.Extended};
			ListBoxSelector.SetEnabled(allListBox, true);
			tableLayoutPanel1 = new Grid();
			tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(44, GridUnitType.Star)});
			tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(56, GridUnitType.Star)});
			Grid.SetRow(allListBox, 0);
			Grid.SetColumn(allListBox, 0);
			tableLayoutPanel1.Children.Add(allListBox);
			tableLayoutPanel1.Margin = new Thickness(0);
			tableLayoutPanel1.RowDefinitions.Add(new RowDefinition());
			Content = tableLayoutPanel1;
		}

		private readonly ListBox allListBox;
		private readonly Grid tableLayoutPanel1;

		public void Init(IList<string> items1){
			items = items1;
			allListBox.Items.Clear();
			foreach (string s in items1){
				allListBox.Items.Add(s);
			}
			ClearSelection();
		}

		public int[][] SelectedIndices{
			get{
				int[][] result = new int[subSelection.Length][];
				for (int i = 0; i < result.Length; i++){
					result[i] = GetSelectedIndices(i);
				}
				return result;
			}
			set{
				ClearSelection();
				for (int i = 0; i < value.Length; i++){
					foreach (int x in value[i]){
						SetSelected(i, x, true);
					}
				}
			}
		}

		private void ClearSelection(){
			foreach (MultiListSelectorSubSelectionControl t in subSelection){
				foreach (object x in t.SelectedListBox.Items){
					AllListBox.Items.Add(x);
				}
				t.SelectedListBox.Items.Clear();
			}
		}

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
				subSelection[i] = new MultiListSelectorSubSelectionControl{MultiListSelectorControl = this, Text = selectorNames[i]};
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
			Grid.SetRow(tableLayoutPanel2, 0);
			Grid.SetColumn(tableLayoutPanel2, 1);
			tableLayoutPanel1.Children.Add(tableLayoutPanel2);
		}

		internal void SelectionHasChanged(MultiListSelectorSubSelectionControl sender, EventArgs e){
			if (SelectionChanged != null){
				SelectionChanged(this, e);
			}
		}

		private HashSet<string> GetSubSelection(int selectorInd){
			return new HashSet<string>(subSelection[selectorInd].SelectedStrings);
		}

		public void SetSelected(int selectorInd, int itemInd, bool b){
			HashSet<string> x = GetSubSelection(selectorInd);
			if (x.Contains(items[itemInd])){
				return;
			}
			if (!AllListBox.Items.Contains(items[itemInd])){
				for (int i = 0; i < subSelection.Length; i++){
					if (i == selectorInd){
						continue;
					}
					if (subSelection[i].SelectedListBox.Items.Contains(items[itemInd])){
						subSelection[i].SelectedListBox.Items.Remove(items[itemInd]);
						AllListBox.Items.Add(items[itemInd]);
						break;
					}
				}
			}
			AllListBox.Items.Remove(items[itemInd]);
			subSelection[selectorInd].SelectedListBox.Items.Add(items[itemInd]);
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