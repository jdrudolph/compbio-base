using BaseLib.Forms.Base;
using BaseLibS.Graph;

namespace BaseLib.Forms.Colors{
	public sealed class ColorStrip : BasicControl{
		public ColorStrip(){
			view = new ColorStripView();
			Activate(view);
		}

		public ColorStrip(Color2 c1, Color2 c2){
			view = new ColorStripView(c1, c2);
			Activate(view);
		}

		public ColorStripView GetView(){
			return (ColorStripView) view;
		}
	}
}