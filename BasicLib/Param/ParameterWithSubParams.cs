using System;
using System.Collections.Generic;

namespace BasicLib.Param{
	[Serializable]
	public abstract class ParameterWithSubParams : Parameter{
		protected ParameterWithSubParams(string name) : base(name) {}
		public abstract Parameters GetSubParameters();

		public IntParam[] GetAllIntSubParams(){
			List<IntParam> result = new List<IntParam>();
			AddIntSubParams(result, GetSubParameters());
			return result.ToArray();
		}

		public DoubleParam[] GetAllDoubleSubParams(){
			List<DoubleParam> result = new List<DoubleParam>();
			AddDoubleSubParams(result, GetSubParameters());
			return result.ToArray();
		}

		private static void AddIntSubParams(ICollection<IntParam> result, Parameters sp){
			foreach (Parameter p in sp.GetAllParameters()){
				if (p is IntParam){
					result.Add((IntParam) p);
				} else if (p is ParameterWithSubParams){
					AddIntSubParams(result, ((ParameterWithSubParams) p).GetSubParameters());
				}
			}
		}

		private static void AddDoubleSubParams(ICollection<DoubleParam> result, Parameters sp){
			foreach (Parameter p in sp.GetAllParameters()){
				if (p is DoubleParam){
					result.Add((DoubleParam) p);
				} else if (p is ParameterWithSubParams){
					AddDoubleSubParams(result, ((ParameterWithSubParams) p).GetSubParameters());
				}
			}
		}
	}
}