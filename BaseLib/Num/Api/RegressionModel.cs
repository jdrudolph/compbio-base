using System;

namespace BaseLib.Num.Api{
	[Serializable]
	public abstract class RegressionModel{
		public abstract float Predict(float[] x);
	}
}