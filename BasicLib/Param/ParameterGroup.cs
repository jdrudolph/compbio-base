using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicLib.Param{
	[Serializable]
	public class ParameterGroup : ICloneable{
		private readonly IList<Parameter> parameters = new List<Parameter>();
		private string name;
		private bool collapsedDefault;

		public ParameterGroup(IList<Parameter> parameters, string name, bool collapsedDefault){
			this.name = name;
			this.parameters = parameters;
			this.collapsedDefault = collapsedDefault;
		}

		private ParameterGroup() {}
		public bool CollapsedDefault { get { return collapsedDefault; } set { collapsedDefault = value; } }
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
				foreach (Parameter parameterGroup in parameters){
					if (parameterGroup.IsModified){
						return true;
					}
				}
				return false;
			}
		}
		public string Name { get { return name; } set { name = value; } }

		public object Clone(){
			ParameterGroup newParam = new ParameterGroup();
			foreach (Parameter p in parameters){
				newParam.parameters.Add((Parameter) p.Clone());
			}
			newParam.name = name;
			newParam.collapsedDefault = collapsedDefault;
			return newParam;
		}

		public IList<Parameter> ParameterList { get { return parameters; } }
		public int Count { get { return parameters.Count; } }
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

		public Parameter this[int i] { get { return parameters[i]; } }

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
	}
}