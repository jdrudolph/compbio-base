using BaseLib.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;
using NumPluginBase.Distance;

namespace NumPluginBase.Classification{
	public class KnnClassification : IClassificationMethod{
		public ClassificationModel Train(BaseVector[] x, int[][] y, int ngroups, Parameters param, int nthreads){
			int k = param.GetIntParam("Number of neighbours").Value;
			IDistance distance = Distances.GetDistanceFunction(param);
			return new KnnClassificationModel(x, y, ngroups, k, distance);
		}

		public Parameters Parameters { get { return new Parameters(new Parameter[]{Distances.GetDistanceParameters(), new IntParam("Number of neighbours", 5)}); } }
		public string Name { get { return "KNN"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 2; } }
		public bool IsActive { get { return true; } }
	}
}