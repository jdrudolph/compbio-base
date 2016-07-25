namespace BaseLibS.Graph{
	public class Pen2{
		public Pen2(Color2 color) : this(color, 1f){}

		public Pen2(Color2 color, float width){
			Color = color;
			Width = width;
			DashCap = DashCap2.Flat;
		}

		public Color2 Color { get; set; }
		public float Width { get; set; }
		public DashCap2 DashCap { get; set; }
		public DashStyle2 DashStyle { get; set; }
		//
		// Summary:
		//     Gets or sets the distance from the start of a line to the beginning of a dash
		//     pattern.
		//
		// Returns:
		//     The distance from the start of a line to the beginning of a dash pattern.
		//
		// Exceptions:
		//   T:System.ArgumentException:
		//     The System.Drawing.Pen.DashOffset property is set on an immutable System.Drawing.Pen,
		//     such as those returned by the System.Drawing.Pens class.
		public float DashOffset { get; set; }
		//
		// Summary:
		//     Gets or sets an array of custom dashes and spaces.
		//
		// Returns:
		//     An array of real numbers that specifies the lengths of alternating dashes and
		//     spaces in dashed lines.
		//
		// Exceptions:
		//   T:System.ArgumentException:
		//     The System.Drawing.Pen.DashPattern property is set on an immutable System.Drawing.Pen,
		//     such as those returned by the System.Drawing.Pens class.
		public float[] DashPattern { get; set; }
	}
}