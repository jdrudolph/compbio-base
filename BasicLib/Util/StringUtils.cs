using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using BasicLib.Graphic;

namespace BasicLib.Util{
	public static class StringUtils{
		/// <summary>
		/// The digits 0 to 9 as subscripts.
		/// </summary>
		private static readonly char[] subscripts = new[]
		{'\u2080', '\u2081', '\u2082', '\u2083', '\u2084', '\u2085', '\u2086', '\u2087', '\u2088', '\u2089'};
		/// <summary>
		/// The digits 0 to 9 as superscripts.
		/// </summary>
		private static readonly char[] superscripts = new[]
		{'\u2070', '\u00b9', '\u00b2', '\u00b3', '\u2074', '\u2075', '\u2076', '\u2077', '\u2078', '\u2079'};

		/// <summary>
		/// Returns a string containing a representation of the given integer as superscript.
		/// </summary>
		/// <param name="n">The integer to be converted to superscript.</param>
		/// <param name="explicitPlus">Whether or not a '+' is added in front of positive numbers.</param>
		/// <param name="explicitBracket"></param>
		/// <returns>Representation of the given integer as superscript string.</returns>
		public static string ToSuperscript(int n, bool explicitPlus, bool explicitBracket){
			bool isNegative = n < 0;
			bool isPositive = n > 0;
			StringBuilder result = new StringBuilder();
			try{
				n = Math.Abs(n);
				string nn = n.ToString(CultureInfo.InvariantCulture);
				if (explicitBracket){
					result.Append("\u207D");
				}
				if (isNegative){
					result.Append('\u207B');
				}
				char[] nnn = nn.ToCharArray();
				foreach (char t in nnn){
					result.Append(superscripts[t - '0']);
				}
				if (isPositive && explicitPlus){
					result.Append('\u207A');
				}
				if (explicitBracket){
					result.Append("\u207E");
				}
			} catch (OverflowException){
				Console.Error.WriteLine("Could not calculate the absolute value of n=" + n);
			}
			return result.ToString();
		}

		/// <summary>
		/// Returns a string containing a representation of the given integer as subscript.
		/// </summary>
		/// <param name="n">The integer to be converted to subscript.</param>
		/// <param name="explicitPlus">Whether or not a '+' is added in front of positive numbers.</param>
		/// <returns>Representation of the given integer as subscript string.</returns>
		public static string ToSubscript(int n, bool explicitPlus){
			bool isNegative = n < 0;
			bool isPositive = n > 0;
			n = Math.Abs(n);
			string nn = n.ToString(CultureInfo.InvariantCulture);
			StringBuilder result = new StringBuilder();
			if (isNegative){
				result.Append('\u208B');
			}
			if (isPositive && explicitPlus){
				result.Append('\u208A');
			}
			char[] nnn = nn.ToCharArray();
			foreach (char t in nnn){
				result.Append(subscripts[t - '0']);
			}
			return result.ToString();
		}

		/// <summary>
		/// Concatenates the string representations of the objects in the given array using the specified separator.
		/// </summary>
		/// <typeparam name="T">Type of objects to be concatenated as strings.</typeparam>
		/// <param name="separator">A string used to separate the array members.</param>
		/// <param name="o">The list of objects to be concatenated.</param>
		/// <returns>The concatenated string of all string representations of the array members.</returns>
		public static string Concat<T>(string separator, T[] o){
			return Concat(separator, o, int.MaxValue);
		}

		/// <summary>
		/// Concatenates the string representations of the objects in the given array using the specified separator.
		/// </summary>
		/// <typeparam name="T">Type of objects to be concatenated as strings.</typeparam>
		/// <param name="separator">A string used to separate the array members.</param>
		/// <param name="o">The list of objects to be concatenated.</param>
		/// <param name="maxLen">The convatenation is terminated such that the length of the resulting string will not exceed this value.</param>
		/// <returns>The concatenated string of all string representations of the array members.</returns>
		public static string Concat<T>(string separator, T[] o, int maxLen){
			if (o == null || o.Length == 0){
				return "";
			}
			if (o.Length == 1){
				return o[0].ToString();
			}
			StringBuilder s = new StringBuilder(o[0].ToString());
			for (int i = 1; i < o.Length; i++){
				string w = separator + o[i];
				if (s.Length + w.Length > maxLen){
					break;
				}
				s.Append(w);
			}
			return s.ToString();
		}

