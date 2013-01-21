using System;
using System.Drawing;

namespace BasicLib.Graphic{
	public class ColorConstants{
		private static readonly Color[] predefinedColors = new[]{
			Color.Blue, Color.FromArgb(192, 192, 255), Color.FromArgb(255, 144, 144), Color.FromArgb(255, 255, 0),
			Color.FromArgb(128, 255, 128), Color.FromArgb(255, 128, 255), Color.FromArgb(197, 188, 137), Color.LightBlue,
			Color.Orange, Color.Cyan, Color.Pink, Color.Turquoise, Color.LightGreen, Color.Brown, Color.DarkRed, Color.Gold,
			Color.DeepPink, Color.LightSkyBlue
		};

		public static Color GetPredefinedColor(int index){
			return predefinedColors[Math.Abs(index%predefinedColors.Length)];
		}
	}
}