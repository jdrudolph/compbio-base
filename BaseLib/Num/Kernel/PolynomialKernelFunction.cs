using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    public class PolynomialKernelFunction : IKernelFunction{
        private int degree;
        private double gamma;
        private double coef0;

        public PolynomialKernelFunction(int degree, double gamma, double coef0){
            this.degree = degree;
            this.gamma = gamma;
            this.coef0 = coef0;
        }

        public PolynomialKernelFunction() : this(3, 0.01, 0) {}

        public bool UsesSquares{
            get { return false; }
        }

        public string Name{
            get { return "Polynomial"; }
        }

        public Parameters Parameters{
            set{
                degree = value.GetIntParam("Degree").Value;
                gamma = value.GetDoubleParam("Gamma").Value;
                coef0 = value.GetDoubleParam("Coef").Value;
            }
        }

        public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej){
            return Powi(gamma*xi.Dot(xj) + coef0, degree);
        }

        public Parameters GetParameters(){
            return
                new Parameters(new Parameter[]
                {new IntParam("Degree", 3), new DoubleParam("Gamma", 0.01), new DoubleParam("Coef", 0)});
        }

        public object Clone(){
            return new PolynomialKernelFunction(degree, gamma, coef0);
        }

        private static double Powi(double base1, int times){
            double tmp = base1, ret = 1.0;
            for (int t = times; t > 0; t /= 2){
                if (t%2 == 1){
                    ret *= tmp;
                }
                tmp = tmp*tmp;
            }
            return ret;
        }
    }
}