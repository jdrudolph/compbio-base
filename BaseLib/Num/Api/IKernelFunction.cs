using System;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Api{
    public interface IKernelFunction : ICloneable{
        bool UsesSquares { get; }
        string Name { get; }
        Parameters Parameters { get; set; }
        double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej);
    }
}