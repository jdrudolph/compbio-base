using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BaseLib.Param;

namespace BaseLib.Wpf{
	public static class WpfUtils{
		public static void SetOkFocus(Control c){
			Window w = Window.GetWindow(c);
			if (w is ParameterWindow){
				ParameterWindow pw = (ParameterWindow) w;
				pw.FocusOkButton();
			}
		}

		public static float GetDpiScaleX(){
			PropertyInfo dpiXProperty = typeof (SystemParameters).GetProperty("DpiX",
				BindingFlags.NonPublic | BindingFlags.Static);
			int dpiX = (int) dpiXProperty.GetValue(null, null);
			return dpiX/96f;
		}

		public static float GetDpiScaleY(){
			PropertyInfo dpiYProperty = typeof (SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);
			int dpiY = (int) dpiYProperty.GetValue(null, null);
			return dpiY/96f;
		}

		public static UIElement GetGridChild(Grid grid, int row, int column){
			return grid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
		}

		public static BitmapSource LoadBitmap(System.Drawing.Bitmap source){
			if (source == null){
				return null;
			}
			return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());
		}

		public static BitmapSource GetHelpBitmap(){
			return LoadBitmap(Properties.Resources.help);
		}

		public static BitmapSource GetMinMaxRibbonBitmap(){
			return LoadBitmap(Properties.Resources.minMaxRibbon);
		}

		public static BitmapSource GetPdfBitmap(){
			return LoadBitmap(Properties.Resources.pdf);
		}

		public static BitmapSource GetToolsBitmap(){
			return LoadBitmap(Properties.Resources.tools);
		}

		public static BitmapSource GetExitBitmap(){
			return LoadBitmap(Properties.Resources.exit);
		}

		public static BitmapSource GetSaveBitmap(){
			return LoadBitmap(Properties.Resources.save);
		}

		public static BitmapSource GetSaveAsBitmap(){
			return LoadBitmap(Properties.Resources.save_as);
		}

		public static BitmapSource GetInfoBitmap(){
			return LoadBitmap(Properties.Resources.info);
		}

		public static BitmapSource GetOpenBitmap(){
			return LoadBitmap(Properties.Resources.open);
		}

		public static BitmapSource GetMonitorsBitmap(){
			return LoadBitmap(Properties.Resources.monitors);
		}

		public static BitmapSource GetNewBitmap(){
			return LoadBitmap(Properties.Resources._new);
		}

		public static BitmapSource GetNewWindowBitmap(){
			return LoadBitmap(Properties.Resources.open_in_new_window);
		}

		public static BitmapSource GetMergeBitmap(){
			return LoadBitmap(Properties.Resources.merge);
		}

		public static BitmapSource GetMinusBitmap(){
			return LoadBitmap(Properties.Resources.minus_icon);
		}

		public static BitmapSource GetPlusBitmap(){
			return LoadBitmap(Properties.Resources.plus_icon);
		}

		public static BitmapSource GetArrowCornerBitmap(){
			return LoadBitmap(Properties.Resources.arrowCorner);
		}

		public static BitmapSource GetRotateBitmap(){
			return LoadBitmap(Properties.Resources.rotate);
		}
	}
}