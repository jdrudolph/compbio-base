using System;
using BaseLib.Num.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    [Serializable]
    public class PolynomialKernelFunction : IKernelFunction{
        private int degree;
        private double gamma;
        private double coef;

        public PolynomialKernelFunction(int degree, double gamma, double coef){
            this.degree = degree;
            this.gamma = gamma;
            this.coef = coef;
        }

        public PolynomialKernelFunction() : this(3, 0.01, 0) {}

        public bool UsesSquares{
            get { return false; }
        }

        public string Name{
            get { return "Polynomial"; }
        }

        public Parameters Parameters{
            get{
                return
                    new Parameters(new Parameter[]
                    {new IntParam("Degree", degree), new DoubleParam("Gamma", gamma), new DoubleParam("Coef", coef)});
            }
            set{
                degree = value.GetIntParam("Degree").Value;
                gamma = value.GetDoubleParam("Gamma").Value;
                coef = value.GetDoubleParam("Coef").Value;
            }
        }

        public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej){
            return Powi(gamma*xi.Dot(xj) + coef, degree);
        }

        public object Clone(){
            return new PolynomialKernelFunction(degree, gamma, coef);
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