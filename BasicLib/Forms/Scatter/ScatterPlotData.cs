using System;
using System.Collections.Generic;
using BasicLib.Util;

namespace BasicLib.Forms.Scatter{
	public class ScatterPlotData{
		private ScatterPlotValues zvals;
		private IList<string> labels;
		private int count;
		private int[] selection = new int[0];

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

		public void Reset(){
			count = 0;
		}

		public double ColorMin { get; set; }
		public double ColorMax { get; set; }
		public string ColorLabel { get; set; }
		public bool HasLabels { get { return labels != null; } }
		public int[] Selection{
			get { return selection; }
			set{
				selection = value;
				Array.Sort(selection);
			}
		}

		public void AddPoint(double x, double y){
			XValues.AddValue(x);
			YValues.AddValue(y);
		}

		public void AddPoint(double x, double y, double z){
			XValues.AddValue(x);
			YValues.AddValue(y);
			ColorValues.AddValue(z);
		}

		public void AddPoint(double[] x, double[] y){
			XValues.AddValue(x);
			YValues.AddValue(y);
		}

		public void AddPoint(double[] x, double[] y, double[] z){
			XValues.AddValue(x);
			YValues.AddValue(y);
			ColorValues.AddValue(z);
		}

		public bool IsSelected(int ind){
			return Array.BinarySearch(selection, ind) >= 0;
		}

		public string GetLabelAt(int index){
			return labels[index];
		}

		public double[] GetDataAt(int index){
			if (XValues == null || YValues == null){
				return new double[0];
			}
			return XValues.Length == YValues.Length
				? new[]{XValues.SingleValues[index], YValues.SingleValues[index]} : new double[]{};
		}

		public void GetData(out double[] x, out double[] y, out double[] z, out string[] label, out int index){
			x = null;
			y = null;
			z = null;
			label = null;
			index = count;
			if (XValues == null || YValues == null || count >= XValues.Length || XValues.Length != YValues.Length){
				return;
			}
			if (!XValues.IsMulti && !YValues.IsMulti){
				try{
					x = new[]{XValues.SingleValues[count]};
					y = new[]{YValues.SingleValues[count]};
					if (zvals != null && XValues.Length == zvals.Length){
						z = new[]{zvals.SingleValues[count]};
					}
					if (labels != null && count < labels.Count){
						label = new[]{labels[count]};
					}
				} catch (Exception){}
				count++;
				return;
			}
			if (!XValues.IsMulti && YValues.IsMulti){
				y = YValues.MultiValues[count];
				x = ArrayUtils.FillArray(i => XValues.SingleValues[count], y.Length);
				if (zvals != null && XValues.Length == zvals.Length){
					if (zvals.IsMulti){
						if (zvals.MultiValues[count].Length == y.Length){
							z = zvals.MultiValues[count];
						}
					} else{
						z = ArrayUtils.FillArray(i => zvals.SingleValues[count], y.Length);
					}
				}
				if (labels != null && count < labels.Count){
					label = ArrayUtils.FillArray(i => labels[count], y.Length);
				}
				count++;
				return;
			}
			if (XValues.IsMulti && !YValues.IsMulti){
				x = XValues.MultiValues[count];
				y = ArrayUtils.FillArray(i => YValues.SingleValues[count], x.Length);
				if (zvals != null && XValues.Length == zvals.Length){
					if (zvals.IsMulti){
						if (zvals.MultiValues[count].Length == x.Length){
							z = zvals.MultiValues[count];
						}
					} else{
						z = ArrayUtils.FillArray(i => zvals.SingleValues[count], x.Length);
					}
				}
				if (labels != null && count < labels.Count){
					label = ArrayUtils.FillArray(i => labels[count], x.Length);
				}
				count++;
				return;
			}
			if (XValues.IsMulti && YValues.IsMulti){
				x = XValues.MultiValues[count];
				y = YValues.MultiValues[count];
				if (zvals != null && XValues.Length == zvals.Length){
					if (zvals.IsMulti){
						if (zvals.MultiValues[count].Length == x.Length){
							z = zvals.MultiValues[count];
						}
					} else{
						z = ArrayUtils.FillArray(i => zvals.SingleValues[count], x.Length);
					}
				}
				if (labels != null && count < labels.Count){
					label = ArrayUtils.FillArray(i => labels[count], x.Length);
				}
				count++;
			}
		}

		public void InvalidateData(){
			ColorMin = double.NaN;
			ColorMax = double.NaN;
		}

		public bool IsEmpty { get { return XValues == null || XValues.Length == 0; } }
	}
}