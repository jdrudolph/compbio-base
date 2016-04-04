using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class FileParam : FileParamS{
		[NonSerialized] private FileParameterControl control;
		public FileParam(string name) : base(name){}
		public FileParam(string name, string value) : base(name, value){}

		public override void SetValueFromControl(){
			Value = control.Text;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override object CreateControl(){
			return
				control = new FileParameterControl{Filter = Filter, ProcessFileName = ProcessFileName, Text = Value, Save = Save};
		}

		public override object Clone(){
			return new FileParam(Name, Value){
				Help = Help,
				Visible = Visible,
				Save = Save,
				Filter = Filter,
				Default = Default,
				ProcessFileName = ProcessFileName
			};
		}
	}
}