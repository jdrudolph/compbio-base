using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using BasicLib.Forms.Select;
using BasicLib.Util;

namespace BasicLib.Param{
	[Serializable]
	public class MultiChoiceParam : Parameter{
		public bool Repeats { get; set; }
		public int[] Value { get; set; }
		public int[] Default { get; private set; }
		public IList<string> Values { get; set; }
		private readonly List<string> defaultSelectionNames = new List<string>();
		private readonly List<string[]> defaultSelections = new List<string[]>();
		public MultiChoiceParam(string name) : this(name, new int[0]) {}

		public MultiChoiceParam(string name, int[] value) : base(name){
			Value = value;
			Default = value;
			Values = new string[0];
			Repeats = false;
		}

		public override string StringValue{
			get { return StringUtils.Concat(",", Value); }
			set{
				if (value.Trim().Length == 0){
					Value = new int[0];
					return;
				}
				string[] q = value.Trim().Split(',');
				Value = new int[q.Length];
				for (int i = 0; i < Value.Length; i++){
					Value[i] = int.Parse(q[i]);
				}
			}
		}
		public int[] Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}

		public override void ResetValue(){
			Value = Default;
		}

		public override void ResetDefault(){
			Default = Value;
		}

		public override bool IsModified { get { return !ArrayUtils.EqualArrays(Value, Default); } }
		public string[] SelectedValues { get { return ArrayUtils.SubArray(Values, Value); } }
		public string[] SelectedValues2{
			get{
				SetValueFromControl();
				return SelectedValues;
			}
		}

		public override void SetValueFromControl(){
			ListSelector ls = (ListSelector) control;
			int[] val = ls.SelectedIndices;
			Value = val;
		}

		public override void Clear(){
			Value = new int[0];
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			ListSelector ls = (ListSelector)control;
			ls.SelectedIndices = Value;
		}

		protected override Control Control{
			get{
				ListSelector ls = new ListSelector{HasMoveButtons = true};
				ls.Items.AddRange(Values.ToArray());
				ls.Repeats = Repeats;
				ls.SelectedIndices = Value;
				ls.SetDefaultSelectors(defaultSelectionNames, defaultSelections);
				return ls;
			}
		}
		public override float Height { get { return 160f; } }

		public override object Clone(){
			return new MultiChoiceParam(Name, Value)
			{Help = Help, Visible = Visible, Repeats = Repeats, Values = Values, Default = Default};
		}

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