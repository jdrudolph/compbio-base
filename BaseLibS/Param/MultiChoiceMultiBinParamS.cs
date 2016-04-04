using System;
using System.Collections.Generic;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLibS.Param{
	[Serializable]
	public class MultiChoiceMultiBinParamS : Parameter<int[][]>{
		public IList<string> Values { get; set; }
		public IList<string> Bins { get; set; }
		public MultiChoiceMultiBinParamS(string name) : this(name, new int[0][]){}

		public MultiChoiceMultiBinParamS(string name, int[][] value) : base(name){
			Value = value;
			Default = new int[value.Length][];
			for (int i = 0; i < value.Length; i++){
				Default[i] = new int[value[i].Length];
				for (int j = 0; j < value[i].Length; j++){
					Default[i][j] = value[i][j];
				}
			}
			Values = new string[0];
			Bins = new string[0];
		}

		public override string StringValue{
			get { return StringUtils.Concat(";", ",", Value); }
			set{
				if (value.Trim().Length == 0){
					Value = new int[0][];
					return;
				}
				string[] q = value.Trim().Split(';');
				Value = new int[q.Length][];
				for (int i = 0; i < Value.Length; i++){
					string[] r = q[i].Trim().Split();
					Value[i] = new int[r.Length];
					for (int j = 0; j < r.Length; j++){
						Value[i][j] = int.Parse(r[j]);
					}
				}
			}
		}

		public string[][] SelectedValues{
			get{
				string[][] result = new string[Value.Length][];
				for (int i = 0; i < result.Length; i++){
					result[i] = ArrayUtils.SubArray(Values, Value[i]);
				}
				return result;
			}
		}

		public string[][] SelectedValues2{
			get{
				SetValueFromControl();
				return SelectedValues;
			}
		}

		public override bool IsModified => !ArrayUtils.EqualArraysOfArrays(Value, Default);

		public override void Clear(){
			Value = new int[0][];
		}

		public override float Height => 310f;

		public override object Clone(){
			MultiChoiceMultiBinParamS s = new MultiChoiceMultiBinParamS(Name, Value){
				Help = Help,
				Visible = Visible,
				Values = Values,
				Default = Default,
			};
			return s;
		}

		public override void ResetSubParamValues(){
			Value = Default;
		}

		public override void ResetSubParamDefaults(){
			Default = Value;
		}
	}
}