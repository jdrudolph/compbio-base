using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;

namespace BaseLib.Wpf{
	/// <summary>This class is a Windows Forms host which supports being inside a scroll container.</summary>
	/// <remarks>When you nest Windows Forms controls inside a WPF visual container the Windows Forms control actually 
	/// sits on top of the WPF window and maintains its own HWND. If the container is a scroll container (ScrollView),
	/// the clipping will not be done correctly. This windows forms sub class does the clipping.</remarks>
	public class ScrollableWindowsFormsHost : WindowsFormsHost{
		/// <summary>Top left x position of window region</summary>
		private int topLeftX = -1;

		/// <summary>Top left y position of window region</summary>
		private int topLeftY = -1;

		/// <summary>Bottom right x position of window region</summary>
		private int bottomRightX = -1;

		/// <summary>Bottom right x position of window region</summary>
		private int bottomRightY = -1;

		/// <summary>Scrollviewer on which scrollchanged event this instance listens.</summary>
		private ScrollViewer scrollViewer;

		/// <summary>Track whether Dispose has been called.</summary>
		private bool disposed;

		/// <summary>Creates a handle to a region specified through upper left and bottom right corner.</summary>
		/// <param name="x1">Upper left X</param>
		/// <param name="y1">Upper left Y</param>
		/// <param name="x2">Bottom right X</param>
		/// <param name="y2">Bottom right Y</param>
		/// <returns>Returns a handle to the created region.</returns>
		[DllImport("GDI32.DLL", EntryPoint = "CreateRectRgn")] private static extern IntPtr CreateRectRgn(Int32 x1, Int32 y1,
			Int32 x2, Int32 y2);

		/// <summary>Sets the specified region to the specified window. That means the specified window can paint
		/// itself only in the specified region.</summary>
		/// <param name="hWnd">Handle to the window</param>
		/// <param name="hRgn">Hanlde to the region</param>
		/// <param name="bRedraw">Boolean indicating whether the specified region should be redrawn.</param>
		/// <returns>Returns nonzero if success, otherwise zero.</returns>
		[DllImport("User32.dll", SetLastError = true)] private static extern Int32 SetWindowRgn(IntPtr hWnd, IntPtr hRgn,
			Boolean bRedraw);

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <param name="bDisposing"><b>true</b> if the method has been called by applicaton code.</param>
		protected override void Dispose(Boolean bDisposing){
			if (bDisposing){
				try{
					if (!disposed){
						if (scrollViewer != null){
							scrollViewer.ScrollChanged -= ScrollHandler;
						}
					}
					disposed = true;
				} finally{
					base.Dispose(true);
				}
			}
		}

		/// <summary>Register a scrollviewer on which this instance listens.</summary>
		/// <param name="viewer">ScrollViewer</param>
		public void RegisterScrollViewer(ScrollViewer viewer){
			if (viewer == null){
				throw new ArgumentNullException("scollViewer");
			}
			if (scrollViewer != null){
				UnregisterScrollViewer(scrollViewer);
			}
			viewer.ScrollChanged += ScrollHandler;
			scrollViewer = viewer;
			// If you move focus inside a scrollviewer the scrollviewer adjusts its scroll value that the focused element is visible.
			// Unfortunately with a windows forms host this doesn't work.
			// So we register a got focus event and adjust the scrol value ourself.
			GotFocus += ScrollableWindowsFormsHostGotFocus;
		}

		/// <summary>Unregister a scrollviewer.</summary>
		/// <param name="viewer">ScrollViewer</param>
		public void UnregisterScrollViewer(ScrollViewer viewer){
			if (viewer == null){
				throw new ArgumentNullException("scollViewer");
			}
			viewer.ScrollChanged -= ScrollHandler;
			GotFocus -= ScrollableWindowsFormsHostGotFocus;
			scrollViewer = null;
		}

		/// <summary>Scroll handler manages the clipping of this windows forms host.</summary>
		/// <param name="sender">Sender</param>
		/// <param name="ea">Event argument</param>
		private void ScrollHandler(Object sender, ScrollChangedEventArgs ea){
			PresentationSource presentationSource = PresentationSource.FromVisual(this);
			if (presentationSource == null){
				return;
			}
			Visual rootVisual = presentationSource.RootVisual;
			if (rootVisual == null){
				return;
			}
			ScrollViewer viewer = (ScrollViewer) sender;
			if (!viewer.IsDescendantOf(rootVisual)){
				return;
			}
			// calculate the rect of scrollview with 0/0 at upper left corner of root visual
			GeneralTransform transform = viewer.TransformToAncestor(rootVisual);
			Rect scrollRect = transform.TransformBounds(new Rect(0, 0, viewer.ViewportWidth, viewer.ViewportHeight));
			// calculate the rect of the scrollable windows forms host instance with 0/0 at upper left corner of root visual
			transform = TransformToAncestor(rootVisual);
			Rect hostRect = transform.TransformBounds(new Rect(Padding.Left, Padding.Right, RenderSize.Width, RenderSize.Height));
			// calculate the intersection of the two rect
			Rect intersectRect = Rect.Intersect(scrollRect, hostRect);
			int topLeftX1 = 0;
			int topLeftY1 = 0;
			int bottomRightX1 = 0;
			int bottomRightY1 = 0;
			if (intersectRect != Rect.Empty){
				// calculate the HRGN points with 0/0 at upper left corner of scrollable windows forms host instance
				topLeftX1 = (Int32) (intersectRect.TopLeft.X - hostRect.TopLeft.X);
				topLeftY1 = (Int32) (intersectRect.TopLeft.Y - hostRect.TopLeft.Y);
				bottomRightX1 = (Int32) (intersectRect.BottomRight.X - hostRect.TopLeft.X);
				bottomRightY1 = (Int32) (intersectRect.BottomRight.Y - hostRect.TopLeft.Y);
			}
			// because the CreateRectRgn / SetWindowRgn api calls are slow we call them only if it has a visual effect
			if (topLeftX != topLeftX1 || topLeftY != topLeftY1 || bottomRightX != bottomRightX1 || bottomRightY != bottomRightY1){
				topLeftX = topLeftX1;
				topLeftY = topLeftY1;
				bottomRightX = bottomRightX1;
				bottomRightY = bottomRightY1;
				// create HRGN object and set it to the windows forms host instance
				IntPtr hrgn = CreateRectRgn(topLeftX, topLeftY, bottomRightX, bottomRightY);
				SetWindowRgn(Handle, hrgn, true);
			}
		}

		/// <summary>Occurs on got focus and adjusts the scroll value of the registered scrollviewer that this is visible.</summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event argument</param>
		private void ScrollableWindowsFormsHostGotFocus(Object sender, RoutedEventArgs e){
			Debug.Assert(scrollViewer != null);
			// calculate the rect of this windows forms host instance with 0/0 at upper left corner of scrollviewer
			GeneralTransform transform = TransformToAncestor(scrollViewer);
			Rect hostRect = transform.TransformBounds(new Rect(Padding.Left, Padding.Right, RenderSize.Width, RenderSize.Height));
			// if this element is not visible scroll to an offset which makes this windows forms host visible
			if (hostRect.Bottom > scrollViewer.ViewportHeight){
				scrollViewer.ScrollToVerticalOffset(hostRect.Bottom - scrollViewer.ViewportHeight + scrollViewer.VerticalOffset);
			}
		}
	}
}