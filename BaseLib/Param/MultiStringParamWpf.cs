using System;
using System.Collections.Generic;
using System.Windows.Controls;
using BaseLibS.Param;
using BaseLibS.Util;

namespace BaseLib.Param{
	[Serializable]
	public class MultiStringParamWpf : MultiStringParam{
		[NonSerialized] private TextBox control;
		public MultiStringParamWpf(string name) : base(name){}
		public MultiStringParamWpf(string name, string[] value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			string text = control.Text;
			string[] b = text.Split('\n');
			List<string> result = new List<string>();
			foreach (string x in b){
				string y = x.Trim();
				if (y.Length > 0){
					result.Add(y);
				}
			}
			Value = result.ToArray();
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = StringUtils.Concat("\n", Value);
		}

		public override object CreateControl(){
			return control = new TextBox{Text = StringUtils.Concat("\n", Value), AcceptsReturn = true};
		}
	}
}