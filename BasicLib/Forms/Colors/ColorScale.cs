using System;
using System.Drawing;
using System.Windows.Forms;
using BasicLib.Forms.Axis;

namespace BasicLib.Forms.Colors{
	public delegate void ColorChangeHandler();

	public partial class ColorScale : UserControl{
		public event ColorChangeHandler OnColorChange;

		public ColorScale(){
			InitializeComponent();
			colorStrip.GetView().Arrow = Arrows.Second;
			colorStrip.GetView().StartupColorMax = Color.Red;
			colorStrip.GetView().StartupColorMin = Color.White;
			colorStrip.GetView().StripWidth = 10;
			colorStrip.GetView().Vertical = false;
			colorStrip.GetView().Weight1 = 1F;
			colorStrip.GetView().Weight2 = 0F;
			axis.GetView().Configurable = true;
			axis.GetView().IndicatorColor = Color.Empty;
			axis.GetView().IsLogarithmic = false;
			axis.GetView().LineWidth = 0.5F;
			axis.GetView().MajorTickLength = 6;
			axis.GetView().MajorTickLineWidth = 0.5F;
			axis.GetView().MinorTickLength = 3;
			axis.GetView().MinorTickLineWidth = 0.5F;
			axis.GetView().MouseMode = AxisMouseMode.Zoom;
			axis.GetView().Positioning = AxisPositioning.Top;
			axis.GetView().Reverse = false;
			axis.GetView().TotalMax = 1;
			axis.GetView().TotalMin = 0;
			axis.GetView().ZeroPoint = double.NaN;
			axis.GetView().ZoomMax = 1;
			axis.GetView().ZoomMin = 0;
			axis.GetView().ZoomType = AxisZoomType.Zoom;
			colorStrip.GetView().OnColorChange += UpdateAxis;
			axis.GetView().OnZoomChange += UpdateColor;
		}

		private void UpdateAxis(){
			FireColorChanged();
		}

		private void UpdateColor(object source, double min, double max){
			FireColorChanged();
		}

		public ColorStrip ColorStrip { get { return colorStrip; } }

		private void FireColorChanged(){
			if (OnColorChange != null){
				OnColorChange();
			}
		}

		//TODO: why is this not visible?
		public override string Text{
			get { return axis.Text; }
			set{
				axis.Text = value;
				axis.Invalidate();
			}
		}

		public void InitColors(Color[] colors, double[] positions){
			colorStrip.GetView().InitColors(colors, positions);
		}

		public AxisPositioning Positioning { get { return axis.GetView().Positioning; } set { axis.GetView().Positioning = value; } }
		public bool Reverse { get { return axis.GetView().Reverse; } set { axis.GetView().Reverse = value; } }
		public bool IsLogarithmic{
			get { return axis.GetView().IsLogarithmic; }
			set{
				bool oldValue = axis.GetView().IsLogarithmic;
				if (oldValue != value){
					if (value){
						axis.GetView().TotalMin = Math.Log(axis.GetView().TotalMin);
						axis.GetView().TotalMax = Math.Log(axis.GetView().TotalMax);
					} else{
						axis.GetView().TotalMin = Math.Exp(axis.GetView().TotalMin);
						axis.GetView().TotalMax = Math.Exp(axis.GetView().TotalMax);
					}
					axis.GetView().ZoomMin = axis.GetView().TotalMin;
					axis.GetView().ZoomMax = axis.GetView().TotalMax;
				}
				axis.GetView().IsLogarithmic = value;
				FireColorChanged();
			}
		}
		public double Min{
			get { return IsLogarithmic ? Math.Exp(axis.GetView().ZoomMin) : axis.GetView().ZoomMin; }
			set{
				double v = IsLogarithmic ? Math.Log(value) : value;
				axis.GetView().TotalMin = v;
				axis.GetView().ZoomMin = v;
				axis.Invalidate();
			}
		}
		public double Max{
			get { return IsLogarithmic ? Math.Exp(axis.GetView().ZoomMax) : axis.GetView().ZoomMax; }
			set{
				double v = IsLogarithmic ? Math.Log(value) : value;
				axis.GetView().TotalMax = v;
				axis.GetView().ZoomMax = v;
				axis.Invalidate();
			}
		}

		public Pen GetPen(double value){
			return colorStrip.GetView().GetPenAt(GetScaledValue(value), colorStrip.Width, colorStrip.Height);
		}

		public Color GetColor(double value){
			return colorStrip.GetView().GetColorAt(GetScaledValue(value), colorStrip.Width, colorStrip.Height);
		}

		public double GetScaledValue(double unscaledValue){
			if (unscaledValue <= Min){
				return 0;
			}
			if (unscaledValue >= Max){
				return 1;
			}
			if (IsLogarithmic){
				unscaledValue = Math.Log(unscaledValue);
			}
			return (unscaledValue - axis.GetView().ZoomMin) / (axis.GetView().ZoomMax - axis.GetView().ZoomMin);
		}

		public void WidenRange(double min, double max, bool fullZoom){
			axis.GetView().WidenRange(min, max, fullZoom);
			FireColorChanged();
		}

		public void AdjustMax(float[,] values){
			float max = 0;
			for (int i = 0; i < values.GetLength(0); i++){
				for (int j = 0; j < values.GetLength(1); j++){
					float v = IsLogarithmic ? (float) Math.Log(Math.Max(1, values[i, j])) : values[i, j];
					if (v > max){
						max = v;
					}
				}
			}
			Max = max;
		}

		public void Adjust(float[,] values){
			float min = float.MaxValue;
			float max = -float.MaxValue;
			for (int i = 0; i < values.GetLength(0); i++){
				for (int j = 0; j < values.GetLength(1); j++){
					float v = IsLogarithmic ? (float) Math.Log(Math.Max(1, values[i, j])) : values[i, j];
					if (v < min){
						min = v;
					}
					if (v > max){
						max = v;
					}
				}
			}
			axis.GetView().SetZoomNoFire(min, max);
		}

		public void SetLogarithmic(bool isLogarithmic){
			axis.GetView().IsLogarithmic = isLogarithmic;
		}

		public override Color BackColor{
			get { return base.BackColor; }
			set{
				base.BackColor = value;
				axis.BackColor = value;
			}
		}
	}
}