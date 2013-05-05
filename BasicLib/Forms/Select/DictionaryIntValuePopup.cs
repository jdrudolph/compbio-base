using System.Collections.Generic;
using System.Windows.Forms;
using BasicLib.Param;

namespace BasicLib.Forms.Select{
	internal partial class DictionaryIntValuePopup : Form{
		internal DictionaryIntValuePopup() {
			InitializeComponent();
		}

		internal void SetData(Dictionary<string, int> v, string[] keys, int d) {
			Parameter[] p = new Parameter[keys.Length];
			for (int i = 0; i < p.Length; i++){
				p[i] = new IntParam(keys[i], v.ContainsKey(keys[i]) ? v[keys[i]] : d);
			}
			parameterPanel1.Init(new Parameters(p));
		}

		internal Dictionary<string, int> GetData(string[] keys) {
			Dictionary<string, int> result = new Dictionary<string, int>();
			foreach (string key in keys){
				int y = parameterPanel1.Parameters.GetIntParam(key).Value;
				result.Add(key, y);
			}
			return result;
		}

		private void OkButtonClick(object sender, System.EventArgs e) {
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelButtonClick(object sender, System.EventArgs e) {
			Close();
		}
	}
}