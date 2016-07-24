using System.Windows.Forms;

namespace BaseLib.Forms.Scroll{
	public interface ICompoundScrollableControlClient{
		void Register(ICompoundScrollableControl control);
		void ProcessCmdKey(Keys keyData);
		void InvalidateBackgroundImages();
		void OnSizeChanged();
	}
}