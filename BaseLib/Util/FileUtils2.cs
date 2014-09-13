using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BaseLib.Api;
using BaseLibS.Util;

namespace BaseLib.Util{
	/// <summary>
	/// Static class containing utility routines for accessing and handling files.
	/// </summary>
	public static class FileUtils2{
		public static string executableFile = Application.ExecutablePath;
		public static string executablePath = Path.GetDirectoryName(executableFile);
		public static string GetConfigPath() { return Path.Combine(Path.GetDirectoryName(executableFile), "conf"); }
		public static string GetContaminantFilePath() { return Path.Combine(GetConfigPath(), "contaminants.fasta"); }
		public static string GetContaminantParseRule() { return ">([^ ]*)"; }

		public static T[] GetPlugins<T>(string[] filenames, bool onlyActive) where T : INamedListItem{
			IEnumerable<string> pluginFiles = GetPluginFiles(filenames);
			List<T> result = new List<T>();
			foreach (string pluginFile in pluginFiles){
				string n = pluginFile.Substring(pluginFile.LastIndexOf("\\", StringComparison.InvariantCulture) + 1,
					pluginFile.IndexOf(".dll", StringComparison.InvariantCulture) -
						pluginFile.LastIndexOf("\\", StringComparison.InvariantCulture) - 1);
				Assembly ass = Assembly.Load(n);
				Type[] types;
				try{
					types = ass.GetTypes();
				} catch (Exception){
					continue;
				}
				foreach (Type t in types){
					try{
						object o = Activator.CreateInstance(t);
						if (o is T){
							T x = (T) o;
							if (!onlyActive || x.IsActive){
								result.Add(x);
							}
						}
					} catch (Exception){}
				}
			}
			return Sort(result.ToArray());
		}

		public static T[] GetPluginsOfType<T>(IList<INamedListItem> plugins){
			List<T> result = new List<T>();
			foreach (INamedListItem t in plugins){
				if (t is T){
					result.Add((T) t);
				}
			}
			return result.ToArray();
		}

		private static IEnumerable<string> GetPluginFiles(IEnumerable<string> filenames){
			List<string> result = new List<string>();
			foreach (string filename in filenames){
				string[] pluginFiles = Directory.GetFiles(executablePath, filename);
				foreach (string pluginFile in pluginFiles){
					result.Add(pluginFile);
				}
			}
			return result;
		}

		private static T[] Sort<T>(IList<T> w) where T : INamedListItem{
			float[] q = new float[w.Count];
			for (int i = 0; i < w.Count; i++){
				q[i] = w[i].DisplayRank;
			}
			int[] o = ArrayUtils.Order(q);
			return ArrayUtils.SubArray(w, o);
		}
	}
}