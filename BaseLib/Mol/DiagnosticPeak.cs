using System.IO;
using BaseLib.Mol;

namespace MsLib.Search{
	public class DiagnosticPeak{
		private string name;
		private string shortname;
		private double mass = double.NaN;
		private string composition = "";

		public DiagnosticPeak(){
			// Default Constructor for Serialization
		}

		public DiagnosticPeak(BinaryReader reader){
			Name = reader.ReadString();
			ShortName = reader.ReadString();
			Composition = reader.ReadString();
			Mass = reader.ReadDouble();
		}

		public DiagnosticPeak(string name, string shortname, string composition, double mass){
			Name = name;
			ShortName = shortname;
			Composition = composition;
			Mass = mass;
		}

		public void Write(BinaryWriter writer){
			writer.Write(Name);
			writer.Write(ShortName);
			writer.Write(Composition);
			writer.Write(Mass);
		}

		[System.Xml.Serialization.XmlAttribute("name")]
		public string Name { get { return name; } set { name = value; } }
		[System.Xml.Serialization.XmlAttribute("shortname")]
		public string ShortName { get { return shortname; } set { shortname = value; } }
		[System.Xml.Serialization.XmlIgnore]
		public double Mass{
			get{
				if (double.IsNaN(mass)){
					int[] counts;
					string[] comp;
					double[] mono;
					ChemElements.DecodeComposition(composition, ChemElements.ElementDictionary, out counts, out comp, out mono);
					mass = 0;
					for (int i = 0; i < mono.Length; i++){
						mass += mono[i]*counts[i];
					}
				}
				return mass;
			}
			set { mass = value; }
		}
		[System.Xml.Serialization.XmlAttribute("composition")]
		public string Composition { get { return composition; } set { composition = value; } }

		public object Clone(){
			return new DiagnosticPeak{Name = name, Mass = mass, Composition = composition, ShortName = shortname};
		}
	}
}