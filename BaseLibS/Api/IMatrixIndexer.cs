using BaseLibS.Num.Vector;

namespace PerseusApi.Matrix{
	public interface IMatrixIndexer{
		void Init(int nrows, int ncols);
		int RowCount { get; }
		int ColumnCount { get; }
		float this[int i, int j] { get; set; }
		void Transpose();
		void Set(float[,] value);
		BaseVector GetRow(int row);
		BaseVector GetColumn(int col);
		bool IsInitialized();
		IMatrixIndexer ExtractRows(int[] rows);
		IMatrixIndexer ExtractColumns(int[] columns);
		void ExtractRowsInPlace(int[] rows);
		void ExtractColumnsInPlace(int[] columns);
		bool ContainsNaNOrInfinity();
	}
}