using BaseLib.Api;
using BaseLib.Param;
using BaseLib.Util;

namespace NumPluginBase.Kernel{
	public static class KernelFunctions{
		private static readonly string[] pluginNames = new[] { "NUMPLUGIN*.DLL" };
		private static readonly IKernelFunction[] allKernelFunctions = InitKernels();
		private static IKernelFunction[] InitKernels(){
			return FileUtils.GetPlugins<IKernelFunction>(pluginNames, true);
		}

		public static string[] GetAllNames(){
			string[] result = new string[allKernelFunctions.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allKernelFunctions[i].Name;
			}
			return result;
		}

		public static Parameters[] GetAllParameters(){
			Parameters[] result = new Parameters[allKernelFunctions.Length];
			for (int i = 0; i < result.Length; i++){
				result[i] = allKernelFunctions[i].Parameters;
			}
			return result;
		}

		public static SingleChoiceWithSubParams GetKernelParameters(){
			return new SingleChoiceWithSubParams("Kernel"){Values = GetAllNames(), SubParams = GetAllParameters(), Value = 0};
		}

		public static IKernelFunction GetKernelFunction(int index, Parameters param){
			IKernelFunction kf = ((IKernelFunction) allKernelFunctions[index].Clone());
			kf.Parameters = param;
			return kf;
		}
	}
}