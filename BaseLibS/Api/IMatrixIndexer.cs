using System;
using System.Collections.Generic;

namespace BaseLibS.Api{
	public interface IMatrixIndexer : ICloneable, IDisposable{
		void Init(int nrows, int ncols);
		bool IsInitialized();
		int RowCount { get; }
		int ColumnCount { get; }
		float this[int i, int j] { get; set; }
		void Set(float[,] value);
		BaseVector GetRow(int row);
		BaseVector GetColumn(int col);
		IMatrixIndexer ExtractRows(IList<int> rows);
		void ExtractRowsInPlace(IList<int> rows);
		IMatrixIndexer ExtractColumns(IList<int> columns);
		void ExtractColumnsInPlace(IList<int> columns);
		IMatrixIndexer Transpose();

		/// <summary>
		/// True if at least one entry is NaN or Infinity.
		/// </summary>
		bool ContainsNaNOrInf();

		/// <summary>
		/// True if all entries are NaN or Infinity in that row.
		/// </summary>
		bool IsNanOrInfRow(int row);

		/// <summary>
		/// True if all entries are NaN or Infinity in that column.
		/// </summary>
		bool IsNanOrInfColumn(int column);
	}
}