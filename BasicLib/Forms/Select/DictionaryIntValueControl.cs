using System.Collections.Generic;
using System.Windows.Forms;

namespace BasicLib.Forms.Select{
	public partial class DictionaryIntValueControl : UserControl{
		public DictionaryIntValueControl(){
			InitializeComponent();
		}

		public Dictionary<string, int> Value { get; set; }
		public string[] Keys { get; set; }
		public int Default { get; set; }

		private void ButtonClick(object sender, System.EventArgs e){
			DictionaryIntValuePopup p = new DictionaryIntValuePopup();
			p.SetData(Value, Keys, Default);
			if (p.ShowDialog() == DialogResult.OK){
				Value = p.GetData(Keys);
			}
		}
	}
}