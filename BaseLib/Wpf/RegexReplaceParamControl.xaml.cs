using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using BaseLib.Annotations;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for RegexReplaceParamControl.xaml
	/// </summary>
	public partial class RegexReplaceParamControl{
		public RegexReplaceParamControl(Regex pattern, string replacement, List<string> items){
			InitializeComponent();
			DataContext = new RegexReplaceParamViewModel(pattern, replacement, items);
		}
	}

	public class Item{
		private readonly Regex regex;
		private readonly string replacement;
		public string Current { get; }
		public string Preview => regex.Replace(Current, replacement);

		public Item(Regex regex, string replacement, string item){
			this.regex = regex;
			this.replacement = replacement;
			Current = item;
		}
	}

	internal class RegexReplaceParamViewModel : INotifyPropertyChanged{
		private Regex pattern;

		public Regex Pattern{
			get { return pattern; }
			set{
				pattern = value;
				OnPropertyChanged(nameof(Pattern));
				OnPropertyChanged(nameof(Items));
			}
		}

		private string replacement;

		public string Replacement{
			get { return replacement; }
			set{
				replacement = value;
				OnPropertyChanged(nameof(Replacement));
				OnPropertyChanged(nameof(Items));
			}
		}

		private readonly List<string> items;

		public ObservableCollection<Item> Items
			=> new ObservableCollection<Item>(items.Select(itm => new Item(Pattern, Replacement, itm)).ToList());

		public RegexReplaceParamViewModel(Regex pattern, string replacement, List<string> items){
			Pattern = pattern;
			Replacement = replacement;
			this.items = items;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName){
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}