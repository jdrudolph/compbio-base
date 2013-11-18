using System;

namespace BasicLib.Forms.Table{
	/// <summary>
	/// This class provides a container for more complex column desciptions then just
	/// a single string.
	/// </summary>
	[Serializable]
	public class ColumnDescription{
		public ColumnDescription(string description) : this(null, description) {}

		public ColumnDescription(string separator, string description){
			Separator = separator;
			Description = description;
		}

		public string Separator { get; set; }
		public string Description { get; set; }

		public override string ToString(){
			return Description;
		}
	}
}