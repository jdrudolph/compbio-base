using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseLib.Forms.Base;
using BaseLib.Forms.Scroll;
using BaseLib.Graphic;
using BaseLib.Properties;
using BaseLib.Symbol;
using BaseLib.Util;

namespace BaseLib.Forms.Table{
	public delegate void RenderTableCell(IGraphics g, bool selected, object o, int width, int x1, int y1);

	public class TableView : CompoundScrollableControl{
		private const int rowHeight = 22;
		private static readonly Color gridColor = Color.FromArgb(172, 168, 153);
		private static readonly Pen gridPen = new Pen(gridColor);
		private static readonly Color headerGridColor = Color.FromArgb(199, 197, 178);
		private static readonly Pen headerGridPen = new Pen(headerGridColor);
		private static readonly Color shadow1Color = Color.FromArgb(203, 199, 184);
		private static readonly Pen shadow1Pen = new Pen(shadow1Color);
		private static readonly Color shadow2Color = Color.FromArgb(214, 210, 194);
		private static readonly Pen shadow2Pen = new Pen(shadow2Color);
		private static readonly Color shadow3Color = Color.FromArgb(226, 222, 205);
		private static readonly Pen shadow3Pen = new Pen(shadow3Color);
		private static readonly Color headerColor = Color.FromArgb(235, 234, 219);
		private static readonly Brush headerBrush = new SolidBrush(headerColor);
		private static readonly Color oddBgColor = Color.FromArgb(224, 224, 224);
		private static readonly Brush oddBgBrush = new SolidBrush(oddBgColor);
		private static readonly Color selectBgColor = Color.FromArgb(49, 106, 197);
		private static readonly Brush selectBgBrush = new SolidBrush(selectBgColor);
		private static readonly Color selectHeader1Color = Color.FromArgb(165, 165, 151);
		private static readonly Pen selectHeader1Pen = new Pen(selectHeader1Color);
		private static readonly Color selectHeader2Color = Color.FromArgb(193, 194, 184);
		private static readonly Pen selectHeader2Pen = new Pen(selectHeader2Color);
		private static readonly Color selectHeader3Color = Color.FromArgb(208, 209, 201);
		private static readonly Pen selectHeader3Pen = new Pen(selectHeader3Color);
		private static readonly Color selectHeader4Color = Color.FromArgb(222, 223, 216);
		private static readonly Brush selectHeader4Brush = new SolidBrush(selectHeader4Color);
		private bool hasRemoveRowsMenuItems;
		private bool hasHelp = true; //TODO
		private bool multiSelect = true;
		public event EventHandler SelectionChanged;
		private ContextMenuStrip contextMenuStrip;
		private ToolStripMenuItem searchToolStripMenuItem;
		private ToolStripMenuItem fontsToolStripMenuItem;
		private ToolStripMenuItem filterToolStripMenuItem;
		private ToolStripMenuItem invertSelectionToolStripMenuItem;
		private ToolStripMenuItem selectionTopToolStripMenuItem;
		private ToolStripMenuItem copySelectedRowsToolStripMenuItem;
		private ToolStripMenuItem copyCellToolStripMenuItem;
		private ToolStripMenuItem exportToolStripMenuItem;
		private ToolStripMenuItem showAllRowsToolStripMenuItem;
		private ToolStripSeparator removeSeparator;
		private ToolStripMenuItem removeSelectedRowsToolStripMenuItem;
		private ToolStripMenuItem removeUnselectedRowsToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private int[] columnWidthSums;
		private int[] columnWidthSumsOld;
		private ITableModel origModel;
		private ITableModel model;
		private Font textFont = new Font("Arial", 9);
		private readonly Font headerFont = new Font("Arial", 9);
		private Brush textBrush = Brushes.Black;
		private Color textColor = Color.Black;
		private bool[] modelRowSel;
		private int[] order;
		private int[] inverseOrder;
		private int selectStart = -1;
		private int sortCol = -1;
		private int resizeCol = -1;
		private int helpCol = -1;
		private int currentRow = -1;
		private int currentCol = -1;
		private int currentX = -1;
		private int currentY = -1;
		private int toolTipX = -1;
		private int toolTipY = -1;
		private bool matrixHelp;
		private SortState sortState = SortState.Unsorted;
		private int colDragX = -1;
		private int deltaDragX;
		//TODO
		private readonly ToolTip columnViewToolTip = new ToolTip();
		//private readonly ToolTip mainViewToolTip = new ToolTip();
		public bool Sortable{
			get { return sortable; }
			set{
				if (selectionTopToolStripMenuItem != null){
					selectionTopToolStripMenuItem.Visible = value;
				}
				sortable = value;
			}
		}
		private FindForm findForm;
		private FilterForm filterForm;
		private int origColumnHeaderHeight = 40;
		private const int maxColHeaderStringSplits = 3;
		private bool hasFilter;
		private bool sortable;
		public TableView() : this("") {}

		public TableView(string name){
			Sortable = true;
			RowHeaderWidth = 70;
			ColumnHeaderHeight = 26;
			Name = name;
			ResizeRedraw = true;
			InitContextMenu();
			filterToolStripMenuItem.Visible = false;
		}

		public bool HasFilter{
			get { return hasFilter; }
			set{
				hasFilter = value;
				filterToolStripMenuItem.Visible = hasFilter;
			}
		}

