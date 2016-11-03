using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BaseLibS.Param;
using BaseLib.Forms;

namespace BaseLib.Param{
	[Serializable]
	internal class RegexReplaceParamWpf : RegexReplaceParam{
		[NonSerialized] private RegexReplaceParamControl _control;

		internal RegexReplaceParamWpf(string name, Regex pattern, string replacement, List<string> items)
			: base(name, pattern, replacement, items){}

		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = _control?.GetValue();
		}

		public override float Height => 200;

		public override object CreateControl(){
            _control = new RegexReplaceParamControl(Value.Item1, Value.Item2, Previews);
			return _control;
		}
	}
}