using System.Windows.Forms;
using BaseLibS.Graph;

namespace BaseLib.Forms.Scroll{
	public interface ICompoundScrollableControlClient{
		void Register(ICompoundScrollableControl control);
		void ProcessCmdKey(Keys keyData);
		void InvalidateBackgroundImages();
		void OnSizeChanged();
	}
}