using BaseLib.Num.Api;
using BaseLib.Param;

namespace NumPluginBase.Regression{
    public class LinearRegression : IRegressionMethod{
        public RegressionModel Train(float[][] x, float[] y, Parameters param, int nthreads){
            throw new System.NotImplementedException();
        }

        public Parameters Parameters { get { return new Parameters(); } }
        public string Name { get { return "Linear regression"; } }
        public string Description { get { return ""; } }
        public float DisplayRank { get { return 0; } }
        public bool IsActive { get { return true; } }
	}
}