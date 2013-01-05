using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace BasicLib.Graphic{
	public abstract class GraphicObject{
		private readonly GraphicsPath path = new GraphicsPath();
		[XmlIgnore]
		public Pen Pen { get; set; }
		[XmlIgnore]
		public bool Visible { get; set; }

		protected GraphicObject(Pen pen){
			Visible = true;
			Pen = pen;
		}

		public GraphicsPath Path { get { return path; } }

		/// <summary>
		/// Befindet sich der angegebene Punkt über der Linie des Objekts?
		/// </summary>
		public virtual bool Hit(Point pt){
			try{
				return path.IsOutlineVisible(pt, new Pen(Brushes.Black, 4));
			} catch (Exception ex){
				Console.WriteLine(path.PathPoints[0] + ex.Message);
				return false;
			}
		}
	}
}