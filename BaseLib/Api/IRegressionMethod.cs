using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Api{
    public interface IRegressionMethod : INamedListItem{
		RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int nthreads);
        Parameters Parameters { get; }
    }
}