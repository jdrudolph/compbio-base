using System;
using System.Linq;
using BaseLibS.Api;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.Classification{
	public static class ClassificationMethods{
		private static readonly IClassificationMethod[] allMethods = InitClassificationMethod();

		private static IClassificationMethod[] InitClassificationMethod(){
			return FileUtils.GetPlugins<IClassificationMethod>(NumPluginUtils.pluginNames, true);
		}

		public static string[] GetAllNames(){
			string[] result = new string[allMethods.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allMethods[i].Name;
			}
			return result;
		}

		public static Parameters[] GetAllSubParameters(){
			Parameters[] result = new Parameters[allMethods.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allMethods[i].Parameters;
			}
			return result;
		}

		public static IClassificationMethod Get(int index){
			return allMethods[index];
		}

		public static IClassificationMethod GetByName(string name){
			foreach (IClassificationMethod method in allMethods.Where(method => method.Name.Equals(name))){
				return method;
			}
			throw new Exception("Unknown type: " + name);
		}
	}
}