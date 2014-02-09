using System;

namespace BaseLib.Forms.Table{
	public class VirtualDataTable2 : TableModelImpl, ITable{
		public Func<int, object[]> GetRowData { private get; set; }
		private int rowInUse = -1;
		private object[] rowDataInUse;
		private readonly int rowCount;

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
			throw new NotImplementedException();
		}
	}
}