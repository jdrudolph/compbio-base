using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using BasicLib.Properties;
using BasicLib.Util;

namespace BasicLib.Mol{
	public class AminoAcid : Molecule{
		public static readonly double massNormalCTerminus = massO + massH;
		public static readonly double massNormalNTerminus = massH;
		public static readonly double weightNormalCTerminus = weightO + weightH;
		public static readonly double weightNormalNTerminus = weightH;
		public static readonly double aIonMassOffset = -massC - massO - massH;
		public static readonly double bIonMassOffset = -massH;
		public static readonly double cIonMassOffset = massN + 2*massH;
		public static readonly double xIonMassOffset = massC + massO - massH;
		public static readonly double yIonMassOffset = massH;
		public static readonly double zIonMassOffset = -massN - 2*massH;
		public static readonly double zDotIonMassOffset = -massN - 1*massH;
		public static readonly double zPrimeIonMassOffset = -massN;
		private static AminoAcid[] aminoAcids;
		private static double[] aaMonoMasses;
		private static Dictionary<char, double> aaOccurences;
		private static Dictionary<char, double> aaWeights;
		private static Dictionary<string, char> codonToAa;
		private static Dictionary<char, AminoAcid> letterToAa;
		private static string singleLetterAas;
		private readonly string abbreviation;
		private readonly string[] codons;
		private readonly string type;
		private readonly char letter;
		private readonly double occurence;
		private readonly double gravy;
		private readonly bool isStandard;
		public static AminoAcid[] AminoAcids { get { return aminoAcids ?? (aminoAcids = InitAminoAcids()); } }
		public static double[] AaMonoMasses { get { return aaMonoMasses ?? (aaMonoMasses = InitMasses()); } }
		public static Dictionary<char, double> AaOccurences { get { return aaOccurences ?? (aaOccurences = InitOccurences()); } }
		public static Dictionary<char, double> AaWeights { get { return aaWeights ?? (aaWeights = InitWeights()); } }
		public static Dictionary<string, char> CodonToAa { get { return codonToAa ?? (codonToAa = InitCodonToAa()); } }
		public static string SingleLetterAas { get { return singleLetterAas ?? (singleLetterAas = ExtractSingleLetterAa(false)); } }
		public static string StandardSingleLetterAas { get { return singleLetterAas ?? (singleLetterAas = ExtractSingleLetterAa(true)); } }
		public static Dictionary<char, AminoAcid> LetterToAa { get { return letterToAa ?? (letterToAa = InitLetter2Aa()); } }

		private static Dictionary<char, AminoAcid> InitLetter2Aa(){
			Dictionary<char,AminoAcid> result = new Dictionary<char, AminoAcid>();
			foreach (AminoAcid aminoAcid in AminoAcids){
				result.Add(aminoAcid.Letter, aminoAcid);
			}
			return result;
		}

