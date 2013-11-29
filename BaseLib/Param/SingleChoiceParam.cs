using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BaseLib.Param{
	[Serializable]
	public class SingleChoiceParam : Parameter{
		public int Value { get; set; }
		public int Default { get; private set; }
		public IList<string> Values { get; set; }
		public SingleChoiceParam(string name) : this(name, 0) {}

		public SingleChoiceParam(string name, int value) : base(name){
			Value = value;
			Default = value;
			Values = new[]{""};
		}

		public override string StringValue { get { return Value.ToString(CultureInfo.InvariantCulture); } set { Value = int.Parse(value); } }
		public int Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}

		public override void ResetValue(){
			Value = Default;
		}

		public override void ResetDefault(){
			Default = Value;
		}

		public override bool IsModified { get { return Value != Default; } }
		public string SelectedValue{
			get{
				if (Value < 0 || Value >= Values.Count){
					return null;
				}
				return Values[Value];
			}
			set{
				for (int i = 0; i < Values.Count; i++){
					if (Values[i].Equals(value)){
						Value = i;
						break;
					}
				}
			}
		}

		public override void Clear(){
			Value = 0;
		}

		public override void SetValueFromControl(){
			ComboBox cb = (ComboBox) control;
			if (cb == null){
				return;
			}
			int val = cb.SelectedIndex;
			Value = val;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			ComboBox cb = (ComboBox) control;
			if (cb != null && Value >= 0 && Value < Values.Count){
				cb.SelectedIndex = Value;
			}
		}

		public void UpdateControlFromValue2(){
			ComboBox cb = (ComboBox) control;
			if (cb != null && Values != null){
				cb.Items.Clear();
				foreach (string value in Values){
					cb.Items.Add(value);
				}
				if (Value >= 0 && Value < Values.Count){
					cb.SelectedIndex = Value;
				}
			}
			if (cb != null && Value >= 0 && Value < Values.Count){
				cb.SelectedIndex = Value;
			}
		}

		protected override FrameworkElement Control{
			get{
				//TODO
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
				return cb;
			}
		}

		public override object Clone(){
			return new SingleChoiceParam(Name, Value){Help = Help, Visible = Visible, Values = Values, Default = Default};
		}
	}
}