using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BaseLib.Forms.Table{
	[Serializable]
	public class DataTable2 : TableModelImpl, IDataTable{
		public Collection<DataRow2> Rows { get; private set; }
		public DataTable2(string name) : this(name, "") {}

		public DataTable2(string name, string description){
			Rows = new Collection<DataRow2>();
			Name = name;
			Description = description;
		}

		public DataRow2 NewRow(){
			return new DataRow2(columnNames.Count, nameMapping);
		}

		public void AddRow(DataRow2 row){
			Rows.Add(row);
		}

		public void InsertRow(int index, DataRow2 row){
			Rows.Insert(index, row);
		}

		public void Clear(){
			Rows.Clear();
		}

		public void Close() {}
		public override int RowCount { get { return Rows.Count; } }

		public override object GetEntry(int row, int column){
			if (row < 0 || row >= Rows.Count){
				return null;
			}
			try{
				return Rows[row][column];
			} catch (Exception){
				return null;
			}
		}

		public override void SetEntry(int row, int column, object value){
			Rows[row][column] = value;
		}

		public void RemoveRow(DataRow2 row){
			Rows.Remove(row);
		}

		public void RemoveRow(int index){
			Rows.RemoveAt(index);
		}

		public void RemoveRows(IList<int> indices){
			if (indices.Count == 0){
				return;
			}
			if (indices.Count == 0){
				RemoveRow(indices[0]);
				return;
			}
			HashSet<int> y = new HashSet<int>(indices);
			Collection<DataRow2> x = new Collection<DataRow2>();
			for (int i = 0; i < RowCount; i++){
				if (!y.Contains(i)){
					x.Add(Rows[i]);
				}
			}
			Rows = x;
		}

		public DataRow2 GetRow(int index){
			return Rows[index];
		}

		public double[] GetValuesInColumn(int index){
			double[] result = new double[RowCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = GetDoubleValue(Rows[i], index);
			}
			return result;
		}

		public int[] GetIntValuesInColumn(int index){
			int[] result = new int[RowCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = GetIntValue(Rows[i], index);
			}
			return result;
		}

		public string[] GetStringValuesInColumn(int index){
			string[] result = new string[RowCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = Rows[i][index].ToString();
			}
			return result;
		}

		public int GetRowIndex(int colInd, object value){
			for (int i = 0; i < RowCount; i++){
				if (value.Equals(GetEntry(i, colInd))){
					return i;
				}
			}
			return -1;
		}

		private double GetDoubleValue(DataRow2 row, int colInd){
			bool isInt = columnTypes[colInd] == ColumnType.Integer;
			bool isDouble = IsNumeric(colInd);
			object o = row[colInd];
			if (isInt || isDouble){
				if (o == null || o is DBNull || o.ToString().Length == 0){
					return double.NaN;
				}
				if (isInt){
					return (int) o;
				}
				return (double) o;
			}
			return double.NaN;
		}

		private int GetIntValue(DataRow2 row, int colInd){
			bool isInt = columnTypes[colInd] == ColumnType.Integer;
			object o = row[colInd];
			if (isInt){
				if (o == null || o is DBNull || o.ToString().Length == 0){
					return 0;
				}
				return (int) o;
			}
			return 0;
		}

		private bool IsNumeric(int ind){
			ColumnType c = columnTypes[ind];
			return c == ColumnType.Expression || c == ColumnType.Integer || c == ColumnType.Numeric || c == ColumnType.NumericLog;
		}
	}
}