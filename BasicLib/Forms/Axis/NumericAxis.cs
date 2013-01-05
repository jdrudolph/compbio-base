using BasicLib.Forms.Base;

namespace BasicLib.Forms.Axis{
	public class NumericAxis : BasicControl{
		public NumericAxis(){
			view = new NumericAxisView();
			view.Activate(this);
		}

		public NumericAxisView GetView(){
			return (NumericAxisView) view;
		}
	}
}