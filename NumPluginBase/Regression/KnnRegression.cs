using BaseLib.Num.Api;
using BaseLib.Param;

namespace NumPluginBase.Regression{
    public class KnnRegression : IRegressionMethod{
        public RegressionModel Train(float[][] x, float[] y, Parameters param, int ntheads){
            int k = param.GetIntParam("Number of neighbours").Value;
            return new KnnRegressionModel(x, y, k);
        }

        public Parameters Parameters { get { return new Parameters(new Parameter[]{new IntParam("Number of neighbours", 5)}); } }
        public string Name { get { return "KNN"; } }
        public string Description { get { return ""; } }
        public float DisplayRank { get { return 0; } }
        public bool IsActive { get { return true; } }
    }
}