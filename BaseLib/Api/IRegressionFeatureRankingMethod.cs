using BaseLib.Param;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Param;

namespace BaseLib.Api{
    public interface IRegressionFeatureRankingMethod: INamedListItem{
		int[] Rank(BaseVector[] x, float[] y, Parameters param, IGroupDataProvider data, int nthreads);
        Parameters GetParameters(IGroupDataProvider data);
    }
}