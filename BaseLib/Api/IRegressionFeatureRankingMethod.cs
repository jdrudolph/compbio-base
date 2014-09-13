using BaseLib.Param;
using BaseLibS.Num.Vector;

namespace BaseLib.Api{
    public interface IRegressionFeatureRankingMethod: INamedListItem{
		int[] Rank(BaseVector[] x, float[] y, Parameters param, IGroupDataProvider data, int nthreads);
        Parameters GetParameters(IGroupDataProvider data);
    }
}