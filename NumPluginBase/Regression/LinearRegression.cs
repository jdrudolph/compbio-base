﻿using BaseLib.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace NumPluginBase.Regression{
	public class LinearRegression : IRegressionMethod{
		public RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int nthreads) { throw new System.NotImplementedException(); }
		public Parameters Parameters { get { return new Parameters(); } }
		public string Name { get { return "Linear regression"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 1; } }
		public bool IsActive { get { return true; } }
	}
}