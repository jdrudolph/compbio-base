using System;
using System.Collections.Generic;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Util;

namespace BaseLibS.Num.Matrix{
	[Serializable]
	public class SparseRowMatrixIndexer : IMatrixIndexer {
		private SparseFloatVector[] vals;
		private int ncolumns;

		public SparseRowMatrixIndexer(SparseFloatVector[] vals, int ncolumns) {
			this.vals = vals;
			this.ncolumns = ncolumns;
		}

		private SparseRowMatrixIndexer() { }

		public void Init(int nrows, int ncolumns1) {
			ncolumns = ncolumns1;
			vals = new SparseFloatVector[nrows];
			for (int i = 0; i < nrows; i++) {
				vals[i] = new SparseFloatVector(new int[0], new float[0], ncolumns);
			}
		}

		public void Set(float[,] value){
			ncolumns = value.GetLength(1);
			vals = new SparseFloatVector[value.GetLength(0)];
			for (int i = 0; i < vals.Length; i++){
				List<int> v = new List<int>();
				for (int j = 0; j < ncolumns; j++){
					if (value[i, j] == 0){
						continue;
					}
					v.Add(j);
				}
				int[] v1 = v.ToArray();
				float[] x = new float[v1.Length];
				for (int j = 0; j < v1.Length; j++){
					x[j] = value[i, v1[j]];
				}
				vals[i] = new SparseFloatVector(v1, x, ncolumns);
			}
		}

		public BaseVector GetRow(int row){
			return vals[row];
		}

		public BaseVector GetColumn(int col){
			List<int> inds = new List<int>();
			List<float> x = new List<float>();
			for (int i = 0; i < vals.Length; i++) {
				float w = (float)vals[i][col];
				if (w == 0) {
					continue;
				}
				inds.Add(i);
				x.Add(w);
			}
			return new SparseFloatVector(inds.ToArray(), x.ToArray(), vals.Length);
		}

		public bool IsInitialized(){
			return vals != null;
		}

		public IMatrixIndexer ExtractRows(int[] rows){
			return new SparseRowMatrixIndexer { vals = ArrayUtils.SubArray(vals, rows), ncolumns = ncolumns };
		}

		public void ExtractRowsInPlace(int[] rows){
			vals = ArrayUtils.SubArray(vals, rows);
		}

		public IMatrixIndexer ExtractColumns(int[] columns){
			SparseFloatVector[] r = new SparseFloatVector[vals.Length];
			for (int i = 0; i < vals.Length; i++) {
				r[i] = (SparseFloatVector)vals[i].SubArray(columns);
			}
			return new SparseRowMatrixIndexer { vals = r, ncolumns = columns.Length };
		}

		public void ExtractColumnsInPlace(int[] columns){
			for (int i = 0; i < vals.Length; i++) {
				vals[i] = (SparseFloatVector)vals[i].SubArray(columns);
			}
			ncolumns = columns.Length;
		}

		public IMatrixIndexer Transpose(){
			return new SparseColumnMatrixIndexer(vals, ncolumns);
		}

		public bool ContainsNaNOrInfinity(){
			foreach (SparseFloatVector val in vals){
				if (val.ContainsNaNOrInfinity()){
					return true;
				}
			}
			return false;
		}

		public int RowCount{
			get { return vals.Length; }
		}

		public int ColumnCount{
			get { return ncolumns; }
		}

		public float this[int i, int j]{
			get { return (float)vals[i][j]; }
			set { vals[i][j] = value; }
		}

		public void Dispose(){
			foreach (SparseFloatVector val in vals){
				val.Dispose();
			}
			vals = null;
		}

		public object Clone(){
			if (vals == null){
				return new SparseRowMatrixIndexer();
			}
			SparseFloatVector[] v = new SparseFloatVector[vals.Length];
			for (int i = 0; i < v.Length; i++){
				v[i] = (SparseFloatVector) vals[i].Clone();
			}
			return new SparseRowMatrixIndexer { vals = v, ncolumns = ncolumns };
		}
	}
}