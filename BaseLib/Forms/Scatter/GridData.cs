namespace BaseLib.Forms.Scatter {
	internal class GridData{
		private readonly bool[,] data;
		internal GridData(int count, bool[,] data){
			Count = count;
			this.data = data;
		}

		public int Count { get; set; }
		public bool[,] Data { get { return data; } }
	}
}
