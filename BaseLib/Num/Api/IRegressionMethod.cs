using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IRegressionMethod{
        RegressionModel Train(float[][] x, float[] y, Parameters param);
        Parameters Parameters { get; }
        string Name { get; }
    }
}