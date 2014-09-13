using System;
using BaseLib.Api;
using BaseLib.Param;
using BaseLibS.Num.Vector;

namespace NumPluginBase.Kernel{
	[Serializable]
	public class PolynomialKernelFunction : IKernelFunction{
		public int Degree { get; set; }
		public double Gamma { get; set; }
		public double Coef { get; set; }

		public PolynomialKernelFunction(int degree, double gamma, double coef){
			Degree = degree;
			Gamma = gamma;
			Coef = coef;
		}

		public PolynomialKernelFunction() : this(3, 0.01, 0) { }
		public bool UsesSquares { get { return false; } }
		public string Name { get { return "Polynomial"; } }

		public Parameters Parameters{
			get{
				return
					new Parameters(new Parameter[]
					{new IntParam("Degree", Degree), new DoubleParam("Gamma", Gamma), new DoubleParam("Coef", Coef)});
			}
			set{
				Degree = value.GetIntParam("Degree").Value;
				Gamma = value.GetDoubleParam("Gamma").Value;
				Coef = value.GetDoubleParam("Coef").Value;
			}
		}

		public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej) { return Powi(Gamma*xi.Dot(xj) + Coef, Degree); }
		public object Clone() { return new PolynomialKernelFunction(Degree, Gamma, Coef); }

		private static double Powi(double base1, int times){
			double tmp = base1;
			double ret = 1.0;
			for (int t = times; t > 0; t /= 2){
				if (t%2 == 1){
					ret *= tmp;
				}
				tmp = tmp*tmp;
			}
			return ret;
		}

		public string Description { get { return ""; } }
		public float DisplayRank { get { return 2; } }
		public bool IsActive { get { return true; } }
	}
}