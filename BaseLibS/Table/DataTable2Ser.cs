using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BaseLibS.Table{
	[Serializable]
	public class DataTable2Ser{
		private readonly Dictionary<string, int> nameMapping;
		private readonly object[][] dataCols;
		private readonly int nrows;
		public DataTable2Ser(IList<DataRow2> rows, int ncols){
			if (rows.Count > 0){
				nameMapping = rows[0].nameMapping;
			}
			dataCols = new object[ncols][];
			for (int i = 0; i < ncols; i++){
				dataCols[i] = new object[rows.Count];
				for (int j = 0; j < rows.Count; j++){
					dataCols[i][j] = rows[j].ItemArray[i];
				}
			}
			nrows = rows.Count;
		}

		public Collection<DataRow2> GetData(){
			Collection<DataRow2> rows = new Collection<DataRow2>();
			for (int i = 0; i < nrows; i++){
				object[] t = new object[dataCols.Length];
				for (int j = 0; j < t.Length; j++){
					t[j] = dataCols[j][i];
				}
				rows.Add(new DataRow2(t, nameMapping));
			}
			return rows;
		}
	}
}