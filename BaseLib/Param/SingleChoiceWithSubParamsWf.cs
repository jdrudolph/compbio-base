using System;
using System.Windows.Forms;
using BaseLibS.Param;

namespace BaseLib.Param{
	[Serializable]
	public class SingleChoiceWithSubParamsWf : SingleChoiceWithSubParams{
		[NonSerialized] private TableLayoutPanel control;
		public SingleChoiceWithSubParamsWf(string name) : base(name){}
		public SingleChoiceWithSubParamsWf(string name, int value) : base(name, value){}
		public override ParamType Type => ParamType.WinForms;

		public override void SetValueFromControl(){
			if (control == null){
				return;
			}
			ComboBox cb = (ComboBox) control.Controls[0];
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
			ComboBox cb = (ComboBox) control.Controls[0];
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
			ComboBox cb = new ComboBox {DropDownStyle = ComboBoxStyle.DropDownList};
			cb.SelectedIndexChanged += (sender, e) =>{
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
			TableLayoutPanel grid = new TableLayoutPanel();
			grid.RowStyles.Add(new RowStyle(SizeType.Absolute, paramHeight));
			grid.RowStyles.Add(new RowStyle(SizeType.AutoSize, 100));
			cb.Dock = DockStyle.Fill;
			grid.Controls.Add(cb, 0, 0);
			for (int i = 0; i < panels.Length; i++){
				panels[i].Visible = (i == Value);
				panels[i].Dock = DockStyle.Fill;
				grid.Controls.Add(panels[i], 0, 1);
			}
			cb.SelectedIndexChanged += (sender, e) =>{
				for (int i = 0; i < panels.Length; i++){
					panels[i].Visible = i == cb.SelectedIndex;
				}
			};
			grid.Width = (int) TotalWidth;
			grid.Dock = DockStyle.Fill;
			control = grid;
			return control;
		}
	}
}