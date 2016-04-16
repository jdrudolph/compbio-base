using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BaseLib.Param;

namespace BaseLib.Wpf{
	public static class WpfUtils{
		public static readonly HashSet<string> categoricalColDefaultNames =
			new HashSet<string>(new[]{
				"pfam names", "gocc names", "gomf names", "gobp names", "kegg pathway names", "chromosome", "strand",
				"interpro name", "prints name", "prosite name", "smart name", "sequence motifs", "reactome", "transcription factors",
				"microrna", "scop class", "scop fold", "scop superfamily", "scop family", "phospho motifs", "mim", "pdb", "intact",
				"corum", "motifs", "best motif", "reverse", "contaminant", "potential contaminant", "only identified by site",
				"type", "amino acid", "raw file", "experiment", "charge", "modifications", "md modification", "dp aa", "dp decoy",
				"dp modification", "fraction", "dp cluster index", "authors", "publication", "year", "publisher", "geography",
				"geography id", "identified", "fragmentation", "mass analyzer", "labeling state", "ion mode", "mode", "composition",
				"isotope cluster index"
			});

		public static readonly HashSet<string> textualColDefaultNames =
			new HashSet<string>(new[]{
				"protein ids", "protein", "majority protein ids", "protein names", "gene names", "uniprot", "ensembl", "ensg",
				"ensp", "enst", "mgi", "kegg ortholog", "dip", "hprd interactors", "sequence window", "sequence", "orf name",
				"names", "proteins", "positions within proteins", "leading proteins", "leading razor protein", "md sequence",
				"md proteins", "md gene names", "md protein names", "dp base sequence", "dp probabilities", "dp proteins",
				"dp gene names", "dp protein names", "name", "dn sequence", "title", "volume", "number", "pages",
				"modified sequence", "formula", "formula2"
			});

		public static readonly HashSet<string> numericColDefaultNames =
			new HashSet<string>(new[]{
				"length", "position", "total position", "peptides (seq)", "razor peptides (seq)", "unique peptides (seq)",
				"localization prob", "size", "p value", "benj. hoch. fdr", "score", "delta score", "combinatorics", "intensity",
				"score for localization", "pep", "m/z", "mass", "resolution", "uncalibrated - calibrated m/z [ppm]",
				"mass error [ppm]", "uncalibrated mass error [ppm]", "uncalibrated - calibrated m/z [da]", "mass error [da]",
				"uncalibrated mass error [da]", "max intensity m/z 0", "retention length", "retention time",
				"calibrated retention time", "calibrated retention time start", "calibrated retention time finish",
				"retention time calibration", "match time difference", "match q-value", "match score", "number of data points",
				"number of scans", "number of isotopic peaks", "pif", "fraction of total spectrum", "base peak fraction",
				"ms/ms count", "ms/ms m/z", "md base scan number", "md mass error", "md time difference", "dp mass difference",
				"dp time difference", "dp score", "dp pep", "dp positional probability", "dp base scan number", "dp mod scan number",
				"dp cluster mass", "dp cluster mass sd", "dp cluster size total", "dp cluster size forward",
				"dp cluster size reverse", "dp peptide length difference", "dn score", "dn normalized score", "dn nterm mass",
				"dn cterm mass", "dn missing mass", "dn score diff", "views", "estimated minutes watched", "average view duration",
				"average percentage viewed", "subscriber views", "subscriber minutes watched", "clicks", "clickable impressions",
				"click through rate", "closes", "closable impressions", "close rate", "impressions", "likes", "likes added",
				"likes removed", "dislikes", "dislikes added", "dislikes removed", "shares", "comments", "favorites",
				"favorites added", "favorites removed", "subscribers", "subscribers gained", "subscribers lost",
				"average view duration (minutes)", "scan number", "ion injection time", "total ion current", "base peak intensity",
				"elapsed time", "precursor full scan number", "precursor intensity", "precursor apex fraction",
				"precursor apex offset", "precursor apex offset time", "scan event number", "scan index", "ms scan index",
				"ms scan number", "agc fill", "parent intensity fraction", "intens comp factor", "ctcd comp", "rawovftt",
				"cycle time", "dead time", "basepeak intensity", "mass calibration", "peak length", "isotope pattern length",
				"multiplet length", "peaks / s", "single peaks / s", "isotope patterns / s", "single isotope patterns / s",
				"multiplets / s", "identified multiplets / s", "multiplet identification rate [%]", "ms/ms / s",
				"identified ms/ms / s", "ms/ms identification rate [%]", "mass fractional part", "mass deficit",
				"mass precision [ppm]", "max intensity m/z 1", "retention length (fwhm)", "min scan number", "max scan number",
				"lys count", "arg count", "intensity", "intensity h", "intensity m", "intensity l", "r count", "k count", "jitter",
				"closest known m/z", "delta [ppm]", "delta [mda]", "uncalibrated delta [ppm]", "uncalibrated delta [mda]",
				"recalibration curve [ppm]", "recalibration curve [mda]", "q-value", "number of frames", "min frame number",
				"max frame number", "ion mobility index", "ion mobility index length", "ion mobility index length (fwhm)",
				"isotope correlation", "peptides", "razor + unique peptides", "unique peptides", "sequence coverage [%]",
				"unique sequence coverage [%]", "unique + razor sequence coverage [%]", "mol. weight [kda]", "dm [mda]", "dm [ppm]",
				"time [sec]", "du"
			});

