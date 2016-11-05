using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BaseLib.Forms;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class RegexMatchParamWpf : RegexMatchParam{
		[NonSerialized] private RegexMatchParamControl _control;
		public RegexMatchParamWpf(string name, Regex value, List<string> replacement) : base(name, value, replacement){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = _control._regex;
		}

		public override void UpdateControlFromValue(){
			_control._preview = Previews;
			_control.Regex = Value.ToString(); // setting as string will refresh the view
		}

		public override float Height => 200;

		public override object CreateControl(){
			_control = new RegexMatchParamControl(Value, Previews);
			return _control;
		}
	}
}