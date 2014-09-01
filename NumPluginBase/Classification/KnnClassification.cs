using BaseLib.Num.Api;
using BaseLib.Param;

namespace NumPluginBase.Classification{
    public class KnnClassification : IClassificationMethod{
        public ClassificationModel Train(float[][] x, int[][] y, int ngroups, Parameters param, int nthreads){
            int k = param.GetIntParam("Number of neighbours").Value;
            return new KnnClassificationModel(x, y, ngroups, k);
        }

        public Parameters Parameters { get { return new Parameters(new Parameter[]{new IntParam("Number of neighbours", 5)}); } }
        public string Name { get { return "KNN"; } }
        public string Description { get { return ""; } }
        public float DisplayRank { get { return 0; } }
        public bool IsActive { get { return true; } }
	}
}