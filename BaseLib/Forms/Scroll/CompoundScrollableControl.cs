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

		protected CompoundScrollableControl(){
			InitializeComponent2();
			ResizeRedraw = true;
		}

		public virtual void InvalidateBackgroundImages() {}

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
		public virtual int ColumnHeaderHeight{
			get { return columnHeaderHeight; }
			set{
				columnHeaderHeight = value;
				tableLayoutPanel2.RowStyles[0] = new BasicRowStyle(BasicSizeType.AbsoluteResizeable, value);
			}
		}
		public virtual int ColumnFooterHeight{
			get { return columnFooterHeight; }
			set{
				columnFooterHeight = value;
				tableLayoutPanel2.RowStyles[2] = new BasicRowStyle(BasicSizeType.AbsoluteResizeable, value);
			}
		}
		public virtual int TotalWidth => 200;
		public virtual int TotalHeight => 200;
		public int ClientWidth => Width - scrollBarWidth;
		public int ClientHeight => Height - scrollBarWidth;
		public virtual int DeltaX => (Width - RowHeaderWidth)/20;
		public virtual int DeltaY => (Height - ColumnHeaderHeight)/20;
		public int VisibleWidth => Width - RowHeaderWidth - RowFooterWidth - scrollBarWidth;
		public int VisibleHeight => Height - ColumnHeaderHeight - ColumnFooterHeight - scrollBarWidth;
		public int TotalClientWidth => TotalWidth + RowHeaderWidth + RowFooterWidth;
		public int TotalClientHeight => TotalHeight + ColumnHeaderHeight + ColumnFooterHeight;

		protected override void OnResize(EventArgs e){
			VisibleX = Math.Max(0, Math.Min(VisibleX, TotalWidth - VisibleWidth - 1));
			VisibleY = Math.Max(0, Math.Min(VisibleY, TotalHeight - VisibleHeight - 1));
			base.OnResize(e);
		}

		public void MoveUp(int delta){
			if (TotalHeight <= VisibleHeight){
				return;
			}
			VisibleY = Math.Max(0, VisibleY - delta);
		}

		public void MoveDown(int delta){
			if (TotalHeight <= VisibleHeight){
				return;
			}
			VisibleY = Math.Min(TotalHeight - VisibleHeight, VisibleY + delta);
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
			if (TotalHeight <= VisibleHeight){
				return;
			}
			VisibleY = Math.Min(Math.Max(0, VisibleY - (int) Math.Round(VisibleHeight*0.001*e.Delta)),
				TotalHeight - VisibleHeight);
			verticalScrollBarView.Invalidate();
			base.OnMouseWheel(e);
		}

		protected internal virtual void OnPaintMainView(IGraphics g, int x, int y, int width, int height){
			g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, VisibleHeight);
		}

		protected internal virtual void OnPaintRowHeaderView(IGraphics g, int y, int height){
			g.FillRectangle(Brushes.White, 0, 0, RowHeaderWidth, VisibleHeight);
		}

		protected internal virtual void OnPaintRowFooterView(IGraphics g, int y, int height){
			g.FillRectangle(Brushes.White, 0, 0, RowFooterWidth, VisibleHeight);
		}

		protected internal virtual void OnPaintColumnHeaderView(IGraphics g, int x, int width){
			g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, ColumnHeaderHeight);
		}

		protected internal virtual void OnPaintColumnFooterView(IGraphics g, int x, int width){
			g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, ColumnFooterHeight);
		}

		protected internal virtual void OnPaintColumnSpacerView(IGraphics g){
			g.FillRectangle(Brushes.White, 0, 0, RowFooterWidth, ColumnHeaderHeight);
		}

		protected internal virtual void OnPaintRowSpacerView(IGraphics g){
			g.FillRectangle(Brushes.White, 0, 0, RowHeaderWidth, ColumnFooterHeight);
		}

		protected internal virtual void OnPaintCornerView(IGraphics g){
			g.FillRectangle(Brushes.White, 0, 0, RowHeaderWidth, ColumnHeaderHeight);
		}

		protected internal virtual void OnPaintMiddleCornerView(IGraphics g){
			g.FillRectangle(Brushes.White, 0, 0, RowFooterWidth, ColumnFooterHeight);
		}

		protected internal virtual void OnMouseClickMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverMainView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveMainView(EventArgs e) {}
		protected internal virtual void OnMouseMoveMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickRowHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickRowHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedRowHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverRowHeaderView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownRowHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpRowHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveRowHeaderView(EventArgs e) {}
		protected internal virtual void OnMouseMoveRowHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickRowFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickRowFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedRowFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverRowFooterView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownRowFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpRowFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveRowFooterView(EventArgs e) {}
		protected internal virtual void OnMouseMoveRowFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickRowSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickRowSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedRowSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverRowSpacerView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownRowSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpRowSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveRowSpacerView(EventArgs e) {}
		protected internal virtual void OnMouseMoveRowSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickColumnHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickColumnHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedColumnHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverColumnHeaderView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownColumnHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpColumnHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveColumnHeaderView(EventArgs e) {}
		protected internal virtual void OnMouseMoveColumnHeaderView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickColumnFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickColumnFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedColumnFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverColumnFooterView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownColumnFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpColumnFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveColumnFooterView(EventArgs e) {}
		protected internal virtual void OnMouseMoveColumnFooterView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickColumnSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickColumnSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedColumnSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverColumnSpacerView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownColumnSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpColumnSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveColumnSpacerView(EventArgs e) {}
		protected internal virtual void OnMouseMoveColumnSpacerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverCornerView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveCornerView(EventArgs e) {}
		protected internal virtual void OnMouseMoveCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseClickMiddleCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickMiddleCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedMiddleCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverMiddleCornerView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownMiddleCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpMiddleCornerView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveMiddleCornerView(EventArgs e) {}
		protected internal virtual void OnMouseMoveMiddleCornerView(BasicMouseEventArgs e) {}

		public virtual int DeltaUpToSelection(){
			return 0;
		}

		public virtual int DeltaDownToSelection(){
			return 0;
		}

		public void Print(IGraphics g, int width, int height){
			tableLayoutPanel2.InvalidateSizes();
			tableLayoutPanel2.Print(g, width, height);
			tableLayoutPanel2.InvalidateSizes();
		}
	}
}