using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace BaseLib.ParamWf{
	[Serializable]
	public class SingleChoiceWithSubParamsWf : ParameterWithSubParamsWf{
		public int Value { get; set; }
		public int Default { get; private set; }
		public IList<string> Values { get; set; }
		public IList<ParametersWf> SubParams { get; set; }
		public float paramNameWidth = 250F;
		public float ParamNameWidth { get { return paramNameWidth; } set { paramNameWidth = value; } }
		public float totalWidth = 1000F;
		public float TotalWidth { get { return totalWidth; } set { totalWidth = value; } }
		public SingleChoiceWithSubParamsWf(string name) : this(name, 0) {}

		public SingleChoiceWithSubParamsWf(string name, int value) : base(name){
			Value = value;
			Default = value;
			Values = new[]{""};
			SubParams = new[]{new ParametersWf()};
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
			foreach (ParametersWf p in SubParams){
				p.ResetValues();
			}
		}

		public override void ResetDefault(){
			Default = Value;
			foreach (ParametersWf p in SubParams){
				p.ResetDefaults();
			}
		}

		public override bool IsModified{
			get{
				if (Value != Default){
					return true;
				}
				foreach (ParametersWf p in SubParams){
					if (p.IsModified){
						return true;
					}
				}
				return false;
			}
		}
		public string SelectedValue { get { return Value < 0 || Value >= Values.Count ? null : Values[Value]; } }

		public override ParametersWf GetSubParameters(){
			return SubParams[Value];
		}

		public override void Clear(){
			Value = 0;
			foreach (ParametersWf parameters in SubParams){
				parameters.Clear();
			}
		}

		public override void SetValueFromControl(){
			TableLayoutPanel tbl = (TableLayoutPanel) control;
			if (tbl == null){
				return;
			}
			ComboBox cb = (ComboBox) tbl.GetControlFromPosition(0, 0);
			if (cb != null){
				Value = cb.SelectedIndex;
			}
			foreach (ParametersWf p in SubParams){
				p.SetValuesFromControl();
			}
		}

		public override void UpdateControlFromValue(){
			if (control == null) {
				return;
			}
			TableLayoutPanel tlp = (TableLayoutPanel)control;
			ComboBox cb = (ComboBox) tlp.GetControlFromPosition(0, 0);
			if (Value >= 0 && Value < Values.Count){
				cb.SelectedIndex = Value;
			}
			foreach (ParametersWf p in SubParams){
				p.UpdateControlsFromValue();
			}
		}

		protected override Control Control{
			get{
				ParameterPanelWf[] panels = new ParameterPanelWf[SubParams.Count];
				for (int i = 0; i < panels.Length; i++){
					panels[i] = new ParameterPanelWf();
					panels[i].Init(SubParams[i], ParamNameWidth, (int) (TotalWidth));
				}
				ComboBox cb = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
				cb.SelectedIndexChanged += (sender, e) => {
					SetValueFromControl();
					ValueHasChanged();
				};
				if (Values != null){
					cb.Items.AddRange(Values.ToArray());
					if (Value >= 0 && Value < Values.Count){
						cb.SelectedIndex = Value;
					}
				}
				cb.Dock = DockStyle.Fill;
				TableLayoutPanel tlp = new TableLayoutPanel{ColumnCount = 1};
				tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
				tlp.RowCount = 2;
				tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
				tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
				tlp.Controls.Add(cb, 0, 0);
				for (int i = 0; i < panels.Length; i++){
					panels[i].Visible = (i == Value);
					panels[i].Dock = DockStyle.Fill;
					tlp.Controls.Add(panels[i], 0, 1);
				}
				tlp.Dock = DockStyle.Fill;
				cb.SelectedIndexChanged += (sender, e) => {
					tlp.SuspendLayout();
					for (int i = 0; i < panels.Length; i++){
						panels[i].Visible = (i == cb.SelectedIndex);
					}
					tlp.ResumeLayout();
				};
				return tlp;
			}
		}
		public override float Height{
			get{
				float max = 0;
				foreach (ParametersWf param in SubParams){
					max = Math.Max(max, param.Height + 6);
				}
				return 44 + max;
			}
		}

		public override object Clone(){
			SingleChoiceWithSubParamsWf s = new SingleChoiceWithSubParamsWf(Name, Value)
			{Help = Help, Visible = Visible, Values = Values, Default = Default, SubParams = new ParametersWf[SubParams.Count]};
			for (int i = 0; i < SubParams.Count; i++){
				s.SubParams[i] = (ParametersWf) SubParams[i].Clone();
			}
			return s;
		}

		public void SetValueChangedHandlerForSubParams(ValueChangedHandler action){
			ValueChanged += action;
			foreach (ParameterWf p in GetSubParameters().GetAllParameters()){
				if (p is IntParamWf || p is DoubleParamWf){
					p.ValueChanged += action;
				} else if (p is SingleChoiceWithSubParamsWf){
					((SingleChoiceWithSubParamsWf) p).SetValueChangedHandlerForSubParams(action);
				}
			}
		}
	}
}