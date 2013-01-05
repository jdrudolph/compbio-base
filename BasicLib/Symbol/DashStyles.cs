using System;
using System.Drawing.Drawing2D;

namespace BasicLib.Symbol{
	public static class DashStyles{
		public static DashStyle DashStyleFromIndex(int index){
			switch (index){
				case 0:
					return DashStyle.Solid;
				case 1:
					return DashStyle.Dash;
				case 2:
					return DashStyle.Dot;
				case 3:
					return DashStyle.DashDot;
				case 4:
					return DashStyle.DashDotDot;
				default:
					throw new ArgumentException();
			}
		}

		public static int DashStyleToIndex(DashStyle dashStyle){
			switch (dashStyle){
				case DashStyle.Solid:
					return 0;
				case DashStyle.Dash:
					return 1;
				case DashStyle.Dot:
					return 2;
				case DashStyle.DashDot:
					return 3;
				case DashStyle.DashDotDot:
					return 4;
				default:
					throw new ArgumentException();
			}
		}
	}
}