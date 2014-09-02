using System;
using BaseLib.Api;
using BaseLib.Num;
using NumPluginBase.Distance;

namespace Utils.Num.Classification{
	[Serializable]
	public class FisherLdaClassificationModel : ClassificationModel{
		private readonly float[,] projection;
		private readonly float[][] projectedGroupMeans;
		private readonly int ngroups;

		public FisherLdaClassificationModel(float[,] projection, float[][] projectedGroupMeans, int ngroups) {
			this.projection = projection;
			this.projectedGroupMeans = projectedGroupMeans;
			this.ngroups = ngroups;
		}

		public override float[] PredictStrength(float[] x) {
			float[] projectedTest = MatrixUtils.VectorTimesMatrix(x, projection);
			float[] distances = new float[ngroups];
			IDistance distance = new EuclideanDistance();
			for (int j = 0; j < ngroups; j++){
				distances[j] = -(float)distance.Get(projectedTest, projectedGroupMeans[j]);
			}
			return distances;
		}
	}
}