using System;
using BaseLib.Mol;

namespace MsLib.Mol{
	[Serializable, System.Diagnostics.DebuggerStepThrough, System.ComponentModel.DesignerCategory("code"),
	 System.Xml.Serialization.XmlType("database")]
	public class SequenceDatabase : StorableItem{
		/// <summary>
		/// Regular expression which describes how to parse the fasta sequence header to 
		/// obtain the accession number.
		/// </summary>
		private string searchExpression;
		/// <summary>
		/// The filename of this database e.g. as known by Andromeda.
		/// </summary>
		private string filename;

		/// <summary>
		/// Default Constructor for Serialization. 
		/// </summary>
		public SequenceDatabase() {}

		public SequenceDatabase(string filename, string searchExpression){
			this.filename = filename;
			this.searchExpression = searchExpression;
		}

		/// <summary>
		/// The filename of this database e.g. as known by Andromeda.
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("filename")]
		public string Filename { get { return filename; } set { filename = value; } }
		/// <summary>
		/// Regular expression which describes how to parse the fasta sequence header to 
		/// obtain the accession number.
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("search_expression")]
		public string SearchExpression { get { return searchExpression; } set { searchExpression = value; } }

		/// <summary>
		/// The human readable species of this database which should be NCBI entry's name
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("species")]
		public string Species { get; set; }

		/// <summary>
		/// The NCBI/NEWT taxonomy id for the species of this database
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("taxid")]
		public string Taxid { get; set; }

		/// <summary>
		/// The human readable species of this database which should be NCBI entry's name
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("source")]
		public string Source { get; set; }
	}
}