		public static int[][] SplitToInt(char separator1, char separator2, string s){
			string[][] x = Split(separator1, separator2, s);
			int[][] result = new int[x.Length][];
			for (int i = 0; i < x.Length; i++){
				result[i] = new int[x[i].Length];
				for (int j = 0; j < x[i].Length; j++){
					result[i][j] = int.Parse(x[i][j]);
				}
			}
			return result;
		}

		public static string[][] Split(char separator1, char separator2, string s){
			if (s == null || s.ToLower().Equals("null")){
				return null;
			}
			if (s.Length == 0){
				return new string[0][];
			}
			string[] q1 = s.Length > 0 ? s.Split(separator1) : new string[0];
			string[][] result = new string[q1.Length][];
			for (int i = 0; i < result.Length; i++){
				string q = q1[i];
				result[i] = q.Length > 0 ? q.Split(separator2) : new string[0];
			}
			return result;
		}

		public static string Concat<T>(string separator1, string separator2, T[][] o){
			return Concat(separator1, separator2, o, int.MaxValue);
		}

		public static string Concat<T>(string separator1, string separator2, T[][] o, int maxLen){
			if (o == null){
				return "";
			}
			if (o.Length == 0){
				return "";
			}
			if (o.Length == 1){
				return Concat(separator2, o[0], maxLen);
			}
			StringBuilder s = new StringBuilder(Concat(separator2, o[0], maxLen));
			for (int i = 1; i < o.Length; i++){
				string w = separator1 + Concat(separator2, o[i], maxLen - s.Length);
				if (s.Length + w.Length > maxLen){
					break;
				}
				s.Append(w);
			}
			return s.ToString();
		}

		/// <summary>
		/// Concatenates the string representations of the objects in the given array using the specified separator.
		/// </summary>
		/// <typeparam name="T">Type of objects to be concatenated as strings.</typeparam>
		/// <param name="separator">A string used to separate the array members.</param>
		/// <param name="o">The list of objects to be concatenated.</param>
		/// <returns>The concatenated string of all string representations of the array members.</returns>
		public static string Concat<T>(string separator, IList<T> o){
			if (o == null){
				return "";
			}
			if (o.Count == 0){
				return "";
			}
			if (o.Count == 1){
				return o[0].ToString();
			}
			StringBuilder s = new StringBuilder(o[0].ToString());
			for (int i = 1; i < o.Count; i++){
				string w = separator + o[i];
				s.Append(w);
			}
			return s.ToString();
		}

