using BaseLib.Param;

namespace BaseLib.Api{
    public interface IRegressionFeatureRankingMethod: INamedListItem{
        int[] Rank(float[][] x, float[] y, Parameters param, IGroupDataProvider data, int nthreads);
        Parameters GetParameters(IGroupDataProvider data);
    }
}