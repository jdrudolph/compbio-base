using System;
using System.Windows.Forms;

namespace BasicLib.ParamWf{
	public delegate void ValueChangedHandler();

	[Serializable]
	public abstract class ParameterWf : ICloneable{
		[field: NonSerialized]
		public event ValueChangedHandler ValueChanged;
		public string Name { get; private set; }
		public string Help { get; set; }
		public bool Visible { get; set; }
		[NonSerialized] protected Control control;

		protected ParameterWf(string name){
			Name = name;
			Help = "";
			Visible = true;
		}

		public ParameterWf SetValFromCtrl(){
			SetValueFromControl();
			return this;
		}

		public virtual string[] Markup { get { return new[]{"<parameter" + " name=\"" + Name + "\" value=\"" + StringValue + "\"></parameter>"}; } }
		public abstract string StringValue { get; set; }
		public abstract void ResetValue();
		public abstract void ResetDefault();
		public abstract void SetValueFromControl();
		public abstract void UpdateControlFromValue();
		public abstract object Clone();
		public abstract void Clear();
		public abstract bool IsModified { get; }
		public virtual bool IsDropTarget { get { return false; } }
		public virtual void Drop(string x) {}
		protected abstract Control Control { get; }

		public Control GetControl(){
			control = Control;
			return control;
		}

		public virtual float Height { get { return 30; } }

		protected virtual void ValueHasChanged(){
			if (ValueChanged != null){
				ValueChanged();
			}
		}
	}
}