using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BaseLib.Properties;
using BaseLibS.Util;

namespace BaseLib.Forms.Table{
	public partial class HelpLabel : Control{
		private bool helpActive;
		private readonly ToolTip toolTip = new ToolTip();
		public string HelpText { get; set; }
		public string HelpTitle { get; set; }

		public HelpLabel(){
			InitializeComponent();
			toolTip.ToolTipIcon = ToolTipIcon.Info;
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		protected override void OnPaint(PaintEventArgs e){
			base.OnPaint(e);
			Graphics g = e.Graphics;
			Brush b = new SolidBrush(Enabled ? ForeColor : Color.FromArgb(255, 177, 177, 209));
			g.DrawString(Text, Font, b, 1, 0);
			if (helpActive){
				g.DrawImage(Resources.question12, 2, 1, 10, 10);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (!string.IsNullOrEmpty(HelpText) && e.X < 11 && e.Y > 1 && e.Y < 11){
				if (!helpActive){
					helpActive = true;
					Invalidate();
				}
			} else{
				if (helpActive){
					helpActive = false;
					Invalidate();
				}
			}
		}

		protected override void OnMouseLeave(EventArgs e){
			toolTip.Active = false;
			helpActive = false;
			Invalidate(true);
		}

		protected override void OnMouseUp(MouseEventArgs e){
			if (!Enabled){
				return;
			}
			toolTip.Active = false;
			helpActive = false;
			Invalidate(true);
		}

		protected override void OnMouseDown(MouseEventArgs e){
			if (!Enabled){
				return;
			}
			if (helpActive){
				toolTip.ToolTipTitle = (string.IsNullOrEmpty(HelpTitle)) ? Text : HelpTitle;
				toolTip.Active = true;
				StringBuilder text = new StringBuilder();
				string[] wrapped = StringUtils.Wrap(HelpText, 75);
				for (int i = 0; i < wrapped.Length; ++i){
					string s = wrapped[i];
					text.Append(s);
					if (i < wrapped.Length - 1){
						text.Append("\n");
					}
				}
				toolTip.Show(text.ToString(), this, e.X + 75, e.Y + 5);
				//toolTip.Show(HelpText, this, e.X, e.Y);
				helpActive = false;
				Invalidate(true);
			}
		}
	}
}