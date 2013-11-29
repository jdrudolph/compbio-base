using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Wpf {
	public static class WpfUtils {
		public static UIElement GetGridChild(Grid grid, int row, int column) {
			return grid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
		}
	}
}
