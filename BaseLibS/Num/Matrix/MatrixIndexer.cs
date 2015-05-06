using System;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Util;

namespace BaseLibS.Num.Matrix{
	[Serializable]
	public class MatrixIndexer : IMatrixIndexer{
		private float[,] vals;
		public MatrixIndexer() {}

		public MatrixIndexer(float[,] vals){
			this.vals = vals;
		}

		public void Init(int nrows, int ncols){
			vals = new float[nrows,ncols];
		}

		public void TransposeInPlace(){
			vals = ArrayUtils.Transpose(vals);
		}

		public IMatrixIndexer Transpose(){
			return new MatrixIndexer(ArrayUtils.Transpose(vals));
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

		public IMatrixIndexer ExtractRows(int[] rows){
			return new MatrixIndexer(ArrayUtils.ExtractRows(vals, rows));
		}

		public IMatrixIndexer ExtractColumns(int[] columns){
			return new MatrixIndexer(ArrayUtils.ExtractColumns(vals, columns));
		}

		public void ExtractRowsInPlace(int[] rows){
			vals = ArrayUtils.ExtractRows(vals, rows);
		}

		public void ExtractColumnsInPlace(int[] columns){
			vals = ArrayUtils.ExtractColumns(vals, columns);
		}

		public bool ContainsNaNOrInfinity(){
			for (int i = 0; i < vals.GetLength(0); i++){
				for (int j = 0; j < vals.GetLength(1); j++){
					if (float.IsNaN(vals[i, j]) || float.IsInfinity(vals[i, j])){
						return true;
					}
				}
			}
			return false;
		}

		public int RowCount { get { return vals.GetLength(0); } }
		public int ColumnCount { get { return vals.GetLength(1); } }

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