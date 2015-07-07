using System;
using BaseLibS.Table;

namespace BaseLib.Forms.Table{
	public class SubTableModel : ITableModel{
		private readonly ITableModel baseModel;
		private readonly int[] rows;
		private readonly int[] columns;
		public SubTableModel(ITableModel baseModel, int[] rows) : this(baseModel, rows, null) {}

		public SubTableModel(ITableModel baseModel, int[] rows, int[] columns){
			this.baseModel = baseModel;
			this.rows = rows;
			this.columns = columns;
		}

		public RenderTableCell GetColumnRenderer(int column){
			return baseModel.GetColumnRenderer(columns == null ? column : columns[column]);
		}

		public int RowCount { get { return rows.Length; } }
		public int ColumnCount { get { return columns == null ? baseModel.ColumnCount : columns.Length; } }
		public string Name { get { return baseModel.Name; } }
		public string Description { get { return baseModel.Description; } }

		public string GetColumnName(int column) {
			return baseModel.GetColumnName(columns == null ? column : columns[column]);
		}

		public bool IsColumnEditable(int column) {
			return baseModel.IsColumnEditable(columns == null ? column : columns[column]);
		}

		public string GetColumnDescription(int column) {
			return baseModel.GetColumnDescription(columns == null ? column : columns[column]);
		}

		public ColumnType GetColumnType(int column){
			return baseModel.GetColumnType(columns == null ? column : columns[column]);
		}

		public int GetColumnWidth(int column){
			return baseModel.GetColumnWidth(columns == null ? column : columns[column]);
		}

		public object GetEntry(int row, int column){
			return baseModel.GetEntry(rows[row], columns == null ? column : columns[column]);
		}

		public object GetEntry(int row, string colname){
			return GetEntry(row, GetColumnIndex(colname));
		}

		public void SetEntry(int row, int column, object value){
			baseModel.SetEntry(rows[row], columns == null ? column : columns[column], value);
		}

		public int AnnotationRowsCount { get { return baseModel.AnnotationRowsCount; } }

		public string GetAnnotationRowName(int index){
			return baseModel.GetAnnotationRowName(index);
		}

		public string GetAnnotationRowDescription(int index){
			return baseModel.GetAnnotationRowDescription(index);
		}

		public object GetAnnotationRowValue(int index, int column){
			return baseModel.GetAnnotationRowValue(index, columns == null ? column : columns[column]);
		}

		public object GetAnnotationRowValue(int index, string colname){
			return GetAnnotationRowValue(index, GetColumnIndex(colname));
		}

		public int GetColumnIndex(string columnName){
			if (columns == null){
				int index = baseModel.GetColumnIndex(columnName);
				if (index == -1){
					throw new ArgumentException(string.Format("Could not find column name {0} in table {1}", columnName, Name));
				}
				return index;
			}
			for (int i = 0; i < ColumnCount; i++){
				if (GetColumnName(i).Equals(columnName)){
					return i;
				}
			}
			throw new ArgumentException(string.Format("Could not find column name {0} in table {1}", columnName, Name));
		}
	}
}