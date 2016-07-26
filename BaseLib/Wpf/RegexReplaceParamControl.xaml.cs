using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RegexReplaceParamControl.xaml
    /// </summary>
    public partial class RegexReplaceParamControl : UserControl
    {
        public RegexReplaceParamControl(Tuple<Regex, string, List<string>> value) : this(value.Item1, value.Item2, value.Item3) { }

        public RegexReplaceParamControl(Regex pattern, string replacement, List<string> items)
        {
            InitializeComponent();
            DataContext = new RegexReplaceParamViewModel(pattern, replacement, items);
        }
    }

    public class Item
    {
        private readonly Regex _regex;
        private readonly string _replacement;
        public string Current { get; }
        public string Preview => _regex.Replace(Current, _replacement);

        public Item(Regex regex, string replacement, string item)
        {
            _regex = regex;
            _replacement = replacement;
            Current = item;
        }
    }

    public class StringToRegexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Regex) value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Regex((string) value);
        }
    }

    public class StringToRegexValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = (string) value;
            if (str == null)
            {
                return new ValidationResult(false, "value was null");
            }
            try
            {
                var regex = new Regex(str);
                return new ValidationResult(true, "");
            }
            catch (ArgumentException)
            {
                return new ValidationResult(false, $"'{str}' is not a valid Regex");
            }
        }
    }

    internal class RegexReplaceParamViewModel : INotifyPropertyChanged
    {
        private Regex _pattern;
        public Regex Pattern { get { return _pattern; } set { _pattern = value; OnPropertyChanged(nameof(Pattern)); OnPropertyChanged(nameof(Items));} }
        private string _replacement;
        public string Replacement { get { return _replacement; } set { _replacement = value; OnPropertyChanged(nameof(Replacement)); OnPropertyChanged(nameof(Items)); } }
        private List<string> _items;
        public ObservableCollection<Item> Items => new ObservableCollection<Item>(_items.Select(itm => new Item(Pattern, Replacement, itm)).ToList()); 

        public RegexReplaceParamViewModel(Regex pattern, string replacement, List<string> items)
        {
            Pattern = pattern;
            Replacement = replacement;
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
