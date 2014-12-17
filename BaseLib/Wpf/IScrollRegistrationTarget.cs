using System.Windows.Controls;

namespace BaseLib.Wpf{
	public interface IScrollRegistrationTarget{
		void RegisterScrollViewer(ScrollViewer scrollViewer);
	}
}