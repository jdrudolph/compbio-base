namespace BaseLib.Wpf{
	public interface ITableSelectionAgent{
		string Title { get; }
		void AddTable(TableViewWpf tableView);
		void RemoveTable(TableViewWpf tableView);
	}
}