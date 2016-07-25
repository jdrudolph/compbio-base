namespace BaseLibS.Graph{
	public struct Point2{
		public static readonly Point2 Empty = new Point2();
		public int X { get; set; }
		public int Y { get; set; }

		public Point2(int x, int y){
			X = x;
			Y = y;
		}
	}
}