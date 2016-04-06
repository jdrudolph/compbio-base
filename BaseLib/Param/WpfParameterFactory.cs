using System;
using BaseLibS.Param;

namespace BaseLib.Param{
	public static class WpfParameterFactory{
		public static Parameter Convert(Parameter p){
			if (p.Type == ParamType.Wpf){
				return p;
			}
			if (p is BoolParamS){
				BoolParamS q = (BoolParamS) p;
				return new BoolParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is BoolWithSubParamsS){
				BoolWithSubParamsS q = (BoolWithSubParamsS) p;
				q.SubParamsFalse?.Convert(Convert);
				q.SubParamsTrue?.Convert(Convert);
				return new BoolWithSubParams(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					SubParamsFalse = q.SubParamsFalse,
					SubParamsTrue = q.SubParamsTrue,
					Default = q.Default
				};
			}
			if (p is DictionaryIntValueParamS){
				DictionaryIntValueParamS q = (DictionaryIntValueParamS) p;
				return new DictionaryIntValueParam(q.Name, q.Value, q.Keys){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is DoubleParamS){
				DoubleParamS q = (DoubleParamS) p;
				return new DoubleParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is FileParamS){
				FileParamS q = (FileParamS) p;
				return new FileParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is FolderParamS){
				FolderParamS q = (FolderParamS) p;
				return new FolderParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is IntParamS){
				IntParamS q = (IntParamS) p;
				return new IntParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is LabelParamS){
				LabelParamS q = (LabelParamS) p;
				return new LabelParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is MultiChoiceMultiBinParamS){
				MultiChoiceMultiBinParamS q = (MultiChoiceMultiBinParamS) p;
				return new MultiChoiceMultiBinParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default
				};
			}
			if (p is MultiChoiceParamS){
				MultiChoiceParamS q = (MultiChoiceParamS) p;
				return new MultiChoiceParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Repeats = q.Repeats,
					Values = q.Values,
					Default = q.Default
				};
			}
			if (p is MultiFileParamS){
				MultiFileParamS q = (MultiFileParamS) p;
				return new MultiFileParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Filter = q.Filter,
					Default = q.Default
				};
			}
			if (p is MultiStringParamS){
				MultiStringParamS q = (MultiStringParamS) p;
				return new MultiStringParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is SingleChoiceParamS){
				SingleChoiceParamS q = (SingleChoiceParamS) p;
				return new SingleChoiceParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default
				};
			}
			if (p is SingleChoiceWithSubParamsS){
				SingleChoiceWithSubParamsS q = (SingleChoiceWithSubParamsS) p;
				foreach (var param in q.SubParams){
					param?.Convert(Convert);
				}
				SingleChoiceWithSubParams s = new SingleChoiceWithSubParams(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default,
					SubParams = new Parameters[q.SubParams.Count]
				};
				for (int i = 0; i < q.SubParams.Count; i++){
					s.SubParams[i] = q.SubParams[i];
				}
				return s;
			}
			if (p is StringParamS){
				StringParamS q = (StringParamS) p;
				return new StringParam(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is Ms1LabelParamS){
				Ms1LabelParamS q = (Ms1LabelParamS) p;
				return new Ms1LabelParam(q.Name, q.Value){
					Values = q.Values,
					Multiplicity = q.Multiplicity,
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default
				};
			}
			throw new Exception("Could not convert parameter");
		}
	}
}