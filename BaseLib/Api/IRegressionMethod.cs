using BaseLib.Param;

namespace BaseLib.Api{
    public interface IRegressionMethod : INamedListItem{
        RegressionModel Train(float[][] x, float[] y, Parameters param, int nthreads);
        Parameters Parameters { get; }
    }
}