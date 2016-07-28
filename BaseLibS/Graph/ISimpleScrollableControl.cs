namespace BaseLibS.Graph{
	public interface ISimpleScrollableControl : IScrollableControl{
		ISimpleScrollableControlClient Client { set; }
	}
}