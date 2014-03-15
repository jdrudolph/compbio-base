using System;
using System.Drawing;
using BaseLib.Num.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    [Serializable]
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
        public string Description { get { return ""; } }
        public float DisplayOrder { get { return 0; } }
        public bool IsActive { get { return true; } }
        public Bitmap DisplayImage { get { return null; } }
    }
}