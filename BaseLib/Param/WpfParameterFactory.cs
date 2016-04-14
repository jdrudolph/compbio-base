using System;
using BaseLibS.Param;

namespace BaseLib.Param{
	public static class WpfParameterFactory{
		public static Parameter Convert(Parameter p){
			if (p.Type == ParamType.Wpf){
				return p;
			}
			if (p is BoolParam){
				BoolParam q = (BoolParam) p;
				return new BoolParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is BoolWithSubParams){
				BoolWithSubParams q = (BoolWithSubParams) p;
				q.SubParamsFalse?.Convert(Convert);
				q.SubParamsTrue?.Convert(Convert);
				return new BoolWithSubParamsWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					SubParamsFalse = q.SubParamsFalse,
					SubParamsTrue = q.SubParamsTrue,
					Default = q.Default,
					ParamNameWidth = q.ParamNameWidth,
					TotalWidth = q.TotalWidth
				};
			}
			if (p is DictionaryIntValueParam){
				DictionaryIntValueParam q = (DictionaryIntValueParam) p;
				return new DictionaryIntValueParamWpf(q.Name, q.Value, q.Keys){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default
				};
			}
			if (p is DoubleParam){
				DoubleParam q = (DoubleParam) p;
				return new DoubleParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is FileParam){
				FileParam q = (FileParam) p;
				return new FileParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Filter = q.Filter,
					ProcessFileName = q.ProcessFileName,
					Save = q.Save
				};
			}
			if (p is FolderParam){
				FolderParam q = (FolderParam) p;
				return new FolderParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is IntParam){
				IntParam q = (IntParam) p;
				return new IntParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is LabelParam){
				LabelParam q = (LabelParam) p;
				return new LabelParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is MultiChoiceMultiBinParam){
				MultiChoiceMultiBinParam q = (MultiChoiceMultiBinParam) p;
				return new MultiChoiceMultiBinParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Bins = q.Bins,
					Default = q.Default
				};
			}
			if (p is MultiChoiceParam){
				MultiChoiceParam q = (MultiChoiceParam) p;
				return new MultiChoiceParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Repeats = q.Repeats,
					Values = q.Values,
					Default = q.Default
				};
			}
			if (p is MultiFileParam){
				MultiFileParam q = (MultiFileParam) p;
				return new MultiFileParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Filter = q.Filter,
					Default = q.Default
				};
			}
			if (p is MultiStringParam){
				MultiStringParam q = (MultiStringParam) p;
				return new MultiStringParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is SingleChoiceParam){
				SingleChoiceParam q = (SingleChoiceParam) p;
				return new SingleChoiceParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default
				};
			}
			if (p is SingleChoiceWithSubParams){
				SingleChoiceWithSubParams q = (SingleChoiceWithSubParams) p;
				foreach (var param in q.SubParams){
					param?.Convert(Convert);
				}
				SingleChoiceWithSubParamsWpf s = new SingleChoiceWithSubParamsWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default,
					SubParams = new Parameters[q.SubParams.Count],
					ParamNameWidth = q.ParamNameWidth,
					TotalWidth = q.TotalWidth
				};
				for (int i = 0; i < q.SubParams.Count; i++){
					s.SubParams[i] = q.SubParams[i];
				}
				return s;
			}
			if (p is StringParam){
				StringParam q = (StringParam) p;
				return new StringParamWpf(q.Name, q.Value){Help = q.Help, Visible = q.Visible, Default = q.Default};
			}
			if (p is Ms1LabelParam){
				Ms1LabelParam q = (Ms1LabelParam) p;
				return new Ms1LabelParamWpf(q.Name, q.Value){
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