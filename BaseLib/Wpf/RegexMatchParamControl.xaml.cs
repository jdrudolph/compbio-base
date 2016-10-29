using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using BaseLib.Annotations;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for RegexMatchParamControl.xaml
	/// </summary>
	public partial class RegexMatchParamControl {
		public RegexMatchParamControl(Regex pattern, List<string> items){
			InitializeComponent();
			DataContext = new RegexMatchParamViewModel(pattern, items);
		}
	}

	internal class MatchItem{
		public Match Match { get; set; }
		public string Input { get; set; }

		internal MatchItem(Match match, string input){
			Match = match;
			Input = input;
		}
	}

	internal class RegexMatchParamViewModel : INotifyPropertyChanged{
		private Regex pattern;

		public Regex Pattern{
			get { return pattern; }
			set{
				pattern = value;
				OnPropertyChanged(nameof(Pattern));
				OnPropertyChanged(nameof(Matches));
				OnPropertyChanged(nameof(MatchColumns));
			}
		}

		private List<string> items;

		public List<string> Items{
			get { return items; }
			set{
				items = value;
				OnPropertyChanged(nameof(Items));
				OnPropertyChanged(nameof(Matches));
				OnPropertyChanged(nameof(MatchColumns));
			}
		}

		public ObservableCollection<MatchItem> Matches
			=> new ObservableCollection<MatchItem>(items.Select(s => new MatchItem(pattern.Match(s), s)));

		public ObservableCollection<DataGridColumn> MatchColumns
			=> new ObservableCollection<DataGridColumn>(new[]{
				new DataGridTextColumn // First column
				{Header = "Input", Binding = new Binding("Input"),}
			}.Concat(
				pattern.GetGroupNames().Skip(1).Select(grp => // rest of columns
				{
					var binding = new Binding($"Match.Groups[{grp}]");
					var col = new DataGridTextColumn(){Header = grp, Binding = binding};
					return col;
				})));

		public RegexMatchParamViewModel(Regex pattern, List<string> items){
			Pattern = pattern;
			Items = items;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName){
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}