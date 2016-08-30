using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Param;
using NumPluginBase.Distance;

namespace NumPluginBase.Regression{
	public class KnnRegression : IRegressionMethod{
		public RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int ntheads){
			int k = param.GetParam<int>("Number of neighbours").Value;
			IDistance distance = Distances.GetDistanceFunction(param);
			return new KnnRegressionModel(x, y, k, distance);
		}

		public Parameters Parameters => new Parameters(Distances.GetDistanceParameters(), new IntParam("Number of neighbours", 5));
		public string Name => "KNN";
		public string Description => "";
		public float DisplayRank => 2;
		public bool IsActive => true;
	}
}