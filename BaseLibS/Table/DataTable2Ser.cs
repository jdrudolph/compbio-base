using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BaseLibS.Table{
	[Serializable]
	public class DataTable2Ser{
		private readonly Dictionary<string, int> nameMapping;
		private readonly object[][] dataRows;
		public DataTable2Ser(IList<DataRow2> rows){
			if (rows.Count > 0){
				nameMapping = rows[0].nameMapping;
			}
			dataRows = new object[rows.Count][];
			for (int i = 0; i < dataRows.Length; i++){
				dataRows[i] = rows[i].ItemArray;
			}
		}

		public Collection<DataRow2> GetData(){
			Collection<DataRow2> rows = new Collection<DataRow2>();
			foreach (object[] t in dataRows){
				rows.Add(new DataRow2(t, nameMapping));
			}
			return rows;
		}
	}
}