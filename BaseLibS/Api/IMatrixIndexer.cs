using System;
using System.Collections.Generic;

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
		IMatrixIndexer ExtractRows(IList<int> rows);
		void ExtractRowsInPlace(IList<int> rows);
		IMatrixIndexer ExtractColumns(IList<int> columns);
		void ExtractColumnsInPlace(IList<int> columns);
		IMatrixIndexer Transpose();
		bool ContainsNaNOrInf();
		bool IsNanOrInfRow(int row);
		bool IsNanOrInfColumn(int column);
	}
}