using System;
using System.Collections.Generic;

namespace BasicLib.ParamWf{
	[Serializable]
	public abstract class ParameterWithSubParamsWf : ParameterWf{
		protected ParameterWithSubParamsWf(string name) : base(name) {}
		public abstract ParametersWf GetSubParameters();

		public IntParamWf[] GetAllIntSubParams(){
			List<IntParamWf> result = new List<IntParamWf>();
			AddIntSubParams(result, GetSubParameters());
			return result.ToArray();
		}

		public DoubleParamWf[] GetAllDoubleSubParams(){
			List<DoubleParamWf> result = new List<DoubleParamWf>();
			AddDoubleSubParams(result, GetSubParameters());
			return result.ToArray();
		}

		private static void AddIntSubParams(ICollection<IntParamWf> result, ParametersWf sp){
			foreach (ParameterWf p in sp.GetAllParameters()){
				if (p is IntParamWf){
					result.Add((IntParamWf) p);
				} else if (p is ParameterWithSubParamsWf){
					AddIntSubParams(result, ((ParameterWithSubParamsWf) p).GetSubParameters());
				}
			}
		}

		private static void AddDoubleSubParams(ICollection<DoubleParamWf> result, ParametersWf sp){
			foreach (ParameterWf p in sp.GetAllParameters()){
				if (p is DoubleParamWf){
					result.Add((DoubleParamWf) p);
				} else if (p is ParameterWithSubParamsWf){
					AddDoubleSubParams(result, ((ParameterWithSubParamsWf) p).GetSubParameters());
				}
			}
		}
	}
}