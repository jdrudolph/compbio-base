using System.Collections.Generic;

namespace BasicLib.Forms.Scatter{
	public class ScatterPlotData{
		internal ScatterPlotValues zvals;
		internal IList<string> labels;

		public ScatterPlotData(){
			ColorMax = double.NaN;
			ColorMin = double.NaN;
		}

		public ScatterPlotValues XValues { get; set; }
		public ScatterPlotValues YValues { get; set; }
		public ScatterPlotValues ColorValues{
			get { return zvals; }
			set{
				ColorMin = double.NaN;
				ColorMax = double.NaN;
				zvals = value;
			}
		}
		public IList<string> Labels { get { return labels; } set { labels = value; } }

		public double ColorMin { get; set; }
		public double ColorMax { get; set; }
		public string ColorLabel { get; set; }
		public bool HasLabels { get { return labels != null; } }

		public string GetLabelAt(int index){
			return labels[index];
		}

		public void InvalidateData(){
			ColorMin = double.NaN;
			ColorMax = double.NaN;
		}

		public bool IsEmpty { get { return XValues == null || XValues.Length == 0; } }
	}
}