using System;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    public class SigmoidKernelFunction : IKernelFunction{
        private double gamma;
        private double coef0;
        public SigmoidKernelFunction() : this(0.01, 0) {}

        public SigmoidKernelFunction(double gamma, double coef0){
            this.gamma = gamma;
            this.coef0 = coef0;
        }

        public bool UsesSquares{
            get { return false; }
        }

        public string Name{
            get { return "Sigmoid"; }
        }

        public Parameters Parameters{
            set{
                gamma = value.GetDoubleParam("Gamma").Value;
                coef0 = value.GetDoubleParam("Coef").Value;
            }
        }

        public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej){
            return Math.Tanh(gamma*xi.Dot(xj) + coef0);
        }

        public Parameters GetParameters(){
            return new Parameters(new Parameter[]{new DoubleParam("Gamma", 0.01), new DoubleParam("Coef", 0)});
        }

        public object Clone(){
            return new SigmoidKernelFunction(gamma, coef0);
        }
    }
}