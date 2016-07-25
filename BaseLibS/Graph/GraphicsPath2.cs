using System.Collections.Generic;

namespace BaseLibS.Graph{
	public class GraphicsPath2{
		private readonly List<PointF2> pathPoints = new List<PointF2>();
		private readonly List<byte> pathTypes = new List<byte>();
		public PointF2[] PathPoints => pathPoints.ToArray();
		public byte[] PathTypes => pathTypes.ToArray();
		public int PointCount => pathPoints.Count;

		public void AddLine(PointF2 p1, PointF2 p2){
			AddLine(p1.X, p1.Y, p2.X, p2.Y);
		}

		public void AddLine(float x1, float y1, float x2, float y2){
			pathPoints.Add(new PointF2(x1, y1));
			pathPoints.Add(new PointF2(x2, y2));
			pathTypes.Add(0);
			pathTypes.Add(1);
		}

		public void Reset(){
			pathPoints.Clear();
			pathTypes.Clear();
		}
	}
}