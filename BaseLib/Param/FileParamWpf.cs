using System;
using BaseLib.Wpf;
using BaseLibS.Param;

namespace BaseLib.Param{
	//TODO should be internal
	[Serializable]
	public class FileParamWpf : FileParam{
		[NonSerialized] private FileParameterControlWpf control;
		public FileParamWpf(string name) : base(name){}
		public FileParamWpf(string name, string value) : base(name, value){}
		public override ParamType Type => ParamType.Wpf;

		public override void SetValueFromControl(){
			var vm = (FileParamterViewModel) control.DataContext;
			Value = vm.FileName;
		}

		public override void UpdateControlFromValue(){
			if (control == null){
				return;
			}
			var vm = (FileParamterViewModel) control.DataContext;
			vm.FileName = Value;
		}

		public override object CreateControl(){
			control = new FileParameterControlWpf(Value, Filter, ProcessFileName, Save);
			var vm = (FileParamterViewModel) control.DataContext;
			vm.PropertyChanged += Vm_PropertyChanged;
			return control;
		}

		private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e){
			switch (e.PropertyName){
				case "FileName":
					SetValueFromControl();
					ValueHasChanged();
					break;
			}
		}
	}
}