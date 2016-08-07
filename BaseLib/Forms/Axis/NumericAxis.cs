using BaseLib.Forms.Base;

namespace BaseLib.Forms.Axis{
	public class NumericAxis : BasicControl{
		public NumericAxis(){
			view = new NumericAxisView();
			Activate(view);
		}

		public NumericAxisView GetView(){
			return (NumericAxisView) view;
		}
	}
}