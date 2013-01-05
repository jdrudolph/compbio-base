using System.Collections.Generic;

namespace BasicLib.Forms.Base{
	public class BasicColumnStyles{
		private readonly List<BasicColumnStyle> list = new List<BasicColumnStyle>();
		private readonly BasicTableLayoutView view;

		internal BasicColumnStyles(BasicTableLayoutView view){
			this.view = view;
		}

		public int Count { get { return list.Count; } }

		public BasicColumnStyle this[int i]{
			get { return list[i]; }
			set{
				list[i] = value;
				view.InvalidateSizes();
			}
		}

		public BasicColumnStyle[] ToArray(){
			return list.ToArray();
		}

		public void Add(BasicColumnStyle x){
			list.Add(x);
			view.InvalidateSizes();
		}
	}
}