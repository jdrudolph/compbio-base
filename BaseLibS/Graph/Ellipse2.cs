namespace BaseLibS.Graph{
	public class Ellipse2{
		private PointF2 center;

		public Ellipse2(PointF2 center, float radiusX, float radiusY){
			this.center = center;
			RadiusX = radiusX;
			RadiusY = radiusY;
		}

		public float RadiusX { get; }
		public float RadiusY { get; }

		public bool Contains(int x, int y){
			if (RadiusX <= 0 || RadiusY <= 0){
				return false;
			}
			PointF2 normalized = new PointF2(x - center.X, y - center.Y);
			float nX = normalized.X;
			float nY = normalized.Y;
			return (double) (nX*nX)/(RadiusX*RadiusX) + (double) (nY*nY)/(RadiusY*RadiusY) <= 1.0;
		}
	}
}