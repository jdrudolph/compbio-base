using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using BaseLib.Forms.Table;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for TableViewWpf.xaml
	/// </summary>
	public partial class TableViewWpf : UserControl{
		public event EventHandler SelectionChanged;
		private readonly TableView tableView;

		public TableViewWpf(){
			InitializeComponent();
			WindowsFormsHost wfh = new WindowsFormsHost();
			tableView = new TableView();
			tableView.SelectionChanged += (sender, args) =>{
				if (SelectionChanged != null){
					SelectionChanged(sender, args);
				}
			};
			wfh.Child = tableView;
			Grid1.Children.Add(wfh);
		}

		public ITableModel TableModel { get { return tableView.TableModel; } set { tableView.TableModel = value; } }
		public bool MultiSelect { get { return tableView.MultiSelect; } set { tableView.MultiSelect = value; } }
		public bool Sortable { get { return tableView.Sortable; } set { tableView.Sortable = value; } }
		public bool HasRemoveRowsMenuItems { get { return tableView.HasRemoveRowsMenuItems; } set { tableView.HasRemoveRowsMenuItems = value; } }
		public int RowCount { get { return tableView.RowCount; } }
		public int RowHeaderWidth { get { return tableView.RowHeaderWidth; } set { tableView.RowHeaderWidth = value; } }
		public int ColumnHeaderHeight { get { return tableView.ColumnHeaderHeight; } set { tableView.ColumnHeaderHeight = value; } }
		public int VisibleX { get { return tableView.VisibleX; } set { tableView.VisibleX = value; } }
		public int VisibleY { get { return tableView.VisibleY; } set { tableView.VisibleY = value; } }

		public void SetSelectedRow(int row){
			tableView.SetSelectedRow(row);
		}

		public void SetSelectedRow(int row, bool add, bool fire){
			tableView.SetSelectedRow(row, add, fire);
		}

		public bool HasSelectedRows(){
			return tableView.HasSelectedRows();
		}

		public void SetSelectedRows(IList<int> rows){
			tableView.SetSelectedRows(rows);
		}

		public void SetSelectedRows(IList<int> rows, bool add, bool fire){
			tableView.SetSelectedRows(rows, add, fire);
		}

		public void SetSelectedRowAndMove(int row){
			tableView.SetSelectedRowAndMove(row);
		}

		public void SetSelectedRowsAndMove(IList<int> rows){
			tableView.SetSelectedRowsAndMove(rows);
		}

		public void Invalidate(){
			tableView.Invalidate();
		}

		public void Invalidate(bool b){
			tableView.Invalidate(b);
		}

		public int[] GetSelectedRows(){
			return tableView.GetSelectedRows();
		}

		public int GetSelectedRow(){
			return tableView.GetSelectedRow();
		}

		public void ScrollToRow(int row){
			tableView.ScrollToRow(row);
		}

		public void FireSelectionChange(){
			tableView.FireSelectionChange();
		}
	}
}