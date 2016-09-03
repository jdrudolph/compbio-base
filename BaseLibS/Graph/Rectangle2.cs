namespace BaseLibS.Graph{
	public struct Rectangle2{
		public static readonly Rectangle2 Empty = new Rectangle2();
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Rectangle2(int x, int y, int width, int height){
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public int Top => Y;
		public int Right => X + Width;
		public int Bottom => Y + Height;
		public int Left => X;

		public bool Contains(float x, float y){
			return X <= x && x < X + Width && Y <= y && y < Y + Height;
		}

		public bool Contains(Point2 pt){
			return Contains(pt.X, pt.Y);
		}

		public bool Contains(Rectangle2 rect){
			return (X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y) &&
					(rect.Y + rect.Height <= Y + Height);
		}
	}
}