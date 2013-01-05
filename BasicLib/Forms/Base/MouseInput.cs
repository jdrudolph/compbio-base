using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BasicLib.Forms.Base{
	[Obsolete]
	public static class MouseInput{
		//TODO: should disappear eventually. Want to get rid of all pinvoke.
		// TME_HOVER 
		// The caller wants hover notification. Notification is delivered as a  
		// WM_MOUSEHOVER message.  If the caller requests hover tracking while  
		// hover tracking is already active, the hover timer will be reset. 
		private const int tmeHover = 0x1;

		private struct Trackmouseevent{
			// Size of the structure - calculated in the constructor 
			private int cbSize;
			// value that we'll set to specify we want to start over Mouse Hover and get 
			// notification when the hover has happened 
			private int dwFlags;
			// Handle to what's interested in the event 
			private IntPtr hwndTrack;
			// How long it takes for a hover to occur 
			private int dwHoverTime;
			// Setting things up specifically for a simple reset 
			public Trackmouseevent(IntPtr hWnd){
				cbSize = Marshal.SizeOf(typeof (Trackmouseevent));
				hwndTrack = hWnd;
				dwHoverTime = SystemInformation.MouseHoverTime;
				dwFlags = tmeHover;
			}
		}

		// Declaration of the Win32API function 
		[DllImport("user32")]
		private static extern bool TrackMouseEvent(ref Trackmouseevent lpEventTrack);

		[Obsolete]
		public static void ResetMouseHover(IntPtr windowTrackingMouseHandle) {
			// Set up the parameter collection for the API call so that the appropriate 
			// control fires the event 
			Trackmouseevent parameterBag = new Trackmouseevent(windowTrackingMouseHandle);
			// The actual API call 
			TrackMouseEvent(ref parameterBag);
		}
	}
}