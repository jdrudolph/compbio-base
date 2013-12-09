using System;
using System.Collections.Generic;

namespace BaseLib.Param{
	[Serializable]
	public class Parameters : ICloneable{
		private readonly List<ParameterGroup> paramGroups = new List<ParameterGroup>();

		public Parameters(IList<Parameter> param, string name){
			AddParameterGroup(param, name, false);
		}

		public Parameters(Parameter param) : this(new[]{param}) {}
		public Parameters(IList<Parameter> param) : this(param, null) {}
		public Parameters() {}

		public Parameter[] GetAllParameters(){
			List<Parameter> result = new List<Parameter>();
			foreach (ParameterGroup pg in paramGroups){
				result.AddRange(pg.ParameterList);
			}
			return result.ToArray();
		}

		public string[] GetAllGroupHeadings() {
			List<string> result = new List<string>();
			foreach (ParameterGroup pg in paramGroups) {
				result.Add(pg.Name);
			}
			return result.ToArray();
		}

		public string[] Markup{
			get{
				List<string> result = new List<string>();
				foreach (ParameterGroup paramGroup in paramGroups){
					result.AddRange(paramGroup.Markup);
				}
				return result.ToArray();
			}
		}
		public bool IsModified{
			get{
				foreach (ParameterGroup parameterGroup in paramGroups){
					if (parameterGroup.IsModified){
						return true;
					}
				}
				return false;
			}
		}

		public void AddParameterGroup(IList<Parameter> param, string name, bool collapsed){
			paramGroups.Add(new ParameterGroup(param, name, collapsed));
		}

		public object Clone(){
			Parameters newParam = new Parameters();
			foreach (ParameterGroup p in paramGroups){
				newParam.paramGroups.Add((ParameterGroup) p.Clone());
			}
			return newParam;
		}

		public int Count{
			get{
				int c = 0;
				foreach (ParameterGroup parameterGroup in paramGroups){
					c += parameterGroup.Count;
				}
				return c;
			}
		}
		public float Height{
			get{
				float h = 0;
				foreach (ParameterGroup parameter in paramGroups){
					h += parameter.Height;
				}
				return h;
			}
		}
		public int GroupCount { get { return paramGroups.Count; } }

		public BoolParam GetBoolParam(string name){
			return (BoolParam) GetParam(name);
		}

		public SingleChoiceParam GetSingleChoiceParam(string name){
			return (SingleChoiceParam) GetParam(name);
		}

		public SingleChoiceWithSubParams GetSingleChoiceWithSubParams(string name){
			return (SingleChoiceWithSubParams) GetParam(name);
		}

		public BoolWithSubParams GetBoolWithSubParams(string name){
			return (BoolWithSubParams) GetParam(name);
		}

		public StringParam GetStringParam(string name){
			return (StringParam) GetParam(name);
		}

		public LabelParam GetLabelParam(string name){
			return (LabelParam) GetParam(name);
		}

		public MultiStringParam GetMultiStringParam(string name){
			return (MultiStringParam) GetParam(name);
		}

		public MultiChoiceParam GetMultiChoiceParam(string name){
			return (MultiChoiceParam) GetParam(name);
		}

		public IntParam GetIntParam(string name){
			return (IntParam) GetParam(name);
		}

		public DoubleParam GetDoubleParam(string name){
			return (DoubleParam) GetParam(name);
		}

		public FileParam GetFileParam(string name){
			return (FileParam) GetParam(name);
		}

		public MultiFileParam GetMultiFileParam(string name){
			return (MultiFileParam) GetParam(name);
		}

		public FolderParam GetFolderParam(string name){
			return (FolderParam) GetParam(name);
		}

		public MultiChoiceMultiBinParam GetMultiChoiceMultiBinParam(string name){
			return (MultiChoiceMultiBinParam) GetParam(name);
		}

		public Parameter GetParam(string name){
			foreach (ParameterGroup parameterGroup in paramGroups){
				Parameter p = parameterGroup.GetParam(name);
				if (p != null){
					return p;
				}
			}
			throw new Exception("Parameter does not exist: " + name);
		}

		public Parameter[] GetDropTargets() {
			List<Parameter> result = new List<Parameter>();
			foreach (ParameterGroup parameterGroup in paramGroups) {
				foreach (Parameter p in parameterGroup.ParameterList) {
					if (p.IsDropTarget) {
						result.Add(p);
					}
				}
			}
			return result.ToArray();
		}

		public void SetSizes(int paramNameWidth, int totalWidth) {
			foreach (ParameterGroup parameterGroup in paramGroups) {
				foreach (Parameter p in parameterGroup.ParameterList) {
					if (p is SingleChoiceWithSubParams){
						SingleChoiceWithSubParams q = (SingleChoiceWithSubParams) p;
						q.paramNameWidth = paramNameWidth;
						q.totalWidth = totalWidth;
					}
				}
			}
		}

		public Parameter GetParamNoException(string name) {
			foreach (ParameterGroup parameterGroup in paramGroups){
				Parameter p = parameterGroup.GetParam(name);
				if (p != null){
					return p;
				}
			}
			return null;
		}

		public void UpdateControlsFromValue(){
			foreach (ParameterGroup parameterGroup in paramGroups){
				parameterGroup.UpdateControlsFromValue();
			}
		}

		public ParameterGroup GetGroup(int i){
			return paramGroups[i];
		}

		public void SetValuesFromControl(){
			foreach (ParameterGroup parameterGroup in paramGroups){
				parameterGroup.SetParametersFromConrtol();
			}
		}

		public void Clear(){
			foreach (ParameterGroup parameterGroup in paramGroups){
				parameterGroup.Clear();
			}
		}

		public void ResetValues(){
			foreach (ParameterGroup parameterGroup in paramGroups){
				parameterGroup.ResetValues();
			}
		}

		public void ResetDefaults(){
			foreach (ParameterGroup parameterGroup in paramGroups){
				parameterGroup.ResetDefaults();
			}
		}

		public Parameter FindParameter(string paramName){
			return FindParameter(paramName, this);
		}

		private static Parameter FindParameter(string paramName, Parameters parameters) {
			Parameter p = parameters.GetParamNoException(paramName);
			if (p != null) {
				return p;
			}
			foreach (Parameter px in parameters.GetAllParameters()) {
				if (px is ParameterWithSubParams) {
					Parameters ps = ((ParameterWithSubParams)px).GetSubParameters();
					Parameter pq = FindParameter(paramName, ps);
					if (pq != null) {
						return pq;
					}
				}
			}
			return null;
		}
	}
}