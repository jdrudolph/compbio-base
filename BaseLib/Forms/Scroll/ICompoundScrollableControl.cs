using System;
using BaseLib.Forms.Base;
using BaseLib.Graphic;

namespace BaseLib.Forms.Scroll{
	public interface ICompoundScrollableControl: IScrollableControl{
		int RowHeaderWidth { get; set; }
		int ColumnHeaderHeight { get; set; }
		bool Enabled { get; }
		void Invalidate(bool p0);
		Action<BasicMouseEventArgs> OnMouseIsDownMainView { get; set; }
		Action<BasicMouseEventArgs> OnMouseDraggedRowHeaderView { get; set; }
		Action<BasicMouseEventArgs> OnMouseDraggedMainView { get; set; }
		Action<BasicMouseEventArgs> OnMouseDraggedColumnHeaderView { get; set; }
		Action<BasicMouseEventArgs> OnMouseMoveMainView { get; set; }
		Action<BasicMouseEventArgs> OnMouseDoubleClickMainView { get; set; }
		Action<BasicMouseEventArgs> OnMouseIsDownRowHeaderView { get; set; }
		Action<BasicMouseEventArgs> OnMouseMoveColumnHeaderView { get; set; }
		Action<BasicMouseEventArgs> OnMouseIsUpColumnHeaderView { get; set; }
		Action<BasicMouseEventArgs> OnMouseIsUpCornerView { get; set; }
		Action<BasicMouseEventArgs> OnMouseIsDownColumnHeaderView { get; set; }
		Action<BasicMouseEventArgs> OnMouseIsDownCornerView { get; set; }
		Action<BasicMouseEventArgs> OnMouseMoveCornerView { get; set; }
		Action<IGraphics, int, int> OnPaintRowHeaderView { get; set; }
		Action<EventArgs> OnMouseHoverMainView { get; set; }
		Action<IGraphics> OnPaintCornerView { get; set; }
		Action<IGraphics, int, int> OnPaintColumnHeaderView { get; set; }
		Action<IGraphics, int, int, int, int> OnPaintMainView { get; set; }
		void InvalidateColumnHeaderView();
		void InvalidateCornerView();
		void MoveDown(int rowHeight);
		void MoveUp(int rowHeight);
	}
}