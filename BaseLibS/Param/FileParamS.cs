using System;

namespace BaseLibS.Param{
	[Serializable]
	public class FileParamS : Parameter<string>{
		public string Filter { get; set; }
		public Func<string, string> ProcessFileName { get; set; }
		public bool Save { get; set; }
		public FileParamS(string name) : this(name, ""){}

		public FileParamS(string name, string value) : base(name){
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
	}
}