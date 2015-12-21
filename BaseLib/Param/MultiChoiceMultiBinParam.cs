using System;
using System.Collections.Generic;
using BaseLib.Wpf;
using BaseLibS.Num;
using BaseLibS.Param;
using BaseLibS.Util;

namespace BaseLib.Param{
	[Serializable]
	public class MultiChoiceMultiBinParam : Parameter<int[][]>{
		public IList<string> Values { get; set; }
		public IList<string> Bins { get; set; }
		public IList<Parameters> SubParams { get; set; }
		[NonSerialized] private MultiListSelectorControl control;
		public MultiChoiceMultiBinParam(string name) : this(name, new int[0][]){}

		public MultiChoiceMultiBinParam(string name, int[][] value) : base(name){
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
			SubParams = new Parameters[0];
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

		public override bool IsModified{
			get{
				if (!ArrayUtils.EqualArraysOfArrays(Value, Default)){
					return true;
				}
				foreach (Parameters p in SubParams){
					if (p != null && p.IsModified){
						return true;
					}
				}
				return false;
			}
		}

		public override void SetValueFromControl(){
			Value = control.SelectedIndices;
		}

		public override void Clear(){
			Value = new int[0][];
			foreach (Parameters parameters in SubParams){
				parameters?.Clear();
			}
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			control.SelectedIndices = Value;
			foreach (Parameters p in SubParams){
				p?.UpdateControlsFromValue();
			}
		}

		public override object CreateControl(){
			control = new MultiListSelectorControl();
			control.Init(Values, Bins, SubParams);
			control.SelectedIndices = Value;
			return control;
		}

		public override float Height => 310f;

		public override object Clone(){
			MultiChoiceMultiBinParam s = new MultiChoiceMultiBinParam(Name, Value){
				Help = Help,
				Visible = Visible,
				Values = Values,
				Default = Default,
				SubParams = new Parameters[SubParams.Count]
			};
			for (int i = 0; i < SubParams.Count; i++){
				s.SubParams[i] = (Parameters) SubParams[i]?.Clone();
			}
			return s;
		}

		public Parameters GetSubParameters(){
			return null;
		}

		public override void ResetSubParamValues(){
			Value = Default;
			foreach (Parameters p in SubParams){
				p?.ResetValues();
			}
		}

		public override void ResetSubParamDefaults(){
			Default = Value;
			foreach (Parameters p in SubParams){
				p?.ResetDefaults();
			}
		}
	}
}