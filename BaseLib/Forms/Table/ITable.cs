using System.Windows;

namespace BaseLib.Forms.Table{
	public interface ITable{
		void AddColumn(string colName, int width, ColumnType columnType, string description, Visibility visibility);
		void AddColumn(string colName, int width, ColumnType columnType, ColumnDescription description, Visibility visibility);
		string Description { get; set; }
	}
}