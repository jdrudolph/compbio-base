using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using BaseLibS.Num;
using BaseLibS.Util;

namespace BaseLibS.Param{
	[Serializable]
	public class Ms1LabelParam : Parameter<int[][]>{
		public int Multiplicity { get; set; }
		public string[] Values { get; set; }

        /// <summary>
        /// for xml serialization only
        /// </summary>
	    private Ms1LabelParam() : this("", new int[0][]) { }

	    public Ms1LabelParam(string name, int[][] value) : base(name){
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

	    public override void ReadXml(XmlReader reader)
	    {
            Multiplicity = Int32.Parse(reader.GetAttribute("Multiplicity"));
	        base.ReadXml(reader);
	        Values = reader.ReadValues().ToArray();
	    }

	    public override void WriteXml(XmlWriter writer)
	    {
            writer.WriteAttributeString("Multiplicity", Multiplicity.ToString());
	        base.WriteXml(writer);
            writer.WriteValues(Values);
	    }
	}
}