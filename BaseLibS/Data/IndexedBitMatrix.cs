using System.Collections.Generic;

namespace BaseLibS.Data{
	public class IndexedBitMatrix{
		private readonly HashSet<int>[] data;
		private readonly int nrows;
		private readonly int ncols;

		public IndexedBitMatrix(int nrows, int ncols){
			this.nrows = nrows;
			this.ncols = ncols;
			data = new HashSet<int>[nrows];
		}

		public int RowCount { get { return nrows; } }
		public int ColumnCount { get { return ncols; } }

		public void Set(int row, int col, bool val){
			if (data[row] == null){
				data[row] = new HashSet<int>();
			}
			if (val){
				data[row].Add(col);
			} else{
				data[row].Remove(col);
			}
		}

		public bool Get(int row, int col){
			return data[row] != null && data[row].Contains(col);
		}
	}
}