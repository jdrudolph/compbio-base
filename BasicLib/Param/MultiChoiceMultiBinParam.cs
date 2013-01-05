using System;
using System.Windows.Forms;
using System.Collections.Generic;
using BasicLib.Forms;
using BasicLib.Forms.Select;
using BasicLib.Util;

namespace BasicLib.Param{
	[Serializable]
	public class MultiChoiceMultiBinParam : Parameter{
		public int[][] Value { get; set; }
		public int[][] Default { get; private set; }
		public IList<string> Values { get; set; }
		public IList<string> Bins { get; set; }
		public MultiChoiceMultiBinParam(string name) : this(name, new int[0][]) {}

		public MultiChoiceMultiBinParam(string name, int[][] value) : base(name){
			Value = value;
			Default = value;
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
		public int[][] Value2{
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

		public override bool IsModified { get { return !ArrayUtils.EqualArraysOfArrays(Value, Default); } }
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

		public override void SetValueFromControl(){
			MultiListSelector ls = (MultiListSelector) control;
			int[][] val = ls.SelectedIndices;
			Value = val;
		}

		public override void Clear(){
			Value = new int[0][];
		}

		public override void UpdateControlFromValue(){
			MultiListSelector ls = (MultiListSelector) control;
			ls.SelectedIndices = Value;
		}

		protected override Control Control{
			get{
				MultiListSelector ls = new MultiListSelector();
				ls.Init(Values, Bins);
				ls.SelectedIndices = Value;
				return ls;
			}
		}
		public override float Height { get { return 310f; } }

		public override object Clone(){
			return new MultiChoiceMultiBinParam(Name, Value){Help = Help, Visible = Visible, Values = Values, Default = Default};
		}
	}
}