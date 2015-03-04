using System.Xml.Serialization;

namespace BaseLibS.Mol{
	public class SequenceDatabase : StorableItem{
		/// <summary>
		/// Default Constructor for Serialization. 
		/// </summary>
		public SequenceDatabase() { }

		public SequenceDatabase(string filename, string searchExpression){
			Filename = filename;
			SearchExpression = searchExpression;
		}

		/// <summary>
		/// The filename of this database e.g. as known by Andromeda.
		/// </summary>
		[XmlAttribute("filename")]
		public string Filename { get; set; }

		/// <summary>
		/// Regular expression which describes how to parse the fasta sequence header to 
		/// obtain the accession number.
		/// </summary>
		[XmlAttribute("search_expression")]
		public string SearchExpression { get; set; }

		/// <summary>
		/// Regular expression which describes how to parse the fasta sequence header to 
		/// obtain the string containing definition of mutations.
		/// </summary>
		[XmlAttribute("mutation_parse_rule")]
		public string MutationParseRule { get; set; }

		/// <summary>
		/// The human readable species of this database which should be NCBI entry's name
		/// </summary>
		[XmlAttribute("species")]
		public string Species { get; set; }

		/// <summary>
		/// The NCBI/NEWT taxonomy id for the species of this database
		/// </summary>
		[XmlAttribute("taxid")]
		public string Taxid { get; set; }

		/// <summary>
		/// The human readable species of this database which should be NCBI entry's name
		/// </summary>
		[XmlAttribute("source")]
		public string Source { get; set; }
	}
}