		public void InitContextMenu(){
			contextMenuStrip = new ContextMenuStrip();
			searchToolStripMenuItem = new ToolStripMenuItem();
			fontsToolStripMenuItem = new ToolStripMenuItem();
			filterToolStripMenuItem = new ToolStripMenuItem();
			invertSelectionToolStripMenuItem = new ToolStripMenuItem();
			selectionTopToolStripMenuItem = new ToolStripMenuItem();
			copySelectedRowsToolStripMenuItem = new ToolStripMenuItem();
			copyCellToolStripMenuItem = new ToolStripMenuItem();
			exportToolStripMenuItem = new ToolStripMenuItem();
			showAllRowsToolStripMenuItem = new ToolStripMenuItem();
			removeSelectedRowsToolStripMenuItem = new ToolStripMenuItem();
			removeSeparator = new ToolStripSeparator();
			removeUnselectedRowsToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator1 = new ToolStripSeparator();
			contextMenuStrip.Items.AddRange(new ToolStripItem[]{
				searchToolStripMenuItem, fontsToolStripMenuItem, filterToolStripMenuItem, invertSelectionToolStripMenuItem,
				selectionTopToolStripMenuItem, exportToolStripMenuItem, copySelectedRowsToolStripMenuItem, copyCellToolStripMenuItem, removeSeparator,
				showAllRowsToolStripMenuItem, removeSelectedRowsToolStripMenuItem, removeUnselectedRowsToolStripMenuItem
			});
			contextMenuStrip.Name = "contextMenuStrip";
			contextMenuStrip.Size = new Size(210, 142);
			searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			searchToolStripMenuItem.Size = new Size(209, 22);
			searchToolStripMenuItem.Text = "Find...";
			searchToolStripMenuItem.Click += SearchToolStripMenuItemClick;
			fontsToolStripMenuItem.Name = "searchToolStripMenuItem";
			fontsToolStripMenuItem.Size = new Size(209, 22);
			fontsToolStripMenuItem.Text = "Font...";
			fontsToolStripMenuItem.Click += FontsToolStripMenuItemClick;
			filterToolStripMenuItem.Name = "filterToolStripMenuItem";
			filterToolStripMenuItem.Size = new Size(209, 22);
			filterToolStripMenuItem.Text = "Filter...";
			filterToolStripMenuItem.Click += FilterToolStripMenuItemClick;
			invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
			invertSelectionToolStripMenuItem.Size = new Size(209, 22);
			invertSelectionToolStripMenuItem.Text = "Invert selection";
			invertSelectionToolStripMenuItem.Click += InvertSelectionToolStripMenuItemClick;
			selectionTopToolStripMenuItem.Name = "selectionTopToolStripMenuItem";
			selectionTopToolStripMenuItem.Size = new Size(209, 22);
			selectionTopToolStripMenuItem.Text = "Bring selection to top";
			selectionTopToolStripMenuItem.Click += SelectionTopToolStripMenuItemClick;
			copySelectedRowsToolStripMenuItem.Name = "copyToolStripMenuItem";
			copySelectedRowsToolStripMenuItem.Size = new Size(209, 22);
			copySelectedRowsToolStripMenuItem.Text = "Copy selected rows";
			copySelectedRowsToolStripMenuItem.Click += CopySelectedRowsToolStripMenuItemClick;
			copyCellToolStripMenuItem.Name = "copyCellToolStripMenuItem";
			copyCellToolStripMenuItem.Size = new Size(209, 22);
			copyCellToolStripMenuItem.Text = "Copy cell";
			copyCellToolStripMenuItem.Click += CopyCellToolStripMenuItemClick;
			exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			exportToolStripMenuItem.Size = new Size(209, 22);
			exportToolStripMenuItem.Text = "Export matrix...";
			exportToolStripMenuItem.Click += ExportToolStripMenuItemClick;
			showAllRowsToolStripMenuItem.Name = "showAllRowsToolStripMenuItem";
			showAllRowsToolStripMenuItem.Size = new Size(209, 22);
			showAllRowsToolStripMenuItem.Text = "Show All Rows";
			showAllRowsToolStripMenuItem.Click += ShowAllRowsToolStripMenuItemClick;
			removeSelectedRowsToolStripMenuItem.Name = "removeSelectedRowsToolStripMenuItem";
			removeSelectedRowsToolStripMenuItem.Size = new Size(209, 22);
			removeSelectedRowsToolStripMenuItem.Text = "Remove Selected Rows";
			removeSelectedRowsToolStripMenuItem.Click += RemoveSelectedRowsToolStripMenuItemClick;
			removeUnselectedRowsToolStripMenuItem.Name = "removeUnselectedRowsToolStripMenuItem";
			removeUnselectedRowsToolStripMenuItem.Size = new Size(209, 22);
			removeUnselectedRowsToolStripMenuItem.Text = "Remove Unselected Rows";
			removeUnselectedRowsToolStripMenuItem.Click += RemoveUnselectedRowsToolStripMenuItemClick;
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(206, 6);
			ContextMenuStrip = contextMenuStrip;
		}

		public bool MultiSelect{
			get { return multiSelect; }
			set{
				if (invertSelectionToolStripMenuItem != null){
					invertSelectionToolStripMenuItem.Visible = value;
				}
				multiSelect = value;
			}
		}
		public bool HasHelp { get { return hasHelp; } set { hasHelp = value; } }
		public override sealed int ColumnHeaderHeight{
			set{
				origColumnHeaderHeight = value;
				base.ColumnHeaderHeight = value;
			}
		}

		public override int DeltaUpToSelection(){
			int[] inds = GetSelectedRowsView();
			if (inds.Length == 0){
				return VisibleY;
			}
			int visRow = (VisibleY + rowHeight - 1)/rowHeight;
			int ind = Array.BinarySearch(inds, visRow);
			if (ind >= 0){
				if (ind < 1){
					return VisibleY;
				}
				return (visRow - inds[ind - 1])*rowHeight;
			}
			ind = -1 - ind;
			if (ind < 1){
				return VisibleY;
			}
			return (visRow - inds[ind - 1])*rowHeight;
		}

		public override int DeltaDownToSelection(){
			int[] inds = GetSelectedRowsView();
			if (inds.Length == 0){
				return TotalHeight - VisibleY - VisibleHeight;
			}
			int visRow = (VisibleY + rowHeight - 1)/rowHeight;
			int ind = Array.BinarySearch(inds, visRow);
			if (ind >= 0){
				if (ind >= inds.Length - 1){
					return TotalHeight - VisibleY - VisibleHeight;
				}
				return (inds[ind + 1] - visRow)*rowHeight;
			}
			ind = -1 - ind;
			if (ind >= inds.Length){
				return TotalHeight - VisibleY - VisibleHeight;
			}
			return (inds[ind] - visRow)*rowHeight;
		}

