using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseLibS.Util;

namespace BaseLibS.Parse.Misc{
	public static class PhosphoSitePlusParser{
		internal static readonly Dictionary<string, string> fileMap = CreateFileMap();

		private static Dictionary<string, string> CreateFileMap(){
			string folder = FileUtils.GetConfigPath() + "\\PSP\\";
			return new Dictionary<string, string>{
				{"Acetylation", folder + "Acetylation_site_dataset"},
				{"Methylation", folder + "Methylation_site_dataset"},
				{"O-GlcNAc", folder + "O-GlcNAc_site_dataset"},
				{"O-GalNAc", folder + "O-GalNAc_site_dataset"},
				{"Phosphorylation", folder + "Phosphorylation_site_dataset"},
				{"Sumoylation", folder + "Sumoylation_site_dataset"},
				{"Ubiquitination", folder + "Ubiquitination_site_dataset"}
			};
		}

		public static string[] GetAllMods(){
			return fileMap.Keys.ToArray();
		}

		public static string GetFilenameForMod(string mod){
			string filename = fileMap[mod];
			if (!File.Exists(filename)){
				if (File.Exists(filename + ".gz")){
					filename = filename + ".gz";
				} else{
					return null;
				}
			}
			return filename;
		}

		public static void ParseKnownMods(string filename, out string[] seqWins, out string[] accs, out string[] pubmedLtp,
			out string[] pubmedMs2, out string[] cstMs2){
			seqWins = TabSep.GetColumn("SITE_+/-7_AA", filename, 3, '\t');
			accs = TabSep.GetColumn("ACC_ID", filename, 3, '\t');
			pubmedLtp = TabSep.GetColumn("LT_LIT", filename, 3, '\t');
			pubmedMs2 = TabSep.GetColumn("MS_LIT", filename, 3, '\t');
			cstMs2 = TabSep.GetColumn("MS_CST", filename, 3, '\t');
		}

		public static void ParseKinaseSubstrate(string filename, out string[] seqWins, out string[] subAccs,
			out string[] kinases, out string[] kinAccs){
			seqWins = TabSep.GetColumn("SITE_+/-7_AA", filename, 3, '\t');
			subAccs = TabSep.GetColumn("SUB_ACC_ID", filename, 3, '\t');
			kinases = TabSep.GetColumn("KINASE", filename, 3, '\t');
			kinAccs = TabSep.GetColumn("KIN_ACC_ID", filename, 3, '\t');
		}

		public static void ParseRegulatorySites(string filename, out string[] seqWins, out string[] accs,
			out string[] function, out string[] process, out string[] protInteract, out string[] otherInteract,
			out string[] notes){
			seqWins = TabSep.GetColumn("SITE_+/-7_AA", filename, 3, '\t');
			accs = TabSep.GetColumn("ACC_ID", filename, 3, '\t');
			function = TabSep.GetColumn("ON_FUNCTION", filename, 3, '\t');
			process = TabSep.GetColumn("ON_PROCESS", filename, 3, '\t');
			protInteract = TabSep.GetColumn("ON_PROT_INTERACT", filename, 3, '\t');
			otherInteract = TabSep.GetColumn("ON_OTHER_INTERACT", filename, 3, '\t');
			notes = TabSep.GetColumn("NOTES", filename, 3, '\t');
		}

		public static string GetKinaseSubstrateFile(){
			string folder = FileUtils.GetConfigPath() + "\\PSP\\";
			string filename = folder + "Kinase_Substrate_Dataset";
			if (!File.Exists(filename)){
				if (File.Exists(filename + ".gz")){
					filename = filename + ".gz";
				} else{
					return null;
				}
			}
			return filename;
		}

		public static string GetRegulatorySitesFile(){
			string folder = FileUtils.GetConfigPath() + "\\PSP\\";
			string filename = folder + "Regulatory_sites";
			if (!File.Exists(filename)){
				if (File.Exists(filename + ".gz")){
					filename = filename + ".gz";
				} else{
					return null;
				}
			}
			return filename;
		}
	}
}