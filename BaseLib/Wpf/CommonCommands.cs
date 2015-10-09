using System.Windows.Input;

namespace BaseLib.Wpf {
	public static class CommonCommands {
		public static RoutedCommand selectAll = new RoutedCommand();		

		static CommonCommands(){
			selectAll.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
		}
	}
}
