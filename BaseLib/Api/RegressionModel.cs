using System;
using BaseLib.Num.Vector;

namespace BaseLib.Api{
	[Serializable]
	public abstract class RegressionModel{
		public abstract float Predict(BaseVector x);
	}
}