using BaseLib.Graphic;
using BaseLibS.Table;

namespace BaseLib.Forms.Table{
	public delegate void RenderTableCell(IGraphics g, bool selected, object o, int width, int x1, int y1);

	public interface ITableModel {
		int RowCount { get; }
		int ColumnCount { get; }
		string Name { get; }
		string Description { get; }
		string GetColumnName(int column);
		bool IsColumnEditable(int column);
		string GetColumnDescription(int column);
		ColumnType GetColumnType(int column);
		int GetColumnWidth(int column);
		RenderTableCell GetColumnRenderer(int column);
		object GetEntry(int row, int column);
		object GetEntry(int row, string colname);
		int GetColumnIndex(string columnName);
		void SetEntry(int row, int column, object value);
		int AnnotationRowsCount { get; }
		string GetAnnotationRowName(int index);
		string GetAnnotationRowDescription(int index);
		object GetAnnotationRowValue(int index, int column);
		object GetAnnotationRowValue(int index, string colname);
	}
}