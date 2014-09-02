using BaseLib.Api;
using BaseLib.Param;
using BaseLib.Util;

namespace NumPluginBase.Distance{
	public static class Distances{
		private static readonly IDistance[] allDistances = InitDistances();
		private static IDistance[] InitDistances() { return FileUtils.GetPlugins<IDistance>(NumPluginUtils.pluginNames, true); }

		public static string[] GetAllNames(){
			string[] result = new string[allDistances.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allDistances[i].Name;
			}
			return result;
		}

		public static Parameters[] GetAllParameters(){
			Parameters[] result = new Parameters[allDistances.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allDistances[i].Parameters;
			}
			return result;
		}

		public static SingleChoiceWithSubParams GetDistanceParameters() { return new SingleChoiceWithSubParams("Distance"){Values = GetAllNames(), SubParams = GetAllParameters(), Value = 0}; }

		public static IDistance GetDistanceFunction(Parameters param){
			SingleChoiceWithSubParams distParam = param.GetSingleChoiceWithSubParams("Distance");
			return GetDistanceFunction(distParam.Value, distParam.GetSubParameters());
		}

		public static IDistance GetDistanceFunction(int index, Parameters param){
			IDistance kf = ((IDistance) allDistances[index].Clone());
			kf.Parameters = param;
			return kf;
		}
	}
}