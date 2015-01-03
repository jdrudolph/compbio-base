namespace PerseusApi.Matrix{
	public interface IBoolMatrixIndexer{
		void Init(int nrows, int ncols);
		int RowCount { get; }
		int ColumnCount { get; }
		bool this[int i, int j] { get; set; }
		void Transpose();
		void Set(bool[,] value);
		bool[,] Get();
		bool[] GetRow(int row);
		bool[] GetColumn(int col);
		bool IsInitialized();
		IBoolMatrixIndexer ExtractRows(int[] rows);
		IBoolMatrixIndexer ExtractColumns(int[] columns);
		void ExtractRowsInPlace(int[] rows);
		void ExtractColumnsInPlace(int[] columns);
	}
}