namespace BasicLib.Forms.Base{
	public class BasicRowStyle : BasicTableLayoutStyle{
		public float Height { get; set; }

		public BasicRowStyle(BasicSizeType sizeType, float height) : base(sizeType){
			Height = height;
		}

		internal override float Size { get { return Height; } set { Height = value; } }
	}
}