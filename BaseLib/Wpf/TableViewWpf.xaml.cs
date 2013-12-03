using System.Windows.Forms.Integration;
using BaseLib.Forms.Table;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for TableViewWpf.xaml
	/// </summary>
	public partial class TableViewWpf : System.Windows.Controls.UserControl{
		private readonly TableView tableView;
		public TableViewWpf(){
			InitializeComponent();
			WindowsFormsHost wfh = new WindowsFormsHost();
			tableView = new TableView();
			wfh.Child = tableView;
			Grid1.Children.Add(wfh);
		}

		public ITableModel TableModel{
			get { return tableView.TableModel; }
			set { tableView.TableModel = value; }
		}

		public bool MultiSelect {
			get { return tableView.MultiSelect; }
			set { tableView.MultiSelect = value; }
		}

		public bool Sortable {
			get { return tableView.Sortable; }
			set { tableView.Sortable = value; }
		}

		public void Invalidate() {
			tableView.Invalidate();
		}

		public void Invalidate(bool b) {
			tableView.Invalidate(b);
		}

		public int[] GetSelectedRows(){
			return tableView.GetSelectedRows();
		}
	}
}