using System;
using System.Drawing;
using System.Windows.Forms;

namespace BasicLib.Forms.Scatter{
	public partial class PlanePropertiesForm : Form{
		public PlanePropertiesForm(Color horizontalGridColor, Color verticalGridColor, GridType horizontalGrid,
			GridType verticalGrid, int horizontalGridWidth, int verticalGridWidth, Color borderColor, Color backgroundColor,
			Color axisColor, int majorTickLength, float majorTickLineWidth, int minorTickLength, float minorTickLineWidth,
			bool topAxisVisible, bool rightAxisVisible, float axisLineWidth, int numbersFontSize, bool numbersFontBold,
			int titleFontSize, bool titleFontBold, Color horizontalZeroColor, Color verticalZeroColor, int horizontalZeroWidth,
			int verticalZeroWidth, bool horizontalZeroVisible, bool verticalZeroVisible){
			InitializeComponent();
			numbersFontSizeNumericUpDown.Value = numbersFontSize;
			numbersFontBoldCheckBox.Checked = numbersFontBold;
			titlesFontSizeNumericUpDown.Value = titleFontSize;
			titlesFontBoldCheckBox.Checked = titleFontBold;
			horizontalGridColorButton.BackColor = horizontalGridColor;
			verticalGridColorButton.BackColor = verticalGridColor;
			lineWidthNumericUpDown.Value = (decimal) axisLineWidth;
			switch (horizontalGrid){
				case GridType.None:
					horizontalGridComboBox.SelectedIndex = 0;
					break;
				case GridType.Major:
					horizontalGridComboBox.SelectedIndex = 1;
					break;
				case GridType.All:
					horizontalGridComboBox.SelectedIndex = 2;
					break;
			}
			switch (verticalGrid){
				case GridType.None:
					verticalGridComboBox.SelectedIndex = 0;
					break;
				case GridType.Major:
					verticalGridComboBox.SelectedIndex = 1;
					break;
				case GridType.All:
					verticalGridComboBox.SelectedIndex = 2;
					break;
			}
			showTopAxisCheckBox.Checked = topAxisVisible;
			showRightAxisCheckBox.Checked = rightAxisVisible;
			horizontalGridNumericUpDown.Value = horizontalGridWidth;
			verticalGridNumericUpDown.Value = verticalGridWidth;
			fillColorButton.BackColor = borderColor;
			backgroundColorButton.BackColor = backgroundColor;
			lineColorButton.BackColor = axisColor;
			majorTicLengthNumericUpDown.Value = majorTickLength;
			minorTicLengthNumericUpDown.Value = minorTickLength;
			majorTicLineWidthNumericUpDown.Value = (decimal) majorTickLineWidth;
			minorTicLineWidthNumericUpDown.Value = (decimal) minorTickLineWidth;
			horizontalZeroColorButton.BackColor = horizontalZeroColor;
			verticalZeroColorButton.BackColor = verticalZeroColor;
			horizontalZeroNumericUpDown.Value = horizontalZeroWidth;
			verticalZeroNumericUpDown.Value = verticalZeroWidth;
			horizontalZeroCheckBox.Checked = horizontalZeroVisible;
			verticalZeroCheckBox.Checked = verticalZeroVisible;
		}

		public bool Ok { get; private set; }
		public Color FillColor { get { return fillColorButton.BackColor; } }
		public bool TopAxesVisible { get { return showTopAxisCheckBox.Checked; } }
		public bool RightAxesVisible { get { return showRightAxisCheckBox.Checked; } }
		public int MajorTickLength { get { return (int) majorTicLengthNumericUpDown.Value; } }
		public int MajorTickLineWidth { get { return (int) majorTicLineWidthNumericUpDown.Value; } }
		public int MinorTickLength { get { return (int) minorTicLengthNumericUpDown.Value; } }
		public int MinorTickLineWidth { get { return (int) minorTicLineWidthNumericUpDown.Value; } }
		public Color LineColor { get { return lineColorButton.BackColor; } }
		public Color BackgroundColor { get { return backgroundColorButton.BackColor; } }
		public Color HorizontalGridColor { get { return horizontalGridColorButton.BackColor; } }
		public Color VerticalGridColor { get { return verticalGridColorButton.BackColor; } }
		public Color HorizontalZeroColor { get { return horizontalZeroColorButton.BackColor; } }
		public Color VerticalZeroColor { get { return verticalZeroColorButton.BackColor; } }
		public GridType HorizontalGrid{
			get{
				int ind = horizontalGridComboBox.SelectedIndex;
				if (ind == 0){
					return GridType.None;
				}
				return ind == 1 ? GridType.Major : GridType.All;
			}
		}
		public GridType VerticalGrid{
			get{
				int ind = verticalGridComboBox.SelectedIndex;
				if (ind == 0){
					return GridType.None;
				}
				return ind == 1 ? GridType.Major : GridType.All;
			}
		}
		public int HorizontalGridWidth { get { return (int) horizontalGridNumericUpDown.Value; } }
		public int VerticalGridWidth { get { return (int) verticalGridNumericUpDown.Value; } }
		public int HorizontalZeroWidth { get { return (int) horizontalZeroNumericUpDown.Value; } }
		public int VerticalZeroWidth { get { return (int) verticalZeroNumericUpDown.Value; } }
		public bool HorizontalZeroVisible { get { return horizontalZeroCheckBox.Checked; } }
		public bool VerticalZeroVisible { get { return verticalZeroCheckBox.Checked; } }
		public int LineWidth { get { return (int) lineWidthNumericUpDown.Value; } }
		public int NumbersFontSize { get { return (int) numbersFontSizeNumericUpDown.Value; } }
		public bool NumbersFontBold { get { return numbersFontBoldCheckBox.Checked; } }
		public int TitleFontSize { get { return (int) titlesFontSizeNumericUpDown.Value; } }
		public bool TitleFontBold { get { return titlesFontBoldCheckBox.Checked; } }

		private void HorizontalGridColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = horizontalGridColorButton.BackColor};
			cd.ShowDialog();
			horizontalGridColorButton.BackColor = cd.Color;
			cd.Dispose();
		}

		private void VerticalGridColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = verticalGridColorButton.BackColor};
			cd.ShowDialog();
			verticalGridColorButton.BackColor = cd.Color;
			cd.Dispose();
		}

		private void BorderColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = fillColorButton.BackColor};
			cd.ShowDialog();
			fillColorButton.BackColor = cd.Color;
			cd.Dispose();
		}

		private void CancelButtonClick(object sender, EventArgs e){
			Close();
		}

		private void OkButtonClick(object sender, EventArgs e){
			Ok = true;
			Close();
		}

		private void BackgroundColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = backgroundColorButton.BackColor};
			cd.ShowDialog();
			backgroundColorButton.BackColor = cd.Color;
			cd.Dispose();
		}

		private void AxisColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = lineColorButton.BackColor};
			cd.ShowDialog();
			lineColorButton.BackColor = cd.Color;
			cd.Dispose();
		}

		private void HorizontalZeroColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = horizontalZeroColorButton.BackColor};
			cd.ShowDialog();
			horizontalZeroColorButton.BackColor = cd.Color;
			cd.Dispose();
		}

		private void VerticalZeroColorButtonClick(object sender, EventArgs e){
			ColorDialog cd = new ColorDialog{Color = verticalZeroColorButton.BackColor};
			cd.ShowDialog();
			verticalZeroColorButton.BackColor = cd.Color;
			cd.Dispose();
		}
	}
}