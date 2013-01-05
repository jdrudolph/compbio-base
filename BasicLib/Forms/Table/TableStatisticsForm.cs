using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BasicLib.Forms.Scatter;

namespace BasicLib.Forms.Table{
	internal partial class TableStatisticsForm : Form{
		private ITableModel table;
		private ScatterPlotData plotData;

		public TableStatisticsForm(){
			InitializeComponent();
		}

		public ScatterPlot ScatterPlot { get { return scatterPlot; } }

		public void SetTable(ITableModel dataTable){
			table = dataTable;
			string[] axisAlternatives = new[]{"Linear", "Logarithmic"};
			string[] numCols = GetNumericColumnNames(dataTable);
			string[] colCols = GetColorColumnNames(dataTable);
			xAxisComboBox.Items.AddRange(numCols);
			xAxisTypeComboBox.Items.AddRange(axisAlternatives);
			yAxisComboBox.Items.AddRange(numCols);
			yAxisTypeComboBox.Items.AddRange(axisAlternatives);
			colorsComboBox.Items.AddRange(colCols);
			colorsTypeComboBox.Items.AddRange(axisAlternatives);
			labelsComboBox.Items.AddRange(GetColumnNames(dataTable));
			colorScale.Text = "";
			selectionColorPanel.Click += SelectionColorPanelClick;
			plotData = new ScatterPlotData();
			scatterPlot.ScatterPlotData = plotData;
			scatterPlot.ColorScale = colorScale;
		}

		private ScatterPlotValues GetValues2(int index, bool isLog){
			ColumnType type = table.GetColumnType(index);
			bool isMulti = TableView.IsMulti(type);
			if (isMulti){
				double[][] result = GetMultiDoubleValuesInColumn(table, index);
				if (isLog){
					foreach (double[] t in result){
						for (int j = 0; j < t.Length; j++){
							t[j] = Math.Log(t[j]);
						}
					}
				}
				return new ScatterPlotValues(result);
			} else{
				double[] result = GetDoubleValuesInColumn(table, index);
				if (isLog){
					for (int i = 0; i < result.Length; i++){
						result[i] = Math.Log(result[i]);
					}
				}
				return new ScatterPlotValues(result);
			}
		}

		private void SelectionColorPanelClick(object sender, EventArgs e){
			if (colorDialog1.ShowDialog() == DialogResult.OK){
				selectionColorPanel.BackColor = colorDialog1.Color;
				scatterPlot.SelectionColor = selectionColorPanel.BackColor;
			}
		}

		private void XAxisComboBoxSelectedIndexChanged(object sender, EventArgs e){
			string colName = (string) xAxisComboBox.SelectedItem;
			plotData.XLabel = (colName);
			int index = GetColIndex(table, colName);
			bool isLog = TableView.IsLogarithmic(table.GetColumnType(index));
			xAxisTypeComboBox.SelectedIndex = isLog ? 1 : 0;
			plotData.XValues = GetValues2(index, isLog);
			scatterPlot.ScatterPlotData = plotData;
		}

		private void XAxisTypeComboBoxSelectedIndexChanged(object sender, EventArgs e){
			int selInd = xAxisTypeComboBox.SelectedIndex;
			bool isLog = selInd == 1;
			plotData.XIsLogarithmic = isLog;
			string colName = (string) xAxisComboBox.SelectedItem;
			int index = GetColIndex(table, colName);
			plotData.XValues = GetValues2(index, isLog);
			scatterPlot.ScatterPlotData = plotData;
		}

		private void YAxisComboBoxSelectedIndexChanged(object sender, EventArgs e){
			string colName = (string) yAxisComboBox.SelectedItem;
			plotData.YLabel = colName;
			int index = GetColIndex(table, colName);
			bool isLog = TableView.IsLogarithmic(table.GetColumnType(index));
			yAxisTypeComboBox.SelectedIndex = isLog ? 1 : 0;
			plotData.YValues = GetValues2(index, isLog);
			scatterPlot.ScatterPlotData = plotData;
		}

		private void YAxisTypeComboBoxSelectedIndexChanged(object sender, EventArgs e){
			int selInd = yAxisTypeComboBox.SelectedIndex;
			bool isLog = selInd == 1;
			plotData.YIsLogarithmic = isLog;
			string colName = (string) yAxisComboBox.SelectedItem;
			int index = GetColIndex(table, colName);
			plotData.YValues = GetValues2(index, isLog);
			scatterPlot.ScatterPlotData = plotData;
		}

