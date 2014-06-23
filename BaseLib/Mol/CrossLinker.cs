using System.Xml.Serialization;

namespace BaseLib.Mol{
	public class CrossLinker : StorableItem{
		private string specificity;
		private string composition;
		private double mass = double.NaN;
		/// <summary>
		/// Description or fullname of Modification
		/// </summary>
		[XmlAttribute("total_composition")]
		public string Composition { get { return composition; } set { composition = value; } }
		[XmlIgnore]
		public double Mass{
			get{
				if (double.IsNaN(mass)){
					mass = ChemElements.GetMassFromComposition(composition);
				}
				return mass;
			}
			set { mass = value; }
		}
		[XmlAttribute("specificity")]
		public string Specificity { get { return specificity; } set { specificity = value; } }


		[XmlArray("modification_names")]
		public string[] ModificationNames { get; set; }

		[XmlArray("modification_composition")]
		public string[] ModificationCompositions { get; set; }
	}
}