using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BaseLib.Graphic;
using BaseLibS.Util;

namespace BaseLib.Util{
	public static class StringUtils2{
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
	}
}