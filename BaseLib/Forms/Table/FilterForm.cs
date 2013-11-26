using System.ComponentModel;
using System.Windows.Forms;

namespace BaseLib.Forms.Table{
	internal partial class FilterForm : Form{
		private readonly TableView tableView;
		private readonly ITableModel tableModel;

		public FilterForm(TableView tableView){
			InitializeComponent();
			this.tableView = tableView;
			tableModel = tableView.TableModel;
		}

		protected override void OnClosing(CancelEventArgs e){
			e.Cancel = true;
			Visible = false;
		}
	}
}