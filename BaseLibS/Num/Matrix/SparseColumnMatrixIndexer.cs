using System;
using System.Collections.Generic;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Util;

namespace BaseLibS.Num.Matrix{
	[Serializable]
	public class SparseColumnMatrixIndexer : IMatrixIndexer {
		private SparseFloatVector[] vals;
		private int nrows;

		public SparseColumnMatrixIndexer(SparseFloatVector[] vals, int nrows){
			this.vals = vals;
			this.nrows = nrows;
		}

		private SparseColumnMatrixIndexer(){}

		public void Init(int nrows1, int ncols){
			nrows = nrows1;
			vals = new SparseFloatVector[ncols];
			for (int i = 0; i < ncols; i++){
				vals[i] = new SparseFloatVector(new int[0], new float[0], nrows);
			}
		}

		public void Set(float[,] value){
			nrows = value.GetLength(0);
			vals = new SparseFloatVector[value.GetLength(1)];
			for (int i = 0; i < vals.Length; i++){
				List<int> v = new List<int>();
				for (int j = 0; j < nrows; j++){
					if (value[j, i] == 0){
						continue;
					}
					v.Add(j);
				}
				int[] v1 = v.ToArray();
				float[] x = new float[v1.Length];
				for (int j = 0; j < v1.Length; j++){
					x[j] = value[v1[j], i];
				}
				vals[i] = new SparseFloatVector(v1, x, nrows);
			}
		}

		public BaseVector GetRow(int row){
			List<int> inds = new List<int>();
			List<float> x = new List<float>();
			for (int i = 0; i < vals.Length; i++){
				float w = (float) vals[i][row];
				if (w == 0){
					continue;
				}
				inds.Add(i);
				x.Add(w);
			}
			return new SparseFloatVector(inds.ToArray(), x.ToArray(), vals.Length);
		}

		public BaseVector GetColumn(int col){
			return vals[col];
		}

		public bool IsInitialized(){
			return vals != null;
		}

		public IMatrixIndexer ExtractRows(IList<int> rows) {
			SparseFloatVector[] r = new SparseFloatVector[vals.Length];
			for (int i = 0; i < vals.Length; i++){
				r[i] = (SparseFloatVector) vals[i].SubArray(rows);
			}
			return new SparseColumnMatrixIndexer{vals = r, nrows = rows.Count};
		}

		public void ExtractRowsInPlace(IList<int> rows) {
			for (int i = 0; i < vals.Length; i++){
				vals[i] = (SparseFloatVector) vals[i].SubArray(rows);
			}
			nrows = rows.Count;
		}

		public IMatrixIndexer ExtractColumns(IList<int> columns) {
			return new SparseColumnMatrixIndexer{vals = ArrayUtils.SubArray(vals, columns), nrows = nrows};
		}

		public void ExtractColumnsInPlace(IList<int> columns) {
			vals = ArrayUtils.SubArray(vals, columns);
		}

		public IMatrixIndexer Transpose(){
			return new SparseRowMatrixIndexer(vals, nrows);
		}

		public bool ContainsNaNOrInfinity(){
			foreach (SparseFloatVector val in vals){
				if (val.ContainsNaNOrInf()){
					return true;
				}
			}
			return false;
		}

		public bool IsNanOrInfRow(int row){
			for (int i = 0; i < ColumnCount; i++) {
				float v = (float)vals[i][row];
				if (!float.IsNaN(v) && !float.IsInfinity(v)) {
					return false;
				}
			}
			return true;
		}

		public bool IsNanOrInfColumn(int column){
			return vals[column].IsNanOrInf();
		}

		public int RowCount{
			get { return nrows; }
		}

		public int ColumnCount{
			get { return vals.Length; }
		}

		public float this[int i, int j]{
			get { return (float) vals[j][i]; }
			set { vals[j][i] = value; }
		}

		public void Dispose(){
			foreach (SparseFloatVector val in vals){
				val.Dispose();
			}
			vals = null;
		}

		public object Clone(){
			if (vals == null){
				return new SparseColumnMatrixIndexer();
			}
			SparseFloatVector[] v = new SparseFloatVector[vals.Length];
			for (int i = 0; i < v.Length; i++){
				v[i] = (SparseFloatVector) vals[i].Clone();
			}
			return new SparseColumnMatrixIndexer{vals = v, nrows = nrows};
		}
	}
}