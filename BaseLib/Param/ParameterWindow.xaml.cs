using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using BaseLib.Util;

namespace BaseLib.Param{
	/// <summary>
	/// Interaction logic for ParameterWindow.xaml
	/// </summary>
	public partial class ParameterWindow{
		[DllImport("user32.dll")] private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
		[DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
		private const int gwlStyle = -16;
		private const int wsMinimizebox = 0x20000;

		private void WindowSourceInitialized(object sender, EventArgs e){
			IntPtr hwnd = new WindowInteropHelper((Window) sender).Handle;
			int value = GetWindowLong(hwnd, gwlStyle);
			SetWindowLong(hwnd, gwlStyle, value & ~wsMinimizebox);
		}

		public ParameterWindow(Parameters parameters, string title, string helpDescription, string helpOutput,
			IList<string> helpSuppls){
			InitializeComponent();
			OkButton.Focus();
			Height = ParameterPanel1.Init(parameters) + 65;
			HelpPanel.Children.Clear();
			List<string> titles = new List<string>();
			List<string> description = new List<string>();
			if (!string.IsNullOrEmpty(helpDescription)){
				titles.Add("Description");
				description.Add(helpDescription);
			}
			if (!string.IsNullOrEmpty(helpOutput)){
				titles.Add(" - ");
				description.Add(null);
				titles.Add("Output");
				description.Add(helpOutput);
			}
			if (helpSuppls != null){
				for (int i = 0; i < helpSuppls.Count; i++){
					if (!string.IsNullOrEmpty(helpSuppls[i])){
						titles.Add(" - ");
						description.Add(null);
						titles.Add("Suppl. table " + (i + 1));
						description.Add(helpSuppls[i]);
					}
				}
			}
			Grid g = new Grid();
			g.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(50, GridUnitType.Star)});
			for (int i = 0; i < titles.Count; i++){
				g.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(50, GridUnitType.Auto)});
			}
			g.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(50, GridUnitType.Star)});
			for (int i = 0; i < titles.Count; i++){
				TextBlock tb = new TextBlock{Text = titles[i]};
				if (description[i] != null){
					tb.ToolTip = new ToolTip{Content = StringUtils.Concat("\n", StringUtils.Wrap(description[i], 75))};
				}
				Grid.SetColumn(tb, i + 1);
				g.Children.Add(tb);
			}
			HelpPanel.Children.Add(g);
			Title = title;
		}

		private void OkButtonClick(object sender, RoutedEventArgs e){
			DialogResult = true;
			ParameterPanel1.SetParameters();
			Close();
		}

		private void CancelButtonClick(object sender, RoutedEventArgs e){
			DialogResult = false;
			Close();
		}

		private void OnKeyDownHandler(object sender, KeyEventArgs e){
			if (e.Key == Key.Return){
				DialogResult = true;
				ParameterPanel1.SetParameters();
				Close();
			}
		}
	}
}