﻿using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using BaseLib.Num;
using BaseLib.Properties;
using BaseLib.Util;

namespace BaseLib.Mol{
	public static class AminoAcids{
		public static readonly double massNormalCTerminus = Molecule.massO + Molecule.massH;
		public static readonly double massNormalNTerminus = Molecule.massH;
		public static readonly double weightNormalCTerminus = Molecule.weightO + Molecule.weightH;
		public static readonly double weightNormalNTerminus = Molecule.weightH;
		public static readonly double aIonMassOffset = -Molecule.massC - Molecule.massO - Molecule.massH;
		public static readonly double bIonMassOffset = -Molecule.massH;
		public static readonly double cIonMassOffset = Molecule.massN + 2*Molecule.massH;
		public static readonly double xIonMassOffset = Molecule.massC + Molecule.massO - Molecule.massH;
		public static readonly double yIonMassOffset = Molecule.massH;
		public static readonly double zIonMassOffset = -Molecule.massN - 2*Molecule.massH;
		public static readonly double zDotIonMassOffset = -Molecule.massN - 1*Molecule.massH;
		public static readonly double zPrimeIonMassOffset = -Molecule.massN;
		public static readonly AminoAcid alanine = new AminoAcid("C3H5NO", "Alanine", "Ala", 'A', 7.4,
			new[]{"GCT", "GCC", "GCA", "GCG"}, "hydrophobic aliphatic", true, 1.8);
		public static readonly AminoAcid arginine = new AminoAcid("C6H12N4O", "Arginine", "Arg", 'R', 4.2,
			new[]{"CGT", "CGC", "CGA", "CGG", "AGA", "AGG"}, "charged basic", true, -4.5);
		public static readonly AminoAcid asparagine = new AminoAcid("C4H6N2O2", "Asparagine", "Asn", 'N', 4.4,
			new[]{"AAT", "AAC"}, "polar neutral", true, -3.5);
		public static readonly AminoAcid asparticAcid = new AminoAcid("C4H5NO3", "Aspartic acid", "Asp", 'D', 5.9,
			new[]{"GAT", "GAC"}, "charged acidic", true, -3.5);
		public static readonly AminoAcid cysteine = new AminoAcid("C3H5NOS", "Cysteine", "Cys", 'C', 3.3, new[]{"TGT", "TGC"},
			"polar neutral", true, 2.5);
		public static readonly AminoAcid glutamicAcid = new AminoAcid("C5H7NO3", "Glutamic acid", "Glu", 'E', 5.8,
			new[]{"GAA", "GAG"}, "charged acidic", true, -3.5);
		public static readonly AminoAcid glutamine = new AminoAcid("C5H8N2O2", "Glutamine", "Gln", 'Q', 3.7,
			new[]{"CAA", "CAG"}, "polar neutral", true, -3.5);
		public static readonly AminoAcid glycine = new AminoAcid("C2H3NO", "Glycine", "Gly", 'G', 7.4,
			new[]{"GGT", "GGC", "GGA", "GGG"}, "", true, -0.4);
		public static readonly AminoAcid histidine = new AminoAcid("C6H7N3O", "Histidine", "His", 'H', 2.9,
			new[]{"CAT", "CAC"}, "charged basic", true, -3.2);
		public static readonly AminoAcid isoleucine = new AminoAcid("C6H11NO", "Isoleucine", "Ile", 'I', 3.8,
			new[]{"ATT", "ATC", "ATA"}, "hydrophobic aliphatic", true, 4.5);
		public static readonly AminoAcid leucine = new AminoAcid("C6H11NO", "Leucine", "Leu", 'L', 7.6,
			new[]{"TTA", "TTG", "CTT", "CTC", "CTA", "CTG"}, "hydrophobic aliphatic", true, 3.8);
		public static readonly AminoAcid lysine = new AminoAcid("C6H12N2O", "Lysine", "Lys", 'K', 7.2, new[]{"AAA", "AAG"},
			"charged basic", true, -3.9);
		public static readonly AminoAcid methionine = new AminoAcid("C5H9NOS", "Methionine", "Met", 'M', 1.8, new[]{"ATG"},
			"polar neutral", true, 1.9);
		public static readonly AminoAcid phenylalanine = new AminoAcid("C9H9NO", "Phenylalanine", "Phe", 'F', 4,
			new[]{"TTT", "TTC"}, "hydrophobic aromatic", true, 2.8);
		public static readonly AminoAcid proline = new AminoAcid("C5H7NO", "Proline", "Pro", 'P', 5,
			new[]{"CCT", "CCC", "CCA", "CCG"}, "", true, -1.6);
		public static readonly AminoAcid serine = new AminoAcid("C3H5NO2", "Serine", "Ser", 'S', 8.1,
			new[]{"TCT", "TCC", "TCA", "TCG", "AGT", "AGC"}, "polar neutral", true, -0.8);
		public static readonly AminoAcid threonine = new AminoAcid("C4H7NO2", "Threonine", "Thr", 'T', 6.2,
			new[]{"ACT", "ACC", "ACA", "ACG"}, "polar neutral", true, -0.7);
		public static readonly AminoAcid tryptophan = new AminoAcid("C11H10N2O", "Tryptophan", "Trp", 'W', 1.3, new[]{"TGG"},
			"hydrophobic aromatic", true, -0.9);
		public static readonly AminoAcid tyrosine = new AminoAcid("C9H9NO2", "Tyrosine", "Tyr", 'Y', 3.3, new[]{"TAT", "TAC"},
			"hydrophobic aromatic", true, -1.3);
		public static readonly AminoAcid valine = new AminoAcid("C5H9NO", "Valine", "Val", 'V', 6.8,
			new[]{"GTT", "GTC", "GTA", "GTG"}, "hydrophobic aliphatic", true, 4.2);
		public static readonly AminoAcid selenocysteine = new AminoAcid("C3H7NO2Se", "Selenocysteine", "Sec", 'U', 0.0,
			new[]{"TGA"}, "", false, 0.0);
		public static readonly AminoAcid[] aminoAcids = new[]{
			alanine, arginine, asparagine, asparticAcid, cysteine, glutamine, glutamicAcid, glycine, histidine, isoleucine,
			leucine, lysine, methionine, phenylalanine, proline, serine, threonine, tryptophan, tyrosine, valine, selenocysteine
		};
		private static double[] aaMonoMasses;
		private static Dictionary<char, double> aaOccurences;
		private static Dictionary<char, double> aaWeights;
		private static Dictionary<string, char> codonToAa;
		private static string singleLetterAas;
		private static string standardSingleLetterAas;
		private static Dictionary<char, AminoAcid> letterToAa;
		public static double[] AaMonoMasses { get { return aaMonoMasses ?? (aaMonoMasses = InitMasses()); } }
		public static Dictionary<char, double> AaOccurences { get { return aaOccurences ?? (aaOccurences = InitOccurences()); } }
		public static Dictionary<char, double> AaWeights { get { return aaWeights ?? (aaWeights = InitWeights()); } }
		public static Dictionary<string, char> CodonToAa { get { return codonToAa ?? (codonToAa = InitCodonToAa()); } }
		public static string SingleLetterAas { get { return singleLetterAas ?? (singleLetterAas = ExtractSingleLetterAa(false)); } }
		public static string StandardSingleLetterAas { get { return standardSingleLetterAas ?? (standardSingleLetterAas = ExtractSingleLetterAa(true)); } }
		public static Dictionary<char, AminoAcid> LetterToAa { get { return letterToAa ?? (letterToAa = InitLetter2Aa()); } }

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

