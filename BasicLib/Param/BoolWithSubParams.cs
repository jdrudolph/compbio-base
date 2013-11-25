using System;
using System.Globalization;
using System.Windows.Controls;

namespace BasicLib.Param{
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
		public BoolWithSubParams(string name) : this(name, false) {}

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

		public override Parameters GetSubParameters(){
			return Value ? SubParamsTrue : SubParamsFalse;
		}

		public override void SetValueFromControl(){
			//TODO
			//TableLayoutPanel tbl = (TableLayoutPanel) control;
			//CheckBox cb = (CheckBox) tbl.GetControlFromPosition(0, 0);
			//Value = cb.IsChecked != null && cb.IsChecked.Value;
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
			//TODO
			//TableLayoutPanel tlp = (TableLayoutPanel) control;
			//CheckBox cb = (CheckBox) tlp.GetControlFromPosition(0, 0);
			//if (cb == null){
			//	return;
			//}
			//cb.IsChecked = Value;
			if (SubParamsFalse != null){
				SubParamsFalse.UpdateControlsFromValue();
			}
			if (SubParamsTrue != null){
				SubParamsTrue.UpdateControlsFromValue();
			}
		}

		protected override Control Control{
			get{
				//TODO
				//ParameterPanel panelFalse = new ParameterPanel();
				//ParameterPanel panelTrue = new ParameterPanel();
				//panelFalse.Init(SubParamsFalse, ParamNameWidth, (int) (TotalWidth));
				//panelTrue.Init(SubParamsTrue, ParamNameWidth, (int) (TotalWidth));
				//CheckBox cb = new CheckBox{IsChecked = Value};
				//cb.CheckStateChanged += (sender, e) => ValueHasChanged();
				//TableLayoutPanel tlp = new TableLayoutPanel{ColumnCount = 1};
				//tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
				//tlp.RowCount = 2;
				//tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
				//tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
				//tlp.Controls.Add(cb, 0, 0);
				//panelFalse.Visible = !Value;
				//panelTrue.Visible = Value;
				//panelFalse.Dock = DockStyle.Fill;
				//panelTrue.Dock = DockStyle.Fill;
				//tlp.Controls.Add(panelFalse, 0, 1);
				//tlp.Controls.Add(panelTrue, 0, 1);
				//tlp.Dock = DockStyle.Fill;
				//cb.CheckedChanged += (sender, e) =>{
				//	panelFalse.Visible = !cb.IsChecked.Value;
				//	panelTrue.Visible = cb.IsChecked.Value;
				//};
				//return tlp;
				return null;
			}
		}
		public override float Height { get { return 50 + Math.Max(SubParamsFalse.Height, SubParamsTrue.Height); } }

		public override object Clone(){
			return new BoolWithSubParams(Name, Value){
				Help = Help, Visible = Visible, SubParamsFalse = (Parameters) SubParamsFalse.Clone(),
				SubParamsTrue = (Parameters) SubParamsTrue.Clone(), Default = Default
			};
		}
	}
}