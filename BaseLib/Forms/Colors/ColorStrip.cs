using System.Drawing;
using BaseLib.Forms.Base;

namespace BaseLib.Forms.Colors{
	public sealed class ColorStrip : BasicControl{
		public ColorStrip(){
			view = new ColorStripView();
			view.Activate(this);
		}

		public ColorStrip(Color c1, Color c2){
			view = new ColorStripView(c1, c2);
			view.Activate(this);
		}

		public ColorStripView GetView(){
			return (ColorStripView) view;
		}
	}
}