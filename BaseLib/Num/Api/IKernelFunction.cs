using System;
using BaseLib.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IKernelFunction : ICloneable, INamedListItem{
        bool UsesSquares { get; }
        Parameters Parameters { get; set; }
        double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej);
    }
}