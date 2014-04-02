using System;
using System.Windows;

namespace BaseLib.Param{
	public delegate void ValueChangedHandler();

	[Serializable]
	public abstract class Parameter : ICloneable{
		public const int paramHeight = 23;

		[field: NonSerialized]
		public event ValueChangedHandler ValueChanged;

		public string Name { get; private set; }
		public string Help { get; set; }
		public bool Visible { get; set; }
		[NonSerialized] protected UIElement control;

		protected Parameter(string name){
			Name = name;
			Help = "";
			Visible = true;
		}

		public Parameter SetValFromCtrl(){
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
		public virtual void Drop(string x) { }
		protected abstract UIElement Control { get; }

		public UIElement GetControl(){
			control = Control;
			return control;
		}

		public virtual float Height { get { return paramHeight; } }

		protected virtual void ValueHasChanged(){
			if (ValueChanged != null){
				ValueChanged();
			}
		}
	}
}