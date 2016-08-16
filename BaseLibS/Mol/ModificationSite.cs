using System.Xml.Serialization;

namespace BaseLibS.Mol{
	public class ModificationSite{
		[XmlAttribute("site")]
		public string Site { get { return "" + Aa; } set { Aa = value[0]; } }
		[XmlIgnore]
		public char Aa { get; set; }
        [XmlArrayItem("neutralloss")]
		public NeutralLoss[] NeutrallossCollection { get; set; }
        [XmlArrayItem("diagnostic")]
		public DiagnosticPeak[] DiagnosticCollection { get; set; }
		public bool HasNeutralLoss => NeutrallossCollection != null && NeutrallossCollection.Length != 0;
		public bool HasDiagnosticPeak => DiagnosticCollection != null && DiagnosticCollection.Length != 0;
	}
}