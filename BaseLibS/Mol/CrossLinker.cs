using System.Xml.Serialization;

namespace BaseLibS.Mol{
	public class CrossLinker : StorableItem{
		private double saturated = double.NaN;
		private double unsaturated = double.NaN;
		private ModificationPosition position = ModificationPosition.anywhere;

		[XmlAttribute("saturated_composition")]
		public string SaturatedComposition { get; set; }

		[XmlAttribute("unsaturated_composition")]
		public string UnsaturatedComposition { get; set; }

		[XmlIgnore]
		public double SaturatedMass{
			get{
				if (double.IsNaN(saturated)){
					saturated = ChemElements.GetMassFromComposition(SaturatedComposition);
				}
				return saturated;
			}
		}

		[XmlIgnore]
		public double UnsaturatedMass{
			get{
				if (double.IsNaN(unsaturated)){
					unsaturated = ChemElements.GetMassFromComposition(UnsaturatedComposition);
				}
				return unsaturated;
			}
		}

		[XmlAttribute("specificity")]
		public string Specificity { get; set; }

		[XmlAttribute("proteinNterm")]
		public bool ProteinNterm { get; set; }

		[XmlAttribute("proteinCterm")]
		public bool ProteinCterm { get; set; }

		[XmlElement("position", typeof (ModificationPosition))]
		public ModificationPosition Position { get { return position; } set { position = value; } }

		public override bool Equals(object obj){
			if (this == obj){
				return true;
			}
			if (obj is Modification){
				return (((Modification) obj).Name != Name);
			}
			return false;
		}

		public override int GetHashCode() { return Name.GetHashCode(); }
	}
}