		public static Molecule GetPeptideMolecule(string aaseq){
			List<Molecule> m = new List<Molecule>();
			foreach (char c in aaseq){
				if (!LetterToAa.ContainsKey(c)){
					return null;
				}
				m.Add(LetterToAa[c]);
			}
			m.Add(new Molecule("H2O"));
			return Molecule.Sum(m);
		}

		public static Image GetImage(AminoAcid aa){
			object resource = Resources.ResourceManager.GetObject(aa.Letter.ToString(CultureInfo.InvariantCulture));
			return resource is Image ? (Image) resource : null;
		}

		public static AminoAcid[] FromLetters(char[] c){
			AminoAcid[] result = new AminoAcid[c.Length];
			for (int i = 0; i < c.Length; i++){
				result[i] = FromLetter(c[i]);
			}
			return result;
		}

		public static AminoAcid FromLetter(char c){
			foreach (AminoAcid t in aminoAcids){
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

		private static Dictionary<char, AminoAcid> InitLetter2Aa(){
			Dictionary<char, AminoAcid> result = new Dictionary<char, AminoAcid>();
			foreach (AminoAcid aminoAcid in aminoAcids){
				result.Add(aminoAcid.Letter, aminoAcid);
			}
			return result;
		}

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
			foreach (AminoAcid aa in aminoAcids){
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
			foreach (AminoAcid aa in aminoAcids){
				result[aa.Letter] = aa.MonoIsotopicMass;
			}
			const int x = 'X';
			result[x] = 111.000000;
			return result;
		}

		private static Dictionary<char, double> InitOccurences(){
			Dictionary<char, double> result = new Dictionary<char, double>();
			foreach (AminoAcid aa in aminoAcids){
				result.Add(aa.Letter, aa.occurence);
			}
			result.Add('X', 1);
			return result;
		}

		private static Dictionary<char, double> InitWeights(){
			Dictionary<char, double> result = new Dictionary<char, double>();
			foreach (AminoAcid aa in aminoAcids){
				result.Add(aa.Letter, aa.MolecularWeight);
			}
			return result;
		}

		private static string ExtractSingleLetterAa(bool onlyStandard){
			StringBuilder result = new StringBuilder();
			foreach (AminoAcid t in aminoAcids){
				if (!onlyStandard || t.isStandard){
					result.Append(t.letter);
				}
			}
			return result.ToString();
		}

		public static Dictionary<string, List<string>> GetPeptideCompositions(double maxMass){
			Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
			double mg = glycine.MonoIsotopicMass;
			int maxN = (int) ((maxMass - Molecule.massWater)/mg);
			string aas = StandardSingleLetterAas;
			double[] masses = AaMonoMasses;
			NumUtils.GetPartitions(maxN, 21, i1 =>{
				double m1 = Molecule.massWater;
				for (int i = 0; i < i1.Length; i++){
					if (i < 20){
						m1 += i1[i]*masses[aas[i]];
					}
				}
				return m1 <= maxMass;
			}, i2 =>{
				string seq = GetSequence(i2, aas);
				Molecule m = GetPeptideMolecule(seq);
				string formula = m.GetEmpiricalFormula();
				if (!result.ContainsKey(formula)){
					result.Add(formula, new List<string>());
				}
				result[formula].Add(seq);
			});
			return result;
		}

		public static HashSet<string> GetPeptideCompositionsNew(double maxMass){
			HashSet<string> result = new HashSet<string>();
			double mg = glycine.MonoIsotopicMass;
			int maxN = (int) ((maxMass - Molecule.massWater)/mg);
			string aas = StandardSingleLetterAas;
			double[] masses = AaMonoMasses;
			Partition(new TmpPartitionNew(maxN), 21, maxMass, masses, aas, result);
			return result;
		}

		private static void Partition(TmpPartitionNew x, int len, double maxMass, IList<double> masses, string aas,
			ISet<string> result){
			if (x.remainder == 0 && x.partition.Count == len){
				IList<int> part = x.partition;
				double m1 = Molecule.massWater;
				for (int i = 0; i < part.Count; i++){
					if (i < 20){
						m1 += part[i]*masses[aas[i]];
					}
				}
				if (m1 <= maxMass){
					string seq = GetSequence(part, aas);
					Molecule m = GetPeptideMolecule(seq);
					string formula = m.GetEmpiricalFormula();
					if (!result.Contains(formula)){
						result.Add(formula);
					}
				}
				return;
			}
			if (x.partition.Count == len){
				return;
			}
			for (int i = 0; i <= x.remainder; i++){
				TmpPartitionNew w = x.Add(i, masses, aas);
				if (w.mass > maxMass){
					break;
				}
				Partition(w, len, maxMass, masses, aas, result);
			}
		}

		private static string GetSequence(IList<int> i1, string aas){
			StringBuilder s = new StringBuilder();
			for (int i = 0; i < 20; i++){
				for (int j = 0; j < i1[i]; j++){
					s.Append(aas[i]);
				}
			}
			return s.ToString();
		}
	}

	internal class TmpPartitionNew{
		public List<int> partition;
		public int remainder;
		public double mass;
		private TmpPartitionNew() {}

		internal TmpPartitionNew(int n){
			remainder = n;
			partition = new List<int>(n);
			mass = 0;
		}

		internal TmpPartitionNew Add(int a, IList<double> masses, string aas){
			TmpPartitionNew result = new TmpPartitionNew{remainder = remainder - a, partition = new List<int>()};
			result.partition.AddRange(partition);
			result.mass = mass;
			if (result.partition.Count < 20){
				result.mass += a*masses[aas[result.partition.Count]];
			}
			result.partition.Add(a);
			return result;
		}
	}
}