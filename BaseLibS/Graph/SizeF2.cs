namespace BaseLibS.Graph{
	public struct SizeF2{
		public float Width { get; set; }
		public float Height { get; set; }
		public static readonly SizeF2 Empty = new SizeF2();

		public SizeF2(float width, float height){
			Width = width;
			Height = height;
		}

		public bool IsEmpty => Width == 0 && Height == 0;
	}
}