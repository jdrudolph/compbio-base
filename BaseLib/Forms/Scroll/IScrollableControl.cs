using System;
using BaseLib.Forms.Base;

namespace BaseLib.Forms.Scroll {
	public interface IScrollableControl : IPrintable{
		Func<int> TotalWidth { get; set; }
		Func<int> TotalHeight { get; set; }
		Func<int> DeltaX { get; set; }
		Func<int> DeltaY { get; set; }
		int VisibleX { get; set; }
		int VisibleY { get; set; }
		int VisibleWidth { get; }
		int VisibleHeight { get; }
		int ClientWidth { get; }
		int ClientHeight { get; }
		int TotalClientWidth { get; }
		int TotalClientHeight { get; }
		int DeltaDownToSelection();
		int DeltaUpToSelection();
	}
}
