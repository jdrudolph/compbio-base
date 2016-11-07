using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BaseLib.Forms.Scroll;
using BaseLib.Forms.Table;

namespace BaseLib.Wpf{
	public partial class TableView : UserControl{
		internal static readonly List<ITableSelectionAgent> selectionAgents = new List<ITableSelectionAgent>();
		public event EventHandler SelectionChanged;
		private readonly CompoundScrollableControl tableView;
		private readonly TableViewControlModel tableViewWf;
		private bool textBoxVisible;
		private bool hasSelectionAgent;
		private ITableSelectionAgent selectionAgent;
		private int selectionAgentColInd = -1;
		private double[] selectionAgentColVals;

		public TableView(){
			InitializeComponent();
		}
	}
}