using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BaseLibS.Param{
	[Serializable]
	public class ParameterGroup : IXmlSerializable
    {
		private List<Parameter> parameters = new List<Parameter>();
		private string name;
		private bool collapsedDefault;

		public ParameterGroup(IList<Parameter> parameters, string name, bool collapsedDefault){
			this.name = name;
			this.parameters = new List<Parameter>(parameters);
			this.collapsedDefault = collapsedDefault;
		}

		private ParameterGroup(){}

		public void Convert(Func<Parameter, Parameter> map){
			for (int i = 0; i < parameters.Count; i++){
				parameters[i] = map(parameters[i]);
			}
		}

		public bool CollapsedDefault{
			get { return collapsedDefault; }
			set { collapsedDefault = value; }
		}

		public string[] Markup{
			get{
				List<string> result = new List<string>();
				foreach (Parameter p in parameters){
					result.AddRange(p.Markup);
				}
				return result.ToArray();
			}
		}

		public bool IsModified{
			get{
				foreach (Parameter parameter in parameters){
					if (parameter.IsModified){
						return true;
					}
				}
				return false;
			}
		}

		public string Name{
			get { return name; }
			set { name = value; }
		}

		public List<Parameter> ParameterList => parameters;

		public int Count => parameters.Count;

		public float Height{
			get{
				float h = 0;
				foreach (Parameter parameter in parameters){
					h += parameter.Height;
				}
				return h;
			}
		}

		public Parameter GetParam(string name1){
			foreach (Parameter p in parameters.Where(p => p.Name.Equals(name1))){
				return p;
			}
			return null;
		}

		public void UpdateControlsFromValue(){
			foreach (Parameter parameter in parameters){
				parameter.UpdateControlFromValue();
			}
		}

		public Parameter this[int i] => parameters[i];

		public void SetParametersFromConrtol(){
			foreach (Parameter parameter in parameters){
				parameter.SetValueFromControl();
			}
		}

		public void Clear(){
			foreach (Parameter parameter in parameters){
				parameter.Clear();
			}
		}

		public void ResetValues(){
			foreach (Parameter parameter in parameters){
				parameter.ResetValue();
			}
		}

		public void ResetDefaults(){
			foreach (Parameter parameter in parameters){
				parameter.ResetDefault();
			}
		}

	    public XmlSchema GetSchema()
	    {
	        throw new NotImplementedException();
	    }

	    public void ReadXml(XmlReader reader)
	    {
	        Name = reader.GetAttribute("Name");
            CollapsedDefault = Boolean.Parse(reader.GetAttribute("CollapsedDefault"));
            //parameters = ((Parameters) new XmlSerializer(typeof(Parameters)).Deserialize(reader)).GetAllParameters().ToList();
	        parameters = new List<Parameter>();
	        while (reader.Read())
	        {
	            switch (reader.NodeType)
	            {
                    case XmlNodeType.EndElement: // </Parameters>
	                    break;
                    case XmlNodeType.Element: // <SomeParam .../>
                        var type = Type.GetType(reader.GetAttribute("Type"));
                        var param = (Parameter) new XmlSerializer(type).Deserialize(reader);
                        parameters.Add(param);
	                    break;
	            }
	        }
	    }

	    public void WriteXml(XmlWriter writer)
	    {
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("CollapsedDefault", CollapsedDefault.ToString());
	        foreach (var parameter in parameters)
	        {
                new XmlSerializer(parameter.GetType()).Serialize(writer, parameter);
	        }
	    }
    }
}