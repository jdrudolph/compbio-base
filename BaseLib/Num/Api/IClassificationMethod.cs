using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IClassificationMethod{
        ClassificationModel Train(float[][] x, int[][] y, int ngroups, Parameters param);
        Parameters Parameters { get; }
        string Name { get; }
    }
}