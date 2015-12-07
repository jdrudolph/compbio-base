using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class SingleChoiceWithSubParams : ParameterWithSubParams<int>{
		public IList<string> Values { get; set; }
		public IList<Parameters> SubParams { get; set; }
		[NonSerialized] private Grid control;
		public SingleChoiceWithSubParams(string name) : this(name, 0){}

		public SingleChoiceWithSubParams(string name, int value) : base(name){
			TotalWidth = 1000F;
			ParamNameWidth = 250F;
			Value = value;
			Default = value;
			Values = new List<string>(new[] { "" });
			SubParams = new List<Parameters>(new[] { new Parameters() });
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = int.Parse(value); }
		}

		public override void ResetSubParamValues(){
			Value = Default;
			foreach (Parameters p in SubParams){
				p.ResetValues();
			}
		}

		public override void ResetSubParamDefaults(){
			Default = Value;
			foreach (Parameters p in SubParams){
				p.ResetDefaults();
			}
		}

		public override bool IsModified{
			get{
				if (Value != Default){
					return true;
				}
				foreach (Parameters p in SubParams){
					if (p.IsModified){
						return true;
					}
				}
				return false;
			}
		}

		public string SelectedValue => Value < 0 || Value >= Values.Count ? null : Values[Value];

		public override Parameters GetSubParameters(){
			return Value < 0 || Value >= Values.Count ? null : SubParams[Value];
		}

		public override void Clear(){
			Value = 0;
			foreach (Parameters parameters in SubParams){
				parameters.Clear();
			}
		}

		public override void SetValueFromControl(){
			if (control == null){
				return;
			}
			ComboBox cb = (ComboBox) WpfUtils.GetGridChild(control, 0, 0);
			if (cb != null){
				Value = cb.SelectedIndex;
			}
			foreach (Parameters p in SubParams){
				p.SetValuesFromControl();
			}
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			ComboBox cb = (ComboBox) WpfUtils.GetGridChild(control, 0, 0);
			if (Value >= 0 && Value < Values.Count){
				cb.SelectedIndex = Value;
			}
			foreach (Parameters p in SubParams){
				p.UpdateControlsFromValue();
			}
		}

		public override object CreateControl(){
			ParameterPanel[] panels = new ParameterPanel[SubParams.Count];
			for (int i = 0; i < panels.Length; i++){
				panels[i] = new ParameterPanel();
				panels[i].Init(SubParams[i], ParamNameWidth, (int) TotalWidth);
			}
			ComboBox cb = new ComboBox();
			cb.SelectionChanged += (sender, e) =>{
				SetValueFromControl();
				ValueHasChanged();
			};
			if (Values != null){
				foreach (string value in Values){
					cb.Items.Add(value);
				}
				if (Value >= 0 && Value < Values.Count){
					cb.SelectedIndex = Value;
				}
			}
			Grid grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(paramHeight, GridUnitType.Pixel)});
			grid.RowDefinitions.Add(new RowDefinition{Height = new GridLength(100, GridUnitType.Star)});
			Grid.SetRow(cb, 0);
			grid.Children.Add(cb);
			for (int i = 0; i < panels.Length; i++){
				panels[i].Visibility = (i == Value) ? Visibility.Visible : Visibility.Hidden;
				panels[i].VerticalAlignment = VerticalAlignment.Top;
				Grid.SetRow(panels[i], 1);
				grid.Children.Add(panels[i]);
			}
			cb.SelectionChanged += (sender, e) =>{
				for (int i = 0; i < panels.Length; i++){
					panels[i].Visibility = (i == cb.SelectedIndex) ? Visibility.Visible : Visibility.Hidden;
				}
			};
			grid.Width = TotalWidth;
			control = grid;
			return control;
		}

		public override float Height{
			get{
				float max = 0;
				foreach (Parameters param in SubParams){
					max = Math.Max(max, param.Height + 6);
				}
				return 44 + max;
			}
		}

		public override object Clone(){
			SingleChoiceWithSubParams s = new SingleChoiceWithSubParams(Name, Value){
				Help = Help,
				Visible = Visible,
				Values = Values,
				Default = Default,
				SubParams = new Parameters[SubParams.Count]
			};
			for (int i = 0; i < SubParams.Count; i++){
				s.SubParams[i] = (Parameters) SubParams[i].Clone();
			}
			return s;
		}

		public void SetValueChangedHandlerForSubParams(ValueChangedHandler action){
			ValueChanged += action;
			foreach (Parameter p in GetSubParameters().GetAllParameters()){
				if (p is IntParam || p is DoubleParam){
					p.ValueChanged += action;
				} else{
					(p as SingleChoiceWithSubParams)?.SetValueChangedHandlerForSubParams(action);
				}
			}
		}
	}
}