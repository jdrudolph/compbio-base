using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Wpf;

namespace BaseLib.Param{
	[Serializable]
	public class BoolWithSubParams : ParameterWithSubParams{
		public bool Value { get; set; }
		public bool Default { get; private set; }
		public Parameters SubParamsFalse { get; set; }
		public Parameters SubParamsTrue { get; set; }
		public float paramNameWidth = 250F;
		public float ParamNameWidth { get { return paramNameWidth; } set { paramNameWidth = value; } }
		public float totalWidth = 1000F;
		public float TotalWidth { get { return totalWidth; } set { totalWidth = value; } }
		public BoolWithSubParams(string name) : this(name, false) { }

		public BoolWithSubParams(string name, bool value) : base(name){
			Value = value;
			Default = value;
			SubParamsFalse = new Parameters();
			SubParamsTrue = new Parameters();
		}

		public override string StringValue { get { return Value.ToString(CultureInfo.InvariantCulture); } set { Value = bool.Parse(value); } }

		public bool Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}

		public override void ResetValue(){
			Value = Default;
			SubParamsTrue.ResetValues();
			SubParamsFalse.ResetValues();
		}

		public override void ResetDefault(){
			Default = Value;
			SubParamsTrue.ResetDefaults();
			SubParamsFalse.ResetDefaults();
		}

		public override bool IsModified{
			get{
				if (Value != Default){
					return true;
				}
				return SubParamsTrue.IsModified || SubParamsFalse.IsModified;
			}
		}

		public override Parameters GetSubParameters() { return Value ? SubParamsTrue : SubParamsFalse; }

		public override void SetValueFromControl(){
			Grid tbl = (Grid) control;
			CheckBox cb = (CheckBox) WpfUtils.GetGridChild(tbl, 0, 0);
			Value = cb.IsChecked != null && cb.IsChecked.Value;
			SubParamsFalse.SetValuesFromControl();
			SubParamsTrue.SetValuesFromControl();
		}

		public override void Clear(){
			Value = false;
			SubParamsTrue.Clear();
			SubParamsFalse.Clear();
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			Grid tlp = (Grid) control;
			CheckBox cb = (CheckBox) WpfUtils.GetGridChild(tlp, 0, 0);
			if (cb == null){
				return;
			}
			cb.IsChecked = Value;
			if (SubParamsFalse != null){
				SubParamsFalse.UpdateControlsFromValue();
			}
			if (SubParamsTrue != null){
				SubParamsTrue.UpdateControlsFromValue();
			}
		}

		protected override UIElement Control{
			get{
				ParameterPanel panelFalse = new ParameterPanel();
				ParameterPanel panelTrue = new ParameterPanel();
				panelFalse.Init(SubParamsFalse, ParamNameWidth, (int) (TotalWidth));
				panelTrue.Init(SubParamsTrue, ParamNameWidth, (int) (TotalWidth));
				CheckBox cb = new CheckBox{IsChecked = Value};
				cb.Checked += (sender, e) => ValueHasChanged();
				cb.Unchecked += (sender, e) => ValueHasChanged();
				cb.VerticalAlignment = VerticalAlignment.Center;
				Grid tlp = new Grid();
				tlp.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(100, GridUnitType.Star)});
				tlp.RowDefinitions.Add(new RowDefinition{Height = new GridLength(paramHeight, GridUnitType.Pixel)});
				tlp.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
				Grid.SetRow(cb, 0);
				tlp.Children.Add(cb);
				panelFalse.Visibility = !Value ? Visibility.Visible : Visibility.Hidden;
				panelTrue.Visibility = Value ? Visibility.Visible : Visibility.Hidden;
				Grid.SetRow(panelFalse, 1);
				tlp.Children.Add(panelFalse);
				Grid.SetRow(panelTrue, 1);
				tlp.Children.Add(panelTrue);
				cb.Checked += (sender, e) =>{
					panelFalse.Visibility = cb.IsChecked != null && !cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
					panelTrue.Visibility = cb.IsChecked != null && cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
				};
				cb.Unchecked += (sender, e) =>{
					panelFalse.Visibility = cb.IsChecked != null && !cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
					panelTrue.Visibility = cb.IsChecked != null && cb.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;
				};
				return tlp;
			}
		}

		public override float Height { get { return 50 + Math.Max(SubParamsFalse.Height, SubParamsTrue.Height); } }

		public override object Clone(){
			return new BoolWithSubParams(Name, Value){
				Help = Help,
				Visible = Visible,
				SubParamsFalse = (Parameters) SubParamsFalse.Clone(),
				SubParamsTrue = (Parameters) SubParamsTrue.Clone(),
				Default = Default
			};
		}
	}
}