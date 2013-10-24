using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BasicLib.Forms.Table{
	/// <summary>
	/// Partial implementation of <code>ITableModel</code>, implementing all functionality that is shared 
	/// between the full implementations of <code>ITableModel</code>.
	/// </summary>
	[Serializable]
	public abstract class TableModelImpl : ITableModel{
		protected readonly List<string> columnNames = new List<string>();
		protected readonly List<int> columnWidths = new List<int>();
		protected readonly List<ColumnType> columnTypes = new List<ColumnType>();
		protected readonly List<RenderTableCell> cellRenderers = new List<RenderTableCell>();
		protected readonly List<ColumnDescription> columnDescriptions = new List<ColumnDescription>();
		protected readonly List<string> annotationRowNames = new List<string>();
		protected readonly List<string> annotationRowDescriptions = new List<string>();
		private readonly Collection<DataAnnotationRow> annotationRows = new Collection<DataAnnotationRow>();
		protected readonly Dictionary<string, int> nameMapping = new Dictionary<string, int>();
		public int ColumnCount { get { return columnNames.Count; } }

		public RenderTableCell GetColumnRenderer(int col){
			return cellRenderers[col];
		}

		public string Name { get; set; }
		public string Description { get; set; }

		public int GetColumnWidth(int col){
			return columnWidths[col];
		}

		public string GetColumnName(int col){
			return columnNames[col];
		}

		public bool IsColumnEditable(int column){
			return false;
		}

		public string GetColumnDescription(int col){
			return columnDescriptions[col].ToString();
		}

		public ColumnType GetColumnType(int col){
			return columnTypes[col];
		}

		public int GetColumnIndex(string colName){
			return columnNames.IndexOf(colName);
		}

		public void AddColumn(string colName, int width, ColumnType columnType, string description, Visibility visibility){
			AddColumn(colName, width, columnType, new ColumnDescription(description), visibility, null);
		}

		public void AddColumn(string colName, int width, ColumnType columnType, string description, Visibility visibility,
			RenderTableCell renderer){
			AddColumn(colName, width, columnType, new ColumnDescription(description), visibility, renderer);
		}

		public object GetEntry(int row, string colname){
			return GetEntry(row, GetColumnIndex(colname));
		}

		public void SetEntry(int row, string colname, object value){
			SetEntry(row, GetColumnIndex(colname), value);
		}

		public abstract int RowCount { get; }
		public abstract object GetEntry(int row, int column);
		public abstract void SetEntry(int row, int column, object value);

		public DataAnnotationRow NewAnnotationRow(){
			return new DataAnnotationRow(columnNames.Count, nameMapping);
		}

		public void AddAnnotationRow(DataAnnotationRow row, string name, string description){
			annotationRows.Add(row);
			annotationRowNames.Add(name);
			annotationRowDescriptions.Add(description);
		}

		public int AnnotationRowsCount { get { return annotationRowNames.Count; } }

		public string GetAnnotationRowName(int index){
			return annotationRowNames[index];
		}

		public string GetAnnotationRowDescription(int index){
			return annotationRowDescriptions[index];
		}

		public object GetAnnotationRowValue(int index, int column){
			return annotationRows[index][column];
		}

		public object GetAnnotationRowValue(int index, string colname){
			return GetAnnotationRowValue(index, GetColumnIndex(colname));
		}

		public virtual void AddColumn(string colName, int width, ColumnType columnType, ColumnDescription description,
			Visibility visibility){
			AddColumn(colName, width, columnType, description, visibility, null);
		}

		public virtual void AddColumn(string colName, int width, ColumnType columnType, ColumnDescription description,
			Visibility visibility, RenderTableCell renderer){
			nameMapping.Add(colName, columnNames.Count);
			columnNames.Add(colName);
			columnWidths.Add(width);
			columnTypes.Add(columnType);
			columnDescriptions.Add(description);
			cellRenderers.Add(renderer);
		}

		public static string ColumnTypeToString(ColumnType ct){
			switch (ct){
				case ColumnType.Boolean:
					return "C";
				case ColumnType.Categorical:
					return "C";
				case ColumnType.Color:
					return "C";
				case ColumnType.DateTime:
					return "T";
				case ColumnType.DashStyle:
					return "C";
				case ColumnType.Expression:
					return "E";
				case ColumnType.Integer:
					return "N";
				case ColumnType.MultiInteger:
					return "M";
				case ColumnType.MultiNumeric:
					return "M";
				case ColumnType.MultiNumericLog:
					return "M";
				case ColumnType.Numeric:
					return "N";
				case ColumnType.NumericLog:
					return "N";
				case ColumnType.Text:
					return "T";
				default:
					return "T";
			}
		}
	}
}