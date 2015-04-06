namespace BaseLib.Forms.Scatter{
	internal enum ScatterPlotMouseMode{
		Zoom,
		Select
	}

	public delegate void ViewZoom2DChangeHandler(
		object source, int imin, int imax, int jmin, int jmax, bool relativeToVisibleAxis, int width, int height);

	public enum ScatterPlotLabelMode{
		None,
		Selected,
		All
	}
}