		public static readonly HashSet<string> multiNumericColDefaultNames =
			new HashSet<string>(new[]
			{"mass deviations [da]", "mass deviations [ppm]", "number of phospho (sty)", "protein group ids"});

		public static void SetOkFocus(Control c){
			Window w = Window.GetWindow(c);
			if (w is ParameterWindow){
				ParameterWindow pw = (ParameterWindow) w;
				pw.FocusOkButton();
			}
		}

		public static float GetDpiScaleX(){
			PropertyInfo dpiXProperty = typeof (SystemParameters).GetProperty("DpiX",
				BindingFlags.NonPublic | BindingFlags.Static);
			int dpiX = (int) dpiXProperty.GetValue(null, null);
			return dpiX/96f;
		}

		public static float GetDpiScaleY(){
			PropertyInfo dpiYProperty = typeof (SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);
			int dpiY = (int) dpiYProperty.GetValue(null, null);
			return dpiY/96f;
		}

		public static UIElement GetGridChild(Grid grid, int row, int column){
			return grid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
		}

		public static BitmapSource LoadBitmap(System.Drawing.Bitmap source){
			if (source == null){
				return null;
			}
			return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions());
		}

		public static BitmapSource GetHelpBitmap(){
			return LoadBitmap(Properties.Resources.help);
		}

		public static BitmapSource GetMinMaxRibbonBitmap(){
			return LoadBitmap(Properties.Resources.minMaxRibbon);
		}

		public static BitmapSource GetPdfBitmap(){
			return LoadBitmap(Properties.Resources.pdf);
		}

		public static BitmapSource GetToolsBitmap(){
			return LoadBitmap(Properties.Resources.tools);
		}

		public static BitmapSource GetExitBitmap(){
			return LoadBitmap(Properties.Resources.exit);
		}

		public static BitmapSource GetSaveBitmap(){
			return LoadBitmap(Properties.Resources.save);
		}

		public static BitmapSource GetSaveAsBitmap(){
			return LoadBitmap(Properties.Resources.save_as);
		}

		public static BitmapSource GetInfoBitmap(){
			return LoadBitmap(Properties.Resources.info);
		}

		public static BitmapSource GetOpenBitmap(){
			return LoadBitmap(Properties.Resources.open);
		}

		public static BitmapSource GetMonitorsBitmap(){
			return LoadBitmap(Properties.Resources.monitors);
		}

		public static BitmapSource GetNewBitmap(){
			return LoadBitmap(Properties.Resources._new);
		}

		public static BitmapSource GetNewWindowBitmap(){
			return LoadBitmap(Properties.Resources.open_in_new_window);
		}

		public static BitmapSource GetMergeBitmap(){
			return LoadBitmap(Properties.Resources.merge);
		}

		public static BitmapSource GetMinusBitmap(){
			return LoadBitmap(Properties.Resources.minus_icon);
		}

		public static BitmapSource GetPlusBitmap(){
			return LoadBitmap(Properties.Resources.plus_icon);
		}

		public static BitmapSource GetArrowCornerBitmap(){
			return LoadBitmap(Properties.Resources.arrowCorner);
		}

		public static BitmapSource GetRotateBitmap(){
			return LoadBitmap(Properties.Resources.rotate);
		}

		public static void SelectExact(ICollection<string> colNames, IList<string> colTypes, MultiListSelectorControl mls){
			for (int i = 0; i < colNames.Count; i++){
				switch (colTypes[i]){
					case "E":
						mls.SetSelected(0, i, true);
						break;
					case "N":
						mls.SetSelected(1, i, true);
						break;
					case "C":
						mls.SetSelected(2, i, true);
						break;
					case "T":
						mls.SetSelected(3, i, true);
						break;
					case "M":
						mls.SetSelected(4, i, true);
						break;
				}
			}
		}

		public static void SelectHeuristic(IList<string> colNames, MultiListSelectorControl mls){
			char guessedType = GuessSilacType(colNames);
			for (int i = 0; i < colNames.Count; i++){
				if (categoricalColDefaultNames.Contains(colNames[i].ToLower())){
					mls.SetSelected(2, i, true);
					continue;
				}
				if (textualColDefaultNames.Contains(colNames[i].ToLower())){
					mls.SetSelected(3, i, true);
					continue;
				}
				if (numericColDefaultNames.Contains(colNames[i].ToLower())){
					mls.SetSelected(1, i, true);
					continue;
				}
				if (multiNumericColDefaultNames.Contains(colNames[i].ToLower())){
					mls.SetSelected(4, i, true);
					continue;
				}
				switch (guessedType){
					case 's':
						if (colNames[i].StartsWith("Norm. Intensity")){
							mls.SetSelected(0, i, true);
						}
						break;
					case 'd':
						if (colNames[i].StartsWith("Ratio H/L Normalized ")){
							mls.SetSelected(0, i, true);
						}
						break;
				}
			}
		}

		public static char GuessSilacType(IEnumerable<string> colnames){
			bool isSilac = false;
			foreach (string s in colnames){
				if (s.StartsWith("Ratio M/L")){
					return 't';
				}
				if (s.StartsWith("Ratio H/L")){
					isSilac = true;
				}
			}
			return isSilac ? 'd' : 's';
		}
	}
}