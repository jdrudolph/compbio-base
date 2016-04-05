namespace BaseLib.Wpf{
	public interface ITableSelectionAgent{
		string Title { get; }
		void AddTable(TableView tableView);
		void RemoveTable(TableView tableView);
	}
}