using System;
using System.Windows;

namespace BaseLib.Wpf{
	/// <summary>
	/// Interaction logic for NumericUpDownControl.xaml
	/// </summary>
	public partial class NumericUpDownControl {
		public event RoutedEventHandler ValueChanged;
		private decimal minimum;
		private decimal maximum;
		public decimal Minimum{
			get { return minimum; }
			set{
				minimum = value;
				Value = Math.Max(Value, minimum);
			}
		}
		public decimal Maximum{
			get { return maximum; }
			set{
				maximum = value;
				Value = Math.Min(Value, maximum);
			}
		}

		public NumericUpDownControl(){
			InitializeComponent();
		}

		private void FireValueChanged(){
			ValueChanged?.Invoke(this, new RoutedEventArgs());
		}

		public decimal Value { get { return decimal.Parse(TextBox1.Text); } set { TextBox1.Text = "" + value; } }

		private void Increase(object sender, RoutedEventArgs e){
			Value = Math.Min(Value + 1, Maximum);
			FireValueChanged();
		}

		private void Decrease(object sender, RoutedEventArgs e){
			Value = Math.Max(Value - 1, Minimum);
			FireValueChanged();
		}
	}
}