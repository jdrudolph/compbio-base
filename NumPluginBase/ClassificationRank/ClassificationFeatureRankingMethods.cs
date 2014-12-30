using System;
using System.Linq;
using BaseLib.Param;
using BaseLibS.Api;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.ClassificationRank{
	public static class ClassificationFeatureRankingMethods{
		private static readonly IClassificationFeatureRankingMethod[] allMethods = InitRankingMethods();
		private static IClassificationFeatureRankingMethod[] InitRankingMethods() { return FileUtils.GetPlugins<IClassificationFeatureRankingMethod>(NumPluginUtils.pluginNames, true); }

		public static string[] GetAllNames(){
			string[] result = new string[allMethods.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allMethods[i].Name;
			}
			return result;
		}

		public static Parameters[] GetAllSubParameters(IGroupDataProvider data){
			Parameters[] result = new Parameters[allMethods.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allMethods[i].GetParameters(data);
			}
			return result;
		}

		public static IClassificationFeatureRankingMethod Get(int index) { return allMethods[index]; }

		public static IClassificationFeatureRankingMethod GetByName(string name){
			foreach (IClassificationFeatureRankingMethod method in allMethods.Where(method => method.Name.Equals(name))){
				return method;
			}
			throw new Exception("Unknown type: " + name);
		}
	}
}