		private void ColorsComboBoxSelectedIndexChanged(object sender, EventArgs e){
			string colName = (string) colorsComboBox.SelectedItem;
			int index = GetColIndex(table, colName);
			bool isLogarithmic = TableView.IsLogarithmic(table.GetColumnType(index));
			colorScale.SetLogarithmic(isLogarithmic);
			plotData.ColorLabel = colName;
			colorScale.Text = colName;
			if (IsNumeric(table, colName)){
				colorsTypeComboBox.Enabled = true;
				colorScale.Enabled = true;
				colorScale.Visible = true;
				colorScale.Text = colName;
				colorsTypeComboBox.SelectedIndex = isLogarithmic ? 1 : 0;
				plotData.ColorValues = GetValues2(index, isLogarithmic);
			} else{
				colorScale.Text = "";
				colorScale.Enabled = false;
				colorScale.Visible = false;
				plotData.ColorValues = null;
				//plotData.Groups = GetCategoricalValues(index);
			}
			scatterPlot.ScatterPlotData = plotData;
		}

		private void ColorsTypeComboBoxSelectedIndexChanged(object sender, EventArgs e){
			int selInd = colorsTypeComboBox.SelectedIndex;
			bool isLog = selInd == 1;
			colorScale.SetLogarithmic(isLog);
			plotData.ColorIsLogarithmic = isLog;
			string colName = (string) colorsComboBox.SelectedItem;
			int index = GetColIndex(table, colName);
			plotData.ColorValues = IsNumeric(table, colName) ? GetValues2(index, isLog) : null;
			scatterPlot.ScatterPlotData = plotData;
		}

		private void LabelsComboBoxSelectedIndexChanged(object sender, EventArgs e){
			if (sender is ComboBox){
				string colName = (sender as ComboBox).SelectedItem as string;
				int index = GetColIndex(table, colName);
				plotData.Labels = GetStringValuesInColumn(table, index);
				scatterPlot.ScatterPlotData = plotData;
			}
		}

		public static string[] GetNumericColumnNames(ITableModel table){
			List<string> result = new List<string>();
			for (int i = 0; i < table.ColumnCount; i++){
				if (IsNumeric(table.GetColumnType(i))){
					result.Add(table.GetColumnName(i));
				}
			}
			return result.ToArray();
		}

		public static string[] GetStringColumnNames(ITableModel table){
			List<string> result = new List<string>();
			for (int i = 0; i < table.ColumnCount; i++){
				if (!IsNumeric(table.GetColumnType(i)) || (table.GetColumnType(i) == ColumnType.Categorical)){
					result.Add(table.GetColumnName(i));
				}
			}
			return result.ToArray();
		}

		public static string[] GetColorColumnNames(ITableModel table){
			List<string> result = new List<string>();
			for (int i = 0; i < table.ColumnCount; i++){
				if (IsNumeric(table.GetColumnType(i)) || table.GetColumnType(i) == ColumnType.Categorical){
					result.Add(table.GetColumnName(i));
				}
			}
			return result.ToArray();
		}

		public static string[] GetColumnNames(ITableModel table){
			string[] result = new string[table.ColumnCount];
			for (int i = 0; i < result.Length; i++){
				result[i] = table.GetColumnName(i);
			}
			return result;
		}

		internal static bool IsNumeric(ColumnType type){
			return type == ColumnType.NumericLog || type == ColumnType.Numeric || type == ColumnType.Expression ||
				type == ColumnType.Integer || type == ColumnType.MultiInteger || type == ColumnType.MultiNumeric ||
				type == ColumnType.MultiNumericLog;
		}

		public static double[] GetDoubleValuesInColumn(ITableModel table, int index){
			double[] result = new double[table.RowCount];
			for (int i = 0; i < result.Length; i++){
				object o = table.GetEntry(i, index);
				if (o == null || o is DBNull){
					continue;
				}
				if (table.GetColumnType(index) == ColumnType.Integer){
					result[i] = (int) o;
				} else{
					if (o is double){
						result[i] = (double) o;
					} else if (o is float){
						result[i] = (float) o;
					} else{
						result[i] = (double) o;
					}
				}
			}
			return result;
		}

		public static double[][] GetMultiDoubleValuesInColumn(ITableModel table, int index){
			double[][] result = new double[table.RowCount][];
			for (int i = 0; i < result.Length; i++){
				object o = table.GetEntry(i, index);
				if (o == null || o is DBNull){
					result[i] = new double[0];
					continue;
				}
				string s = (string) o;
				s = s.Trim();
				string[] w = s.Length == 0 ? new string[0] : s.Split(';');
				result[i] = new double[w.Length];
				for (int j = 0; j < w.Length; j++){
					result[i][j] = double.Parse(w[j]);
				}
			}
			return result;
		}

		public static string[] GetStringValuesInColumn(ITableModel table, int index){
			string[] result = new string[table.RowCount];
			for (int i = 0; i < result.Length; i++){
				object o = table.GetEntry(i, index);
				result[i] = o != null ? o.ToString() : "";
			}
			return result;
		}

		public static int GetColIndex(ITableModel table, string columnName){
			for (int i = 0; i < table.ColumnCount; i++){
				if (table.GetColumnName(i).Equals(columnName)){
					return i;
				}
			}
			return -1;
		}

		public static bool IsNumeric(ITableModel table, string columnName){
			int index = GetColIndex(table, columnName);
			return IsNumeric(table.GetColumnType(index));
		}
	}
}