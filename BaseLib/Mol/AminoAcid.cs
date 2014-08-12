namespace BaseLib.Mol{
	public class AminoAcid : Molecule{
		internal readonly bool isStandard;
		public string Abbreviation { get; private set; }
		public string Type { get; private set; }
		public char Letter { get; private set; }
		public double Occurence { get; private set; }
		public string[] Codons { get; private set; }
		public double Gravy { get; private set; }

		internal AminoAcid(string empiricalFormula, string name, string abbreviation, char letter, double occurence,
			string[] codons, string type, bool isStandard, double gravy) : base(empiricalFormula){
			Abbreviation = abbreviation;
			Letter = letter;
			Occurence = occurence/100.0;
			Gravy = gravy;
			Codons = codons;
			Type = type;
			this.isStandard = isStandard;
			Name = name;
		}

		public override bool Equals(object obj){
			if (obj == null){
				return false;
			}
			if (this == obj){
				return true;
			}
			if (obj is AminoAcid){
				var other = (AminoAcid) obj;
				return other.Letter == Letter;
			}
			return false;
		}

		public override int GetHashCode() { return Letter + 1; }
	}
}