using System.Reflection;
using System.Windows;

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
	}
}