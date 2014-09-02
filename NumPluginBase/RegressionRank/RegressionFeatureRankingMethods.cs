using System;
using System.Linq;
using BaseLib.Api;
using BaseLib.Param;

namespace Utils.Num.Regression{
    public static class RegressionFeatureRankingMethods{
        private static readonly IRegressionFeatureRankingMethod[] allMethods = new IRegressionFeatureRankingMethod[]
        {};

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

        public static IRegressionFeatureRankingMethod Get(int index){
            return allMethods[index];
        }

        public static IRegressionFeatureRankingMethod GetByName(string name){
            foreach (IRegressionFeatureRankingMethod method in allMethods.Where(method => method.Name.Equals(name))){
                return method;
            }
            throw new Exception("Unknown type: " + name);
        }
    }
}