using BaseLib.Api;
using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IClassificationFeatureRankingMethod : INamedListItem{
        int[] Rank(float[][] x, int[][] y, int ngroups, Parameters param, IGroupDataProvider data, int nthreads);
        Parameters GetParameters(IGroupDataProvider data);
    }
}