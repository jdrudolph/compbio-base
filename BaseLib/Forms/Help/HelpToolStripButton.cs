using System;
using System.Text;
using System.Windows.Forms;
using BaseLib.Util;

namespace BaseLib.Forms.Help{
	public class HelpToolStripButton : ToolStripButton{
		private readonly ToolTip toolTip = new ToolTip();
		public string HelpText { get; set; }
		public string HelpTitle { get; set; }

		protected override void OnMouseDown(MouseEventArgs e){
			if (e.Button == MouseButtons.Right){
				if (!Enabled || HelpText == null){
					return;
				}
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
				toolTip.Show(text.ToString(), Parent, e.X + 75, e.Y + 5);
				Invalidate();
			}
		}

		protected override void OnMouseLeave(EventArgs e){
			base.OnMouseLeave(e);
			toolTip.Active = false;
			Invalidate();
		}
	}
}