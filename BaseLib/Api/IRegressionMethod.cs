using BaseLib.Param;
using BaseLibS.Api;
using BaseLibS.Num.Vector;

namespace BaseLib.Api{
    public interface IRegressionMethod : INamedListItem{
		RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int nthreads);
        Parameters Parameters { get; }
    }
}