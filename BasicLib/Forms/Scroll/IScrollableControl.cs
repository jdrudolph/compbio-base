namespace BasicLib.Forms.Scroll {
	internal interface IScrollableControl {
		int DeltaX { get; }
		int DeltaY { get; }
		int VisibleX { get; set; }
		int VisibleY { get; set; }
		int VisibleWidth { get; }
		int VisibleHeight { get; }
		int TotalWidth { get; }
		int TotalHeight { get; }
		int DeltaDownToSelection();
		int DeltaUpToSelection();
	}
}
