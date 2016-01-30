using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BaseLibS.Num;
using BaseLibS.Table;
using BaseLibS.Util;

namespace BaseLib.Forms.Table{
	internal partial class FindForm : Form{
		private const int expandedHeight = 700;
		private readonly TableViewWf tableView;
		private readonly ITableModel tableModel;
		private int searchRowIndView = -1;
		private int[] multipleColumns = new int[0];

		public FindForm(TableViewWf tableView){
			InitializeComponent();
			this.tableView = tableView;
			tableModel = tableView.TableModel;
			wildcardsComboBox.SelectedIndex = 0;
			wildcardsComboBox.Enabled = false;
			helpButton.Enabled = false;
			columnSelectButton.Enabled = false;
			lookInComboBox.Items.Add("Whole table");
			for (int i = 0; i < tableModel.ColumnCount; i++){
				lookInComboBox.Items.Add(tableModel.GetColumnName(i));
			}
			lookInComboBox.Items.Add("Multiple columns");
			lookInComboBox.SelectedIndex = 0;
			lookInComboBox.SelectedIndexChanged += LookInComboBoxSelectedIndexChanged;
			useCheckBox.Visible = false;
			wildcardsComboBox.Visible = false;
			helpButton.Visible = false;
			tableView1.SelectionChanged += TableView1SelectionChanged;
			expressionTextBox.Focus();
			expressionTextBox.SelectAll();
		}

		protected override void OnLoad(EventArgs e){
			expressionTextBox.Select();
		}

		protected override void OnClosing(CancelEventArgs e) {
			e.Cancel = true;
			Visible = false;
		}

		private void TableView1SelectionChanged(object sender, EventArgs e) {
			tableView.ClearSelection();
			int[] rows = tableView1.GetSelectedRows();
			foreach (int ind in rows.Select(row => (int)tableView1.GetEntry(row, 0) - 1)) {
				tableView.SetSelectedViewIndex(ind);
			}
			if (rows.Length > 0) {
				int ind0 = (int)tableView1.GetEntry(rows[0], 0) - 1;
				tableView.ScrollToRow(ind0);
			}
			tableView.Invalidate();
		}

		private void LookInComboBoxSelectedIndexChanged(object sender, EventArgs e) {
			columnSelectButton.Enabled = lookInComboBox.SelectedIndex == tableModel.ColumnCount + 1;
		}

		private IEnumerable<int> GetColumnIndices(){
			int ind = lookInComboBox.SelectedIndex;
			if (ind == 0){
				return ArrayUtils.ConsecutiveInts(0, tableModel.ColumnCount);
			}
			return ind <= tableModel.ColumnCount ? new[]{ind - 1} : multipleColumns;
		}

		private bool MatchCase => matchCaseCheckBox.Checked;
		private bool MatchWholeWord => matchWholeWordCheckBox.Checked;
		private bool SearchUp => searchUpCheckBox.Checked;
		private string SearchString => expressionTextBox.Text;

		private void UseCheckBoxCheckedChanged(object sender, EventArgs e){
			wildcardsComboBox.Enabled = useCheckBox.Checked;
			helpButton.Enabled = useCheckBox.Checked;
		}

		private void FindAllButtonClick(object sender, EventArgs e){
			toolStripStatusLabel1.Text = "";
			if (lookInComboBox.SelectedIndex > tableModel.ColumnCount){
				if (multipleColumns.Length == 0){
					MessageBox.Show("Please select columns.");
					return;
				}
			}
			bool matchCase = MatchCase;
			bool matchWholeWord = MatchWholeWord;
			string searchString = SearchString;
			if (string.IsNullOrEmpty(searchString)){
				MessageBox.Show("Please enter a search string.");
				return;
			}
			IEnumerable<int> colInds = GetColumnIndices();
			if (!matchCase){
				searchString = searchString.ToLower();
			}
			int[][] matchingCols;
			int[] searchInds = FindAll(matchCase, matchWholeWord, searchString, colInds, out matchingCols);
			if (searchInds.Length == 0){
				toolStripStatusLabel1.Text = "Search string not found.";
			}
			Height = expandedHeight;
			tableView1.TableModel = CreateTable(searchInds, matchingCols);
			tableView1.VisibleY = 0;
			tableView1.Invalidate(true);
		}

		private ITableModel CreateTable(IList<int> searchInds, IList<int[]> matchingCols){
			DataTable2 table = new DataTable2("Search results", "Search results");
			table.AddColumn("Row", 100, ColumnType.Integer, "");
			table.AddColumn("Columns", 80, ColumnType.Text, "");
			for (int index = 0; index < searchInds.Count; index++){
				int searchInd = searchInds[index];
				DataRow2 row = table.NewRow();
				row["Row"] = searchInd + 1;
				string[] colNames = new string[matchingCols[index].Length];
				for (int i = 0; i < matchingCols[index].Length; i++){
					colNames[i] = tableModel.GetColumnName(matchingCols[index][i]);
				}
				row["Columns"] = StringUtils.Concat(";", colNames);
				table.AddRow(row);
			}
			return table;
		}

