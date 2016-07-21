using System;

namespace BaseLibS.Num.Space{
	public class Vec3Float{
		public float x;
		public float y;
		public float z;
		public Vec3Float(){}

		public Vec3Float(float x, float y, float z){
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vec3Float(Vec3Float vec){
			x = vec.x;
			y = vec.y;
			z = vec.z;
		}

		public void Set(float x1, float y1, float z1){
			x = x1;
			y = y1;
			z = z1;
		}

		public void Set(float[] t){
			x = t[0];
			y = t[1];
			z = t[2];
		}

		public void Set(Vec3Float t1){
			x = t1.x;
			y = t1.y;
			z = t1.z;
		}

		public void Add(Vec3Float t1, Vec3Float t2){
			x = t1.x + t2.x;
			y = t1.y + t2.y;
			z = t1.z + t2.z;
		}

		public void Add(Vec3Float t1){
			x += t1.x;
			y += t1.y;
			z += t1.z;
		}

		public float DistanceSquared(Vec3Float p1){
			double dx = x - p1.x;
			double dy = y - p1.y;
			double dz = z - p1.z;
			return (float) (dx*dx + dy*dy + dz*dz);
		}

		public float Distance(Vec3Float p1){
			return (float) Math.Sqrt(DistanceSquared(p1));
		}

		public void Subtract(Vec3Float t1, Vec3Float t2){
			x = t1.x - t2.x;
			y = t1.y - t2.y;
			z = t1.z - t2.z;
		}

		public static Vec3Float SubtractNew(Vec3Float t1, Vec3Float t2){
			return new Vec3Float(t1.x - t2.x, t1.y - t2.y, t1.z - t2.z);
		}

		public void Subtract(Vec3Float t1){
			x -= t1.x;
			y -= t1.y;
			z -= t1.z;
		}

		public void Scale(float s){
			x *= s;
			y *= s;
			z *= s;
		}

		public void Add(float a, float b, float c){
			x += a;
			y += b;
			z += c;
		}

		public void Scale(Vec3Float p){
			x *= p.x;
			y *= p.y;
			z *= p.z;
		}

		public void ScaleAdd(float s, Vec3Float t1, Vec3Float t2){
			x = s*t1.x + t2.x;
			y = s*t1.y + t2.y;
			z = s*t1.z + t2.z;
		}

		public void Average(Vec3Float a, Vec3Float b){
			x = (a.x + b.x)/2f;
			y = (a.y + b.y)/2f;
			z = (a.z + b.z)/2f;
		}

		public float Dot(Vec3Float v){
			return x*v.x + y*v.y + z*v.z;
		}

		public float NormSquared(){
			return x*x + y*y + z*z;
		}

		public float Norm(){
			return (float) Math.Sqrt(NormSquared());
		}

		public void Normalize(){
			double d = Norm();
			x = (float) (x/d);
			y = (float) (y/d);
			z = (float) (z/d);
		}

		public void Cross(Vec3Float v1, Vec3Float v2){
			Set(v1.y*v2.z - v1.z*v2.y, v1.z*v2.x - v1.x*v2.z, v1.x*v2.y - v1.y*v2.x);
		}

		public override bool Equals(Object t1){
			if (!(t1 is Vec3Float)){
				return false;
			}
			Vec3Float t2 = (Vec3Float) t1;
			return x == t2.x && y == t2.y && z == t2.z;
		}

		protected bool Equals(Vec3Float other){
			return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
		}

		public override int GetHashCode(){
			unchecked{
				int hashCode = x.GetHashCode();
				hashCode = (hashCode*397) ^ y.GetHashCode();
				hashCode = (hashCode*397) ^ z.GetHashCode();
				return hashCode;
			}
		}

		public override string ToString(){
			return "{" + x + ", " + y + ", " + z + "}";
		}

		public float Angle(Vec3Float v1){
			double xx = y*v1.z - z*v1.y;
			double yy = z*v1.x - x*v1.z;
			double zz = x*v1.y - y*v1.x;
			double cross = Math.Sqrt(xx*xx + yy*yy + zz*zz);
			return (float) Math.Abs(Math.Atan2(cross, Dot(v1)));
		}
	}
}