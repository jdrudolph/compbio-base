using System;

namespace BaseLibS.Param{
	public delegate void ValueChangedHandler();

	[Serializable]
	public abstract class Parameter {
		public const int paramHeight = 23;

		[field: NonSerialized]
		public event ValueChangedHandler ValueChanged;

		public string Name { get; }
		public string Help { get; set; }
		public bool Visible { get; set; }
		public virtual ParamType Type => ParamType.Wpf;

		internal Parameter(string name){
			Name = name;
			Help = "";
			Visible = true;
		}

		public virtual void SetValueFromControl(){}
		public virtual void UpdateControlFromValue(){}

		public virtual object CreateControl(){
			return null;
		}

		public virtual void Drop(string x){}
		public abstract string StringValue { get; set; }
		public abstract void ResetValue();
		public abstract void ResetDefault();
		public abstract void Clear();
		public abstract bool IsModified { get; }
		public virtual bool IsDropTarget => false;
		public virtual float Height => paramHeight;

		public virtual string[] Markup
			=> new[]{"<parameter" + " name=\"" + Name + "\" value=\"" + StringValue + "\"></parameter>"};

		protected void ValueHasChanged(){
			ValueChanged?.Invoke();
		}
	}

	[Serializable]
	public abstract class Parameter<T> : Parameter{
		protected Parameter(string name) : base(name){}
		public T Value { get; set; }
		public T Default { get; set; }

		public sealed override void ResetValue(){
			if (Value is ICloneable){
				Value = (T) ((ICloneable) Default).Clone();
			} else{
				Value = Default;
			}
			ResetSubParamValues();
		}

		public sealed override void ResetDefault(){
			if (Value is ICloneable){
				Default = (T) ((ICloneable) Value).Clone();
			} else{
				Default = Value;
			}
			ResetSubParamDefaults();
		}

		public override bool IsModified => !Equals(Value, Default);
		public virtual void ResetSubParamValues(){}
		public virtual void ResetSubParamDefaults(){}

		public T Value2{
			get{
				SetValueFromControl();
				return Value;
			}
		}
	}
}