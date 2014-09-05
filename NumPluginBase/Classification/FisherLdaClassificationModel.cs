using System;
using BaseLib.Api;
using BaseLib.Num;
using BaseLib.Num.Vector;
using NumPluginBase.Distance;

namespace NumPluginBase.Classification{
	[Serializable]
	public class FisherLdaClassificationModel : ClassificationModel{
		private readonly double[,] projection;
		private readonly double[][] projectedGroupMeans;
		private readonly int ngroups;

		public FisherLdaClassificationModel(double[,] projection, double[][] projectedGroupMeans, int ngroups){
			this.projection = projection;
			this.projectedGroupMeans = projectedGroupMeans;
			this.ngroups = ngroups;
		}

		public override float[] PredictStrength(BaseVector x){
			double[] projectedTest = MatrixUtils.VectorTimesMatrix(x, projection);
			float[] distances = new float[ngroups];
			IDistance distance = new EuclideanDistance();
			for (int j = 0; j < ngroups; j++){
				distances[j] = -(float) distance.Get(projectedTest, projectedGroupMeans[j]);
			}
			return distances;
		}
	}
}