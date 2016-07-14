using System;
using BaseLib.Forms.Base;

namespace BaseLib.Forms.Scroll {
	public interface IScrollableControl : IPrintable{
		int DeltaX { get; }
		int DeltaY { get; }
		int VisibleX { get; set; }
		int VisibleY { get; set; }
		int VisibleWidth { get; }
		int VisibleHeight { get; }
		Func<int> TotalWidth { get; set; }
		Func<int> TotalHeight { get; set; }
		int ClientWidth { get; }
		int ClientHeight { get; }
		int TotalClientWidth { get; }
		int TotalClientHeight { get; }
		int DeltaDownToSelection();
		int DeltaUpToSelection();
	}
}
