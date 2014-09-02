using BaseLib.Api;
using BaseLib.Param;
using NumPluginBase.Distance;

namespace NumPluginBase.Classification{
    public class KnnClassification : IClassificationMethod{
        public ClassificationModel Train(float[][] x, int[][] y, int ngroups, Parameters param, int nthreads){
            int k = param.GetIntParam("Number of neighbours").Value;
			//TODO
            return new KnnClassificationModel(x, y, ngroups, k, new EuclideanDistance());
        }

        public Parameters Parameters { get { return new Parameters(new Parameter[]{new IntParam("Number of neighbours", 5)}); } }
        public string Name { get { return "KNN"; } }
        public string Description { get { return ""; } }
        public float DisplayRank { get { return 0; } }
        public bool IsActive { get { return true; } }
	}
}