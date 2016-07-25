namespace BaseLibS.Graph{
	public struct Size2{
		public int Width { get; set; }
		public int Height { get; set; }
		public static readonly Size2 Empty = new Size2();

		public Size2(int width, int height){
			Width = width;
			Height = height;
		}

		public bool IsEmpty => Width == 0 && Height == 0;
	}
}