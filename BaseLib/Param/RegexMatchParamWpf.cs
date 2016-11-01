using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class RegexMatchParamWpf : RegexMatchParam{
		[NonSerialized] private RegexMatchParamViewModel viewModel;
		public RegexMatchParamWpf(string name, Regex value, List<string> replacement) : base(name, value, replacement){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = viewModel.Pattern;
		}

		public override void UpdateControlFromValue(){
			viewModel.Pattern = Value;
			viewModel.Items = Previews;
		}

		public override float Height => 200;

		public override object CreateControl(){
			var control = new RegexMatchParamControl(Value, Previews);
			viewModel = control.DataContext as RegexMatchParamViewModel;
			return control;
		}
	}
}