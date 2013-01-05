using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BasicLib.Graphic{
	public class Connector : GraphicObject{
		private readonly List<RectangleF> list = new List<RectangleF>();
		private readonly List<PointF> points = new List<PointF>();

		public Connector(PointF p0, RectangleF r, Pen pen) : base(pen){
			points.Clear();
			List.Clear();
			Path.Reset();
			Pen = pen;
			PointF p1 = new PointF((float) (r.X + (r.Width*0.5)), (float) (r.Y + (r.Height*0.5)));
			if (p0.X == p1.X){
				if (p0.Y > p1.Y){
					Path.AddLine(p0, new PointF(p1.X, r.Y + r.Height));
					List.Add(new RectangleF(p0, new SizeF(1, Math.Abs(p0.Y - r.Y + r.Height))));
					points.Add(p0);
					points.Add(new PointF(p1.X, r.Y + r.Height));
				}
			} else{
				if (p0.X > r.X && p0.X < r.X + r.Width){
					Path.AddLine(p0.X, p0.Y, p0.X, r.Y + r.Height);
					Path.AddLine(p0.X, r.Y + r.Height, p1.X, r.Y + r.Height);
					List.Add(new RectangleF(p0, new SizeF(1, Math.Abs(p0.Y - r.Y + r.Height))));
					List.Add(new RectangleF(new PointF(p0.X, r.Y + r.Height), new SizeF(Math.Abs(p0.X - p1.X), 1)));
					points.Add(new PointF(p0.X, p0.Y));
					points.Add(new PointF(p0.X, r.Y + r.Height));
					points.Add(new PointF(p1.X, r.Y + r.Height));
				} else{
					if (p0.X > p1.X){
						Path.AddLine(p0.X, p0.Y, p0.X, p1.Y);
						Path.AddLine(p0.X, p1.Y, r.X + r.Width, p1.Y);
						List.Add(new RectangleF(p0, new SizeF(1, Math.Abs(p0.Y - p1.Y))));
						List.Add(new RectangleF(new PointF(p0.X, p1.Y), new SizeF(Math.Abs(p0.X - r.X + r.Width), 1)));
						points.Add(new PointF(p0.X, p0.Y));
						points.Add(new PointF(p0.X, p1.Y));
						points.Add(new PointF(r.X + r.Width, p1.Y));
					} else{
						Path.AddLine(p0.X, p0.Y, p0.X, p1.Y);
						Path.AddLine(p0.X, p1.Y, r.X, p1.Y);
						List.Add(new RectangleF(p0, new SizeF(1, Math.Abs(p0.Y - p1.Y))));
						List.Add(new RectangleF(new PointF(p0.X, p1.Y), new SizeF(Math.Abs(p0.X - r.X), 1)));
						points.Add(new PointF(p0.X, p0.Y));
						points.Add(new PointF(p0.X, p1.Y));
						points.Add(new PointF(r.X, p1.Y));
					}
				}
			}
		}

		public List<RectangleF> List { get { return list; } }

		public virtual void Draw(IGraphics g){
			try{
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.DrawPath(Pen, Path);
			} catch (Exception ex){
				Console.WriteLine(Path.PathPoints[0] + ex.Message);
			}
		}
	}
}