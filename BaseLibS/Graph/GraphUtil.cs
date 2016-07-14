using System;

namespace BaseLibS.Graph{
	public static class GraphUtil{
		private static readonly Color2[] predefinedColors ={
			Color2.Blue, Color2.FromArgb(255, 144, 144),
			Color2.FromArgb(255, 0, 255), Color2.FromArgb(168, 156, 82), Color2.LightBlue, Color2.Orange, Color2.Cyan,
			Color2.Pink, Color2.Turquoise, Color2.LightGreen, Color2.Brown, Color2.DarkGoldenrod, Color2.DeepPink,
			Color2.LightSkyBlue, Color2.BlueViolet, Color2.Crimson
		};

		public static Color2 GetPredefinedColor(int index){
			return predefinedColors[Math.Abs(index%predefinedColors.Length)];
		}

		public static void FillShadedRectangle(Bitmap2 b, int w, int h){
			b.FillRectangle(Color2.White, 0, 0, w - 1, h - 1);
			b.SetPixel(1, 1, Color2.FromArgb(230, 238, 252));
			b.SetPixel(1, h - 3, Color2.FromArgb(219, 227, 248));
			b.SetPixel(w - 3, 1, Color2.FromArgb(220, 230, 249));
			b.SetPixel(w - 3, h - 3, Color2.FromArgb(217, 227, 246));
			b.SetPixel(w - 1, h - 3, Color2.FromArgb(174, 192, 214));
			b.SetPixel(w - 2, h - 2, Color2.FromArgb(174, 196, 219));
			b.SetPixel(0, h - 2, Color2.FromArgb(195, 212, 231));
			b.SetPixel(0, h - 1, Color2.FromArgb(237, 241, 243));
			b.SetPixel(w - 2, h - 1, Color2.FromArgb(236, 242, 247));
			int wi = w - 5;
			int he = h - 5;
			int[][] upper = InterpolateRgb(new[]{225, 234, 254}, new[]{188, 206, 250}, wi);
			int[][] lower = InterpolateRgb(new[]{183, 203, 249}, new[]{174, 200, 247}, wi);
			for (int i = 0; i < wi; i++){
				int[][] pix = InterpolateRgb(new[]{upper[0][i], upper[1][i], upper[2][i]},
					new[]{lower[0][i], lower[1][i], lower[2][i]}, he);
				for (int j = 0; j < he; j++){
					b.SetPixel(i + 2, j + 2, Color2.FromArgb(pix[0][j], pix[1][j], pix[2][j]));
				}
			}
			int[][] pix2 = InterpolateRgb(new[]{208, 223, 252}, new[]{170, 192, 243}, he);
			for (int j = 0; j < he; j++){
				b.SetPixel(1, j + 2, Color2.FromArgb(pix2[0][j], pix2[1][j], pix2[2][j]));
			}
			pix2 = InterpolateRgb(new[]{185, 202, 243}, new[]{176, 197, 242}, he);
			for (int j = 0; j < he; j++){
				b.SetPixel(w - 3, j + 2, Color2.FromArgb(pix2[0][j], pix2[1][j], pix2[2][j]));
			}
			pix2 = InterpolateRgb(new[]{208, 223, 252}, new[]{175, 197, 244}, wi);
			for (int i = 0; i < wi; i++){
				b.SetPixel(i + 2, 1, Color2.FromArgb(pix2[0][i], pix2[1][i], pix2[2][i]));
			}
			pix2 = InterpolateRgb(new[]{183, 198, 241}, new[]{176, 196, 242}, wi);
			for (int i = 0; i < wi; i++){
				b.SetPixel(i + 2, h - 3, Color2.FromArgb(pix2[0][i], pix2[1][i], pix2[2][i]));
			}
			pix2 = InterpolateRgb(new[]{238, 237, 229}, new[]{160, 181, 211}, he + 2);
			for (int i = 0; i < he + 2; i++){
				b.SetPixel(w - 1, i, Color2.FromArgb(pix2[0][i], pix2[1][i], pix2[2][i]));
			}
			pix2 = InterpolateRgb(new[]{170, 192, 225}, new[]{126, 159, 211}, w/2);
			for (int i = 1; i <= w/2; i++){
				b.SetPixel(i, h - 1, Color2.FromArgb(pix2[0][i - 1], pix2[1][i - 1], pix2[2][i - 1]));
			}
			pix2 = InterpolateRgb(new[]{126, 159, 211}, new[]{148, 176, 221}, w - 3 - w/2);
			for (int i = w/2 + 1; i <= w - 3; i++){
				b.SetPixel(i, h - 1, Color2.FromArgb(pix2[0][i - w/2 - 1], pix2[1][i - w/2 - 1], pix2[2][i - w/2 - 1]));
			}
		}

		public static int[][] InterpolateRgb(int[] start, int[] end, int n){
			if (n == 0){
				return new[]{new int[0], new int[0], new int[0]};
			}
			if (n == 1){
				int r1 = (start[0] + end[0])/2;
				int g1 = (start[1] + end[1])/2;
				int b1 = (start[2] + end[2])/2;
				return new[]{new[]{r1}, new[]{g1}, new[]{b1}};
			}
			int[] r = new int[n];
			int[] g = new int[n];
			int[] b = new int[n];
			double rstep = (end[0] - start[0])/(n - 1.0);
			double gstep = (end[1] - start[1])/(n - 1.0);
			double bstep = (end[2] - start[2])/(n - 1.0);
			for (int i = 0; i < n; i++){
				r[i] = (int) Math.Round(start[0] + i*rstep);
				g[i] = (int) Math.Round(start[1] + i*gstep);
				b[i] = (int) Math.Round(start[2] + i*bstep);
			}
			return new[]{r, g, b};
		}
	}
}