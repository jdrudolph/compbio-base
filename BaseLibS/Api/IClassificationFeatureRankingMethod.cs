using BaseLibS.Param;

namespace BaseLibS.Api{
    public interface IClassificationFeatureRankingMethod : INamedListItem{
		int[] Rank(BaseVector[] x, int[][] y, int ngroups, Parameters param, IGroupDataProvider data, int nthreads);
        Parameters GetParameters(IGroupDataProvider data);
    }
}