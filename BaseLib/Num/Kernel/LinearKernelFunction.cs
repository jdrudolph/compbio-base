using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    public class LinearKernelFunction : IKernelFunction{
        public bool UsesSquares{
            get { return false; }
        }

        public string Name{
            get { return "Linear"; }
        }

        public Parameters Parameters{
            set { }
            get { return new Parameters(); }
        }

        public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej){
            return xi.Dot(xj);
        }

        public object Clone(){
            return new LinearKernelFunction();
        }
    }
}