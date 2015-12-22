using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BaseLibS.Num;
using BaseLibS.Param;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for MultiListSelectorControl.xaml
	/// </summary>
	public partial class MultiListSelectorControl{
		public event EventHandler SelectionChanged;
		public IList<string> items;
		internal ListBox AllListBox { get; }
		private SubSelectionControl[] subSelection;
		private readonly Grid tableLayoutPanel1;

		public MultiListSelectorControl(){
			InitializeComponent();
			AllListBox = new ListBox{SelectionMode = SelectionMode.Extended};
			ListBoxSelector.SetEnabled(AllListBox, true);
			tableLayoutPanel1 = new Grid();
			tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(44, GridUnitType.Star)});
			tableLayoutPanel1.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(56, GridUnitType.Star)});
			Grid.SetRow(AllListBox, 0);
			Grid.SetColumn(AllListBox, 0);
			tableLayoutPanel1.Children.Add(AllListBox);
			tableLayoutPanel1.Margin = new Thickness(0);
			tableLayoutPanel1.RowDefinitions.Add(new RowDefinition());
			Content = tableLayoutPanel1;
		}

		public void Init(IList<string> items1){
			items = items1;
			AllListBox.Items.Clear();
			foreach (string s in items1){
				AllListBox.Items.Add(s);
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
			foreach (SubSelectionControl t in subSelection){
				foreach (object x in t.SelectedListBox.Items){
					AllListBox.Items.Add(x);
				}
				t.SelectedListBox.Items.Clear();
			}
		}

		public void Init(IList<string> items1, IList<string> selectorNames){
			Init(items1, selectorNames, new Func<string[], Parameters>[0]);
		}

		public void Init(IList<string> items1, IList<string> selectorNames, IList<Func<string[], Parameters>> subParams){
			items = items1;
			foreach (string s in items1){
				AllListBox.Items.Add(s);
			}
			int n = selectorNames.Count;
			Grid tableLayoutPanel2 = new Grid();
			subSelection = new SubSelectionControl[n];
			for (int i = 0; i < n; i++){
				subSelection[i] = new SubSelectionControl{MultiListSelectorControl = this, Text = selectorNames[i]};
				if (subParams != null && i < subParams.Count && subParams[i] != null){
					subSelection[i].Parameters = subParams[i];
				}
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

		internal void SelectionHasChanged(SubSelectionControl sender, EventArgs e){
			SelectionChanged?.Invoke(this, e);
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