		private static AminoAcid[] InitAminoAcids(){
			AminoAcid alanine = new AminoAcid("C3H5NO", "Alanine", "Ala", 'A', 7.4, new[]{"GCT", "GCC", "GCA", "GCG"},
				"hydrophobic aliphatic", true, 1.8);
			AminoAcid arginine = new AminoAcid("C6H12N4O", "Arginine", "Arg", 'R', 4.2,
				new[]{"CGT", "CGC", "CGA", "CGG", "AGA", "AGG"}, "charged basic", true, -4.5);
			AminoAcid asparagine = new AminoAcid("C4H6N2O2", "Asparagine", "Asn", 'N', 4.4, new[]{"AAT", "AAC"}, "polar neutral",
				true, -3.5);
			AminoAcid asparticAcid = new AminoAcid("C4H5NO3", "Aspartic acid", "Asp", 'D', 5.9, new[]{"GAT", "GAC"},
				"charged acidic", true, -3.5);
			AminoAcid cysteine = new AminoAcid("C3H5NOS", "Cysteine", "Cys", 'C', 3.3, new[]{"TGT", "TGC"}, "polar neutral", true,
				2.5);
			AminoAcid glutamicAcid = new AminoAcid("C5H7NO3", "Glutamic acid", "Glu", 'E', 5.8, new[]{"GAA", "GAG"},
				"charged acidic", true, -3.5);
			AminoAcid glutamine = new AminoAcid("C5H8N2O2", "Glutamine", "Gln", 'Q', 3.7, new[]{"CAA", "CAG"}, "polar neutral",
				true, -3.5);
			AminoAcid glycine = new AminoAcid("C2H3NO", "Glycine", "Gly", 'G', 7.4, new[]{"GGT", "GGC", "GGA", "GGG"}, "", true,
				-0.4);
			AminoAcid histidine = new AminoAcid("C6H7N3O", "Histidine", "His", 'H', 2.9, new[]{"CAT", "CAC"}, "charged basic",
				true, -3.2);
			AminoAcid isoleucine = new AminoAcid("C6H11NO", "Isoleucine", "Ile", 'I', 3.8, new[]{"ATT", "ATC", "ATA"},
				"hydrophobic aliphatic", true, 4.5);
			AminoAcid leucine = new AminoAcid("C6H11NO", "Leucine", "Leu", 'L', 7.6,
				new[]{"TTA", "TTG", "CTT", "CTC", "CTA", "CTG"}, "hydrophobic aliphatic", true, 3.8);
			AminoAcid lysine = new AminoAcid("C6H12N2O", "Lysine", "Lys", 'K', 7.2, new[]{"AAA", "AAG"}, "charged basic", true,
				-3.9);
			AminoAcid methionine = new AminoAcid("C5H9NOS", "Methionine", "Met", 'M', 1.8, new[]{"ATG"}, "polar neutral", true,
				1.9);
			AminoAcid phenylalanine = new AminoAcid("C9H9NO", "Phenylalanine", "Phe", 'F', 4, new[]{"TTT", "TTC"},
				"hydrophobic aromatic", true, 2.8);
			AminoAcid proline = new AminoAcid("C5H7NO", "Proline", "Pro", 'P', 5, new[]{"CCT", "CCC", "CCA", "CCG"}, "", true,
				-1.6);
			AminoAcid serine = new AminoAcid("C3H5NO2", "Serine", "Ser", 'S', 8.1,
				new[]{"TCT", "TCC", "TCA", "TCG", "AGT", "AGC"}, "polar neutral", true, -0.8);
			AminoAcid threonine = new AminoAcid("C4H7NO2", "Threonine", "Thr", 'T', 6.2, new[]{"ACT", "ACC", "ACA", "ACG"},
				"polar neutral", true, -0.7);
			AminoAcid tryptophan = new AminoAcid("C11H10N2O", "Tryptophan", "Trp", 'W', 1.3, new[]{"TGG"}, "hydrophobic aromatic",
				true, -0.9);
			AminoAcid tyrosine = new AminoAcid("C9H9NO2", "Tyrosine", "Tyr", 'Y', 3.3, new[]{"TAT", "TAC"},
				"hydrophobic aromatic", true, -1.3);
			AminoAcid valine = new AminoAcid("C5H9NO", "Valine", "Val", 'V', 6.8, new[]{"GTT", "GTC", "GTA", "GTG"},
				"hydrophobic aliphatic", true, 4.2);
			AminoAcid selenocysteine = new AminoAcid("C3H7NO2Se", "Selenocysteine", "Sec", 'U', 0.0, new[]{"TGA"}, "", false, 0.0);
			AminoAcid[] aas = new[]{
				alanine, arginine, asparagine, asparticAcid, cysteine, glutamine, glutamicAcid, glycine, histidine, isoleucine,
				leucine, lysine, methionine, phenylalanine, proline, serine, threonine, tryptophan, tyrosine, valine, selenocysteine
			};
			return aas;
		}

		public Molecule GetPeptideMolecule(string aaseq){
			List<Molecule> m = new List<Molecule>();
			foreach (char c in aaseq){
				if (!LetterToAa.ContainsKey(c)) {
					return null;
				}
				m.Add(LetterToAa[c]);
			}
			m.Add(new Molecule("H20"));
			return Sum(m);
		}

		public static Image GetImage(AminoAcid aa){
			object resource = Resources.ResourceManager.GetObject(aa.Letter.ToString(CultureInfo.InvariantCulture));
			return resource is Image ? (Image) resource : null;
		}

		internal AminoAcid(string empiricalFormula, string name, string abbreviation, char letter, double occurence,
			string[] codons, string type, bool isStandard, double gravy) : base(empiricalFormula){
			this.abbreviation = abbreviation;
			this.letter = letter;
			this.occurence = occurence/100.0;
			this.gravy = gravy;
			this.codons = codons;
			this.type = type;
			this.isStandard = isStandard;
			Name = name;
		}

