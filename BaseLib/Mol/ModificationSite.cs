using System.Xml.Serialization;
using BaseLib.Mol;

namespace MsLib.Mol{
	public class ModificationSite{
		[XmlAttribute("site")]
		public string Site { get { return "" + Aa; } set { Aa = value[0]; } }
		[XmlIgnore]
		public char Aa { get; set; }
        [XmlArrayItem("neutralloss")]
		public NeutralLoss[] neutralloss_collection { get; set; }
        [XmlArrayItem("diagnostic")]
		public DiagnosticPeak[] diagnostic_collection { get; set; }
		public bool HasNeutralLoss { get { return neutralloss_collection != null && neutralloss_collection.Length != 0; } }
		public bool HasDiagnosticPeak { get { return diagnostic_collection != null && diagnostic_collection.Length != 0; } }
	}
}