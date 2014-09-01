using BaseLib.Param;

namespace BaseLib.Api{
    public interface IClassificationMethod : INamedListItem{
        ClassificationModel Train(float[][] x, int[][] y, int ngroups, Parameters param, int nthreads);
        Parameters Parameters { get; }
    }
}