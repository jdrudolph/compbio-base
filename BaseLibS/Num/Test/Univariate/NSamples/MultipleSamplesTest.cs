namespace BaseLibS.Num.Test.Univariate.NSamples{
	public abstract class MultipleSamplesTest : UnivariateTest{
		public static MultipleSamplesTest[] allTests = {new OneWayAnovaTest()};
		//, new KruskalWallisTest()
		public static string[] allNames;

		static MultipleSamplesTest(){
			allNames = new string[allTests.Length];
			for (int i = 0; i < allNames.Length; i++){
				allNames[i] = allTests[i].Name;
			}
		}

		public abstract double Test(double[][] data, out double statistic, double s0, out double pvalS0, out double[] gmeans);

		public double Test(double[][] data, out double statistic, double s0, out double pvalS0){
			double[] gmeans;
			return Test(data, out statistic, s0, out pvalS0, out gmeans);
		}

		public double Test(double[][] data, out double statistic){
			double dummy;
			return Test(data, out statistic, 0.0, out dummy);
		}
	}
}