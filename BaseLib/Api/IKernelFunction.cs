using System;
using BaseLib.Param;
using BaseLibS.Num.Vector;

namespace BaseLib.Api{
	public interface IKernelFunction : ICloneable, INamedListItem{
		bool UsesSquares { get; }
		Parameters Parameters { get; set; }
		double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej);
	}
}