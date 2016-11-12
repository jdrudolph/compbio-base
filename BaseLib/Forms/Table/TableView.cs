using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Forms.Scroll;
using BaseLibS.Num;
using BaseLibS.Table;
using BaseLibS.Util;

namespace BaseLib.Forms.Table{
	public partial class TableView : UserControl{
		internal static readonly List<ITableSelectionAgent> selectionAgents = new List<ITableSelectionAgent>();
		public event EventHandler SelectionChanged;
		private readonly CompoundScrollableControl tableView;
		private readonly TableViewControlModel tableViewWf;
		private bool textBoxVisible;
		private bool hasSelectionAgent;
		private ITableSelectionAgent selectionAgent;
		private int selectionAgentColInd = -1;
		private double[] selectionAgentColVals;
		private readonly TextBox auxTextBox;

		public TableView(){
			InitializeComponent();
			tableView = new CompoundScrollableControl{Dock = DockStyle.Fill, Margin = new Padding(0)};
			tableViewWf = new TableViewControlModel();
			tableView.Client = tableViewWf;
			tableViewWf.SelectionChanged += (sender, args) =>{
				SelectionChanged?.Invoke(sender, args);
				long c = tableViewWf.SelectedCount;
				long t = tableViewWf.RowCount;
				selectedLabel.Text = c > 0 && MultiSelect ? "" + StringUtils.WithDecimalSeparators(c) + " selected" : "";
				itemsLabel.Text = "" + StringUtils.WithDecimalSeparators(t) + " item" + (t == 1 ? "" : "s");
			};
			mainPanel.Controls.Add(tableView);
			textButton.Click += TextButton_OnClick;
			selectionAgentButton.Click += SelectionAgentButton_OnClick;
			KeyDown += (sender, args) => tableView.Focus();
			auxTextBox = new TextBox{Dock = DockStyle.Fill, Padding = new Padding(0), Multiline = true};
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
					selectionAgentButton.Visible = true;
				}
			}
		}

		//public static readonly DependencyProperty TableModelProperty = DependencyProperty.Register("TableModel",
		//	typeof (ITableModel), typeof (TableViewWpf), new PropertyMetadata(default(ITableModel), (o, args) =>{
		//		var x = (TableViewWpf) o;
		//		var value = (ITableModel) args.NewValue;
		//		x.tableViewWf.TableModel = value;
		//		x.ItemsTextBlock.Text = value != null ? "" + StringUtils.WithDecimalSeparators(value.RowCount) + " items" : "";
		//	}));

		/// <summary>
		/// Get the table model.
		/// Use <code>Dispatcher.Invoke(() => view.TableModel ... )</code> to access this property for a non-GUI thread
		/// </summary>
		public ITableModel TableModel{
			get { return tableViewWf.TableModel; }
			set{
				tableViewWf.TableModel = value;
				itemsLabel.Text = value != null ? "" + StringUtils.WithDecimalSeparators(value.RowCount) + " items" : "";
			}
		}

		//public void Select(){
		//	tableView.Select();
		//}
		SplitContainer splitContainer;
		public void SwitchOnTextBox(){
			tableViewWf.SetCellText = s => auxTextBox.Text = s;
			mainPanel.Controls.Remove(tableView);
			splitContainer = new SplitContainer();
			splitContainer.Panel1.Controls.Add(tableView);
			splitContainer.Panel2.Controls.Add(auxTextBox);
			splitContainer.Margin = new Padding(0);
			splitContainer.Dock = DockStyle.Fill;
			splitContainer.Orientation = Orientation.Horizontal;
			mainPanel.Controls.Add(splitContainer);
		}

		public void SwitchOffTextBox(){
			auxTextBox.Text = "";
			tableViewWf.SetCellText = null;
			mainPanel.Controls.Remove(splitContainer);
			splitContainer.Panel1.Controls.Remove(tableView);
			splitContainer.Panel2.Controls.Remove(auxTextBox);
			splitContainer = null;
			mainPanel.Controls.Add(tableView);
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
				tableView.ColumnHeaderHeight = value;
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

		public object GetEntry(int row, int col){
			return tableViewWf.GetEntry(row, col);
		}

		private void TextButton_OnClick(object sender, EventArgs e){
			if (textBoxVisible){
				SwitchOffTextBox();
			} else{
				SwitchOnTextBox();
			}
			textBoxVisible = !textBoxVisible;
		}

		//public void RegisterScrollViewer(ScrollViewer scrollViewer){
		//	MainPanel.RegisterScrollViewer(scrollViewer);
		//}

		//public void UnregisterScrollViewer(ScrollViewer scrollViewer){
		//	MainPanel.UnregisterScrollViewer(scrollViewer);
		//}

		public void ClearSelectionFire(){
			tableViewWf.ClearSelectionFire();
		}

		private void SelectionAgentButton_OnClick(object sender, EventArgs e){
			Point p = selectionAgentButton.PointToScreen(new Point(0, 0));
			TableViewSelectionAgentForm w = new TableViewSelectionAgentForm(TableModel){
				Top = p.Y - 125,
				Left = p.X - 300
			};
			if (w.ShowDialog() == DialogResult.OK){
				int ind1 = w.sourceBox.SelectedIndex;
				int ind2 = w.columnBox.SelectedIndex;
				if (ind1 >= 0 && ind2 >= 0){
					selectionAgent = selectionAgents[ind1];
					selectionAgentColInd = ind2;
					selectionAgentColVals = GetTimeVals(ind2);
					//TODO
					//selectionAgent.AddTable(this);
				} else{
					selectionAgent = null;
					selectionAgentColInd = -1;
					selectionAgentColVals = null;
					//TODO
					//selectionAgent.RemoveTable(this);
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