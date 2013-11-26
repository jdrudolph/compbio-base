using System;
using System.Windows.Forms;

namespace BaseLib.Forms.Base{
	public class BasicMouseEventArgs{
		private readonly int x;
		private readonly int y;
		private readonly int width;
		private readonly int height;
		private readonly bool isMainButton;
		private readonly Func<Keys> modifierKeys;
		private readonly Action<string, int, int, int> showTip;

		internal BasicMouseEventArgs(MouseEventArgs e, int width, int height, Func<Keys> modifierKeys,
			Action<string, int, int, int> showTip){
			x = e.X;
			y = e.Y;
			this.width = width;
			this.height = height;
			isMainButton = e.Button == MouseButtons.Left;
			this.modifierKeys = modifierKeys;
			this.showTip = showTip;
		}

		public BasicMouseEventArgs(BasicMouseEventArgs e, int dx, int dy, int width, int height){
			x = e.X - dx;
			y = e.Y - dy;
			this.width = width;
			this.height = height;
			isMainButton = e.isMainButton;
			modifierKeys = e.modifierKeys;
			showTip = e.showTip;
		}

		public int X { get { return x; } }
		public int Y { get { return y; } }
		public int Width { get { return width; } }
		public int Height { get { return height; } }
		public bool ControlPressed { get { return (modifierKeys() & Keys.Control) == Keys.Control; } }
		public bool IsMainButton { get { return isMainButton; } }

		public void ViewToolTip(string text){
			showTip(text, X + 10, Y + 10, 5000);
		}
	}
}