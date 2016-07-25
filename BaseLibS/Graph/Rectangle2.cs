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
	}
}