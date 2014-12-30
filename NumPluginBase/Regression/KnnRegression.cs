﻿using BaseLib.Param;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Param;
using NumPluginBase.Distance;

namespace NumPluginBase.Regression{
	public class KnnRegression : IRegressionMethod{
		public RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int ntheads){
			int k = param.GetParam<int>("Number of neighbours").Value;
			IDistance distance = Distances.GetDistanceFunction(param);
			return new KnnRegressionModel(x, y, k, distance);
		}

		public Parameters Parameters { get { return new Parameters(new Parameter[]{Distances.GetDistanceParameters(), new IntParam("Number of neighbours", 5)}); } }
		public string Name { get { return "KNN"; } }
		public string Description { get { return ""; } }
		public float DisplayRank { get { return 2; } }
		public bool IsActive { get { return true; } }
	}
}