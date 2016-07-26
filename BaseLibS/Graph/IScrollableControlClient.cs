namespace BaseLibS.Graph{
	public interface IScrollableControlClient{
		void ProcessCmdKey(Keys2 keyData);
		void InvalidateBackgroundImages();
		void OnSizeChanged();
	}
}