using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseLib.Annotations;
using iTextSharp.text.pdf;

namespace BaseLib.Wpf
{
    /// <summary>
    /// Interaction logic for RegexMatchParamControl.xaml
    /// </summary>
    public partial class RegexMatchParamControl : UserControl
    {
        public RegexMatchParamControl(Tuple<Regex, List<string>> value) : this(value.Item1, value.Item2) { }

        public RegexMatchParamControl(Regex pattern, List<string> items)
        {
            InitializeComponent();
            DataContext = new RegexMatchParamViewModel(pattern, items);
        }
    }

    internal class MatchItem
    {
        public Match Match { get; set; }
        public string Input { get; set; }

        internal MatchItem(Match match, string input)
        {
            Match = match;
            Input = input;
        }
}

    internal class RegexMatchParamViewModel : INotifyPropertyChanged
    {
        private Regex _pattern;

        public Regex Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                OnPropertyChanged(nameof(Pattern));
                OnPropertyChanged(nameof(Matches));
                OnPropertyChanged(nameof(MatchColumns));
            }
        }

        private List<string> _items;
        public List<string> Items {
            get { return _items; }
            set { _items = value; OnPropertyChanged(nameof(Items)); OnPropertyChanged(nameof(Matches)); OnPropertyChanged(nameof(MatchColumns)); }
        }

        public ObservableCollection<MatchItem> Matches => new ObservableCollection<MatchItem>(_items.Select(s => new MatchItem(_pattern.Match(s), s)));
        public ObservableCollection<DataGridColumn> MatchColumns => new ObservableCollection<DataGridColumn>(new [] {
            new DataGridTextColumn // First column
            {
                Header = "Input",
                Binding = new Binding("Input"),
            }}.Concat(
            _pattern.GetGroupNames().Skip(1).Select(grp => // rest of columns
            {
                var binding = new Binding($"Match.Groups[{grp}]");
                var col =  new DataGridTextColumn() {Header = grp, Binding=binding};
                return col;
            })));

        public RegexMatchParamViewModel(Regex pattern, List<string> items)
        {
            Pattern = pattern;
            Items = items;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
