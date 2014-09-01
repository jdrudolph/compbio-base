using System;
using BaseLib.Num.Api;
using BaseLib.Num.Vector;
using NumPluginSvm.Svm;

namespace NumPluginSvm{
    [Serializable]
    public class SvmRegressionModel : RegressionModel{
        private readonly SvmModel model;

        public SvmRegressionModel(SvmModel model){
            this.model = model;
        }

        public override float Predict(float[] x){
            return SvmMain.SvmPredict(model, new FloatArrayVector(x));
        }
    }
}