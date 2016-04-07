using System;
using System.Collections.Generic;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLibS.Param{
	[Serializable]
	public class Ms1LabelParamS : Parameter<int[][]>{
		public int Multiplicity { get; set; }
		public string[] Values { get; set; }

		public Ms1LabelParamS(string name, int[][] value) : base(name){
			Value = value;
			Default = (int[][]) value.Clone();
		}

		public override ParamType Type => ParamType.Server;

		public override string StringValue{
			get { return StringUtils.Concat(",", ";", Value); }
			set { Value = StringUtils.SplitToInt(',', ';', value); }
		}

		public override void Clear(){
			Value = new int[Multiplicity][];
		}

		public override bool IsModified => !ArrayUtils.EqualArraysOfArrays(Value, Default);
		public override float Height => 154f;

		public string[] GetLabels(int ind){
			return ArrayUtils.SubArray(Values, Value[ind]);
		}

		public void SetLabels(string[][] labels){
			for (int i = 0; i < labels.Length; i++){
				Value[i] = GetIndices(Values, labels[i]);
			}
		}

		public static int[] GetIndices(string[] values, IList<string> strings){
			int[] result = new int[strings.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = ArrayUtils.IndexOf(values, strings[i]);
			}
			return result;
		}
	}
}