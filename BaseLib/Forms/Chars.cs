using System;
using System.Collections.Generic;
using System.Drawing;
using BaseLib.Properties;

namespace BaseLib.Forms {
	public static class Chars {
		public static Bitmap GetImage(char c, int width, int height, Color fgColor, Color bgColor) {
			if (height <= 1 || width <= 1) {
				return null;
			}
			Bitmap template = GetImage(c);
			if (template == null) {
				return null;
			}
			int twidth = template.Width;
			int theight = template.Height;
			Bitmap result = new Bitmap(width, height);
			for (int i = 0; i < width; i++) {
				float wI1;
				float wI2;
				int indI = CalcInd(i, width, twidth, out wI1, out wI2);
				for (int j = 0; j < height; j++) {
					float wJ1;
					float wJ2;
					int indJ = CalcInd(j, height, theight, out wJ1, out wJ2);
					Color ij = IsSet(indI, indJ, template) ? fgColor : bgColor;
					Color ipj = IsSet(indI + 1, indJ, template) ? fgColor : bgColor;
					Color ijp = IsSet(indI, indJ + 1, template) ? fgColor : bgColor;
					Color ipjp = IsSet(indI + 1, indJ + 1, template) ? fgColor : bgColor;
					Color col = Average(new[] { ij, ipj, ijp, ipjp }, new[] { wI1 * wJ1, wI2 * wJ1, wI1 * wJ2, wI2 * wJ2 });
					result.SetPixel(i, j, col);
				}
			}
			return result;
		}

		private static Color Average(IList<Color> colors, IList<float> weights) {
			float r = 0;
			float g = 0;
			float b = 0;
			float norm = 0;
			for (int i = 0; i < colors.Count; i++) {
				Color col = colors[i];
				float w = weights[i];
				r += col.R * w;
				g += col.G * w;
				b += col.B * w;
				norm += w;
			}
			Color x = Color.FromArgb(255, (int)Math.Round(r / norm), (int)Math.Round(g / norm), (int)Math.Round(b / norm));
			return x;
		}

		private static bool IsSet(int i, int j, Bitmap b) {
			return b.GetPixel(i, j).R == 0;
		}

		private static int CalcInd(int i, int width, int twidth, out float wI1, out float wI2) {
			double dind = i / (width - 1.0) * (twidth - 1.0);
			int ind = (int)Math.Round(dind);
			if (dind < 0.0001) {
				wI1 = 1;
				wI2 = 0;
				return 0;
			}
			if (dind > twidth - 1.0001) {
				wI1 = 0;
				wI2 = 1;
				return twidth - 2;
			}
			if (ind == 0) {
				wI1 = (float)(1 - dind);
				wI2 = (float)dind;
				return 0;
			}
			if (ind == twidth - 1) {
				wI1 = (float)(twidth - 1 - dind);
				wI2 = (float)(2 - twidth + dind);
				return twidth - 2;
			}
			int ind1 = (int)dind;
			wI1 = (float)(1.0 - dind + ind1);
			wI2 = (float)(dind - ind1);
			return ind1;
		}

		public static Bitmap GetImage(char c) {
			switch (c) {
				case 'A':
					return Resources.charA;
				case 'B':
					return Resources.charB;
				case 'C':
					return Resources.charC;
				case 'D':
					return Resources.charD;
				case 'E':
					return Resources.charE;
				case 'F':
					return Resources.charF;
				case 'G':
					return Resources.charG;
				case 'H':
					return Resources.charH;
				case 'I':
					return Resources.charI;
				case 'J':
					return Resources.charJ;
				case 'K':
					return Resources.charK;
				case 'L':
					return Resources.charL;
				case 'M':
					return Resources.charM;
				case 'N':
					return Resources.charN;
				case 'O':
					return Resources.charO;
				case 'P':
					return Resources.charP;
				case 'Q':
					return Resources.charQ;
				case 'R':
					return Resources.charR;
				case 'S':
					return Resources.charS;
				case 'T':
					return Resources.charT;
				case 'U':
					return Resources.charU;
				case 'V':
					return Resources.charV;
				case 'W':
					return Resources.charW;
				case 'X':
					return Resources.charX;
				case 'Y':
					return Resources.charY;
				case 'Z':
					return Resources.charZ;
				case 'a':
					return Resources.char_a;
				case 'b':
					return Resources.char_b;
				case 'c':
					return Resources.char_c;
				case 'd':
					return Resources.char_d;
				case 'e':
					return Resources.char_e;
				case 'f':
					return Resources.char_f;
				case 'g':
					return Resources.char_g;
				case 'h':
					return Resources.char_h;
				case 'i':
					return Resources.char_i;
				case 'j':
					return Resources.char_j;
				case 'k':
					return Resources.char_k;
				case 'l':
					return Resources.char_l;
				case 'm':
					return Resources.char_m;
				case 'n':
					return Resources.char_n;
				case 'o':
					return Resources.char_o;
				case 'p':
					return Resources.char_p;
				case 'q':
					return Resources.char_q;
				case 'r':
					return Resources.char_r;
				case 's':
					return Resources.char_s;
				case 't':
					return Resources.char_t;
				case 'u':
					return Resources.char_u;
				case 'v':
					return Resources.char_v;
				case 'w':
					return Resources.char_w;
				case 'x':
					return Resources.char_x;
				case 'y':
					return Resources.char_y;
				case 'z':
					return Resources.char_z;
			}
			return null;
		}
	}
}
