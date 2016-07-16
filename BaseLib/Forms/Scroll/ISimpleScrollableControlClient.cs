using System;
using System.Windows.Forms;

namespace BaseLib.Forms.Scroll{
	public interface ISimpleScrollableControlClient{
		void Register(SimpleScrollableControl control, Func<Keys> getModifierKeys);
		void ProcessCmdKey(Keys keyData);
		void InvalidateBackgroundImages();
		void OnSizeChanged();
	}
}