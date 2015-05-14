using System;
using System.Collections.Generic;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Util;

namespace BaseLibS.Num.Matrix{
	[Serializable]
	public class MatrixIndexer : IMatrixIndexer{
		private float[,] vals;
		public MatrixIndexer(){}

		public MatrixIndexer(float[,] vals){
			this.vals = vals;
		}

		public void Init(int nrows, int ncols){
			vals = new float[nrows,ncols];
		}

		public void TransposeInPlace(){
			if (vals != null){
				vals = ArrayUtils.Transpose(vals);
			}
		}

		public IMatrixIndexer Transpose(){
			return vals == null ? new MatrixIndexer() : new MatrixIndexer(ArrayUtils.Transpose(vals));
		}

		public void Set(float[,] value){
			vals = value;
		}

		public BaseVector GetRow(int row){
			float[] result = new float[ColumnCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = vals[row, i];
			}
			return new FloatArrayVector(result);
		}

		public BaseVector GetColumn(int col){
			float[] result = new float[RowCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = vals[i, col];
			}
			return new FloatArrayVector(result);
		}

		public bool IsInitialized(){
			return vals != null;
		}

		public IMatrixIndexer ExtractRows(IList<int> rows) {
			return new MatrixIndexer(ArrayUtils.ExtractRows(vals, rows));
		}

		public IMatrixIndexer ExtractColumns(IList<int> columns) {
			return new MatrixIndexer(ArrayUtils.ExtractColumns(vals, columns));
		}

		public void ExtractRowsInPlace(IList<int> rows) {
			if (vals != null){
				vals = ArrayUtils.ExtractRows(vals, rows);
			}
		}

		public void ExtractColumnsInPlace(IList<int> columns) {
			if (vals != null){
				vals = ArrayUtils.ExtractColumns(vals, columns);
			}
		}

		public bool ContainsNaNOrInf(){
			for (int i = 0; i < vals.GetLength(0); i++){
				for (int j = 0; j < vals.GetLength(1); j++){
					if (float.IsNaN(vals[i, j]) || float.IsInfinity(vals[i, j])){
						return true;
					}
				}
			}
			return false;
		}

		public bool IsNanOrInfRow(int row) {
			for (int i = 0; i < ColumnCount; i++){
				float v = vals[row, i];
				if (!float.IsNaN(v) && !float.IsInfinity(v)) {
					return false;
				}
			}
			return true;
		}

		public bool IsNanOrInfColumn(int column) {
			for (int i = 0; i < RowCount; i++){
				float v = vals[i, column];
				if (!float.IsNaN(v) && !float.IsInfinity(v)) {
					return false;
				}
			}
			return true;
		}


		public int RowCount{
			get { return vals.GetLength(0); }
		}

		public int ColumnCount{
			get { return vals.GetLength(1); }
		}

		public float this[int i, int j]{
			get { return !IsInitialized() ? float.NaN : vals[i, j]; }
			set{
				if (!IsInitialized()){
					return;
				}
				vals[i, j] = value;
			}
		}

		public void Dispose(){
			vals = null;
		}

		public object Clone(){
			return vals == null ? new MatrixIndexer(null) : new MatrixIndexer((float[,]) vals.Clone());
		}
	}
}