using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BasicLib.Forms.Table{
	[Serializable]
	public class DataRow2{
		public object[] ItemArray { get; set; }
		private readonly Dictionary<string, int> nameMapping;

		public DataRow2(int count, Dictionary<string, int> nameMapping){
			ItemArray = new object[count];
			this.nameMapping = nameMapping;
		}

		public object this[int column] { get { return ItemArray[column]; } set { ItemArray[column] = value; } }

		public object this[string colName]{
			get { return ItemArray[nameMapping[colName]]; }
			set{
				if (!nameMapping.ContainsKey(colName)){
					MessageBox.Show("Unknown column: " + colName);
				}
				ItemArray[nameMapping[colName]] = value;
			}
		}
	}
}