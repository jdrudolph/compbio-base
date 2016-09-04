using System.Globalization;

namespace BaseLibS.Graph{
	public struct PointF2{
		public static readonly PointF2 Empty = new PointF2();
		public float X { get; set; }
		public float Y { get; set; }

		public PointF2(float x, float y){
			X = x;
			Y = y;
		}

		public bool IsEmpty => X == 0f && Y == 0f;

		public static PointF2 operator +(PointF2 pt, SizeF2 sz){
			return Add(pt, sz);
		}

		public static PointF2 operator -(PointF2 pt, SizeF2 sz){
			return Subtract(pt, sz);
		}

		public static bool operator ==(PointF2 left, PointF2 right){
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(PointF2 left, PointF2 right){
			return !(left == right);
		}

		public static PointF2 Add(PointF2 pt, SizeF2 sz){
			return new PointF2(pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static PointF2 Subtract(PointF2 pt, SizeF2 sz){
			return new PointF2(pt.X - sz.Width, pt.Y - sz.Height);
		}

		public override bool Equals(object obj){
			if (!(obj is PointF2)){
				return false;
			}
			PointF2 comp = (PointF2) obj;
			return comp.X == this.X && comp.Y == this.Y && comp.GetType().Equals(this.GetType());
		}

		public override int GetHashCode(){
			return base.GetHashCode();
		}

		/// <include file="doc\PointF.uex" path='docs/doc[@for="PointF.ToString"]/*'>
		public override string ToString(){
			return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", X, Y);
		}
	}
}