using System.Collections.Generic;
using System.Windows.Forms;
using BasicLib.Param;

namespace BasicLib.Forms.Select{
	public partial class DictionaryIntValuePopup : Form{
		public DictionaryIntValuePopup(){
			InitializeComponent();
		}

		public void SetData(Dictionary<string, int> v, string[] keys, int d){
			Parameter[] p = new Parameter[keys.Length];
			for (int i = 0; i < p.Length; i++){
				p[i] = new IntParam(keys[i], v.ContainsKey(keys[i]) ? v[keys[i]] : d);
			}
			parameterPanel1.Init(new Parameters(p));
		}

		public Dictionary<string, int> GetData(string[] keys){
			Dictionary<string, int> result = new Dictionary<string, int>();
			foreach (string key in keys){
				int y = parameterPanel1.Parameters.GetIntParam(key).Value;
				result.Add(key, y);
			}
			return result;
		}
	}
}