		/// <summary>
		/// Concatenates the string representations of the objects in the given array using the specified separator.
		/// </summary>
		/// <typeparam name="T">Type of objects to be concatenated as strings.</typeparam>
		/// <param name="separator">A string used to separate the array members.</param>
		/// <param name="o">The list of objects to be concatenated.</param>
		/// <returns>The concatenated string of all string representations of the array members.</returns>
		public static string Concat<T>(string separator, IEnumerable<T> o){
			if (o == null){
				return "";
			}
			StringBuilder s = new StringBuilder();
			foreach (T i in o){
				if (s.Length == 0){
					s.Append(i);
					continue;
				}
				string w = separator + i;
				s.Append(w);
			}
			return s.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string[] Wrap(string text, int maxLength){
			string[] words = text.Split(' ');
			int currentLineLength = 0;
			ArrayList lines = new ArrayList(text.Length/maxLength);
			bool inTag = false;
			string currentLine = "";
			foreach (string currentWord in words){
				//ignore html
				if (currentWord.Length > 0){
					if (currentWord.Substring(0, 1) == "<"){
						inTag = true;
					}
					if (inTag){
						//handle filenames inside html tags
						if (currentLine.EndsWith(".")){
							currentLine += currentWord;
						} else{
							currentLine += " " + currentWord;
						}
						if (currentWord.IndexOf(">", StringComparison.InvariantCulture) > -1){
							inTag = false;
						}
					} else{
						if (currentLineLength + currentWord.Length + 1 < maxLength){
							currentLine += (currentLineLength == 0 ? "" : " ") + currentWord;
							currentLineLength += (currentWord.Length + 1);
						} else{
							if (!string.IsNullOrEmpty(currentLine)){
								lines.Add(currentLine);
							}
							currentLine = currentWord;
							currentLineLength = currentWord.Length;
						}
					}
				}
			}
			if (currentLine != ""){
				lines.Add(currentLine);
			}
			string[] textLinesStr = new string[lines.Count];
			lines.CopyTo(textLinesStr, 0);
			return textLinesStr;
		}

		public static bool ContainsAll(string x, IEnumerable<string> strings){
			x = x.ToLower();
			foreach (string s in strings){
				if (x.IndexOf(s.ToLower(), StringComparison.InvariantCulture) == -1){
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns a string that is the same as the input string, except that all whitespace characters are removed.
		/// </summary>
		public static string RemoveWhitespace(string str){
			StringBuilder s = new StringBuilder();
			int len = str.Length;
			for (int i = 0; i < len; i++){
				char c = str[i];
				if (!Char.IsWhiteSpace(c)){
					s.Append(c);
				}
			}
			return s.ToString();
		}

		/// <summary>
		/// Returns a string that is the same as the input string, except that all consecutive sets of whitespace 
		/// characters are replaced by a single blank character.
		/// </summary>
		public static string ReduceWhitespace(string str){
			StringBuilder s = new StringBuilder();
			int len = str.Length;
			bool previousWasWhiteSpace = false;
			for (int i = 0; i < len; i++){
				char c = str[i];
				if (!char.IsWhiteSpace(c)){
					s.Append(c);
					previousWasWhiteSpace = false;
				} else{
					if (!previousWasWhiteSpace){
						s.Append(' ');
					}
					previousWasWhiteSpace = true;
				}
			}
			return s.ToString().Trim();
		}

		public static string Replace(string x, string[] oldChar, string newChar){
			if (string.IsNullOrEmpty(x)){
				return null;
			}
			string result = x;
			foreach (string t in oldChar){
				if (!string.IsNullOrEmpty(x)){
					result = result.Replace(t, newChar);
				}
			}
			return result;
		}

		public static int OccurenceCount(string s, char c){
			if (s == null){
				return 0;
			}
			int count = 0;
			foreach (char w in s){
				if (w == c){
					count++;
				}
			}
			return count;
		}

		public static string JoinQuotedCsv(string[] x){
			if (x.Length == 0){
				return "";
			}
			if (x[0].StartsWith("\"")){
				return Concat(",", x);
			}
			return "\"" + Concat("\",\"", x) + "\"";
		}

		public static string[] SplitQuotedCsv(string line){
			line = line.Trim();
			if (line.StartsWith("\"") && line.EndsWith("\"")){
				line = line.Substring(1, line.Length - 2);
				return line.Split(new[]{"\",\""}, StringSplitOptions.None);
			}
			return line.Split(',');
		}

		public static string[] Split(string seq, int n){
			if (seq.Length <= n){
				return new[]{seq};
			}
			int q = (int) Math.Ceiling(seq.Length/(double) n);
			string[] result = new string[q];
			for (int i = 0; i < q - 1; i++){
				result[i] = seq.Substring(i*n, n);
			}
			result[q - 1] = seq.Substring((q - 1)*n);
			return result;
		}

		public static string ToString(IDictionary d){
			StringBuilder result = new StringBuilder();
			foreach (string key in d.Keys){
				result.AppendLine(key + "\t" + d[key]);
			}
			return result.ToString();
		}

		public static string[] WrapString(IGraphics g, string s, int width, Font font){
			if (width < 20){
				return new[]{s};
			}
			if (g.MeasureString(s, font).Width < width - 7){
				return new[]{s};
			}
			s = ReduceWhitespace(s);
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

		public static int[] AllIndicesOf(string str, string word){
			List<int> result = new List<int>();
			int found = str.IndexOf(word, StringComparison.InvariantCulture);
			while (found != -1){
				result.Add(found);
				found = str.IndexOf(word, found + 1, StringComparison.InvariantCulture);
			}
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

		public static string Repeat(string s, int n){
			if (n == 0){
				return "";
			}
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < n; i++){
				result.Append(s);
			}
			return result.ToString();
		}

		public static string Repeat(char c, int n){
			if (n == 0){
				return "";
			}
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < n; i++){
				result.Append(c);
			}
			return result.ToString();
		}
	}
}