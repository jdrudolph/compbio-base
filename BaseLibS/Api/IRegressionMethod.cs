using BaseLibS.Num.Vector;
using BaseLibS.Param;

namespace BaseLibS.Api{
    public interface IRegressionMethod : INamedListItem{
		RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int nthreads);
        Parameters Parameters { get; }
    }
}