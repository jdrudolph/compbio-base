using System;
using BaseLib.Num.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    [Serializable]
    public class SigmoidKernelFunction : IKernelFunction{
        private double gamma;
        private double coef;
        public SigmoidKernelFunction() : this(0.01, 0) {}

        public SigmoidKernelFunction(double gamma, double coef){
            this.gamma = gamma;
            this.coef = coef;
        }

        public bool UsesSquares{
            get { return false; }
        }

        public string Name{
            get { return "Sigmoid"; }
        }

        public Parameters Parameters{
            get { return new Parameters(new Parameter[]{new DoubleParam("Gamma", gamma), new DoubleParam("Coef", coef)}); }
            set{
                gamma = value.GetDoubleParam("Gamma").Value;
                coef = value.GetDoubleParam("Coef").Value;
            }
        }

        public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej){
            return Math.Tanh(gamma*xi.Dot(xj) + coef);
        }

        public object Clone(){
            return new SigmoidKernelFunction(gamma, coef);
        }
    }
}