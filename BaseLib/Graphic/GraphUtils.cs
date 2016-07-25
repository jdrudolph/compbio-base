using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BaseLibS.Graph;
using BaseLibS.Util;

namespace BaseLib.Graphic{
	public static class GraphUtils{
		public static string[] WrapString(IGraphics g, string s, int width, Font font){
			if (width < 20){
				return new[]{s};
			}
			if (g.MeasureString(s, font).Width < width - 7){
				return new[]{s};
			}
			s = StringUtils.ReduceWhitespace(s);
			string[] q = s.Split(' ');
			List<string> result = new List<string>();
			string current = q[0];
			for (int i = 1; i < q.Length; i++){
				string next = current + " " + q[i];
				if (g.MeasureString(next, font).Width > width - 7){
					result.Add(current);
					current = q[i];
				} else{
					current += " " + q[i];
				}
			}
			result.Add(current);
			return result.ToArray();
		}

		public static string GetStringValue(IGraphics g, string s, int width, Font font){
			if (width < 20){
				return "";
			}
			if (g.MeasureString(s, font).Width < width - 7){
				return s;
			}
			StringBuilder sb = new StringBuilder();
			foreach (char t in s){
				if (g.MeasureString(sb.ToString(), font).Width < width - 21){
					sb.Append(t);
				} else{
					break;
				}
			}
			return sb + "...";
		}

		public static Color2 ToColor2(Color c){
			return Color2.FromArgb(c.A, c.R, c.G, c.B);
		}

		public static Color ToColor(Color2 c){
			return Color.FromArgb(c.A, c.R, c.G, c.B);
		}

		public static Bitmap2 ToBitmap2(Bitmap bitmap){
			if (bitmap == null){
				return null;
			}
			Bitmap2 result = new Bitmap2(bitmap.Width, bitmap.Height);
			for (int i = 0; i < bitmap.Width; i++){
				for (int j = 0; j < bitmap.Height; j++){
					result.SetPixel(i, j, bitmap.GetPixel(i, j).ToArgb());
				}
			}
			return result;
		}

		public static Bitmap ToBitmap(Bitmap2 bitmap){
			if (bitmap == null){
				return null;
			}
			Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
			for (int i = 0; i < bitmap.Width; i++){
				for (int j = 0; j < bitmap.Height; j++){
					result.SetPixel(i, j, Color.FromArgb(bitmap.GetPixel(i, j)));
				}
			}
			return result;
		}
	}
}