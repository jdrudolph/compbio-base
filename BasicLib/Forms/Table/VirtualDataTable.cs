using System;

namespace BasicLib.Forms.Table{
	public abstract class VirtualDataTable : TableModelImpl, ITable{
		private int rowInUse = -1;
		private object[] rowDataInUse;
		private readonly int rowCount;

		protected VirtualDataTable(string name, string description, int rowCount){
			Name = name;
			Description = description;
			this.rowCount = rowCount;
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

		public abstract object[] GetRowData(int rowModel);
	}
}