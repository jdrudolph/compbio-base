using System.Windows.Forms;

namespace BaseLib.Forms.Scroll{
	public interface ISimpleScrollableControlClient{
		void Register(ISimpleScrollableControl control);
		void ProcessCmdKey(Keys keyData);
		void InvalidateBackgroundImages();
		void OnSizeChanged();
	}
}