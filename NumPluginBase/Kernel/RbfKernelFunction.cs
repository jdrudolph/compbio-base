using System;
using BaseLib.Api;
using BaseLib.Param;
using BaseLibS.Num.Vector;

namespace NumPluginBase.Kernel{
	[Serializable]
	public class RbfKernelFunction : IKernelFunction{
		private double Sigma { get; set; }
		public RbfKernelFunction() : this(1) { }
		public RbfKernelFunction(double sigma) { Sigma = sigma; }
		public bool UsesSquares { get { return true; } }
		public string Name { get { return "RBF"; } }
		public Parameters Parameters { get { return new Parameters(new DoubleParam("Sigma", Sigma){Help = "Standard deviation parameter."}); } set { Sigma = value.GetDoubleParam("Sigma").Value; } }
		public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej) { return Math.Exp(-(xSquarei + xSquarej - 2*xi.Dot(xj))/2.0/xi.Length/Sigma/Sigma); }
		public object Clone() { return new RbfKernelFunction(Sigma); }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 1; } }
		public bool IsActive { get { return true; } }
	}
}