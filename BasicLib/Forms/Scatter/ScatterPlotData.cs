using System;
using System.Collections.Generic;
using BasicLib.Util;

namespace BasicLib.Forms.Scatter{
	public class ScatterPlotData{
		public event EventHandler SelectionChanged;

		internal void FireSelectionChanged(){
			if (SelectionChanged != null){
				SelectionChanged(this, new EventArgs());
			}
		}

		private ScatterPlotValues zvals;
		private IList<string> labels;
		private int count;
		private int[] selection = new int[0];
		private double colorMin = double.NaN;
		private double colorMax = double.NaN;
		public ScatterPlotValues XValues { get; set; }
		public ScatterPlotValues YValues { get; set; }
		public ScatterPlotValues ColorValues{
			get { return zvals; }
			set{
				colorMin = double.NaN;
				colorMax = double.NaN;
				zvals = value;
			}
		}
		public IList<string> Labels { get { return labels; } set { labels = value; } }

		public void Reset(){
			count = 0;
		}

		public bool XIsLogarithmic { get; set; }
		public bool YIsLogarithmic { get; set; }
		public bool ColorIsLogarithmic { get; set; }
		public double ColorMin { get { return colorMin; } set { colorMin = value; } }
		public double ColorMax { get { return colorMax; } set { colorMax = value; } }
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

		public void Select(double x1, double x2, double y1, double y2, bool add, bool toggle){
			if (toggle){
				HashSet<int> sel = add ? new HashSet<int>(selection) : new HashSet<int>();
				for (int i = 0; i < XValues.Length; i++){
					if (XValues.SingleValues[i] >= x1 && XValues.SingleValues[i] <= x2 && YValues.SingleValues[i] >= y1 &&
						YValues.SingleValues[i] <= y2){
						if (sel.Contains(i)){
							sel.Remove(i);
						} else{
							sel.Add(i);
						}
					}
				}
				Selection = ArrayUtils.ToArray(sel);
			} else{
				List<int> sel = new List<int>();
				for (int i = 0; i < XValues.Length; i++){
					if (XValues.SingleValues[i] >= x1 && XValues.SingleValues[i] <= x2 && YValues.SingleValues[i] >= y1 &&
						YValues.SingleValues[i] <= y2){
						sel.Add(i);
					}
				}
				if (add && selection.Length > 0){
					Selection = ArrayUtils.UniqueValues(ArrayUtils.Concat(sel.ToArray(), selection));
				} else{
					Selection = sel.ToArray();
				}
			}
			FireSelectionChanged();
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
			colorMin = double.NaN;
			colorMax = double.NaN;
		}

		public bool IsEmpty { get { return XValues == null || XValues.Length == 0; } }
	}
}