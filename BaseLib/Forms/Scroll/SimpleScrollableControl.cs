using System;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Forms.Base;
using BaseLib.Graphic;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	public sealed class SimpleScrollableControl : UserControl, ISimpleScrollableControl{
		private int visibleX;
		private int visibleY;
		private BasicView horizontalScrollBar;
		private BasicView verticalScrollBar;
		private BasicControl mainControl;
		private BasicView mainView;
		private BasicView smallCornerView;
		public Action<IGraphics, int, int, int, int> OnPaintMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseClickMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDoubleClickMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseDraggedMainView { get; set; }
		public Action<EventArgs> OnMouseHoverMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsDownMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseIsUpMainView { get; set; }
		public Action<EventArgs> OnMouseLeaveMainView { get; set; }
		public Action<BasicMouseEventArgs> OnMouseMoveMainView { get; set; }

		public SimpleScrollableControl(){
			InitializeComponent2();
			ResizeRedraw = true;
			DoubleBuffered = true;
			OnPaintMainView = (g, x, y, width, height) => { };
			TotalWidth = () => 200;
			TotalHeight = () => 200;
			DeltaX = () => Width/20;
			DeltaY = () => Height/20;
			DeltaUpToSelection = () => 0;
			DeltaDownToSelection = () => 0;
		}

		public void InvalidateBackgroundImages(){
			client?.InvalidateBackgroundImages();
		}

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

		public int Width1 => Width;
		public int Height1 => Height;
		public Func<int> TotalWidth { get; set; }
		public Func<int> TotalHeight { get; set; }
		public Func<int> DeltaX { get; set; }
		public Func<int> DeltaY { get; set; }
		public Func<int> DeltaUpToSelection { get; set; }
		public Func<int> DeltaDownToSelection { get; set; }
		public int TotalClientWidth => TotalWidth();
		public int TotalClientHeight => TotalHeight();
		public int VisibleWidth => mainControl.Width;
		public int VisibleHeight => mainControl.Height;
		private ISimpleScrollableControlModel client;

		public ISimpleScrollableControlModel Client{
			set{
				client = value;
				value.Register(this);
			}
		}

		protected override void OnResize(EventArgs e){
			if (TotalWidth == null || TotalHeight == null){
				return;
			}
			VisibleX = Math.Max(0, Math.Min(VisibleX, TotalWidth() - VisibleWidth - 1));
			VisibleY = Math.Max(0, Math.Min(VisibleY, TotalHeight() - VisibleHeight - 1));
			InvalidateBackgroundImages();
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
			TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
			mainView = new SimpleScrollableControlMainView(this);
			horizontalScrollBar = new HorizontalScrollBarView(this);
			verticalScrollBar = new VerticalScrollBarView(this);
			smallCornerView = new ScrollableControlSmallCornerView();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, GraphUtil.scrollBarWidth));
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
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, GraphUtil.scrollBarWidth));
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
			if (TotalHeight() <= VisibleHeight){
				return;
			}
			VisibleY = Math.Min(Math.Max(0, VisibleY - (int) Math.Round(VisibleHeight*0.001*e.Delta)),
				TotalHeight() - VisibleHeight);
			verticalScrollBar.Invalidate();
			base.OnMouseWheel(e);
		}

		public void Print(IGraphics g, int width, int height){
			mainView.Print(g, width, height);
		}

		public void ExportGraphic(string name, bool showDialog){
			ExportGraphics.ExportGraphic(this, name, showDialog);
		}

		public Tuple<int, int> GetOrigin(){
			Point q = PointToScreen(new Point(0, 0));
			return new Tuple<int, int>(q.X, q.Y);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData){
			client?.ProcessCmdKey((Keys2)keyData);
			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override void OnSizeChanged(EventArgs e){
			base.OnSizeChanged(e);
			client?.OnSizeChanged();
		}
	}
}