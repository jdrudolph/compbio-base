using System;
using BaseLib.Api;
using BaseLib.Param;
using BaseLibS.Num.Vector;
using BaseLibS.Param;

namespace NumPluginBase.Kernel{
	[Serializable]
	public class LinearKernelFunction : IKernelFunction{
		public bool UsesSquares { get { return false; } }
		public string Name { get { return "Linear"; } }
		public Parameters Parameters { set { } get { return new Parameters(); } }
		public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej) { return xi.Dot(xj); }
		public object Clone() { return new LinearKernelFunction(); }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 0; } }
		public bool IsActive { get { return true; } }
	}
}