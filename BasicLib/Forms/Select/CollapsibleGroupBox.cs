using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Graphic;
using BasicLib.Properties;

namespace BasicLib.Forms.Select{
	/// <summary>
	/// GroupBox control that provides functionality to 
	/// allow it to be collapsed.
	/// </summary>
	[ToolboxBitmap(typeof (CollapsibleGroupBox))]
	public partial class CollapsibleGroupBox : GroupBox{
		private Rectangle mToggleRect = new Rectangle(8, 2, 11, 11);
		private Boolean mCollapsed;
		private Boolean mBResizingFromCollapse;
		private const int mCollapsedHeight = 20;
		private Size mFullSize = Size.Empty;
		public Size FullSize { get { return mFullSize; } set { mFullSize = value; } }

		/// <summary>Fired when the Collapse Toggle button is pressed</summary>
		public delegate void CollapseBoxClickedEventHandler(object sender, EventArgs e);

		public event CollapseBoxClickedEventHandler CollapseBoxClickedEvent;

		public CollapsibleGroupBox(){
			InitializeComponent();
			ResizeRedraw = true;
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int FullHeight { get { return mFullSize.Height; } }
		[DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsCollapsed{
			get { return mCollapsed; }
			set{
				if (value != mCollapsed){
					mCollapsed = value;
					if (!value){
						// Expand
						Size = mFullSize;
					} else{
						// Collapse
						mBResizingFromCollapse = true;
						Height = mCollapsedHeight;
						mBResizingFromCollapse = false;
					}
					foreach (Control c in Controls){
						c.Visible = !value;
					}
					Invalidate();
				}
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CollapsedHeight { get { return mCollapsedHeight; } }

		protected override void OnMouseUp(MouseEventArgs e){
			if (mToggleRect.Contains(e.Location)){
				ToggleCollapsed();
			} else{
				base.OnMouseUp(e);
			}
		}

		protected override void OnPaint(PaintEventArgs e){
			HandleResize();
			DoPaintBackground(new CGraphics(e.Graphics));
			DoPaint(new CGraphics(e.Graphics));
		}

		public void DoPaint(IGraphics g){
			DrawGroupBox(g);
			DrawToggleButton(g);
		}

		public void DoPaintBackground(IGraphics g){
			g.FillRectangle(new SolidBrush(BackColor), Location.X, Location.Y, Width, Height);
		}

		private void DrawGroupBox(IGraphics g){
			// Get windows to draw the GroupBox
			Rectangle bounds = new Rectangle(ClientRectangle.X + Margin.Left, ClientRectangle.Y + Margin.Top + Padding.Top,
				ClientRectangle.Width - Margin.Left - Margin.Right,
				ClientRectangle.Height - Margin.Top - Margin.Bottom - Padding.Top);
			g.DrawRectangle(BackColor != SystemColors.Control ? SystemPens.Control : new Pen(Color.Black), bounds.X, bounds.Y,
				bounds.Width, bounds.Height, 5, RectangleCorners.All);
			// Text Formating positioning & Size
			int iTextPos = (bounds.X + 8) + mToggleRect.Width + 2;
			int iTextSize = (int) g.MeasureString(Text, Font).Width;
			iTextSize = iTextSize < 1 ? 1 : iTextSize;
			int iEndPos = iTextPos + iTextSize + 1;
			// Draw a line to cover the GroupBox border where the text will sit
			if (g is CGraphics){
				g.DrawLine(SystemPens.Control, iTextPos, bounds.Y, iEndPos, bounds.Y);
			}
			// Draw the GroupBox text
			using (SolidBrush drawBrush = new SolidBrush(Color.FromArgb(0, 70, 213))){
				g.DrawString(Text, Font, drawBrush, iTextPos, bounds.Y - (Font.Height*0.5f));
			}
		}

		private void DrawToggleButton(IGraphics g){
			if (g is CGraphics){
				g.DrawImage(IsCollapsed ? Resources.plus : Resources.minus, mToggleRect);
			}
		}

		private void ToggleCollapsed(){
			IsCollapsed = !IsCollapsed;
			if (CollapseBoxClickedEvent != null){
				CollapseBoxClickedEvent(this, new EventArgs());
			}
		}

		private void HandleResize(){
			if (!mBResizingFromCollapse && !mCollapsed){
				mFullSize = Size;
			}
		}
	}
}