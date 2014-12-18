using System;
using System.Windows;

namespace BaseLib.Param{
	public delegate void ValueChangedHandler();

	[Serializable]
	public abstract class Parameter : ICloneable{
		public const int paramHeight = 23;

		[field: NonSerialized]
		public event ValueChangedHandler ValueChanged;

		[NonSerialized] protected UIElement control;
		public string Name { get; private set; }
		public string Help { get; set; }
		public bool Visible { get; set; }

		protected Parameter(string name){
			Name = name;
			Help = "";
			Visible = true;
		}

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
		public virtual float Height { get { return paramHeight; } }
		public virtual string[] Markup { get { return new[]{"<parameter" + " name=\"" + Name + "\" value=\"" + StringValue + "\"></parameter>"}; } }
		protected abstract UIElement CreateControl();

		internal UIElement GetControl(){
			return control = CreateControl();
		}

		protected void ValueHasChanged(){
			if (ValueChanged != null){
				ValueChanged();
			}
		}
	}
}