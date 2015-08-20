using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class FileParam : Parameter<string>{
		public string Filter { get; set; }
		public Func<string, string> ProcessFileName { get; set; }
		public bool Save { get; set; }
		[NonSerialized] private FileParameterControl control;
		public FileParam(string name) : this(name, ""){}

		public FileParam(string name, string value) : base(name){
			Value = value;
			Default = value;
			Filter = null;
			Save = false;
		}

		public override string StringValue{
			get { return Value; }
			set { Value = value; }
		}

		public override void SetValueFromControl(){
			Value = control.Text;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.Text = Value;
		}

		public override void Clear(){
			Value = "";
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