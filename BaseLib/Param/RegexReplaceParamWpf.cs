using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class RegexReplaceParamWpf : RegexReplaceParam{
		[NonSerialized] private RegexReplaceParamViewModel viewModel;

		internal RegexReplaceParamWpf(string name, Regex pattern, string replacement, List<string> items)
			: base(name, pattern, replacement, items){}

		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = Tuple.Create(viewModel.Pattern, viewModel.Replacement);
		}

		public override float Height => 200;

		public override object CreateControl(){
			RegexReplaceParamControl control = new RegexReplaceParamControl(Value.Item1, Value.Item2, Previews);
			viewModel = control.DataContext as RegexReplaceParamViewModel;
			return control;
		}
	}
}