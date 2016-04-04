using System;
using System.Collections.Generic;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLibS.Param{
	[Serializable]
	public class MultiChoiceParamS : Parameter<int[]>{
		public bool Repeats { get; set; }
		public IList<string> Values { get; set; }
		protected readonly List<string> defaultSelectionNames = new List<string>();
		protected readonly List<string[]> defaultSelections = new List<string[]>();
		public MultiChoiceParamS(string name) : this(name, new int[0]){}

		public MultiChoiceParamS(string name, int[] value) : base(name){
			Value = value;
			Default = new int[Value.Length];
			for (int i = 0; i < Value.Length; i++){
				Default[i] = Value[i];
			}
			Values = new string[0];
			Repeats = false;
		}

		public override string StringValue{
			get { return StringUtils.Concat(";", ArrayUtils.SubArray(Values, Value)); }
			set{
				if (value.Trim().Length == 0){
					Value = new int[0];
					return;
				}
				string[] q = value.Trim().Split(';');
				Value = new int[q.Length];
				for (int i = 0; i < Value.Length; i++){
					Value[i] = Values.IndexOf(q[i]);
				}
				Value = Filter(Value);
			}
		}

		private static int[] Filter(IEnumerable<int> value){
			List<int> result = new List<int>();
			foreach (int i in value){
				if (i >= 0){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		public override bool IsModified => !ArrayUtils.EqualArrays(Value, Default);

		public override void Clear(){
			Value = new int[0];
		}

		public override float Height => 160f;

		public void AddSelectedIndex(int index){
			if (Array.BinarySearch(Value, index) >= 0){
				return;
			}
			Value = InsertSorted(Value, index);
		}

		private static int[] InsertSorted(IList<int> value, int index){
			int[] result = ArrayUtils.Concat(value, index);
			Array.Sort(result);
			return result;
		}

		public void AddDefaultSelector(string title, string[] sel){
			defaultSelectionNames.Add(title);
			defaultSelections.Add(sel);
		}

		public void SetFromStrings(string[] x){
			List<int> indices = new List<int>();
			foreach (string s in x){
				int ind = Values.IndexOf(s);
				indices.Add(ind);
			}
			indices.Sort();
			Value = indices.ToArray();
		}
	}
}