		public string Abbreviation { get { return abbreviation; } }
		public string Type { get { return type; } }
		public char Letter { get { return letter; } }
		public double Occurence { get { return occurence; } }
		public string[] Codons { get { return codons; } }
		public double Gravy { get { return gravy; } }
		//     IUB/GCG      Meaning     Complement   Staden/Sanger
		// A             A             T             A
		// C             C             G             C
		// G             G             C             G
		//T/U            T             A             T
		// M           A or C          K             M
		// R           A or G          Y             R
		// W           A or T          W             W
		// S           C or G          S             S
		// Y           C or T          R             Y
		// K           G or T          M             K
		// V        A or C or G        B             V
		// H        A or C or T        D             H
		// D        A or G or T        H             D
		// B        C or G or T        V             B
		//X/N     G or A or T or C    X/N            N
		//./~      gap character      ./~            -
		private static Dictionary<string, char> InitCodonToAa(){
			Dictionary<string, char> result = new Dictionary<string, char>();
			foreach (AminoAcid aa in AminoAcids){
				foreach (string codon in aa.codons){
					result.Add(codon, aa.letter);
				}
			}
			result.Add("TAG", '*');
			result.Add("TGA", '*');
			result.Add("TAA", '*');
			string[] keys = ArrayUtils.GetKeys(result);
			foreach (string key in keys){
				if (key.Contains("T")){
					result.Add(key.Replace('T', 'U'), result[key]);
				}
			}
			return result;
		}

		private static double[] InitMasses(){
			double[] result = new double[128];
			foreach (AminoAcid aa in AminoAcids){
				result[aa.Letter] = aa.MonoIsotopicMass;
			}
			const int x = 'X';
			result[x] = 111.000000;
			return result;
		}

		private static Dictionary<char, double> InitOccurences(){
			Dictionary<char, double> result = new Dictionary<char, double>();
			foreach (AminoAcid aa in AminoAcids){
				result.Add(aa.Letter, aa.occurence);
			}
			result.Add('X', 1);
			return result;
		}

		private static Dictionary<char, double> InitWeights(){
			Dictionary<char, double> result = new Dictionary<char, double>();
			foreach (AminoAcid aa in AminoAcids){
				result.Add(aa.Letter, aa.MolecularWeight);
			}
			return result;
		}

		private static string ExtractSingleLetterAa(bool onlyStandard){
			StringBuilder result = new StringBuilder();
			foreach (AminoAcid t in AminoAcids){
				if (!onlyStandard || t.isStandard){
					result.Append(t.letter);
				}
			}
			return result.ToString();
		}

		public static AminoAcid[] FromLetters(char[] c){
			AminoAcid[] result = new AminoAcid[c.Length];
			for (int i = 0; i < c.Length; i++){
				result[i] = FromLetter(c[i]);
			}
			return result;
		}

		public static AminoAcid FromLetter(char c){
			foreach (AminoAcid t in AminoAcids){
				if (t.Letter == c){
					return t;
				}
			}
			return null;
		}

		public static char[] GetSingleLetters(AminoAcid[] aas){
			char[] result = new char[aas.Length];
			for (int i = 0; i < aas.Length; i++){
				result[i] = aas[i].letter;
			}
			return result;
		}

		public override bool Equals(object obj){
			if (this == obj){
				return true;
			}
			if (obj is AminoAcid){
				AminoAcid other = (AminoAcid) obj;
				return other.letter == letter;
			}
			return false;
		}

		public override int GetHashCode(){
			return letter + 1;
		}

		public static bool ValidSequence(string sequence){
			foreach (char x in sequence){
				if (SingleLetterAas.IndexOf(x) < 0){
					return false;
				}
			}
			return true;
		}

		public static double CalcMolecularWeight(string sequence){
			double result = weightNormalCTerminus + weightNormalNTerminus;
			foreach (char aa in sequence){
				if (AaWeights.ContainsKey(aa)){
					result += AaWeights[aa];
				}
			}
			return result;
		}

		public static double CalcMonoisotopicMass(string sequence){
			double result = massNormalCTerminus + massNormalNTerminus;
			foreach (char aa in sequence){
				result += AaMonoMasses[aa];
			}
			return result;
		}
	}
}