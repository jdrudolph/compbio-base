using System.Collections.Generic;
using BaseLibS.Api;

namespace NumPluginSvm.Svm{
	public class SvmProblem{
		public BaseVector[] x;
		public float[] y;

		public SvmProblem(IList<BaseVector> x, float[] y){
			this.x = new BaseVector[x.Count];
			this.y = y;
			for (int i = 0; i < this.x.Length; i++){
				this.x[i] = x[i];
			}
		}

		public SvmProblem() { }
		public int Count { get { return x.Length; } }

		public SvmProblem Copy(){
			SvmProblem newProb = new SvmProblem{x = new BaseVector[Count], y = new float[Count]};
			for (int i = 0; i < Count; ++i){
				newProb.x[i] = x[i].Copy();
				newProb.y[i] = y[i];
			}
			return newProb;
		}

		public SvmProblem ExtractFeatures(int[] indices){
			SvmProblem reducedData = new SvmProblem{x = new BaseVector[Count], y = new float[Count]};
			for (int i = 0; i < Count; i++){
				reducedData.x[i] = x[i].SubArray(indices);
				reducedData.y[i] = y[i];
			}
			return reducedData;
		}
	}
}