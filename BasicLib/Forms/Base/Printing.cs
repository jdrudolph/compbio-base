using System.IO;
using System.Windows.Forms;
using BasicLib.Graphic;

namespace BasicLib.Forms.Base{
	public static class Printing{
		public static void Print(IPrintable printable, string filename, int width, int height){
			filename = ShowDialog(filename);
			if (filename == null){
				return;
			}
			string extension = System.IO.Path.GetExtension(filename).ToLower();
			if (File.Exists(filename)){
				File.Delete(filename);
			}
			BasicImageFormat format = BasicImageFormat.GetFromExtension(extension);
			if (format == null){
				MessageBox.Show("Could not find the specified file format: " + extension);
			}
			IGraphics graphics = format.CreateGraphics(filename, width, height);
			printable.Print(graphics, width, height);
			graphics.Close();
			graphics.Dispose();
		}

		private static string ShowDialog(string filename){
			SaveFileDialog dialog = new SaveFileDialog{Filter = BasicImageFormat.GetFilter(), FileName = filename};
			return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
		}
	}
}