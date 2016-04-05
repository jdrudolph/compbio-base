using System;
using System.Collections.Generic;
using BaseLib.Wpf;
using BaseLibS.Num;
using BaseLibS.Param;
using BaseLibS.Util;

namespace BaseLib.Param{
	/// <summary>
	/// A type of Parameter (value plus control element) whose fields are
	/// Multiplicity (how many label states there are), a 2D array of int, 
	/// and a 1D array of strings. The constructor is called only in the 
	/// CreateMultiplicityParam method of WorkflowModel.
	/// The first index corresponds to the multiplicity, the second to the label. 
	/// For example, Value[2] is used when the multiplicity is 3, with Value[2][0] 
	/// representing the light peptides, Value[2][1] the medium peptides, and 
	/// Value[2][2] the heavy peptides. Set by SetLabels(string[][] labels).
	/// </summary>
	[Serializable]
	public class Ms1LabelParam : Parameter<int[][]>{
		public int Multiplicity { get; set; }
		/// <summary>
		/// The labels to choose from, e.g., 18O, Arg10, Arg6, ...
		/// </summary>
		public string[] Values { get; set; }
		[NonSerialized] private Ms1LabelPanel control;

		public Ms1LabelParam(string name, int[][] value) : base(name){
			Value = value;
			Default = (int[][]) value.Clone();
		}

		public override string StringValue{
			get { return StringUtils.Concat(",", ";", Value); }
			set { Value = StringUtils.SplitToInt(',', ';', value); }
		}

		public override void SetValueFromControl(){
			Value = control.SelectedIndices;
		}

		public override void Clear(){
			Value = new int[Multiplicity][];
		}

		public override bool IsModified => !ArrayUtils.EqualArraysOfArrays(Value, Default);

		public override void UpdateControlFromValue(){
			control.SelectedIndices = Value;
		}

		public override object CreateControl(){
			return control = new Ms1LabelPanel(Multiplicity, Values){SelectedIndices = Value};
		}

		public override float Height => 154f;

		public string[] GetLabels(int ind){
			return ArrayUtils.SubArray(Values, Value[ind]);
		}

		/// <summary>
		/// Set the 2D int array Value to the indices at which the elements 
		/// of the 2D string array argument can be found within the 1D string array Values.
		/// </summary>
		/// <param name="labels"></param>
		public void SetLabels(string[][] labels){
			for (int i = 0; i < labels.Length; i++){
				Value[i] = GetIndices(Values, labels[i]);
			}
		}

		/// <summary>
		/// For each string in the second argument, return the index at which that string 
		/// can be found in the first argument.
		/// </summary>
		/// <param name="values">The reference array of strings against which matching is done. 
		/// The return values will refer to positions within this array.</param>
		/// <param name="strings">The list of strings which will be matched, 
		/// one after another, against the reference array.</param>
		/// <returns>An array of int, with the same length as the first argument, 
		/// and values between -1 and one less than the second argument.</returns>
		public static int[] GetIndices(string[] values, IList<string> strings){
			int[] result = new int[strings.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = ArrayUtils.IndexOf(values, strings[i]);
			}
			return result;
		}
	}
}