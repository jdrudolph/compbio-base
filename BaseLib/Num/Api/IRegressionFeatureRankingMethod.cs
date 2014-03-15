using BaseLib.Api;
using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IRegressionFeatureRankingMethod: INamedListItem{
        int[] Rank(float[][] x, float[] y, Parameters param, IGroupDataProvider data, int nthreads);
        Parameters GetParameters(IGroupDataProvider data);
    }
}