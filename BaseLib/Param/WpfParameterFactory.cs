using System;
using BaseLibS.Param;

namespace BaseLib.Param{
	public static class WpfParameterFactory{
        /// <summary>
        /// Convert <see cref="BaseLib.Param"/> to <see cref="BaseLibS.Param"/>
        /// used for xml serialization
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
	    public static Parameter ConvertBack(Parameter p)
	    {
			if (p.Type == ParamType.Server){
				return p;
			}
			if (p is RegexReplaceParamWpf){
				RegexReplaceParamWpf q = (RegexReplaceParamWpf) p;
				RegexReplaceParam b = new RegexReplaceParam(q.Name, q.Value.Item1, q.Value.Item2, q.Previews){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is RegexMatchParamWpf){
				RegexMatchParamWpf q = (RegexMatchParamWpf) p;
				RegexMatchParam b = new RegexMatchParam(q.Name, q.Value, q.Previews){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is BoolParamWpf){
				BoolParamWpf q = (BoolParamWpf) p;
				BoolParam b = new BoolParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is BoolWithSubParamsWpf){
				BoolWithSubParamsWpf q = (BoolWithSubParamsWpf) p;
				q.SubParamsFalse?.Convert(ConvertBack);
				q.SubParamsTrue?.Convert(ConvertBack);
				BoolWithSubParams b = new BoolWithSubParams(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					SubParamsFalse = q.SubParamsFalse,
					SubParamsTrue = q.SubParamsTrue,
					Default = q.Default,
					ParamNameWidth = q.ParamNameWidth,
					TotalWidth = q.TotalWidth,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is DictionaryIntValueParamWpf){
				DictionaryIntValueParamWpf q = (DictionaryIntValueParamWpf) p;
				DictionaryIntValueParam b = new DictionaryIntValueParam(q.Name, q.Value, q.Keys){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is DoubleParamWpf){
				DoubleParamWpf q = (DoubleParamWpf) p;
				DoubleParam b = new DoubleParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is FileParamWpf){
				FileParamWpf q = (FileParamWpf) p;
				FileParam b = new FileParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Filter = q.Filter,
					ProcessFileName = q.ProcessFileName,
					Save = q.Save,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is FolderParamWpf){
				FolderParamWpf q = (FolderParamWpf) p;
				FolderParamWpf b = new FolderParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is IntParamWpf){
				IntParamWpf q = (IntParamWpf) p;
				IntParam b = new IntParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is LabelParamWpf){
				LabelParamWpf q = (LabelParamWpf) p;
				LabelParam b = new LabelParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiChoiceMultiBinParamWpf){
				MultiChoiceMultiBinParamWpf q = (MultiChoiceMultiBinParamWpf) p;
				MultiChoiceMultiBinParam b = new MultiChoiceMultiBinParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Bins = q.Bins,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiChoiceParamWpf){
				MultiChoiceParamWpf q = (MultiChoiceParamWpf) p;
				MultiChoiceParam b = new MultiChoiceParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Repeats = q.Repeats,
					Values = q.Values,
					Default = q.Default,
					DefaultSelections = q.DefaultSelections,
					DefaultSelectionNames = q.DefaultSelectionNames,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiFileParamWpf){
				MultiFileParamWpf q = (MultiFileParamWpf) p;
				MultiFileParam b = new MultiFileParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Filter = q.Filter,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiStringParamWpf){
				MultiStringParamWpf q = (MultiStringParamWpf) p;
				MultiStringParam b = new MultiStringParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is SingleChoiceParamWpf){
				SingleChoiceParamWpf q = (SingleChoiceParamWpf) p;
				SingleChoiceParam b = new SingleChoiceParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is SingleChoiceWithSubParamsWpf){
				SingleChoiceWithSubParamsWpf q = (SingleChoiceWithSubParamsWpf) p;
				foreach (Parameters param in q.SubParams){
					param?.Convert(ConvertBack);
				}
				SingleChoiceWithSubParams b = new SingleChoiceWithSubParams(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default,
					SubParams = new Parameters[q.SubParams.Count],
					ParamNameWidth = q.ParamNameWidth,
					TotalWidth = q.TotalWidth,
					Url = q.Url
				};
				for (int i = 0; i < q.SubParams.Count; i++){
					b.SubParams[i] = q.SubParams[i];
				}
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is StringParamWpf){
				StringParamWpf q = (StringParamWpf) p;
				StringParam b = new StringParam(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is Ms1LabelParamWpf){
				Ms1LabelParamWpf q = (Ms1LabelParamWpf) p;
				Ms1LabelParam b = new Ms1LabelParam(q.Name, q.Value){
					Values = q.Values,
					Multiplicity = q.Multiplicity,
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
            return p; // and hope for the best
	    }
        /// <summary>
        /// Convert <see cref="BaseLibS.Param"/> to <see cref="BaseLib.Param"/>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
		public static Parameter Convert(Parameter p){
			if (p.Type == ParamType.Wpf){
				return p;
			}
			if (p is RegexReplaceParam){
				RegexReplaceParam q = (RegexReplaceParam) p;
				RegexReplaceParamWpf b = new RegexReplaceParamWpf(q.Name, q.Value.Item1, q.Value.Item2, q.Previews){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is RegexMatchParam){
				RegexMatchParam q = (RegexMatchParam) p;
				RegexMatchParamWpf b = new RegexMatchParamWpf(q.Name, q.Value, q.Previews){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is BoolParam){
				BoolParam q = (BoolParam) p;
				BoolParamWpf b = new BoolParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is BoolWithSubParams){
				BoolWithSubParams q = (BoolWithSubParams) p;
				q.SubParamsFalse?.Convert(Convert);
				q.SubParamsTrue?.Convert(Convert);
				BoolWithSubParamsWpf b = new BoolWithSubParamsWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					SubParamsFalse = q.SubParamsFalse,
					SubParamsTrue = q.SubParamsTrue,
					Default = q.Default,
					ParamNameWidth = q.ParamNameWidth,
					TotalWidth = q.TotalWidth,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is DictionaryIntValueParam){
				DictionaryIntValueParam q = (DictionaryIntValueParam) p;
				DictionaryIntValueParamWpf b = new DictionaryIntValueParamWpf(q.Name, q.Value, q.Keys){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is DoubleParam){
				DoubleParam q = (DoubleParam) p;
				DoubleParamWpf b = new DoubleParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is FileParam){
				FileParam q = (FileParam) p;
				FileParamWpf b = new FileParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Filter = q.Filter,
					ProcessFileName = q.ProcessFileName,
					Save = q.Save,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is FolderParam){
				FolderParam q = (FolderParam) p;
				FolderParamWpf b = new FolderParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is IntParam){
				IntParam q = (IntParam) p;
				IntParamWpf b = new IntParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is LabelParam){
				LabelParam q = (LabelParam) p;
				LabelParamWpf b = new LabelParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiChoiceMultiBinParam){
				MultiChoiceMultiBinParam q = (MultiChoiceMultiBinParam) p;
				MultiChoiceMultiBinParamWpf b = new MultiChoiceMultiBinParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Bins = q.Bins,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiChoiceParam){
				MultiChoiceParam q = (MultiChoiceParam) p;
				MultiChoiceParamWpf b = new MultiChoiceParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Repeats = q.Repeats,
					Values = q.Values,
					Default = q.Default,
					DefaultSelections = q.DefaultSelections,
					DefaultSelectionNames = q.DefaultSelectionNames,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiFileParam){
				MultiFileParam q = (MultiFileParam) p;
				MultiFileParamWpf b = new MultiFileParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Filter = q.Filter,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is MultiStringParam){
				MultiStringParam q = (MultiStringParam) p;
				MultiStringParamWpf b = new MultiStringParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is SingleChoiceParam){
				SingleChoiceParam q = (SingleChoiceParam) p;
				SingleChoiceParamWpf b = new SingleChoiceParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is SingleChoiceWithSubParams){
				SingleChoiceWithSubParams q = (SingleChoiceWithSubParams) p;
				foreach (Parameters param in q.SubParams){
					param?.Convert(Convert);
				}
				SingleChoiceWithSubParamsWpf b = new SingleChoiceWithSubParamsWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Values = q.Values,
					Default = q.Default,
					SubParams = new Parameters[q.SubParams.Count],
					ParamNameWidth = q.ParamNameWidth,
					TotalWidth = q.TotalWidth,
					Url = q.Url
				};
				for (int i = 0; i < q.SubParams.Count; i++){
					b.SubParams[i] = q.SubParams[i];
				}
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is StringParam){
				StringParam q = (StringParam) p;
				StringParamWpf b = new StringParamWpf(q.Name, q.Value){
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			if (p is Ms1LabelParam){
				Ms1LabelParam q = (Ms1LabelParam) p;
				Ms1LabelParamWpf b = new Ms1LabelParamWpf(q.Name, q.Value){
					Values = q.Values,
					Multiplicity = q.Multiplicity,
					Help = q.Help,
					Visible = q.Visible,
					Default = q.Default,
					Url = q.Url
				};
				foreach (ValueChangedHandler act in q.GetPropertyChangedHandlers()){
					b.ValueChanged += act;
				}
				return b;
			}
			throw new Exception("Could not convert parameter");
		}
	}
}