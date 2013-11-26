using BaseLib.Forms.Base;

namespace BaseLib.Forms.Axis{
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