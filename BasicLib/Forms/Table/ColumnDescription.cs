using System;

namespace BasicLib.Forms.Table{
	/// <summary>
	/// This class provides a container for more complex column desciptions then just
	/// a single string.
	/// </summary>
	[Serializable]
	public class ColumnDescription{
		public ColumnDescription(string description) : this(null, description, null) {}
		public ColumnDescription(string separator, string description) : this(separator, description, null) {}

		public ColumnDescription(string separator, string description, string options){
			Separator = separator;
			Description = description;
			Options = options;
		}

		public string Separator { get; set; }
		public string Description { get; set; }
		public string Options { get; set; }

		public override string ToString(){
			return Description;
		}
	}
}