using System;

namespace BaseLibS.Param{
	[Serializable]
	public class FolderParamS : Parameter<string>{
		public FolderParamS(string name) : this(name, ""){}

		public FolderParamS(string name, string value) : base(name){
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
	}
}