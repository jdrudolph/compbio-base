using System;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    public interface IKernelFunction : ICloneable{
        bool UsesSquares { get; }
        string Name { get; }
        Parameters Parameters { set; }
        double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej);
        Parameters GetParameters();
    }
}