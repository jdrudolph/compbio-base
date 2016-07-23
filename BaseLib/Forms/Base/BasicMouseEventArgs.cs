using System;
using System.Windows.Forms;

namespace BaseLib.Forms.Base{
	public class BasicMouseEventArgs{
		private readonly Func<Keys> modifierKeys;
		private readonly Action<string, int, int, int> showTip;

		internal BasicMouseEventArgs(MouseEventArgs e, int width, int height, Func<Keys> modifierKeys,
			Action<string, int, int, int> showTip){
			X = e.X;
			Y = e.Y;
			Width = width;
			Height = height;
			IsMainButton = e.Button == MouseButtons.Left;
			this.modifierKeys = modifierKeys;
			this.showTip = showTip;
		}

		public BasicMouseEventArgs(BasicMouseEventArgs e, int dx, int dy, int width, int height){
			X = e.X - dx;
			Y = e.Y - dy;
			Width = width;
			Height = height;
			IsMainButton = e.IsMainButton;
			modifierKeys = e.modifierKeys;
			showTip = e.showTip;
		}

		public int X { get; }
		public int Y { get; }
		public int Width { get; }
		public int Height { get; }
		public bool ControlPressed => (modifierKeys() & Keys.Control) == Keys.Control;
		public bool IsMainButton { get; }

		public void ViewToolTip(string text){
			showTip(text, X + 10, Y + 10, 5000);
		}
	}
}