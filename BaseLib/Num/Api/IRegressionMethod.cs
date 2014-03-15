using BaseLib.Api;
using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IRegressionMethod : INamedListItem{
        RegressionModel Train(float[][] x, float[] y, Parameters param, int nthreads);
        Parameters Parameters { get; }
    }
}