namespace BaseLibS.Graph.Base{
	public abstract class BasicTableLayoutStyle{
		public BasicSizeType SizeType { get; private set; }
		public abstract float Size { get; set; }

		protected BasicTableLayoutStyle(BasicSizeType sizeType){
			SizeType = sizeType;
		}
	}
}