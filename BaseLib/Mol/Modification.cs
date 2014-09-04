using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BaseLib.Mol{
	public class Modification : StorableItem{
		private double deltaMass = double.NaN;
		private string composition;
		private ModificationPosition position = ModificationPosition.anywhere;
		private ModificationSite[] sites = new ModificationSite[0];
		private Dictionary<char, ModificationSite> sitesMap;
		private char[] sitesArray;
		private ModificationType modificationType = ModificationType.standard;
		private NewTerminusType newTerminusType = NewTerminusType.none;
		[XmlAttribute("reporterCorrectionM2")]
		public double ReporterCorrectionM2 { get; set; }
		[XmlAttribute("reporterCorrectionM1")]
		public double ReporterCorrectionM1 { get; set; }
		[XmlAttribute("reporterCorrectionP1")]
		public double ReporterCorrectionP1 { get; set; }
		[XmlAttribute("reporterCorrectionP2")]
		public double ReporterCorrectionP2 { get; set; }
		/// <summary>
		/// Monoisotopic Mass of Modification (Composition)
		/// </summary>
		[XmlAttribute("delta_mass"), XmlIgnore]
		public double DeltaMass{
			get{
				if (double.IsNaN(deltaMass)){
					deltaMass = ChemElements.GetMassFromComposition(composition);
				}
				return deltaMass;
			}
			set { deltaMass = value; }
		}
		/// <summary>
		/// Composition of Modification
		/// </summary>
		[XmlAttribute("composition")]
		public string Composition { get { return composition; } set { composition = value; } }
        /// <summary>
        /// Equivalent Unimod id
        /// </summary>
        [XmlAttribute("unimod")]
        public string Unimod { get; set; }
		/// <summary>
		/// Position of Modification
		/// </summary>
		[XmlElement("position", typeof (ModificationPosition))]
		public ModificationPosition Position { get { return position; } set { position = value; } }
		/// <summary>
		/// Sites of Modification
		/// </summary>
		[XmlElement("modification_site")]
		public ModificationSite[] Sites{
			set{
				sites = value;
				sitesMap = new Dictionary<char, ModificationSite>();
				foreach (var modificationSite in sites){
					sitesMap.Add(modificationSite.Aa, modificationSite);
				}
			}
			get { return sites; }
		}
		/// <summary>
		/// Determines if this is a standard modification, a label or an isobaric label
		/// </summary>
		[XmlElement("type", typeof (ModificationType))]
		public ModificationType ModificationType { get { return modificationType; } set { modificationType = value; } }
		[XmlElement("terminus_type", typeof (NewTerminusType))]
		public NewTerminusType NewTerminusType { get { return newTerminusType; } set { newTerminusType = value; } }
		public int AaCount { get { return sites.Length; } }
		public string Abbreviation { get { return Name.Substring(0, 2).ToLower(); } }
		public bool IsPhosphorylation { get { return Math.Abs(deltaMass - 79.96633) < 0.0001; } }
		public bool IsInternal{
			get{
				return position == ModificationPosition.anywhere || position == ModificationPosition.notNterm ||
					position == ModificationPosition.notCterm || position == ModificationPosition.notTerm;
			}
		}
		public bool IsNterminal { get { return position == ModificationPosition.anyNterm || position == ModificationPosition.proteinNterm; } }
		public bool IsCterminal { get { return position == ModificationPosition.anyCterm || position == ModificationPosition.proteinCterm; } }
		public bool IsNterminalStep{
			get{
				return position == ModificationPosition.anyNterm || position == ModificationPosition.proteinNterm ||
					position == ModificationPosition.anywhere || position == ModificationPosition.notCterm;
			}
		}
		public bool IsCterminalStep{
			get{
				return position == ModificationPosition.anyCterm || position == ModificationPosition.proteinCterm ||
					position == ModificationPosition.anywhere || position == ModificationPosition.notNterm;
			}
		}
		public bool IsProteinTerminal { get { return position == ModificationPosition.proteinNterm || position == ModificationPosition.proteinCterm; } }

		public ModificationSite GetSite(char aa){
			return sitesMap[aa];
		}

		public override bool Equals(object obj){
			if (this == obj){
				return true;
			}
			if (obj is Modification){
				return (((Modification) obj).Name != Name);
			}
			return false;
		}

		public override int GetHashCode(){
			return Name.GetHashCode();
		}

		public bool HasAa(char aa){
			foreach (ModificationSite x in sites){
				if (x.Aa == aa){
					return true;
				}
			}
			return false;
		}

		public char GetAaAt(int j){
			return sites[j].Aa;
		}

		public static string[] ToStrings(Modification[] mods){
			string[] result = new string[mods.Length];
			for (int i = 0; i < mods.Length; i++){
				result[i] = mods[i].Name;
			}
			return result;
		}

		public override string ToString(){
			return Name;
		}

		public static Dictionary<char, ushort> ToDictionary(Modification[] modifications){
			Dictionary<char, ushort> result = new Dictionary<char, ushort>();
			foreach (Modification modification in modifications.Where(modification => modification.IsInternal)){
				for (int i = 0; i < modification.AaCount; i++){
					char c = modification.GetAaAt(i);
					if (result.ContainsKey(c)){
						throw new ArgumentException("Conflicting modifications.");
					}
					result.Add(c, modification.Index);
				}
			}
			return result;
		}

		public char[] GetSiteArray(){
			if (sitesArray == null){
				sitesArray = new char[sites.Length];
				for (int i = 0; i < sitesArray.Length; i++){
					sitesArray[i] = sites[i].Aa;
				}
			}
			return sitesArray;
		}

		public string GetFormula(){
			string formula = Composition;
			formula = formula.Replace("(", "");
			formula = formula.Replace(")", "");
			formula = formula.Replace(" ", "");
			formula = formula.Trim();
			return formula;
		}

		public bool HasNeutralLoss(){
			foreach (ModificationSite site in sites){
				if (site.HasNeutralLoss){
					return true;
				}
			}
			return false;
		}

		public bool IsIsotopicLabel{
			get{
				if (modificationType == ModificationType.isobaricLabel || modificationType == ModificationType.standard){
					return false;
				}
				Tuple<Molecule, Molecule> x = Molecule.GetDifferences(new Molecule(), new Molecule(GetFormula()));
				Molecule labelingDiff1 = x.Item1;
				Molecule labelingDiff2 = x.Item2;
				if (!labelingDiff1.IsIsotopicLabel && !labelingDiff2.IsIsotopicLabel){
					return false;
				}
				Molecule d1 = labelingDiff1.NaturalVersion;
				Molecule d2 = labelingDiff2.NaturalVersion;
				Tuple<Molecule, Molecule> d = Molecule.GetDifferences(d1, d2);
				return d.Item1.IsEmpty && d.Item2.IsEmpty;
			}
		}
	}
}