using System;

namespace BaseLibS.Api{
	public interface IMatrixIndexer : ICloneable, IDisposable{
		void Init(int nrows, int ncols);
		int RowCount { get; }
		int ColumnCount { get; }
		float this[int i, int j] { get; set; }
		void Set(float[,] value);
		BaseVector GetRow(int row);
		BaseVector GetColumn(int col);
		bool IsInitialized();
		IMatrixIndexer ExtractRows(int[] rows);
		void ExtractRowsInPlace(int[] rows);
		IMatrixIndexer ExtractColumns(int[] columns);
		void ExtractColumnsInPlace(int[] columns);
		IMatrixIndexer Transpose();
		bool ContainsNaNOrInfinity();
	}
}