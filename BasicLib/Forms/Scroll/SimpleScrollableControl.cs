using System;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms.Base;
using BasicLib.Graphic;

namespace BasicLib.Forms.Scroll{
	public class SimpleScrollableControl : UserControl, IScrollableControl, IPrintable{
		private int visibleX;
		private int visibleY;
		private BasicView horizontalScrollBar;
		private BasicView verticalScrollBar;
		private BasicControl mainControl;
		private BasicView mainView;
		private BasicView smallCornerView;

		protected SimpleScrollableControl(){
			InitializeComponent2();
			ResizeRedraw = true;
		}

		public virtual void InvalidateBackgroundImages() {}

		public void InvalidateScrollbars(){
			horizontalScrollBar.Invalidate();
			verticalScrollBar.Invalidate();
		}

		public void EnableContent(){
			mainView.Enabled = true;
		}

		public void DisableContent(){
			mainView.Enabled = false;
		}

		public int VisibleX{
			get { return visibleX; }
			set{
				visibleX = value;
				InvalidateBackgroundImages();
				mainView.Invalidate();
				horizontalScrollBar.Invalidate();
			}
		}
		public int VisibleY{
			get { return visibleY; }
			set{
				visibleY = value;
				InvalidateBackgroundImages();
				mainView.Invalidate();
				verticalScrollBar.Invalidate();
			}
		}

		public void InvalidateMainView(){
			mainView.Invalidate();
		}

		public virtual int TotalWidth { get { return 200; } }
		public virtual int TotalHeight { get { return 200; } }
		public virtual int DeltaX { get { return (Width)/20; } }
		public virtual int DeltaY { get { return (Height)/20; } }
		public int VisibleWidth { get { return mainControl.Width; } }
		public int VisibleHeight { get { return mainControl.Height; } }

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
			TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
			mainView = new SimpleScrollableControlMainView(this);
			horizontalScrollBar = new HorizontalScrollBarView(this);
			verticalScrollBar = new VerticalScrollBarView(this);
			smallCornerView = new ScrollableControlSmallCornerView();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CompoundScrollableControl.scrollBarWidth));
			mainControl = mainView.CreateControl();
			tableLayoutPanel1.Controls.Add(mainControl, 0, 0);
			tableLayoutPanel1.Controls.Add(horizontalScrollBar.CreateControl(), 0, 1);
			tableLayoutPanel1.Controls.Add(verticalScrollBar.CreateControl(), 1, 0);
			tableLayoutPanel1.Controls.Add(smallCornerView.CreateControl(), 1, 1);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Margin = new Padding(0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, CompoundScrollableControl.scrollBarWidth));
			tableLayoutPanel1.Size = new Size(409, 390);
			tableLayoutPanel1.TabIndex = 0;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel1);
			Name = "ScrollableControl2";
			Size = new Size(409, 390);
			tableLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		protected override void OnMouseWheel(MouseEventArgs e){
			if (TotalHeight <= VisibleHeight){
				return;
			}
			VisibleY = Math.Min(Math.Max(0, VisibleY - (int) Math.Round(VisibleHeight*0.001*e.Delta)),
				TotalHeight - VisibleHeight);
			verticalScrollBar.Invalidate();
			base.OnMouseWheel(e);
		}

		protected internal virtual void OnPaintMainView(IGraphics g, int x, int y, int width, int height){
			g.FillRectangle(Brushes.White, 0, 0, VisibleWidth, VisibleHeight);
		}

		protected internal virtual void OnMouseClickMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDoubleClickMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseDraggedMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseHoverMainView(EventArgs e) {}
		protected internal virtual void OnMouseIsDownMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseIsUpMainView(BasicMouseEventArgs e) {}
		protected internal virtual void OnMouseLeaveMainView(EventArgs e) {}
		protected internal virtual void OnMouseMoveMainView(BasicMouseEventArgs e) {}

		public virtual int DeltaUpToSelection(){
			return 0;
		}

		public virtual int DeltaDownToSelection(){
			return 0;
		}

		public void Print(IGraphics g, int width, int height){
			mainView.Print(g, width, height);
		}
	}
}