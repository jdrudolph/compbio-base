using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class FolderParam : FolderParamS{
		[NonSerialized] private FolderParameterControl control;
		public FolderParam(string name) : base(name){}
		public FolderParam(string name, string value) : base(name, value){}

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
			return control = new FolderParameterControl{Text = Value};
		}

		public override object Clone(){
			return new FolderParam(Name, Value){Help = Help, Visible = Visible, Default = Default};
		}
	}
}