namespace BaseLibS.Graph{
	public class Font2{
		public Font2(string name, float size) : this(name, size, FontStyle2.Regular){}

		public Font2(string name, float size, FontStyle2 style){
			Name = name;
			Size = size;
			Style = style;
		}
			
		public int Height => (int) (1.6*Size);
		public string Name { get; private set; }
		public float Size { get; }
		public FontStyle2 Style { get; }
		public bool Bold => Style==FontStyle2.Bold;
	}
}