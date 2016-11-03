using System.ComponentModel;
using System.Linq;
using BaseLib.Properties;

namespace BaseLib.Wpf{
	public class ViewModelBase : INotifyPropertyChanged{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName){
			var x = PropertyChanged?.GetInvocationList().ToArray();
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}