namespace BaseLibS.Graph{
	public interface ICompoundScrollableControlClient : IScrollableControlClient{
		void Register(ICompoundScrollableControl control);
	}
}