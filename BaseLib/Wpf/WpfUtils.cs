using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BaseLib.Graphic;
using BaseLibS.Graph;

namespace BaseLib.Wpf{
	public static class WpfUtils{
		public static float GetDpiScaleX(){
			PropertyInfo dpiXProperty = typeof (SystemParameters).GetProperty("DpiX",
				BindingFlags.NonPublic | BindingFlags.Static);
			if (dpiXProperty == null){
				return 1;
			}
			int dpiX = (int) dpiXProperty.GetValue(null, null);
			return dpiX/96f;
		}

		public static float GetDpiScaleY(){
			PropertyInfo dpiYProperty = typeof (SystemParameters).GetProperty("DpiY",
				BindingFlags.NonPublic | BindingFlags.Static);
			if (dpiYProperty == null){
				return 1;
			}
			int dpiY = (int) dpiYProperty.GetValue(null, null);
			return dpiY/96f;
		}

		public static UIElement GetGridChild(Grid grid, int row, int column){
			return grid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
		}

		public static BitmapSource LoadBitmap(Bitmap2 source){
			return LoadBitmap(GraphUtils.ToBitmap(source));
		}

		public static BitmapSource LoadBitmap(System.Drawing.Bitmap source){
			if (source == null){
				return null;
			}
			return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());
		}

		public static BitmapSource GetSaveBitmap(){
			return LoadBitmap(Bitmap2.GetImage("save.png"));
		}

		public static BitmapSource GetSaveAsBitmap(){
			return LoadBitmap(Bitmap2.GetImage("save_as.png"));
		}

		public static BitmapSource GetNewBitmap(){
			return LoadBitmap(Bitmap2.GetImage("new.png"));
		}

		public static BitmapSource GetHelpBitmap(){
			return LoadBitmap(Bitmap2.GetImage("help.png"));
		}

		public static BitmapSource GetMinMaxRibbonBitmap(){
			return LoadBitmap(Bitmap2.GetImage("minMaxRibbon.png"));
		}

		public static BitmapSource GetPdfBitmap(){
			return LoadBitmap(Bitmap2.GetImage("pdf.png"));
		}

		public static BitmapSource GetToolsBitmap(){
			return LoadBitmap(Bitmap2.GetImage("tools.png"));
		}

		public static BitmapSource GetExitBitmap(){
			return LoadBitmap(Bitmap2.GetImage("exit.png"));
		}

		public static BitmapSource GetInfoBitmap(){
			return LoadBitmap(Bitmap2.GetImage("info.png"));
		}

		public static BitmapSource GetOpenBitmap(){
			return LoadBitmap(Bitmap2.GetImage("open.png"));
		}

		public static BitmapSource GetMonitorsBitmap(){
			return LoadBitmap(Bitmap2.GetImage("monitors.png"));
		}

		public static BitmapSource GetNewWindowBitmap(){
			return LoadBitmap(Bitmap2.GetImage("open_in_new_window.png"));
		}

		public static BitmapSource GetMergeBitmap(){
			return LoadBitmap(Bitmap2.GetImage("merge.png"));
		}

		public static BitmapSource GetMinusBitmap(){
			return LoadBitmap(Bitmap2.GetImage("minus-icon.png"));
		}

		public static BitmapSource GetPlusBitmap(){
			return LoadBitmap(Bitmap2.GetImage("plus-icon.png"));
		}

		public static BitmapSource GetArrowCornerBitmap(){
			return LoadBitmap(Bitmap2.GetImage("arrowCorner.png"));
		}

		public static BitmapSource GetRotateBitmap(){
			return LoadBitmap(Bitmap2.GetImage("rotate.png"));
		}
	}
}