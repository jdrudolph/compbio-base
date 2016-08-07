using System;
using System.Drawing;
using System.Windows.Forms;
using BaseLib.Forms.Axis;
using BaseLib.Forms.Base;
using BaseLibS.Graph;
using BaseLibS.Graph.Axis;

namespace BaseLib.Forms.Colors{
	public delegate void ColorChangeHandler();

	public partial class ColorScale : UserControl{
		public event ColorChangeHandler OnColorChange;
		private readonly NumericAxisView axis;
		public bool Locked { get; set; }
		public ColorStrip ColorStrip { get; }

		public ColorScale(){
			InitializeComponent();
			ColorStrip = new ColorStrip();
			axis = new NumericAxisView();
			SuspendLayout();
			ColorStrip.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ColorStrip.Location = new Point(0, 59);
			ColorStrip.Margin = new Padding(0);
			ColorStrip.Name = "colorStrip";
			ColorStrip.Size = new Size(749, 31);
			ColorStrip.TabIndex = 1;
			axis.ForeColor = Color2.Black;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(ColorStrip);
			Controls.Add(BasicControl.CreateControl(axis));
			Name = "ColorScale";
			Size = new Size(749, 90);
			ResumeLayout(false);
			ColorStrip.GetView().Arrow = Arrows.Second;
			ColorStrip.GetView().StartupColorMax = Color2.Red;
			ColorStrip.GetView().StartupColorMin = Color2.White;
			ColorStrip.GetView().StripWidth = 10;
			ColorStrip.GetView().Vertical = false;
			ColorStrip.GetView().Weight1 = 1F;
			ColorStrip.GetView().Weight2 = 0F;
			axis.Configurable = true;
			axis.IndicatorColor = Color2.Transparent;
			axis.IsLogarithmic = false;
			axis.LineWidth = 0.5F;
			axis.MajorTickLength = 6;
			axis.MajorTickLineWidth = 0.5F;
			axis.MinorTickLength = 3;
			axis.MinorTickLineWidth = 0.5F;
			axis.MouseMode = AxisMouseMode.Zoom;
			axis.Positioning = AxisPositioning.Top;
			axis.Reverse = false;
			axis.TotalMax = 1;
			axis.TotalMin = 0;
			axis.ZeroPoint = double.NaN;
			axis.ZoomMax = 1;
			axis.ZoomMin = 0;
			axis.ZoomType = AxisZoomType.Zoom;
			ColorStrip.GetView().OnColorChange += UpdateAxis;
			axis.OnZoomChange += UpdateColor;
		}

		private void UpdateAxis(){
			FireColorChanged();
		}

		private void UpdateColor(object source, double min, double max){
			Locked = true;
			FireColorChanged();
		}

		private void FireColorChanged(){
			OnColorChange?.Invoke();
		}

		//TODO: why is this not visible?
		public override string Text{
			get { return axis.Text; }
			set{
				axis.Text = value;
				axis.Invalidate();
			}
		}

		public void InitColors(Color2[] colors, double[] positions){
			ColorStrip.GetView().InitColors(colors, positions);
		}

		public AxisPositioning Positioning{
			get { return axis.Positioning; }
			set { axis.Positioning = value; }
		}

		public bool Reverse{
			get { return axis.Reverse; }
			set { axis.Reverse = value; }
		}

		public bool IsLogarithmic{
			get { return axis.IsLogarithmic; }
			set{
				bool oldValue = axis.IsLogarithmic;
				if (oldValue != value){
					if (value){
						axis.TotalMin = Math.Log(axis.TotalMin);
						axis.TotalMax = Math.Log(axis.TotalMax);
					} else{
						axis.TotalMin = Math.Exp(axis.TotalMin);
						axis.TotalMax = Math.Exp(axis.TotalMax);
					}
					axis.ZoomMin = axis.TotalMin;
					axis.ZoomMax = axis.TotalMax;
				}
				axis.IsLogarithmic = value;
				FireColorChanged();
			}
		}

		public double Min{
			get { return IsLogarithmic ? Math.Exp(axis.ZoomMin) : axis.ZoomMin; }
			set{
				double v = IsLogarithmic ? Math.Log(value) : value;
				axis.TotalMin = v;
				axis.ZoomMin = v;
				axis.Invalidate();
			}
		}

		public double Max{
			get { return IsLogarithmic ? Math.Exp(axis.ZoomMax) : axis.ZoomMax; }
			set{
				double v = IsLogarithmic ? Math.Log(value) : value;
				axis.TotalMax = v;
				axis.ZoomMax = v;
				axis.Invalidate();
			}
		}

		public Pen2 GetPen(double value){
			return ColorStrip.GetView().GetPenAt(GetScaledValue(value), ColorStrip.Width, ColorStrip.Height);
		}

		public Color2 GetColor(double value){
			return ColorStrip.GetView().GetColorAt(GetScaledValue(value), ColorStrip.Width, ColorStrip.Height);
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
			return (unscaledValue - axis.ZoomMin)/(axis.ZoomMax - axis.ZoomMin);
		}

		public void WidenRange(double min, double max, bool fullZoom){
			axis.WidenRange(min, max, fullZoom);
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
			axis.SetZoomNoFire(min, max);
		}

		public void SetLogarithmic(bool isLogarithmic){
			axis.IsLogarithmic = isLogarithmic;
		}

		public override Color BackColor{
			get { return base.BackColor; }
			set{
				base.BackColor = value;
				axis.BackColor = Color2.FromArgb(value.A, value.R, value.G, value.B) ;
			}
		}
	}
}