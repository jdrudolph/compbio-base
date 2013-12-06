using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BaseLib.Wpf {
	public static class WpfUtils {
		public static UIElement GetGridChild(Grid grid, int row, int column) {
			return grid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
		}

		public static BitmapSource LoadBitmap(System.Drawing.Bitmap source) {
			if (source == null){
				return null;
			}
			return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());
			
		}
	}
}
