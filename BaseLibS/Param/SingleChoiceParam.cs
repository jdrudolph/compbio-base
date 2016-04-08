using System;
using System.Collections.Generic;

namespace BaseLibS.Param{
	[Serializable]
	public class SingleChoiceParam : Parameter<int>{
		public IList<string> Values { get; set; }
		public SingleChoiceParam(string name) : this(name, 0){}

		public SingleChoiceParam(string name, int value) : base(name){
			Value = value;
			Default = value;
			Values = new[]{""};
		}

		public override string StringValue{
			get{
				if (Value < 0 || Value >= Values.Count){
					return null;
				}
				return Values[Value];
			}
			set{
				for (int i = 0; i < Values.Count; i++){
					if (Values[i].Equals(value)){
						Value = i;
						break;
					}
				}
			}
		}

		public override void Clear(){
			Value = 0;
		}
		public override ParamType Type => ParamType.Server;
	}
}