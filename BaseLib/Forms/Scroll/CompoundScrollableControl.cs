using System;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	public class CompoundScrollableControl : UserControl, IScrollableControl{
		public const int scrollBarWidth = 18;
		private int rowHeaderWidth = 40;
		private int rowFooterWidth;
		protected int columnHeaderHeight = 40;
		protected int columnFooterHeight;
		private int visibleX;
		private int visibleY;
		private BasicTableLayoutView tableLayoutPanel1;
		private BasicControl tableLayoutControl;
		private BasicTableLayoutView tableLayoutPanel2;
		private BasicView horizontalScrollBarView;
		private BasicView verticalScrollBarView;
		private BasicView mainView;
		private BasicView rowHeaderView;
		private BasicView rowFooterView;
		private BasicView rowSpacerView;
		private BasicView columnHeaderView;
		private BasicView columnFooterView;
		private BasicView columnSpacerView;
		private BasicView cornerView;
		private BasicView smallCornerView;
		private BasicView middleCornerView;
		public Action<BasicMouseEventArgs> OnMouseClickMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedMainView { get; set; }
		public Action<EventArgs> OnMouseHoverMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpMainView { get; set; }
		public Action<EventArgs> OnMouseLeaveMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickRowHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickRowHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedRowHeaderView { get; set; }
		public Action<EventArgs> OnMouseHoverRowHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownRowHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpRowHeaderView { get; set; }
		public Action<EventArgs> OnMouseLeaveRowHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveRowHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickRowFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickRowFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedRowFooterView { get; set; }
		public Action<EventArgs> OnMouseHoverRowFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownRowFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpRowFooterView { get; set; }
		public Action<EventArgs> OnMouseLeaveRowFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveRowFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickRowSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickRowSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedRowSpacerView { get; set; }
		public Action<EventArgs> OnMouseHoverRowSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownRowSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpRowSpacerView { get; set; }
		public Action<EventArgs> OnMouseLeaveRowSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveRowSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickColumnHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickColumnHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedColumnHeaderView { get; set; }
		public Action<EventArgs> OnMouseHoverColumnHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownColumnHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpColumnHeaderView { get; set; }
		public Action<EventArgs> OnMouseLeaveColumnHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveColumnHeaderView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickColumnFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickColumnFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedColumnFooterView { get; set; }
		public Action<EventArgs> OnMouseHoverColumnFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownColumnFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpColumnFooterView { get; set; }
		public Action<EventArgs> OnMouseLeaveColumnFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveColumnFooterView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickColumnSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickColumnSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedColumnSpacerView { get; set; }
		public Action<EventArgs> OnMouseHoverColumnSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownColumnSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpColumnSpacerView { get; set; }
		public Action<EventArgs> OnMouseLeaveColumnSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveColumnSpacerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedCornerView { get; set; }
		public Action<EventArgs> OnMouseHoverCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpCornerView { get; set; }
		public Action<EventArgs> OnMouseLeaveCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickMiddleCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickMiddleCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedMiddleCornerView { get; set; }
		public Action<EventArgs> OnMouseHoverMiddleCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownMiddleCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpMiddleCornerView { get; set; }
		public Action<EventArgs> OnMouseLeaveMiddleCornerView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveMiddleCornerView { get; set; }
		public Action<IGraphics, int, int, int, int> OnPaintMainView { get; set; }
		public Action<IGraphics, int, int> OnPaintRowHeaderView { get; set; }
		public Action<IGraphics, int, int> OnPaintRowFooterView { get; set; }
		public Action<IGraphics, int, int> OnPaintColumnHeaderView { get; set; }
		public Action<IGraphics, int, int> OnPaintColumnFooterView { get; set; }
		public Action<IGraphics> OnPaintColumnSpacerView { get; set; }
		public Action<IGraphics> OnPaintRowSpacerView { get; set; }
		public Action<IGraphics> OnPaintCornerView { get; set; }
		public Action<IGraphics> OnPaintMiddleCornerView { get; set; }

		protected CompoundScrollableControl(){
			InitializeComponent2();
			ResizeRedraw = true;
			//OnPaintMainView = (g, x, y, width, height) => { g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, VisibleHeight); };
			//OnPaintRowHeaderView = (g, y, height) => { g.FillRectangle(Brushes.White, 0, 0, RowHeaderWidth, VisibleHeight); };
			//OnPaintRowFooterView = (g, y, height) => { g.FillRectangle(Brushes.White, 0, 0, RowFooterWidth, VisibleHeight); };
			//OnPaintColumnHeaderView =
			//	(g, x, width) => { g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, GetColumnHeaderHeight()); };
			//OnPaintColumnFooterView =
			//	(g, x, width) => { g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, ColumnFooterHeight); };
			//OnPaintColumnSpacerView = g => { g.FillRectangle(Brushes.White, 0, 0, RowFooterWidth, GetColumnHeaderHeight()); };
			//OnPaintRowSpacerView = g => { g.FillRectangle(Brushes.White, 0, 0, RowHeaderWidth, ColumnFooterHeight); };
			//OnPaintCornerView = g => { g.FillRectangle(Brushes.White, 0, 0, RowHeaderWidth, GetColumnHeaderHeight()); };
			//OnPaintMiddleCornerView = g => { g.FillRectangle(Brushes.White, 0, 0, RowFooterWidth, ColumnFooterHeight); };
			OnPaintMainView = (g, x, y, width, height) => { };
			OnPaintRowHeaderView = (g, y, height) => { };
			OnPaintRowFooterView = (g, y, height) => { };
			OnPaintColumnHeaderView = (g, x, width) => { };
			OnPaintColumnFooterView = (g, x, width) => { };
			OnPaintColumnSpacerView = g => { };
			OnPaintRowSpacerView = g => { };
			OnPaintCornerView = g => { };
			OnPaintMiddleCornerView = g => { };
			TotalWidth = () => 200;
			TotalHeight = () => 200;
			DeltaX = () => (Width - RowHeaderWidth)/20;
			DeltaY = () => (Height - GetColumnHeaderHeight())/20;
			DeltaUpToSelection = () => 0;
			DeltaDownToSelection = () => 0;
		}

		public virtual void InvalidateBackgroundImages(){}

		public void InvalidateScrollbars(){
			horizontalScrollBarView.Invalidate();
			verticalScrollBarView.Invalidate();
		}

		public void EnableContent(){
			mainView.Enabled = true;
			rowHeaderView.Enabled = true;
			rowFooterView.Enabled = true;
			rowSpacerView.Enabled = true;
			columnHeaderView.Enabled = true;
			columnFooterView.Enabled = true;
			columnSpacerView.Enabled = true;
			cornerView.Enabled = true;
			middleCornerView.Enabled = true;
		}

		public void DisableContent(){
			mainView.Enabled = false;
			rowHeaderView.Enabled = false;
			rowFooterView.Enabled = false;
			rowSpacerView.Enabled = false;
			columnHeaderView.Enabled = false;
			columnFooterView.Enabled = false;
			columnSpacerView.Enabled = false;
			cornerView.Enabled = false;
			middleCornerView.Enabled = false;
		}

		public int VisibleX{
			get { return visibleX; }
			set{
				visibleX = value;
				InvalidateBackgroundImages();
				mainView.Invalidate();
				columnHeaderView.Invalidate();
				columnFooterView.Invalidate();
				horizontalScrollBarView.Invalidate();
			}
		}

		public int VisibleY{
			get { return visibleY; }
			set{
				visibleY = value;
				InvalidateBackgroundImages();
				mainView.Invalidate();
				rowHeaderView.Invalidate();
				rowFooterView.Invalidate();
				verticalScrollBarView.Invalidate();
			}
		}

		protected void InvalidateColumnHeaderView(){
			columnHeaderView.Invalidate();
			horizontalScrollBarView.Invalidate();
		}

		protected void InvalidateCornerView(){
			cornerView.Invalidate();
		}

		protected void InvalidateRowHeaderView(){
			rowHeaderView.Invalidate();
			verticalScrollBarView.Invalidate();
		}

		public void InvalidateMainView(){
			mainView.Invalidate();
		}

		public int RowHeaderWidth{
			get { return rowHeaderWidth; }
			set{
				rowHeaderWidth = value;
				tableLayoutPanel2.ColumnStyles[0] = new BasicColumnStyle(BasicSizeType.AbsoluteResizeable, value);
			}
		}

		public int RowFooterWidth{
			get { return rowFooterWidth; }
			set{
				rowFooterWidth = value;
				tableLayoutPanel2.ColumnStyles[2] = new BasicColumnStyle(BasicSizeType.AbsoluteResizeable, value);
			}
		}

		public int GetColumnHeaderHeight(){
			return columnHeaderHeight;
		}

		public virtual void SetColumnHeaderHeight(int value){
			columnHeaderHeight = value;
			tableLayoutPanel2.RowStyles[0] = new BasicRowStyle(BasicSizeType.AbsoluteResizeable, value);
		}

		public int ColumnFooterHeight{
			get { return columnFooterHeight; }
			set{
				columnFooterHeight = value;
				tableLayoutPanel2.RowStyles[2] = new BasicRowStyle(BasicSizeType.AbsoluteResizeable, value);
			}
		}

		public Func<int> TotalWidth { get; set; }
		public Func<int> TotalHeight { get; set; }
		public Func<int> DeltaX { get; set; }
		public Func<int> DeltaY { get; set; }
		public Func<int> DeltaUpToSelection { get; set; }
		public Func<int> DeltaDownToSelection { get; set; }
		public int ClientWidth => Width - scrollBarWidth;
		public int ClientHeight => Height - scrollBarWidth;
		public int VisibleWidth => Width - RowHeaderWidth - RowFooterWidth - scrollBarWidth;
		public int VisibleHeight => Height - GetColumnHeaderHeight() - ColumnFooterHeight - scrollBarWidth;
		public int TotalClientWidth => TotalWidth() + RowHeaderWidth + RowFooterWidth;
		public int TotalClientHeight => TotalHeight() + GetColumnHeaderHeight() + ColumnFooterHeight;

		protected override void OnResize(EventArgs e){
			if (TotalWidth == null || TotalHeight == null){
				return;
			}
			VisibleX = Math.Max(0, Math.Min(VisibleX, TotalWidth() - VisibleWidth - 1));
			VisibleY = Math.Max(0, Math.Min(VisibleY, TotalHeight() - VisibleHeight - 1));
			base.OnResize(e);
		}

		public void MoveUp(int delta){
			if (TotalHeight() <= VisibleHeight){
				return;
			}
			VisibleY = Math.Max(0, VisibleY - delta);
		}

		public void MoveDown(int delta){
			if (TotalHeight() <= VisibleHeight){
				return;
			}
			VisibleY = Math.Min(TotalHeight() - VisibleHeight, VisibleY + delta);
		}

		private void InitializeComponent2(){
			tableLayoutPanel1 = new BasicTableLayoutView();
			tableLayoutPanel2 = new BasicTableLayoutView();
			mainView = new ScrollableControlMainView(this);
			rowHeaderView = new ScrollableControlRowHeaderView(this);
			rowFooterView = new ScrollableControlRowFooterView(this);
			rowSpacerView = new ScrollableControlRowSpacerView(this);
			columnHeaderView = new ScrollableControlColumnHeaderView(this);
			columnFooterView = new ScrollableControlColumnFooterView(this);
			columnSpacerView = new ScrollableControlColumnSpacerView(this);
			horizontalScrollBarView = new HorizontalScrollBarView(this);
			verticalScrollBarView = new VerticalScrollBarView(this);
			cornerView = new ScrollableControlCornerView(this);
			middleCornerView = new ScrollableControlMiddleCornerView(this);
			smallCornerView = new ScrollableControlSmallCornerView();
			SuspendLayout();
			tableLayoutPanel1.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.Absolute, scrollBarWidth));
			tableLayoutPanel1.Add(tableLayoutPanel2, 0, 0);
			tableLayoutPanel1.Add(horizontalScrollBarView, 0, 1);
			tableLayoutPanel1.Add(verticalScrollBarView, 1, 0);
			tableLayoutPanel1.Add(smallCornerView, 1, 1);
			tableLayoutPanel1.RowStyles.Add(new BasicRowStyle(BasicSizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new BasicRowStyle(BasicSizeType.Absolute, scrollBarWidth));
			tableLayoutPanel2.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.AbsoluteResizeable, rowHeaderWidth));
			tableLayoutPanel2.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.Percent, 100F));
			tableLayoutPanel2.ColumnStyles.Add(new BasicColumnStyle(BasicSizeType.AbsoluteResizeable, rowFooterWidth));
			tableLayoutPanel2.Add(mainView, 1, 1);
			tableLayoutPanel2.Add(rowHeaderView, 0, 1);
			tableLayoutPanel2.Add(rowFooterView, 2, 1);
			tableLayoutPanel2.Add(rowSpacerView, 0, 2);
			tableLayoutPanel2.Add(columnHeaderView, 1, 0);
			tableLayoutPanel2.Add(columnFooterView, 1, 2);
			tableLayoutPanel2.Add(columnSpacerView, 2, 0);
			tableLayoutPanel2.Add(cornerView, 0, 0);
			tableLayoutPanel2.Add(middleCornerView, 2, 2);
			tableLayoutPanel2.RowStyles.Add(new BasicRowStyle(BasicSizeType.AbsoluteResizeable, columnHeaderHeight));
			tableLayoutPanel2.RowStyles.Add(new BasicRowStyle(BasicSizeType.Percent, 100F));
			tableLayoutPanel2.RowStyles.Add(new BasicRowStyle(BasicSizeType.AbsoluteResizeable, columnFooterHeight));
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			tableLayoutControl = tableLayoutPanel1.CreateControl();
			Controls.Add(tableLayoutControl);
			Name = "ScrollableControl2";
			Size = new Size(409, 390);
			ResumeLayout(false);
		}

		protected override void OnMouseWheel(MouseEventArgs e){
			if (TotalHeight() <= VisibleHeight){
				return;
			}
			VisibleY = Math.Min(Math.Max(0, VisibleY - (int) Math.Round(VisibleHeight*0.001*e.Delta)),
				TotalHeight() - VisibleHeight);
			verticalScrollBarView.Invalidate();
			base.OnMouseWheel(e);
		}

		public void Print(IGraphics g, int width, int height){
			tableLayoutPanel2.InvalidateSizes();
			tableLayoutPanel2.Print(g, width, height);
			tableLayoutPanel2.InvalidateSizes();
		}
	}
}