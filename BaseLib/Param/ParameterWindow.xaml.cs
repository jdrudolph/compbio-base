using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace BaseLib.Param {
	/// <summary>
	/// Interaction logic for ParameterWindow.xaml
	/// </summary>
	public partial class ParameterWindow : Window {
		public ParameterWindow(Parameters parameters, string title, string helpDescription, string helpOutput,
			IList<string> helpSuppls) {
			InitializeComponent();
			cancelButton.Click += CancelButtonClick;
			okButton.Click += OkButtonClick;
			parameterPanel1.Init(parameters);
			if (!string.IsNullOrEmpty(helpDescription)) {
				helpDoc.Blocks.Add(new Paragraph(new Run("Description:")));
				helpDoc.Blocks.Add(new Paragraph(new Run(helpDescription)));
			}
			if (!string.IsNullOrEmpty(helpOutput)) {
				helpDoc.Blocks.Add(new Paragraph(new Run("Output:")));
				helpDoc.Blocks.Add(new Paragraph(new Run(helpOutput)));
			}
			if (helpSuppls != null) {
				for (int i = 0; i < helpSuppls.Count; i++) {
					if (!string.IsNullOrEmpty(helpSuppls[i])) {
						helpDoc.Blocks.Add(new Paragraph(new Run("Suppl. table " + (i + 1) + ":")));
						helpDoc.Blocks.Add(new Paragraph(new Run(helpSuppls[i])));
					}
				}
			}
			Title = title;
		}

		private void OkButtonClick(object sender, RoutedEventArgs e) {
			Ok = true;
			parameterPanel1.SetParameters();
			Close();
		}

		private void CancelButtonClick(object sender, RoutedEventArgs e) {
			Close();
		}

		public bool Ok { get; private set; }
	}
}
