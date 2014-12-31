using System;

namespace BaseLibS.Param{
	public delegate void ValueChangedHandler();

	[Serializable]
	public abstract class Parameter : ICloneable{
		public const int paramHeight = 23;

		[field: NonSerialized]
		public event ValueChangedHandler ValueChanged;

		public string Name { get; private set; }
		public string Help { get; set; }
		public bool Visible { get; set; }

		internal Parameter(string name){
			Name = name;
			Help = "";
			Visible = true;
		}

		public abstract void SetValueFromControl();
		public abstract void UpdateControlFromValue();
		public abstract object CreateControl();
		public virtual void Drop(string x) { }

		public abstract string StringValue { get; set; }
		public abstract void ResetValue();
		public abstract void ResetDefault();
		public abstract object Clone();
		public abstract void Clear();
		public abstract bool IsModified { get; }
		public virtual bool IsDropTarget { get { return false; } }
		public virtual float Height { get { return paramHeight; } }
		public virtual string[] Markup { get { return new[]{"<parameter" + " name=\"" + Name + "\" value=\"" + StringValue + "\"></parameter>"}; } }

		protected void ValueHasChanged(){
			if (ValueChanged != null){
				ValueChanged();
			}
		}
	}

	public abstract class Parameter<T> : Parameter{
		protected Parameter(string name) : base(name) { }
		public T Value { get; set; }
		public T Default { get; protected set; }

		public override sealed void ResetValue(){
			if (Value is ICloneable){
				Value = (T) ((ICloneable) Default).Clone();
			} else{
				Value = Default;
			}
			ResetSubParamValues();
		}

		public override sealed void ResetDefault(){
			if (Value is ICloneable){
				Default = (T) ((ICloneable) Value).Clone();
			} else{
				Default = Value;
			}
			ResetSubParamDefaults();
		}

		public override bool IsModified { get { return !Equals(Value, Default); } }
		public virtual void ResetSubParamValues() { }
		public virtual void ResetSubParamDefaults() { }

		public T Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}
	}
}