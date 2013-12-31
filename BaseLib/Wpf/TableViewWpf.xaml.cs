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

		public void SetSelectedRows(IList<int> rows){
			tableView.SetSelectedRows(rows);
		}

		public void SetSelectedRows(IList<int> rows, bool add){
			tableView.SetSelectedRows(rows, add);
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
	}
}