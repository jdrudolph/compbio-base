using System;

namespace BaseLibS.Api{
	[Serializable]
	public abstract class RegressionModel{
		public abstract float Predict(BaseVector x);
	}
}