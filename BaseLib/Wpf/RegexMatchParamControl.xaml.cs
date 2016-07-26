using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
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

    public class DataGridColumnsBehavior
    {
        public static readonly DependencyProperty BindableColumnsProperty =
            DependencyProperty.RegisterAttached("BindableColumns",
                                                typeof(ObservableCollection<DataGridColumn>),
                                                typeof(DataGridColumnsBehavior),
                                                new UIPropertyMetadata(null, BindableColumnsPropertyChanged));
        private static void BindableColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DataGrid dataGrid = source as DataGrid;
            ObservableCollection<DataGridColumn> columns = e.NewValue as ObservableCollection<DataGridColumn>;
            dataGrid.Columns.Clear();
            if (columns == null)
            {
                return;
            }
            foreach (DataGridColumn column in columns)
            {
                dataGrid.Columns.Add(column);
            }
            columns.CollectionChanged += (sender, e2) =>
            {
                NotifyCollectionChangedEventArgs ne = e2 as NotifyCollectionChangedEventArgs;
                if (ne.Action == NotifyCollectionChangedAction.Reset)
                {
                    dataGrid.Columns.Clear();
                    foreach (DataGridColumn column in ne.NewItems)
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (DataGridColumn column in ne.NewItems)
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Move)
                {
                    dataGrid.Columns.Move(ne.OldStartingIndex, ne.NewStartingIndex);
                }
                else if (ne.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (DataGridColumn column in ne.OldItems)
                    {
                        dataGrid.Columns.Remove(column);
                    }
                }
                else if (ne.Action == NotifyCollectionChangedAction.Replace)
                {
                    dataGrid.Columns[ne.NewStartingIndex] = ne.NewItems[0] as DataGridColumn;
                }
            };
        }
        public static void SetBindableColumns(DependencyObject element, ObservableCollection<DataGridColumn> value)
        {
            element.SetValue(BindableColumnsProperty, value);
        }
        public static ObservableCollection<DataGridColumn> GetBindableColumns(DependencyObject element)
        {
            return (ObservableCollection<DataGridColumn>)element.GetValue(BindableColumnsProperty);
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

        private readonly List<string> _items;
        public RegexMatchParamViewModel(Regex pattern, List<string> items)
        {
            Pattern = pattern;
            _items = items;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
