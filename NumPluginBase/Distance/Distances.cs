﻿using BaseLibS.Api;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.Distance{
	public static class Distances{
		private static readonly IDistance[] allDistances = InitDistances();

		private static IDistance[] InitDistances(){
			return FileUtils.GetPlugins<IDistance>(NumPluginUtils.pluginNames, true);
		}

		public static SingleChoiceWithSubParams GetDistanceParameters(){
			return GetDistanceParameters("");
		}

		public static SingleChoiceWithSubParams GetDistanceParameters(string help){
			return GetDistanceParameters(help, 666, 100);
		}

		public static SingleChoiceWithSubParams GetDistanceParameters(string help, int totalWidth, int paramNameWidth){
			return new SingleChoiceWithSubParams("Distance"){
				Values = GetAllNames(),
				SubParams = GetAllParameters(),
				Value = 0,
				Help = help,
				TotalWidth = totalWidth,
				ParamNameWidth = paramNameWidth
			};
		}

		public static IDistance GetDistanceFunction(Parameters param){
			ParameterWithSubParams<int> distParam = param.GetParamWithSubParams<int>("Distance");
			return GetDistanceFunction(distParam.Value, distParam.GetSubParameters());
		}

		private static string[] GetAllNames(){
			string[] result = new string[allDistances.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allDistances[i].Name;
			}
			return result;
		}

		private static Parameters[] GetAllParameters(){
			Parameters[] result = new Parameters[allDistances.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allDistances[i].Parameters;
			}
			return result;
		}

		private static IDistance GetDistanceFunction(int index, Parameters param){
			IDistance kf = (IDistance) allDistances[index].Clone();
			kf.Parameters = param;
			return kf;
		}
	}
}