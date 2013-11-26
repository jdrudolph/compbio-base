using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseLib.ParamWf{
	[Serializable]
	public class ParameterGroupWf : ICloneable{
		private readonly IList<ParameterWf> parameters = new List<ParameterWf>();
		private string name;
		private bool collapsedDefault;

		public ParameterGroupWf(IList<ParameterWf> parameters, string name, bool collapsedDefault){
			this.name = name;
			this.parameters = parameters;
			this.collapsedDefault = collapsedDefault;
		}

		private ParameterGroupWf() {}
		public bool CollapsedDefault { get { return collapsedDefault; } set { collapsedDefault = value; } }
		public string[] Markup{
			get{
				List<string> result = new List<string>();
				foreach (ParameterWf p in parameters){
					result.AddRange(p.Markup);
				}
				return result.ToArray();
			}
		}
		public bool IsModified{
			get{
				foreach (ParameterWf parameterGroup in parameters){
					if (parameterGroup.IsModified){
						return true;
					}
				}
				return false;
			}
		}
		public string Name { get { return name; } set { name = value; } }

		public object Clone(){
			ParameterGroupWf newParam = new ParameterGroupWf();
			foreach (ParameterWf p in parameters){
				newParam.parameters.Add((ParameterWf) p.Clone());
			}
			newParam.name = name;
			newParam.collapsedDefault = collapsedDefault;
			return newParam;
		}

		public IList<ParameterWf> ParameterList { get { return parameters; } }
		public int Count { get { return parameters.Count; } }
		public float Height{
			get{
				float h = 0;
				foreach (ParameterWf parameter in parameters){
					h += parameter.Height;
				}
				return h;
			}
		}

		public ParameterWf GetParam(string name1){
			foreach (ParameterWf p in parameters.Where(p => p.Name.Equals(name1))){
				return p;
			}
			return null;
		}

		public void UpdateControlsFromValue(){
			foreach (ParameterWf parameter in parameters){
				parameter.UpdateControlFromValue();
			}
		}

		public ParameterWf this[int i] { get { return parameters[i]; } }

		public void SetParametersFromConrtol(){
			foreach (ParameterWf parameter in parameters){
				parameter.SetValueFromControl();
			}
		}

		public void Clear(){
			foreach (ParameterWf parameter in parameters){
				parameter.Clear();
			}
		}

		public void ResetValues(){
			foreach (ParameterWf parameter in parameters){
				parameter.ResetValue();
			}
		}

		public void ResetDefaults(){
			foreach (ParameterWf parameter in parameters){
				parameter.ResetDefault();
			}
		}
	}
}