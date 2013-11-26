namespace BaseLib.Forms.Base{
	public abstract class BasicTableLayoutStyle{
		public BasicSizeType SizeType { get; private set; }
		internal abstract float Size { get; set; }

		protected BasicTableLayoutStyle(BasicSizeType sizeType){
			SizeType = sizeType;
		}
	}
}