using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Param;

namespace NumPluginBase.Regression{
	public class LinearRegression : IRegressionMethod{
		public RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int nthreads) { throw new System.NotImplementedException(); }
		public Parameters Parameters => new Parameters();
		public string Name => "Linear regression";
		public string Description => "";
		public float DisplayRank => 1;
		public bool IsActive => true;
	}
}