using System;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLibS.Param{
	[Serializable]
	public class MultiStringParamS : Parameter<string[]>{
		public MultiStringParamS(string name) : this(name, new string[0]){}

		public MultiStringParamS(string name, string[] value) : base(name){
			Value = value;
			Default = new string[Value.Length];
			for (int i = 0; i < Value.Length; i++){
				Default[i] = Value[i];
			}
		}

		public override string StringValue{
			get { return StringUtils.Concat(",", Value); }
			set{
				if (value.Trim().Length == 0){
					Value = new string[0];
					return;
				}
				Value = value.Split(',');
			}
		}

		public override bool IsModified => !ArrayUtils.EqualArrays(Default, Value);

		public override void Clear(){
			Value = new string[0];
		}

		public override float Height => 150f;
	}
}