using System;
using System.Globalization;

namespace BaseLibS.Graph{
	public struct RectangleF2{
		public static readonly RectangleF2 Empty = new RectangleF2();
		public float X { get; set; }
		public float Y { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }

		public RectangleF2(float x, float y, float width, float height) : this(){
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public RectangleF2(PointF2 point, SizeF2 sizeF){
			X = point.X;
			Y = point.Y;
			Width = sizeF.Width;
			Height = sizeF.Height;
		}

		public static RectangleF2 FromLTRB(float left, float top, float right, float bottom){
			return new RectangleF2(left, top, right - left, bottom - top);
		}

		public PointF2 Location{
			get { return new PointF2(X, Y); }
			set{
				X = value.X;
				Y = value.Y;
			}
		}

		public SizeF2 Size{
			get { return new SizeF2(Width, Height); }
			set{
				Width = value.Width;
				Height = value.Height;
			}
		}

		public float Left => X;
		public float Top => Y;
		public float Right => X + Width;
		public float Bottom => Y + Height;
		public bool IsEmpty => (Width <= 0) || (Height <= 0);

		public override bool Equals(object obj){
			if (!(obj is RectangleF2)){
				return false;
			}
			RectangleF2 comp = (RectangleF2) obj;
			return (comp.X == X) && (comp.Y == Y) && (comp.Width == Width) && (comp.Height == Height);
		}

		public static bool operator ==(RectangleF2 left, RectangleF2 right){
			return (left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height);
		}

		public static bool operator !=(RectangleF2 left, RectangleF2 right){
			return !(left == right);
		}

		public bool Contains(float x, float y){
			return X <= x && x < X + Width && Y <= y && y < Y + Height;
		}

		public bool Contains(PointF2 pt){
			return Contains(pt.X, pt.Y);
		}

		public bool Contains(RectangleF2 rect){
			return (X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y) &&
					(rect.Y + rect.Height <= Y + Height);
		}

		public override int GetHashCode(){
			return
				(int)
					((uint) X ^ (((uint) Y << 13) | ((uint) Y >> 19)) ^ (((uint) Width << 26) | ((uint) Width >> 6)) ^
					(((uint) Height << 7) | ((uint) Height >> 25)));
		}

		public void Inflate(float x, float y){
			X -= x;
			Y -= y;
			Width += 2*x;
			Height += 2*y;
		}

		public void Inflate(SizeF2 size){
			Inflate(size.Width, size.Height);
		}

		public static RectangleF2 Inflate(RectangleF2 rect, float x, float y){
			RectangleF2 r = rect;
			r.Inflate(x, y);
			return r;
		}

		public void Intersect(RectangleF2 rect){
			RectangleF2 result = Intersect(rect, this);
			X = result.X;
			Y = result.Y;
			Width = result.Width;
			Height = result.Height;
		}

		public static RectangleF2 Intersect(RectangleF2 a, RectangleF2 b){
			float x1 = Math.Max(a.X, b.X);
			float x2 = Math.Min(a.X + a.Width, b.X + b.Width);
			float y1 = Math.Max(a.Y, b.Y);
			float y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);
			if (x2 >= x1 && y2 >= y1){
				return new RectangleF2(x1, y1, x2 - x1, y2 - y1);
			}
			return Empty;
		}

		public bool IntersectsWith(RectangleF2 rect){
			return (rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);
		}

		public static RectangleF2 Union(RectangleF2 a, RectangleF2 b){
			float x1 = Math.Min(a.X, b.X);
			float x2 = Math.Max(a.X + a.Width, b.X + b.Width);
			float y1 = Math.Min(a.Y, b.Y);
			float y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);
			return new RectangleF2(x1, y1, x2 - x1, y2 - y1);
		}

		public void Offset(PointF2 pos){
			Offset(pos.X, pos.Y);
		}

		public void Offset(float x, float y){
			X += x;
			Y += y;
		}

		public static implicit operator RectangleF2(Rectangle2 r){
			return new RectangleF2(r.X, r.Y, r.Width, r.Height);
		}

		public override string ToString(){
			return "{x=" + X.ToString(CultureInfo.CurrentCulture) + ",y=" + Y.ToString(CultureInfo.CurrentCulture) + ",Width=" +
					Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + Height.ToString(CultureInfo.CurrentCulture) + "}";
		}
	}
}