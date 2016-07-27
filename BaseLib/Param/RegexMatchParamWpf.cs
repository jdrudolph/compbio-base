using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class RegexMatchParamWpf : RegexMatchParam {
		[NonSerialized] private RegexMatchParamViewModel viewModel;
		public RegexMatchParamWpf(string name, Tuple<Regex, List<string>> value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			Value = Tuple.Create(viewModel.Pattern, Value.Item2);
		}

	    public override void UpdateControlFromValue()
	    {
	        viewModel.Pattern = Value.Item1;
	        viewModel.Items = Value.Item2;
	    }

	    public override float Height => 200;

	    public override object CreateControl(){
			var control = new RegexMatchParamControl(Value);
		    viewModel = control.DataContext as RegexMatchParamViewModel;
            return control;
		}
	}
}