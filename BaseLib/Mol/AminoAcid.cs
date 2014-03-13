namespace BaseLib.Mol{
	public class AminoAcid : Molecule{
		private readonly string abbreviation;
		internal readonly string[] codons;
		private readonly string type;
		internal readonly char letter;
		internal readonly double occurence;
		private readonly double gravy;
		internal readonly bool isStandard;

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

        //Test
		public string Abbreviation { get { return abbreviation; } }
		public string Type { get { return type; } }
		public char Letter { get { return letter; } }
		public double Occurence { get { return occurence; } }
		public string[] Codons { get { return codons; } }
		public double Gravy { get { return gravy; } }
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
	}
}