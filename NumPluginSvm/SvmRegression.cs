using BaseLibS.Api;
using BaseLibS.Num.Vector;
using BaseLibS.Param;
using NumPluginBase.Kernel;
using NumPluginSvm.Svm;

namespace NumPluginSvm{
	public class SvmRegression : IRegressionMethod{
		public RegressionModel Train(BaseVector[] x, float[] y, Parameters param, int nthreads){
			ParameterWithSubParams<int> kernelParam = param.GetParamWithSubParams<int>("Kernel");
			SvmParameter sp = new SvmParameter{
				kernelFunction = KernelFunctions.GetKernelFunction(kernelParam.Value, kernelParam.GetSubParameters()),
				svmType = SvmType.EpsilonSvr,
				c = param.GetParam<double>("C").Value
			};
			SvmModel model = SvmMain.SvmTrain(new SvmProblem(x, y), sp);
			return new SvmRegressionModel(model);
		}

		public Parameters Parameters
			=>
				new Parameters(KernelFunctions.GetKernelParameters(), new DoubleParam("C", 100){Help = SvmClassification.cHelp});

		public string Name => "Support vector machine";
		public string Description => "";
		public float DisplayRank => 0;
		public bool IsActive => true;
	}
}