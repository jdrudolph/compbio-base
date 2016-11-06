using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaseLib.Forms{
	public partial class RegexMatchParamControl : UserControl{
		internal Regex _regex;
		internal List<string> _preview;

		public RegexMatchParamControl(Regex regex, List<string> preview){
			InitializeComponent();
			_regex = regex;
			RegexTextBox.Text = Regex;
			_preview = preview;
		}

		public string Regex{
			get { return _regex.ToString(); }
			set{
				if (value == Regex){
					return;
				}
				try{
					_regex = new Regex(value);
					UpdatePreviewTable();
				} catch (ArgumentException){
					Debug.WriteLine("Unable to parse regex");
				}
			}
		}

		private void UpdatePreviewTable(){
			var table = new DataTable("Preview");
			var groupNames = _regex.GetGroupNames().Skip(1).ToArray();
			var inputColumn = "Input";
			table.Columns.Add(inputColumn);
			foreach (var groupName in groupNames){
				table.Columns.Add(groupName);
			}
			foreach (var preview in _preview){
				var match = _regex.Match(preview);
				var row = table.NewRow();
				row[inputColumn] = preview;
				foreach (var groupName in groupNames){
					row[groupName] = match.Groups[groupName];
				}
				table.Rows.Add(row);
			}
			PreviewDataGridView.DataSource = table;
			Debug.WriteLine("update table");
		}

		private void RegexMatchParamControl_Load(object sender, EventArgs e){
			UpdatePreviewTable();
		}

		private void RegexTextBox_TextChanged(object sender, EventArgs e){
			Regex = RegexTextBox.Text;
		}

		private void PreviewDataGridView_SelectionChanged(object sender, EventArgs e){
			PreviewDataGridView.ClearSelection();
		}
	}
}