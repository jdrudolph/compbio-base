using BaseLib.Mol;

namespace MsLib.Mol{
	[System.SerializableAttribute]
	[System.Diagnostics.DebuggerStepThroughAttribute]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRoot("modifications", IsNullable = false)]
	public class ModificationList{
		private Modification[] modifications;

		[System.Xml.Serialization.XmlElementAttribute("modification", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Modification[] Modifications{
			get { return modifications; }
			set { modifications = value; }
		}
	}
}