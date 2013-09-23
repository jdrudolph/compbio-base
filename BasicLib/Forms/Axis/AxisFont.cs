using System;
using System.Drawing;

namespace BasicLib.Forms.Axis{
	[Obsolete]
	public class AxisFont{
		private float size;
		private string familyname;
		private bool bold;
		private const int minFontsize = 2;

		public AxisFont(string familyname, float size, bool bold){
			this.familyname = familyname;
			this.size = size;
			this.bold = bold;
			Font = new Font(familyname, size, bold ? FontStyle.Bold : FontStyle.Regular);
		}

		public Font Font { get; set; }
		public float Size{
			set{
				if (size >= minFontsize){
					size = value;
					Update();
				}
			}
			get { return size; }
		}
		public string FontFamily{
			set{
				familyname = value;
				Update();
			}
		}
		public bool Bold{
			set{
				bold = value;
				Update();
			}
			get { return bold; }
		}
		public float Height { get { return Font.Height; } }

		private void Update(){
			Font = new Font(familyname, size, bold ? FontStyle.Bold : FontStyle.Regular);
		}
	}
}