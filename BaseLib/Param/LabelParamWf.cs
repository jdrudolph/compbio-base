using System;
using System.Windows.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class LabelParamWf : LabelParam{
		[NonSerialized] protected Label control;

	    public LabelParamWf(string name) : this(name, "") { }

	    public LabelParamWf(string name, string value) : base(name, value)
	    {
	        control = new Label() {Text = Value};
	    }
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			Value = control.Text;
		}

		public override void UpdateControlFromValue(){
			control.Text = Value;
		}

		public override object CreateControl()
		{
		    return control;
		}
	}
}