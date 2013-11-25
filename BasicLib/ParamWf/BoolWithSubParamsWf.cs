using System;
using System.Globalization;
using System.Windows.Forms;

namespace BasicLib.ParamWf{
	[Serializable]
	public class BoolWithSubParamsWf : ParameterWithSubParamsWf{
		public bool Value { get; set; }
		public bool Default { get; private set; }
		public ParametersWf SubParamsFalse { get; set; }
		public ParametersWf SubParamsTrue { get; set; }
		public float paramNameWidth = 250F;
		public float ParamNameWidth { get { return paramNameWidth; } set { paramNameWidth = value; } }
		public float totalWidth = 1000F;
		public float TotalWidth { get { return totalWidth; } set { totalWidth = value; } }
		public BoolWithSubParamsWf(string name) : this(name, false) {}

		public BoolWithSubParamsWf(string name, bool value) : base(name){
			Value = value;
			Default = value;
			SubParamsFalse = new ParametersWf();
			SubParamsTrue = new ParametersWf();
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

		public override ParametersWf GetSubParameters(){
			return Value ? SubParamsTrue : SubParamsFalse;
		}

		public override void SetValueFromControl(){
			TableLayoutPanel tbl = (TableLayoutPanel) control;
			CheckBox cb = (CheckBox) tbl.GetControlFromPosition(0, 0);
			Value = cb.Checked;
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
			TableLayoutPanel tlp = (TableLayoutPanel) control;
			CheckBox cb = (CheckBox) tlp.GetControlFromPosition(0, 0);
			if (cb == null){
				return;
			}
			cb.Checked = Value;
			if (SubParamsFalse != null){
				SubParamsFalse.UpdateControlsFromValue();
			}
			if (SubParamsTrue != null){
				SubParamsTrue.UpdateControlsFromValue();
			}
		}

		protected override Control Control{
			get{
				ParameterPanelWf panelFalse = new ParameterPanelWf();
				ParameterPanelWf panelTrue = new ParameterPanelWf();
				panelFalse.Init(SubParamsFalse, ParamNameWidth, (int) (TotalWidth));
				panelTrue.Init(SubParamsTrue, ParamNameWidth, (int) (TotalWidth));
				CheckBox cb = new CheckBox{Checked = Value, Dock = DockStyle.Fill};
				cb.CheckStateChanged += (sender, e) => ValueHasChanged();
				TableLayoutPanel tlp = new TableLayoutPanel{ColumnCount = 1};
				tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
				tlp.RowCount = 2;
				tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
				tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
				tlp.Controls.Add(cb, 0, 0);
				panelFalse.Visible = !Value;
				panelTrue.Visible = Value;
				panelFalse.Dock = DockStyle.Fill;
				panelTrue.Dock = DockStyle.Fill;
				tlp.Controls.Add(panelFalse, 0, 1);
				tlp.Controls.Add(panelTrue, 0, 1);
				tlp.Dock = DockStyle.Fill;
				cb.CheckedChanged += (sender, e) =>{
					tlp.SuspendLayout();
					panelFalse.Visible = !cb.Checked;
					panelTrue.Visible = cb.Checked;
					tlp.ResumeLayout();
				};
				return tlp;
			}
		}
		public override float Height { get { return 50 + Math.Max(SubParamsFalse.Height, SubParamsTrue.Height); } }

		public override object Clone(){
			return new BoolWithSubParamsWf(Name, Value){
				Help = Help, Visible = Visible, SubParamsFalse = (ParametersWf) SubParamsFalse.Clone(),
				SubParamsTrue = (ParametersWf) SubParamsTrue.Clone(), Default = Default
			};
		}
	}
}