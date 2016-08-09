using System.Windows;
using BaseLibS.Table;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for TableViewSelectionAgentWindow.xaml
	/// </summary>
	public partial class TableViewSelectionAgentWindow{
		public TableViewSelectionAgentWindow(ITableModel tableModel){
			InitializeComponent();
			foreach (ITableSelectionAgent agent in TableView.selectionAgents){
				SourceBox.Items.Add(agent.Title);
			}
			for (int i = 0; i < tableModel.ColumnCount; i++){
				ColumnBox.Items.Add(tableModel.GetColumnName(i));
			}
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e){
			DialogResult = false;
			Close();
		}

		private void OkButton_OnClick(object sender, RoutedEventArgs e){
			DialogResult = true;
			Close();
		}
	}
}