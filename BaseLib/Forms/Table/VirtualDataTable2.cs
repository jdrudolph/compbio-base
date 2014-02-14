using System;

namespace BaseLib.Forms.Table{
	public class VirtualDataTable2 : TableModelImpl, ITable{
		public Func<int, object[]> GetRowData { private get; set; }
		private int rowInUse = -1;
		private object[] rowDataInUse;
		private readonly int rowCount;
		private int[] persistentColInds;
		private DataTable2 persistentTable;
		
		public VirtualDataTable2(string name, string description, int rowCount){
			Name = name;
			Description = description;
			this.rowCount = rowCount;
		}

		public DataRow2 NewRow(){
			return new DataRow2(columnNames.Count, nameMapping);
		}

		public override int RowCount { get { return rowCount; } }

		public override object GetEntry(int row, int col){
			if (row >= RowCount || row < 0){
				return null;
			}
			if (rowInUse != row){
				rowDataInUse = GetRowData(row);
				rowInUse = row;
			}
			return col >= rowDataInUse.Length ? null : rowDataInUse[col];
		}

		public override void SetEntry(int row, int column, object value){
			if (persistentTable == null){
				throw new Exception("The table has no persistent columns.");
			}
			int ind = Array.BinarySearch(persistentColInds, column);
			if (ind < 0){
				throw new Exception("The column is not persistent.");
			}
			persistentTable.SetEntry(row, ind, value);
		}
	}
}