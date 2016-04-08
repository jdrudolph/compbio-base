using System;

namespace BaseLibS.Param{
	[Serializable]
	public class FolderParam : Parameter<string>{
		public FolderParam(string name) : this(name, ""){}

		public FolderParam(string name, string value) : base(name){
			Value = value;
			Default = value;
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