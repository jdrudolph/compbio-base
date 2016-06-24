using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	internal class RegexParamWpf : RegexParam {
		[NonSerialized] private RegexParamViewModel viewModel;
		internal RegexParamWpf(string name) : base(name){}
		internal RegexParamWpf(string name, Regex pattern, string replacement, List<string> items) : base(name, pattern, replacement, items){}
		internal RegexParamWpf(string name, Tuple<Regex, string, List<string>> value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			Value = Tuple.Create(viewModel.Pattern, viewModel.Replacement, Value.Item3);
		}

	    public override float Height => 200;

	    public override object CreateControl(){
			var control = new RegexParamControl(Value);
		    viewModel = control.DataContext as RegexParamViewModel;
            return control;
		}
	}
}