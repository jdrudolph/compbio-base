namespace BasicLib.Forms.Scatter{
	public class ScatterPlotValues{
		public double[] SingleValues { get; private set; }
		public double[][] MultiValues { get; private set; }
		public bool IsMulti { get; private set; }

		public ScatterPlotValues(double[] vals){
			SingleValues = vals;
			MultiValues = null;
			IsMulti = false;
		}

		public ScatterPlotValues(double[][] vals){
			SingleValues = null;
			MultiValues = vals;
			IsMulti = true;
		}

		public int Length { get { return IsMulti ? MultiValues.Length : SingleValues.Length; } }
	}
}