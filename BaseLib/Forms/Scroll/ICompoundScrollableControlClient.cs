using System;
using System.Windows.Forms;

namespace BaseLib.Forms.Scroll{
	public interface ICompoundScrollableControlClient{
		void Register(CompoundScrollableControl control, Func<Keys> getModifierKeys);
		void ProcessCmdKey(Keys keyData);
		void InvalidateBackgroundImages();
		void OnSizeChanged();
	}
}