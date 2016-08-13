using System;

namespace BaseLibS.Num.Space{
	public class Vector4Fa{
		public Vector4Fa(float x, float y, float z, float w){
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float W { get; set; }
		public static readonly Vector4Fa Zero = new Vector4Fa(0, 0, 0, 0);
		public static readonly Vector4Fa One = new Vector4Fa(1, 1, 1, 1);
		public Vector4Fa(float a) : this(a, a, a, a){}

		public Vector4Fa(Vector3F v, float w){
			X = v.X;
			Y = v.Y;
			Z = v.Z;
			W = w;
		}

		public Vector4Fa(){}

		public static Vector4Fa Clamp(Vector4Fa x, Vector4Fa min, Vector4Fa max){
			return new Vector4Fa(NumUtils.Clamp(x.X, min.X, max.X), NumUtils.Clamp(x.Y, min.Y, max.Y),
				NumUtils.Clamp(x.Z, min.Z, max.Z), NumUtils.Clamp(x.W, min.W, max.W));
		}

		public static Vector4Fa operator +(Vector4Fa left, Vector4Fa right){
			return new Vector4Fa(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		public static Vector4Fa operator *(Vector4Fa left, Vector4Fa right){
			return new Vector4Fa(left.X*right.X, left.Y*right.Y, left.Z*right.Z, left.W*right.W);
		}

		public static Vector4Fa operator *(Vector4Fa left, float right){
			return new Vector4Fa(left.X + right, left.Y + right, left.Z + right, left.W + right);
		}

		public static Vector4Fa operator -(Vector4Fa left, Vector4Fa right){
			return new Vector4Fa(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		public static Vector4Fa Abs(Vector4Fa v){
			return new Vector4Fa(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z), Math.Abs(v.W));
		}

		public static Vector4Fa LinearInterpolate(Vector4Fa a, Vector4Fa b, float b1){
			float a1 = 1 - b1;
			return new Vector4Fa(a1*a.X + b1*b.X, a1*a.Y + b1*b.Y, a1*a.Z + b1*b.Z, a1*a.W + b1*b.W);
		}
	}
}