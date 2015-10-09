namespace BaseLibS.Table{
	public interface ITable{
		void AddColumn(string colName, int width, ColumnType columnType, string description);
		string Description { get; set; }
	}
}