		private int[] FindAll(bool matchCase, bool matchWholeWord, string searchString, IEnumerable<int> colInds,
			out int[][] matchingCols){
			List<int> result = new List<int>();
			List<int[]> matchingCols2 = new List<int[]>();
			for (int i = 0; i < tableModel.RowCount; i++){
				int modelInd = tableView.GetModelIndex(i);
				int[] matchingCols1;
				if (MatchRow(modelInd, colInds, matchCase, matchWholeWord, searchString, out matchingCols1)){
					result.Add(i);
					matchingCols2.Add(matchingCols1);
				}
			}
			matchingCols = matchingCols2.ToArray();
			return result.ToArray();
		}

		private void FindNextButtonClick(object sender, EventArgs e){
			if (lookInComboBox.SelectedIndex > tableModel.ColumnCount){
				if (multipleColumns.Length == 0){
					MessageBox.Show("Please select columns.");
					return;
				}
			}
			bool matchCase = MatchCase;
			bool matchWholeWord = MatchWholeWord;
			bool searchUp = SearchUp;
			string searchString = SearchString;
			if (string.IsNullOrEmpty(searchString)){
				MessageBox.Show("Please enter a search string.");
				return;
			}
			IEnumerable<int> colInds = GetColumnIndices();
			if (!matchCase){
				searchString = searchString.ToLower();
			}
			if (searchUp){
				if (searchRowIndView < 0){
					searchRowIndView = tableModel.RowCount;
				}
				FindUp(matchCase, matchWholeWord, searchString, colInds);
			} else{
				FindDown(matchCase, matchWholeWord, searchString, colInds);
			}
		}

		private void FindUp(bool matchCase, bool matchWholeWord, string searchString, IEnumerable<int> colInds){
			searchRowIndView--;
			while (searchRowIndView >= 0){
				int modelInd = tableView.GetModelIndex(searchRowIndView);
				int[] matchingCols;
				if (MatchRow(modelInd, colInds, matchCase, matchWholeWord, searchString, out matchingCols)){
					tableView.ClearSelection();
					tableView.SetSelectedViewIndex(searchRowIndView);
					tableView.ScrollToRow(searchRowIndView);
					return;
				}
				searchRowIndView--;
			}
			toolStripStatusLabel1.Text = "Search string not found.";
		}

		private void FindDown(bool matchCase, bool matchWholeWord, string searchString, IEnumerable<int> colInds){
			searchRowIndView++;
			while (searchRowIndView < tableModel.RowCount){
				int modelInd = tableView.GetModelIndex(searchRowIndView);
				int[] matchingCols;
				if (MatchRow(modelInd, colInds, matchCase, matchWholeWord, searchString, out matchingCols)){
					tableView.ClearSelection();
					tableView.SetSelectedViewIndex(searchRowIndView);
					tableView.ScrollToRow(searchRowIndView);
					return;
				}
				searchRowIndView++;
			}
			toolStripStatusLabel1.Text = "Search string not found.";
		}

		private bool MatchRow(int rowInd, IEnumerable<int> columnIndices, bool matchCase, bool matchWholeWord,
			string searchString, out int[] matchingCols){
			List<int> matchingCols1 = new List<int>();
			foreach (int columnIndex in columnIndices){
				object e = tableModel.GetEntry(rowInd, columnIndex);
				if (e == null){
					continue;
				}
				string val = e.ToString();
				if (!matchCase){
					val = val.ToLower();
				}
				if (MatchCell(val, matchWholeWord, searchString)){
					matchingCols1.Add(columnIndex);
				}
			}
			matchingCols = matchingCols1.Count > 0 ? matchingCols1.ToArray() : null;
			return matchingCols1.Count > 0;
		}

		private static bool MatchCell(string val, bool matchWholeWord, string searchString){
			if (!matchWholeWord){
				return val.Contains(searchString);
			}
			val = StringUtils.ReduceWhitespace(val);
			string[] vals = val.Split(' ', ';');
			foreach (string s in vals){
				if (s.Equals(searchString)){
					return true;
				}
			}
			return false;
		}

		private void CancelButtonClick(object sender, EventArgs e){
			Close();
		}

		private void ColumnSelectButtonClick(object sender, EventArgs e){
			string[] names = new string[tableModel.ColumnCount];
			for (int i = 0; i < names.Length; i++){
				names[i] = tableModel.GetColumnName(i);
			}
			SelectColumnsForm scf = new SelectColumnsForm(names, multipleColumns);
			scf.ShowDialog();
			multipleColumns = scf.SelectedIndices;
		}

		public void FocusInputField(){
			expressionTextBox.Focus();
			expressionTextBox.SelectAll();
		}
	}
}