		public void RemoveSelectedRows(){
			int[] x = GetUnselectedRows();
			model = new SubTableModel(model, x);
			modelRowSel = new bool[model.RowCount];
			order = ArrayUtils.ConsecutiveInts(model.RowCount);
			inverseOrder = ArrayUtils.ConsecutiveInts(model.RowCount);
			sortCol = -1;
			sortState = SortState.Unsorted;
			VisibleY = 0;
			Invalidate(true);
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void ShowAllRows(){
			model = origModel;
			modelRowSel = new bool[model.RowCount];
			order = ArrayUtils.ConsecutiveInts(model.RowCount);
			inverseOrder = ArrayUtils.ConsecutiveInts(model.RowCount);
			sortCol = -1;
			sortState = SortState.Unsorted;
			Invalidate(true);
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void RemoveUnselectedRows(){
			int[] x = GetSelectedRows();
			model = new SubTableModel(model, x);
			modelRowSel = new bool[model.RowCount];
			order = ArrayUtils.ConsecutiveInts(model.RowCount);
			inverseOrder = ArrayUtils.ConsecutiveInts(model.RowCount);
			sortCol = -1;
			sortState = SortState.Unsorted;
			VisibleY = 0;
			Invalidate(true);
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void SetVisibleRows(IList<int> rows){
			model = new SubTableModel(origModel, rows.ToArray());
			modelRowSel = new bool[model.RowCount];
			order = ArrayUtils.ConsecutiveInts(model.RowCount);
			inverseOrder = ArrayUtils.ConsecutiveInts(model.RowCount);
			sortCol = -1;
			sortState = SortState.Unsorted;
			Invalidate(true);
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void SetSelectedRows(IList<int> rows){
			SetSelectedRows(rows, false);
		}

		public void SetSelectedRows(IList<int> rows, bool add){
			if (!add || modelRowSel == null){
				modelRowSel = new bool[RowCount];
			}
			foreach (int row in rows){
				if (row < 0){
					continue;
				}
				if (add){
					modelRowSel[row] = !modelRowSel[row];
				} else{
					modelRowSel[row] = true;
				}
			}
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void FireSelectionChange(){
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void SetSelectedRowsAndMove(IList<int> rows){
			modelRowSel = new bool[RowCount];
			foreach (int row in rows){
				modelRowSel[row] = true;
			}
			if (rows.Count > 0){
				ScrollToRow(inverseOrder[rows[0]]);
			} else{
				Invalidate(true);
			}
		}

		public int[] GetSelectedRows(){
			if (model == null){
				return new int[0];
			}
			CheckSizes();
			List<int> result = new List<int>();
			for (int i = 0; i < model.RowCount; i++){
				if (modelRowSel[i]){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		public int[] GetSelectedRowsView(){
			if (model == null){
				return new int[0];
			}
			CheckSizes();
			List<int> result = new List<int>();
			for (int i = 0; i < model.RowCount; i++){
				if (modelRowSel[order[i]]){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		public void AddContextMenuItem(ToolStripItem item){
			contextMenuStrip.Items.Add(item);
		}

		public bool HasRemoveRowsMenuItems{
			get { return hasRemoveRowsMenuItems; }
			set{
				hasRemoveRowsMenuItems = value;
				removeSelectedRowsToolStripMenuItem.Visible = value;
				showAllRowsToolStripMenuItem.Visible = value;
				removeUnselectedRowsToolStripMenuItem.Visible = value;
				removeSeparator.Visible = value;
			}
		}
		public ITableModel TableModel{
			get { return model; }
			set{
				model = value;
				if (model == null){
					return;
				}
				modelRowSel = new bool[model.RowCount];
				order = ArrayUtils.ConsecutiveInts(model.RowCount);
				inverseOrder = ArrayUtils.ConsecutiveInts(model.RowCount);
				columnWidthSums = new int[model.ColumnCount];
				if (model.ColumnCount > 0){
					columnWidthSums[0] = model.GetColumnWidth(0);
				}
				for (int i = 1; i < model.ColumnCount; i++){
					columnWidthSums[i] = columnWidthSums[i - 1] + model.GetColumnWidth(i);
				}
				origModel = model;
				if (model.AnnotationRowsCount > 0){
					base.ColumnHeaderHeight = origColumnHeaderHeight + model.AnnotationRowsCount*rowHeight;
				}
			}
		}
		public override sealed int TotalWidth{
			get{
				if (model == null){
					return 0;
				}
				if (columnWidthSums == null){
					return 0;
				}
				int ind = model.ColumnCount - 1;
				if (ind < 0 || ind >= columnWidthSums.Length){
					return 0;
				}
				return columnWidthSums[ind] + 5;
			}
		}
		public override sealed int TotalHeight { get { return model == null ? 0 : rowHeight*model.RowCount + 5; } }
		public override sealed int DeltaX { get { return 40; } }
		public override sealed int DeltaY { get { return rowHeight; } }
		public int RowCount { get { return model == null ? 0 : model.RowCount; } }

		public bool ViewRowIsSelected(int row){
			if (modelRowSel == null){
				return false;
			}
			if (row < 0 || order == null || row >= order.Length){
				return false;
			}
			return modelRowSel[order[row]];
		}

		public bool ModelRowIsSelected(int row){
			if (modelRowSel == null){
				return false;
			}
			if (row < 0 || row >= modelRowSel.Length){
				return false;
			}
			return modelRowSel[row];
		}

		protected internal override sealed void OnPaintRowHeaderView(IGraphics g, int y, int height){
			if (model == null){
				return;
			}
			g.FillRectangle(headerBrush, 0, 0, RowHeaderWidth - 1, height);
			g.DrawLine(gridPen, 0, 0, 0, height);
			g.DrawLine(gridPen, RowHeaderWidth - 1, 0, RowHeaderWidth - 1, height);
			g.DrawLine(Pens.White, 1, 0, 1, height);
			g.DrawLine(shadow1Pen, RowHeaderWidth - 2, 0, RowHeaderWidth - 2, height);
			g.DrawLine(shadow2Pen, RowHeaderWidth - 3, 0, RowHeaderWidth - 3, height);
			g.DrawLine(shadow3Pen, RowHeaderWidth - 4, 0, RowHeaderWidth - 4, height);
			int offset = -y%rowHeight;
			for (int y1 = offset - rowHeight; y1 < height; y1 += rowHeight){
				int row = (y + y1)/rowHeight;
				if (model == null || row > model.RowCount){
					break;
				}
				g.DrawLine(headerGridPen, 5, y1 - 1, RowHeaderWidth - 6, y1 - 1);
				g.DrawLine(Pens.White, 5, y1, RowHeaderWidth - 6, y1);
			}
			for (int y1 = offset - rowHeight; y1 < height; y1 += rowHeight){
				int row = (y + y1)/rowHeight;
				if (row < 0){
					continue;
				}
				if (model == null || row >= model.RowCount){
					break;
				}
				if (ViewRowIsSelected(row)){
					g.DrawLine(selectHeader1Pen, 2, y1 + 1, RowHeaderWidth - 2, y1 + 1);
					g.DrawLine(selectHeader1Pen, 2, y1 + rowHeight, RowHeaderWidth - 2, y1 + rowHeight);
					g.DrawLine(selectHeader1Pen, RowHeaderWidth - 2, y1 + 1, RowHeaderWidth - 2, y1 + rowHeight);
					g.DrawLine(selectHeader2Pen, 2, y1 + 2, RowHeaderWidth - 3, y1 + 2);
					g.DrawLine(selectHeader2Pen, 2, y1 + 2, 2, y1 + rowHeight - 1);
					g.DrawLine(selectHeader3Pen, 3, y1 + 3, RowHeaderWidth - 3, y1 + 3);
					g.DrawLine(selectHeader3Pen, 3, y1 + 3, 3, y1 + rowHeight - 1);
					g.FillRectangle(selectHeader4Brush, 4, y1 + 4, RowHeaderWidth - 6, rowHeight - 4);
				}
				g.DrawString("" + (row + 1), textFont, Brushes.Black, 5, y1 + 4);
			}
		}

		protected internal override sealed void OnPaintColumnHeaderView(IGraphics g, int x, int width){
			if (model == null){
				return;
			}
			g.FillRectangle(headerBrush, 0, 0, width, ColumnHeaderHeight - 1);
			g.DrawLine(gridPen, 0, 0, width, 0);
			g.DrawLine(gridPen, 0, ColumnHeaderHeight - 1, width, ColumnHeaderHeight - 1);
			g.DrawLine(Pens.White, 0, 1, width, 1);
			g.DrawLine(shadow1Pen, 0, ColumnHeaderHeight - 2, width, ColumnHeaderHeight - 2);
			g.DrawLine(shadow2Pen, 0, ColumnHeaderHeight - 3, width, ColumnHeaderHeight - 3);
			g.DrawLine(shadow3Pen, 0, ColumnHeaderHeight - 4, width, ColumnHeaderHeight - 4);
			if (columnWidthSums != null){
				int startInd = ArrayUtils.CeilIndex(columnWidthSums, x);
				int endInd = ArrayUtils.FloorIndex(columnWidthSums, x + width);
				if (startInd >= 0){
					for (int i = startInd; i <= endInd; i++){
						int x1 = columnWidthSums[i] - x;
						g.DrawLine(headerGridPen, x1, 5, x1, ColumnHeaderHeight - 6);
						g.DrawLine(Pens.White, x1 + 1, 5, x1 + 1, ColumnHeaderHeight - 6);
					}
				}
			}
			if (columnWidthSums != null && columnWidthSums.Length > 0){
				int startInd = Math.Max(0, ArrayUtils.CeilIndex(columnWidthSums, x) - 1);
				int endInd = Math.Min(columnWidthSums.Length - 1, ArrayUtils.FloorIndex(columnWidthSums, x + width) + 1);
				if (startInd >= 0){
					for (int i = startInd; i <= endInd; i++){
						int x1 = (i > 0 ? columnWidthSums[i - 1] : 0) - x;
						int w = i == 0 ? columnWidthSums[0] : columnWidthSums[i] - columnWidthSums[i - 1];
						string[] q = GetStringHeader(g, i, w, headerFont);
						for (int j = 0; j < q.Length; j++){
							g.DrawString(q[j], headerFont, Brushes.Black, x1 + 3, 4 + 11*j);
						}
					}
					if (sortCol != -1 && sortState != SortState.Unsorted){
						int x1 = columnWidthSums[sortCol] - x - 11;
						if (x1 >= -15 && x1 <= width){
							g.DrawImage(sortState == SortState.Increasing ? Resources.arrowDown1 : Resources.arrowUp1, x1, 6, 9, 13);
						}
					}
					if (helpCol != -1){
						int x1 = (helpCol == 0 ? 0 : columnWidthSums[helpCol - 1]) - x + 5;
						if (x1 >= -15 && x1 <= width){
							g.DrawImage(Resources.question12, x1, 7, 10, 10);
						}
					}
					if (model != null && model.AnnotationRowsCount > 0){
						for (int i = startInd; i <= endInd; i++){
							int x1 = (i > 0 ? columnWidthSums[i - 1] : 0) - x;
							int x2 = (i >= 0 ? columnWidthSums[i] : 0) - x;
							for (int k = 0; k < model.AnnotationRowsCount; k++){
								int y1 = origColumnHeaderHeight + k*rowHeight;
								g.DrawLine(headerGridPen, x1 + 5, y1 - 1, x2 - 6, y1 - 1);
								g.DrawLine(Pens.White, x1 + 5, y1, x2 - 6, y1);
								string s = (string) model.GetAnnotationRowValue(k, i);
								if (s != null){
									g.DrawString("" + GetStringValue(g, s, x2 - x1 - 2, headerFont), textFont, Brushes.Black, x1 + 3, y1 + 3);
								}
							}
						}
					}
				}
			}
		}

		protected internal override sealed void OnPaintCornerView(IGraphics g){
			if (model == null){
				return;
			}
			g.FillRectangle(headerBrush, 0, 0, RowHeaderWidth - 1, ColumnHeaderHeight - 1);
			g.DrawRectangle(gridPen, 0, 0, RowHeaderWidth - 1, ColumnHeaderHeight - 1);
			g.DrawLine(Pens.White, 1, 1, RowHeaderWidth - 2, 1);
			g.DrawLine(Pens.White, 1, 1, 1, ColumnHeaderHeight - 2);
			if (matrixHelp){
				g.DrawImage(Resources.question12, 7, 7, 10, 10);
			}
			if (model != null && model.AnnotationRowsCount > 0){
				for (int i = 0; i < model.AnnotationRowsCount; i++){
					int y1 = origColumnHeaderHeight + i*rowHeight;
					g.DrawLine(headerGridPen, 5, y1 - 1, RowHeaderWidth - 6, y1 - 1);
					g.DrawLine(Pens.White, 5, y1, RowHeaderWidth - 6, y1);
					string s = model.GetAnnotationRowName(i);
					if (s != null){
						g.DrawString("" + GetStringValue(g, s, RowHeaderWidth - 6, headerFont), textFont, Brushes.Black, 3, y1 + 3);
					}
				}
			}
		}

		protected internal override void OnMouseLeaveMainView(EventArgs e){
			//TODO
			//mainViewToolTip.Hide(this);
		}

		public static bool IsLogarithmic(ColumnType columnType){
			return columnType == ColumnType.NumericLog || columnType == ColumnType.MultiNumericLog ||
				columnType == ColumnType.Expression;
		}

		public static bool IsMulti(ColumnType columnType){
			return columnType == ColumnType.MultiNumeric || columnType == ColumnType.MultiNumericLog ||
				columnType == ColumnType.MultiInteger;
		}

		public static bool IsNumeric(ColumnType columnType){
			return columnType == ColumnType.NumericLog || columnType == ColumnType.Expression || columnType == ColumnType.Numeric ||
				columnType == ColumnType.Integer || columnType == ColumnType.MultiInteger || columnType == ColumnType.MultiNumeric ||
				columnType == ColumnType.MultiNumericLog;
		}

		public void ScrollToRow(int row){
			if ((RowCount - row)*rowHeight < VisibleHeight){
				ScrollToEnd();
			} else{
				VisibleY = row*rowHeight;
			}
		}

		public void ScrollToEnd(){
			if ((RowCount)*rowHeight < VisibleHeight){
				VisibleY = 0;
			} else{
				VisibleY = (RowCount - (int) (VisibleHeight/(double) rowHeight))*rowHeight;
			}
		}

		public int GetModelIndex(int rowIndView){
			return order[rowIndView];
		}

		public void ClearSelection(){
			if (modelRowSel == null){
				return;
			}
			for (int i = 0; i < modelRowSel.Length; i++){
				modelRowSel[i] = false;
			}
		}

		public void ClearSelectionFire(){
			ClearSelection();
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void SelectAll(){
			if (modelRowSel == null){
				return;
			}
			for (int i = 0; i < modelRowSel.Length; i++){
				modelRowSel[i] = true;
			}
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void SetSetectedViewIndex(int index){
			CheckSizes();
			if (!multiSelect){
				ClearSelection();
			}
			modelRowSel[order[index]] = true;
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public void SetSetectedIndex(int index){
			SetSetectedIndex(index, this);
		}

		public void SetectAll(){
			CheckSizes();
			bool[] x = new bool[RowCount];
			for (int i = 0; i < x.Length; i++){
				x[i] = true;
			}
			SetSetection(x);
		}

		public void SetSetectedIndex(int index, object sender){
			CheckSizes();
			if (!multiSelect){
				ClearSelection();
			}
			modelRowSel[index] = true;
			if (SelectionChanged != null){
				SelectionChanged(sender, new EventArgs());
			}
		}

		public void SetSetection(bool[] s){
			modelRowSel = s;
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		public object GetEntry(int row, int col){
			CheckSizes();
			return model == null ? null : model.GetEntry(order[row], col);
		}

		private void SearchToolStripMenuItemClick(object sender, EventArgs e){
			Find();
		}

		private void FontsToolStripMenuItemClick(object sender, EventArgs e){
			ChangeFonts();
		}

		private void FilterToolStripMenuItemClick(object sender, EventArgs e){
			Filter();
		}

		private void Filter(){
			if (model == null){
				return;
			}
			if (filterForm == null){
				filterForm = new FilterForm(this){TopMost = true};
			}
			filterForm.Visible = false;
			if (filterForm.WindowState == FormWindowState.Minimized){
				filterForm.WindowState = FormWindowState.Normal;
			}
			filterForm.BringToFront();
			filterForm.Focus();
			filterForm.Show(this);
		}

		private void Find(){
			if (model == null){
				return;
			}
			if (findForm == null){
				findForm = new FindForm(this){TopMost = true};
			}
			findForm.Visible = false;
			if (findForm.WindowState == FormWindowState.Minimized){
				findForm.WindowState = FormWindowState.Normal;
			}
			findForm.BringToFront();
			findForm.Focus();
			findForm.Show(this);
		}

		private void ChangeFonts(){
			if (model == null){
				return;
			}
			FontDialog fontDialog = new FontDialog{ShowColor = true, Font = textFont, Color = textColor};
			if (fontDialog.ShowDialog() != DialogResult.Cancel){
				textFont = fontDialog.Font;
				textColor = fontDialog.Color;
				textBrush = new SolidBrush(textColor);
			}
			fontDialog.Dispose();
			Invalidate(true);
		}

		private void InvertSelectionToolStripMenuItemClick(object sender, EventArgs e){
			if (model == null){
				return;
			}
			if (modelRowSel == null){
				return;
			}
			for (int i = 0; i < modelRowSel.Length; i++){
				modelRowSel[i] = !modelRowSel[i];
			}
			Invalidate(true);
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		private void SelectionTopToolStripMenuItemClick(object sender, EventArgs e){
			if (model == null){
				return;
			}
			if (modelRowSel == null){
				return;
			}
			List<int> l1 = new List<int>();
			List<int> l2 = new List<int>();
			for (int i = 0; i < modelRowSel.Length; i++){
				if (modelRowSel[i]){
					l1.Add(i);
				} else{
					l2.Add(i);
				}
			}
			order = ArrayUtils.Concat(l1, l2);
			Invalidate(true);
		}

		private void CopySelectedRowsToolStripMenuItemClick(object sender, EventArgs e) {
			if (model == null) {
				return;
			}
			Copy();
		}

		private void CopyCellToolStripMenuItemClick(object sender, EventArgs e) {
			if (model == null) {
				return;
			}
			CopyCell();
		}

		private void ExportToolStripMenuItemClick(object sender, EventArgs e) {
			if (model == null){
				return;
			}
			SaveFileDialog ofd = new SaveFileDialog{Filter = "Text File (*.txt)|*.txt"};
			if (ofd.ShowDialog() == DialogResult.OK){
				ExportMatrix(ofd.FileName);
			}
		}

		private void ExportMatrix(string fileName){
			StreamWriter writer = new StreamWriter(fileName);
			StringBuilder line = new StringBuilder();
			if (model.ColumnCount > 0){
				line.Append(model.GetColumnName(0));
			}
			for (int i = 1; i < model.ColumnCount; i++){
				line.Append("\t" + model.GetColumnName(i));
			}
			writer.WriteLine(line.ToString());
			for (int j = 0; j < model.RowCount; j++){
				line = new StringBuilder();
				if (model.ColumnCount > 0){
					line.Append(model.GetEntry(j, 0));
				}
				for (int i = 1; i < model.ColumnCount; i++){
					line.Append("\t" + model.GetEntry(j, i));
				}
				writer.WriteLine(line.ToString());
			}
			writer.Close();
		}

		private void RemoveSelectedRowsToolStripMenuItemClick(object sender, EventArgs e){
			RemoveSelectedRows();
		}

		private void ShowAllRowsToolStripMenuItemClick(object sender, EventArgs e){
			ShowAllRows();
		}

		private void RemoveUnselectedRowsToolStripMenuItemClick(object sender, EventArgs e){
			RemoveUnselectedRows();
		}

		private int[] GetUnselectedRows(){
			if (model == null){
				return new int[0];
			}
			CheckSizes();
			List<int> result = new List<int>();
			for (int i = 0; i < model.RowCount; i++){
				if (!modelRowSel[i]){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		private void RenderCell(IGraphics g, bool selected, int row, int col, int width, int x1, int y1){
			object o = model.GetEntry(row, col);
			RenderTableCell render = model.GetColumnRenderer(col);
			if (render != null){
				render(g, selected, o, width, x1, y1);
				return;
			}
			ColumnType type = model.GetColumnType(col);
			switch (type){
				case ColumnType.Color:
					RenderCellColor(g, selected, o, x1, y1);
					break;
				case ColumnType.DashStyle:
					RenderCellDashStyle(g, selected, o, x1, y1);
					break;
				default:
					RenderCellString(g, selected, o, width, x1, y1);
					break;
			}
		}

		private static void RenderCellDashStyle(IGraphics g, bool selected, object o, int x1, int y1){
			int dashInd = (int) o;
			Pen p = selected ? Pens.White : Pens.Black;
			p = (Pen) p.Clone();
			p.DashStyle = DashStyles.DashStyleFromIndex(dashInd);
			p.Width = 2;
			g.DrawLine(p, x1 + 4, y1 + 11, x1 + 40, y1 + 11);
		}

		private static void RenderCellColor(IGraphics g, bool selected, object o, int x1, int y1){
			Color c = (Color) o;
			Brush b = new SolidBrush(c);
			Pen p = selected ? Pens.White : Pens.Black;
			const int w = 14;
			g.FillRectangle(b, x1 + 3, y1 + 4, w, w);
			g.DrawRectangle(p, x1 + 3, y1 + 4, w, w);
		}

		private void RenderCellString(IGraphics g, bool selected, object o, int width, int x1, int y1){
			g.DrawString(GetStringValue(g, o, width, textFont), textFont, selected ? Brushes.White : textBrush, x1 + 3, y1 + 4);
		}

		private static string GetStringValue(IGraphics g, object o, int width, Font font){
			if (o is double){
				double x = (double) o;
				if (double.IsNaN(x)){
					o = "NaN";
				}
			}
			string s = "" + o;
			return StringUtils.GetStringValue(g, s, width, font);
		}

		private void CheckSizes(){
			if (model == null){
				return;
			}
			if (order == null || order.Length != model.RowCount){
				order = ArrayUtils.ConsecutiveInts(model.RowCount);
				inverseOrder = ArrayUtils.ConsecutiveInts(model.RowCount);
				sortState = SortState.Unsorted;
				sortCol = -1;
			}
			if (modelRowSel == null || modelRowSel.Length != model.RowCount){
				modelRowSel = new bool[model.RowCount];
			}
		}

		private string[] GetStringHeader(IGraphics g, int col, int width, Font font){
			string s = model.GetColumnName(col);
			string[] q = StringUtils.WrapString(g, s, width, font);
			if (q.Length > maxColHeaderStringSplits){
				Array.Resize(ref q, maxColHeaderStringSplits);
			}
			for (int i = 0; i < q.Length; i++){
				q[i] = GetStringValue(g, q[i], width, font);
			}
			return q;
		}

		private void Unsort(){
			sortState = SortState.Unsorted;
			for (int i = 0; i < order.Length; i++){
				order[i] = i;
				inverseOrder[i] = i;
			}
		}

		private void InvertOrder(){
			sortState = SortState.Decreasing;
			ArrayUtils.Revert(order);
			ArrayUtils.Revert(inverseOrder);
		}

		private void Sort(){
			sortState = SortState.Increasing;
			ColumnType type = model.GetColumnType(sortCol);
			switch (type){
				case ColumnType.Integer:
					order = SortInt();
					break;
				case ColumnType.NumericLog:
				case ColumnType.Numeric:
				case ColumnType.Expression:
					order = SortDouble();
					break;
				default:
					order = SortString();
					break;
			}
			inverseOrder = new int[order.Length];
			for (int i = 0; i < order.Length; i++){
				inverseOrder[order[i]] = i;
			}
		}

		private int[] SortInt(){
			int[] data = new int[model.RowCount];
			for (int i = 0; i < data.Length; i++){
				object o = model.GetEntry(i, sortCol);
				if (o == null || o is DBNull){
					data[i] = int.MaxValue;
				} else{
					data[i] = o is int ? (int) o : o is ushort ? (ushort) o : int.MaxValue;
				}
			}
			return ArrayUtils.Order(data);
		}

		private int[] SortDouble(){
			double[] data = new double[model.RowCount];
			for (int i = 0; i < data.Length; i++){
				object o = model.GetEntry(i, sortCol);
				if (o == null || o is DBNull){
					data[i] = double.MaxValue;
				} else{
					data[i] = o is double ? (double) o : o is float ? (float) o : o is int ? (int) o : double.NaN;
				}
			}
			return ArrayUtils.Order(data);
		}

		private int[] SortString(){
			string[] data = new string[model.RowCount];
			for (int i = 0; i < data.Length; i++){
				object o = model.GetEntry(i, sortCol);
				data[i] = o != null ? o.ToString() : "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ";
			}
			return ArrayUtils.Order(data);
		}

		private void SelectRange(int start, int end){
			for (int i = Math.Min(start, end); i <= Math.Max(start, end); i++){
				modelRowSel[order[i]] = true;
			}
		}

		private void Goto(){
			//TODO
		}

		private void CopyCell(){
			int x1 = VisibleX + e.X;
			if (columnWidthSums == null) {
				return;
			}
			int ind = ArrayUtils.ClosestIndex(columnWidthSums, x1);
			if (ind >= 0) {
				if (Math.Abs(columnWidthSums[ind] - x1) < 5) {
					Cursor.Current = Cursors.VSplit;
					resizeCol = ind;
				} else {
					Cursor.Current = Cursors.Default;
					resizeCol = -1;
				}
			}
			int indf = ArrayUtils.CeilIndex(columnWidthSums, x1);
			//TODO
		}

		private void Copy(){
			StringBuilder str = new StringBuilder();
			// store the column-names
			str.Append(TableModel.GetColumnName(0));
			for (int i = 1; i < TableModel.ColumnCount; ++i){
				str.Append("\t");
				str.Append(TableModel.GetColumnName(i));
			}
			str.Append("\n");
			// store the selected rows
			int[] selection = GetSelectedRows();
			for (int i = 0; i < selection.Length; i++){
				int t = selection[i];
				str.Append(TableModel.GetEntry(t, 0));
				for (int col = 1; col < TableModel.ColumnCount; ++col){
					str.Append("\t");
					str.Append(TableModel.GetEntry(t, col));
				}
				if (i != selection.Length - 1){
					str.Append("\n");
				}
			}
			Clipboard.Clear();
			Clipboard.SetDataObject(str.ToString());
		}

		protected internal override void OnMouseIsDownMainView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (e.IsMainButton){
				try{
					int row = (VisibleY + e.Y)/rowHeight;
					if (model == null || row >= model.RowCount || row < 0){
						return;
					}
					bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
					bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
					if (!ctrl || !multiSelect){
						ClearSelection();
					}
					int ox = order[row];
					if (ox < 0 || ox >= modelRowSel.Length){
						return;
					}
					modelRowSel[ox] = !ctrl || !modelRowSel[ox];
					if (multiSelect && shift && selectStart != -1){
						SelectRange(selectStart, row);
					} else{
						selectStart = row;
					}
					Invalidate(true);
					if (SelectionChanged != null){
						SelectionChanged(this, new EventArgs());
					}
				} catch (Exception){}
			}
		}

		protected internal override void OnMouseDraggedRowHeaderView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			OnMouseDraggedMainView(e);
		}

		protected internal override void OnMouseDraggedMainView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (!MultiSelect){
				OnMouseIsDownMainView(e);
				return;
			}
			if (e.IsMainButton){
				if (modelRowSel == null){
					return;
				}
				int row = (VisibleY + e.Y)/rowHeight;
				if (row >= modelRowSel.Length || row < 0){
					return;
				}
				modelRowSel[order[row]] = true;
				if (selectStart != row){
					selectStart = row;
					Invalidate(true);
					if (SelectionChanged != null){
						SelectionChanged(this, new EventArgs());
					}
				}
			}
		}

		protected internal override sealed void OnPaintMainView(IGraphics g, int x, int y, int width, int height){
			if (model == null){
				return;
			}
			try{
				CheckSizes();
				g.FillRectangle(Brushes.White, 0, 0, width, height);
				int offset = -y%rowHeight;
				if (columnWidthSums == null || columnWidthSums.Length == 0){
					return;
				}
				for (int y1 = offset; y1 < height; y1 += rowHeight){
					int xmax = Math.Min(width, columnWidthSums[columnWidthSums.Length - 1] - x);
					g.DrawLine(gridPen, 0, y1, xmax, y1);
					int row = (y + y1)/rowHeight;
					if (row < 0){
						continue;
					}
					if (model == null || row >= model.RowCount){
						break;
					}
					bool sel = ViewRowIsSelected(row);
					if (sel){
						g.FillRectangle(selectBgBrush, 0, y1 + 1, xmax, rowHeight - 1);
					} else if (row%2 == 1){
						g.FillRectangle(oddBgBrush, 0, y1 + 1, xmax, rowHeight - 1);
					}
					int startInd = Math.Max(0, ArrayUtils.CeilIndex(columnWidthSums, x) - 1);
					int endInd = Math.Min(columnWidthSums.Length - 1, ArrayUtils.FloorIndex(columnWidthSums, x + width) + 1);
					if (order.Length == 0){
						return;
					}
					if (startInd >= 0 && endInd >= 0){
						for (int i = startInd; i <= endInd; i++){
							if (i >= columnWidthSums.Length){
								continue;
							}
							int x1 = (i > 0 ? columnWidthSums[i - 1] : 0) - x;
							int w;
							if (i == 0){
								w = columnWidthSums[0];
							} else{
								if (i >= columnWidthSums.Length || i - 1 < 0){
									continue;
								}
								w = columnWidthSums[i] - columnWidthSums[i - 1];
							}
							RenderCell(g, sel, order[row], i, w, x1, y1);
						}
					}
				}
				{
					int startInd = ArrayUtils.CeilIndex(columnWidthSums, x);
					int endInd = ArrayUtils.FloorIndex(columnWidthSums, x + width);
					if (startInd >= 0 && endInd >= 0){
						int ymax = Math.Min(height, rowHeight*model.RowCount - y);
						for (int i = startInd; i <= endInd; i++){
							if (i >= columnWidthSums.Length || i < 0){
								continue;
							}
							int x1 = columnWidthSums[i] - x;
							g.DrawLine(gridPen, x1, 0, x1, ymax);
						}
					}
				}
			} catch (Exception){
				//This is an exceptional case where we put an unspecific try/catch block around code. Everything is working fine
				//except for extremely rare index out of bounds crashes, which are probably due to lack of thread safety. This avoilds
				//crashes of the MaxQuant interface during very long running times.
			}
		}

		protected internal override void OnMouseHoverMainView(EventArgs e){
			HandleToolTip(true);
		}

		private void HandleToolTip(bool hover){
			if (currentX < 0 || currentY < 0 || currentRow < 0 || currentCol < 0 || model == null || currentRow >= model.RowCount ||
				currentCol >= model.ColumnCount){
				//TODO
				//mainViewToolTip.Hide(this);
				return;
			}
			if (order.Length != model.RowCount){
				return;
			}
			object o = model.GetEntry(order[currentRow], currentCol);
			if (o == null){
				//TODO
				//mainViewToolTip.Hide(this);
				return;
			}
			string s = o.ToString();
			if (s.Length > 0 && (hover || currentX != toolTipX || currentY != toolTipY)){
				//TODO
				//mainViewToolTip.Show(s, this, currentX, currentY, 5000);
				toolTipX = currentX;
				toolTipY = currentY;
			}
		}

		protected internal override void OnMouseMoveMainView(BasicMouseEventArgs e){
			currentX = e.X;
			currentY = e.Y;
			HandleToolTip(false);
			CalcCurrentRowAndColumn(e);
		}

		protected internal override void OnMouseDoubleClickMainView(BasicMouseEventArgs e){
			if (e.IsMainButton){
				int row = (VisibleY + e.Y)/rowHeight;
				if (model == null || row >= model.RowCount || row < 0){
					return;
				}
			}
			//TODO: edit
		}

		public void CalcCurrentRowAndColumn(BasicMouseEventArgs e){
			int x1 = VisibleX + e.X;
			int y1 = VisibleY + e.Y;
			try{
				int indf = ArrayUtils.CeilIndex(columnWidthSums, x1);
				if (model != null){
					if (indf >= 0){
						currentCol = indf;
					} else{
						currentCol = -1;
					}
				}
			} catch (Exception){}
			currentRow = (y1)/rowHeight;
		}

		protected internal override void OnMouseIsDownRowHeaderView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			OnMouseIsDownMainView(e);
		}

		protected internal override void OnMouseMoveColumnHeaderView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			int x1 = VisibleX + e.X;
			if (columnWidthSums == null){
				return;
			}
			int ind = ArrayUtils.ClosestIndex(columnWidthSums, x1);
			if (ind >= 0){
				if (Math.Abs(columnWidthSums[ind] - x1) < 5){
					Cursor.Current = Cursors.VSplit;
					resizeCol = ind;
				} else{
					Cursor.Current = Cursors.Default;
					resizeCol = -1;
				}
			}
			int indf = ArrayUtils.CeilIndex(columnWidthSums, x1);
			try{
				if (model != null){
					if (indf >= 0 && !string.IsNullOrEmpty(model.GetColumnDescription(indf)) &&
						Math.Abs((indf == 0 ? 0 : columnWidthSums[indf - 1]) + 9 - x1) < 4 && e.Y > 7 && e.Y < 17){
						if (helpCol != indf){
							helpCol = indf;
							InvalidateColumnHeaderView();
						}
					} else{
						if (helpCol != -1){
							helpCol = -1;
							InvalidateColumnHeaderView();
						}
					}
				}
			} catch (Exception){}
		}

		protected internal override void OnMouseMoveCornerView(BasicMouseEventArgs e){
			if (model == null){
				return;
			}
			int x1 = e.X;
			if (!string.IsNullOrEmpty(model.Description) && Math.Abs(9 - x1) < 4 && e.Y > 7 && e.Y < 17){
				if (!matrixHelp){
					matrixHelp = true;
					InvalidateCornerView();
				} else{
					matrixHelp = false;
					InvalidateCornerView();
				}
			}
		}

		protected internal override void OnMouseIsUpColumnHeaderView(BasicMouseEventArgs e){
			//TODO
			columnViewToolTip.Hide(this);
		}

		protected internal override void OnMouseIsUpCornerView(BasicMouseEventArgs e){
			//TODO
			columnViewToolTip.Hide(this);
		}

		protected internal override void OnMouseIsDownColumnHeaderView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (columnWidthSums == null){
				return;
			}
			if (resizeCol != -1){
				colDragX = e.X;
				columnWidthSumsOld = (int[]) columnWidthSums.Clone();
				return;
			}
			if (helpCol >= 0){
				//TODO
				columnViewToolTip.ToolTipTitle = model.GetColumnName(helpCol);
				StringBuilder text = new StringBuilder();
				string[] wrapped = StringUtils.Wrap(model.GetColumnDescription(helpCol), 75);
				for (int i = 0; i < wrapped.Length; ++i) {
					string s = wrapped[i];
					text.Append(s);
					if (i < wrapped.Length - 1) {
						text.Append("\n");
					}
				}
				columnViewToolTip.Show(text.ToString(), this, e.X + 75, e.Y + 5);
				helpCol = -1;
				InvalidateColumnHeaderView();
				return;
			}
			if (Sortable){
				int ind = Math.Min(columnWidthSums.Length - 1, ArrayUtils.FloorIndex(columnWidthSums, VisibleX + e.X) + 1);
				if (sortCol == ind){
					switch (sortState){
						case SortState.Unsorted:
							Sort();
							break;
						case SortState.Increasing:
							InvertOrder();
							break;
						case SortState.Decreasing:
							Unsort();
							break;
					}
				} else{
					sortCol = ind;
					Sort();
				}
			}
			Invalidate(true);
		}

		protected internal override void OnMouseIsDownCornerView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (matrixHelp){
				//TODO
				columnViewToolTip.ToolTipTitle = model.Name;
				StringBuilder text = new StringBuilder();
				string[] wrapped = StringUtils.Wrap(model.Description, 75);
				for (int i = 0; i < wrapped.Length; ++i) {
					string s = wrapped[i];
					text.Append(s);
					if (i < wrapped.Length - 1) {
						text.Append("\n");
					}
				}
				columnViewToolTip.Show(text.ToString(), this, e.X + 75, e.Y + 5);
				matrixHelp = false;
				InvalidateCornerView();
				return;
			}
			Invalidate(true);
		}

		protected internal override void OnMouseDraggedColumnHeaderView(BasicMouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (resizeCol != -1){
				int mind = resizeCol == 0
					? 6 - columnWidthSumsOld[0] : columnWidthSumsOld[resizeCol - 1] - columnWidthSumsOld[resizeCol] + 6;
				deltaDragX = Math.Max(mind, e.X - colDragX);
				for (int i = resizeCol; i < columnWidthSums.Length; i++){
					columnWidthSums[i] = columnWidthSumsOld[i] + deltaDragX;
				}
				Invalidate(true);
			}
		}

		protected override void Dispose(bool disposing){
			base.Dispose(disposing);
			if (findForm != null){
				findForm.Close();
				findForm.Dispose();
			}
			if (filterForm != null){
				filterForm.Close();
				filterForm.Dispose();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
			switch (keyData){
				case Keys.Shift | Keys.Home:
				case Keys.Home:{
					VisibleX = 0;
					Invalidate(true);
				}
					break;
				case Keys.Shift | Keys.End:
				case Keys.End:{
					ScrollToEnd();
					Invalidate(true);
				}
					break;
				case Keys.Shift | Keys.Down:
				case Keys.Down:{
					int[] selection = GetSelectedRowsView();
					if (selection != null && selection.Length != 0 && selection[selection.Length - 1] < RowCount - 1){
						if (keyData == Keys.Down){
							modelRowSel[order[selection[selection.Length - 1]]] = false;
						}
						SetSetectedViewIndex(selection[selection.Length - 1] + 1);
						MoveDown(rowHeight);
						Invalidate(true);
					}
				}
					break;
				case Keys.Shift | Keys.Up:
				case Keys.Up:{
					int[] selection = GetSelectedRowsView();
					if (selection != null && selection.Length != 0 && selection[0] > 0){
						if (keyData == Keys.Up){
							modelRowSel[order[selection[0]]] = false;
						}
						SetSetectedViewIndex(selection[0] - 1);
						MoveUp(rowHeight);
						Invalidate(true);
					}
				}
					break;
				case Keys.Control | Keys.A:
					SelectAll();
					Invalidate(true);
					break;
				case Keys.Control | Keys.C:
					Copy();
					break;
				case Keys.Control | Keys.F:
					Find();
					break;
				case Keys.Control | Keys.G:
					Goto();
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}