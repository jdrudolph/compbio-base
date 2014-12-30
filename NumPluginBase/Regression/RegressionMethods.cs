using System;
using System.Linq;
using BaseLib.Api;
using BaseLib.Param;
using BaseLibS.Param;
using BaseLibS.Util;

namespace NumPluginBase.Regression{
	public static class RegressionMethods{
		private static readonly IRegressionMethod[] allMethods = InitRegressionMethod();

		private static IRegressionMethod[] InitRegressionMethod(){
			return FileUtils.GetPlugins<IRegressionMethod>(NumPluginUtils.pluginNames, true);
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

		public static IRegressionMethod Get(int index){
			return allMethods[index];
		}

		public static IRegressionMethod GetByName(string name){
			foreach (IRegressionMethod method in allMethods.Where(method => method.Name.Equals(name))){
				return method;
			}
			throw new Exception("Unknown type: " + name);
		}
	}
}