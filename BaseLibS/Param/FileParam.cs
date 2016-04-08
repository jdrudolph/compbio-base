using System;

namespace BaseLibS.Param{
	[Serializable]
	public class FileParam : Parameter<string>{
		public string Filter { get; set; }
		public Func<string, string> ProcessFileName { get; set; }
		public bool Save { get; set; }
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

		public override void Clear(){
			Value = "";
		}
		public override ParamType Type => ParamType.Server;
	}
}