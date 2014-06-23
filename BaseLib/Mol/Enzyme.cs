using System.Collections.Generic;
using System.Xml.Serialization;
using BaseLib.Util;

namespace BaseLib.Mol{
	public class Enzyme : StorableItem{
		private HashSet<string> specificity;
		[XmlArray("specificity")]
		public string[] Specificity { get { return ArrayUtils.ToArray(specificity); } set { specificity = new HashSet<string>(value); } }

		public bool Cleaves(char c1, char c2){
			return specificity.Contains("" + c1 + c2);
		}

		public bool CleavesAnyCterm(char c){
			foreach (string s in specificity){
				if (s[0] == c){
					return true;
				}
			}
			return false;
		}

		public bool CleavesAnyNterm(char c){
			foreach (string s in specificity){
				if (s[1] == c){
					return true;
				}
			}
			return false;
		}
	}
}