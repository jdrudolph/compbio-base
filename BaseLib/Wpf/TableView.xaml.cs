using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using BaseLib.Forms.Scroll;
using BaseLib.Forms.Table;
using BaseLibS.Num;
using BaseLibS.Table;
using BaseLibS.Util;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for TableView.xaml
	/// </summary>
	public partial class TableView{
		internal static readonly List<ITableSelectionAgent> selectionAgents = new List<ITableSelectionAgent>();
		public event EventHandler SelectionChanged;
		private readonly CompoundScrollableControl tableView;
		private readonly TableViewClient tableViewWf;
		private bool textBoxVisible;
		private bool hasSelectionAgent;
		private ITableSelectionAgent selectionAgent;
		private int selectionAgentColInd = -1;
		private double[] selectionAgentColVals;

		public TableView(){
			InitializeComponent();
			tableView = new CompoundScrollableControl();
			tableViewWf = new TableViewClient();
			tableView.Client = tableViewWf;
			tableViewWf.SelectionChanged += (sender, args) =>{
				SelectionChanged?.Invoke(sender, args);
				long c = tableViewWf.SelectedCount;
				long t = tableViewWf.RowCount;
				SelectedTextBlock.Text = c > 0 && MultiSelect ? "" + StringUtils.WithDecimalSeparators(c) + " selected" : "";
				ItemsTextBlock.Text = "" + StringUtils.WithDecimalSeparators(t) + " item" + (t == 1 ? "" : "s");
			};
			MainPanel.Child = tableView;
			KeyDown += (sender, args) => tableView.Focus();
		}

		public void SelectTime(double timeMs){
			if (selectionAgentColInd < 0){
				return;
			}
			int ind = ArrayUtils.ClosestIndex(selectionAgentColVals, timeMs);
			ClearSelection();
			SetSelectedIndex(ind);
		}

		public static void RegisterSelectionAgent(ITableSelectionAgent agent){
			selectionAgents.Add(agent);
		}

		public static void UnregisterSelectionAgent(ITableSelectionAgent agent){
			selectionAgents.Remove(agent);
		}

		public bool HasSelectionAgent{
			get { return hasSelectionAgent; }
			set{
				hasSelectionAgent = value;
				if (hasSelectionAgent && selectionAgents.Count > 0){
					SelectionAgentButton.Visibility = Visibility.Visible;
				}
			}
		}

		public ITableModel TableModel{
			get { return tableViewWf.TableModel; }
			set{
				tableViewWf.TableModel = value;
				ItemsTextBlock.Text = value != null ? "" + StringUtils.WithDecimalSeparators(value.RowCount) + " items" : "";
			}
		}

		public void Select(){
			tableView.Select();
		}

		public void SwitchOnTextBox(){
			tableViewWf.SetCellText = s => AuxTextBox.Text = s;
			MainGrid.RowDefinitions.Clear();
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(5)});
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(30)});
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(17)});
		}

		public void SwitchOffTextBox(){
			AuxTextBox.Text = "";
			tableViewWf.SetCellText = null;
			MainGrid.RowDefinitions.Clear();
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(0)});
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(0)});
			MainGrid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(17)});
		}

		public bool MultiSelect{
			get { return tableViewWf.MultiSelect; }
			set { tableViewWf.MultiSelect = value; }
		}

		public bool Sortable{
			get { return tableViewWf.Sortable; }
			set { tableViewWf.Sortable = value; }
		}

		public int RowCount => tableViewWf.RowCount;

		public int RowHeaderWidth{
			get { return tableView.RowHeaderWidth; }
			set { tableView.RowHeaderWidth = value; }
		}

		public int ColumnHeaderHeight{
			get { return tableView.ColumnHeaderHeight; }
			set{
				tableViewWf.origColumnHeaderHeight = value;
				tableView.ColumnHeaderHeight=(value);
			}
		}

		public int VisibleX{
			get { return tableView.VisibleX; }
			set { tableView.VisibleX = value; }
		}

		public int VisibleY{
			get { return tableView.VisibleY; }
			set { tableView.VisibleY = value; }
		}

		public void SetSelectedRow(int row){
			tableViewWf.SetSelectedRow(row);
		}

		public void SetSelectedRow(int row, bool add, bool fire){
			tableViewWf.SetSelectedRow(row, add, fire);
		}

		public bool HasSelectedRows(){
			return tableViewWf.HasSelectedRows();
		}

		public void SetSelectedRows(IList<int> rows){
			tableViewWf.SetSelectedRows(rows);
		}

		public void SetSelectedRows(IList<int> rows, bool add, bool fire){
			tableViewWf.SetSelectedRows(rows, add, fire);
		}

		public void SetSelectedRowAndMove(int row){
			tableViewWf.SetSelectedRowAndMove(row);
		}

		public void SetSelectedRowsAndMove(IList<int> rows){
			tableViewWf.SetSelectedRowsAndMove(rows);
		}

		public void Invalidate(){
			tableView.Invalidate(true);
		}

		public int[] GetSelectedRows(){
			return tableViewWf.GetSelectedRows();
		}

		public int GetSelectedRow(){
			return tableViewWf.GetSelectedRow();
		}

		public void ScrollToRow(int row){
			tableViewWf.ScrollToRow(row);
		}

		public void BringSelectionToTop(){
			tableViewWf.BringSelectionToTop();
		}

		public void FireSelectionChange(){
			tableViewWf.FireSelectionChange();
		}

		public bool ModelRowIsSelected(int row){
			return tableViewWf.ModelRowIsSelected(row);
		}

		public void ClearSelection(){
			tableViewWf.ClearSelection();
		}

		public void SelectAll(){
			tableViewWf.SelectAll();
		}

		public void SetSelection(bool[] selection){
			tableViewWf.SetSelection(selection);
		}

		public void SetSelectedIndex(int index){
			tableViewWf.SetSelectedIndex(index);
		}

		public void SetSelectedViewIndex(int index){
			tableViewWf.SetSelectedViewIndex(index);
		}

		public void SetSelectedIndex(int index, object sender){
			tableViewWf.SetSelectedIndex(index, sender);
		}

		public void AddContextMenuItem(ToolStripItem item){
			tableViewWf.AddContextMenuItem(item);
		}

		public object GetEntry(int row, int col){
			return tableViewWf.GetEntry(row, col);
		}

		private void TextButton_OnClick(object sender, RoutedEventArgs e){
			if (textBoxVisible){
				SwitchOffTextBox();
			} else{
				SwitchOnTextBox();
			}
			textBoxVisible = !textBoxVisible;
		}

		public void RegisterScrollViewer(ScrollViewer scrollViewer){
			MainPanel.RegisterScrollViewer(scrollViewer);
		}

		public void UnregisterScrollViewer(ScrollViewer scrollViewer){
			MainPanel.UnregisterScrollViewer(scrollViewer);
		}

		public void ClearSelectionFire(){
			tableViewWf.ClearSelectionFire();
		}

		private void SelectionAgentButton_OnClick(object sender, RoutedEventArgs e){
			Point p = SelectionAgentButton.PointToScreen(new Point(0, 0));
			TableViewSelectionAgentWindow w = new TableViewSelectionAgentWindow(TableModel){Top = p.Y - 125, Left = p.X - 300};
			if (w.ShowDialog() == true){
				int ind1 = w.SourceBox.SelectedIndex;
				int ind2 = w.ColumnBox.SelectedIndex;
				if (ind1 >= 0 && ind2 >= 0){
					selectionAgent = selectionAgents[ind1];
					selectionAgentColInd = ind2;
					selectionAgentColVals = GetTimeVals(ind2);
					selectionAgent.AddTable(this);
				} else{
					selectionAgent = null;
					selectionAgentColInd = -1;
					selectionAgentColVals = null;
					selectionAgent.RemoveTable(this);
				}
			}
		}

		private double[] GetTimeVals(int ind2){
			double[] result = new double[TableModel.RowCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = (double) TableModel.GetEntry(i, ind2);
			}
			return result;
		}
	}
}