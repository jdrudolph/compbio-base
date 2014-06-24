namespace BaseLib.Util{
	public static class GlobalConstants{
		/// <summary>
		/// "Reverse" Added to file name at two points in method AddFastaFile. Removed in method GetProteinIndices.
		/// Used in method PluginMzTab.mztab.AddContent.AddIdentifierOrigin.
		/// </summary>
		public const string revPrefix = "REV__";
		/// <summary>
		/// "Contaminants" Added to file name at two points in method AddFastaFile.
		/// Used in method PluginMzTab.mztab.AddContent.AddIdentifierOrigin.
		/// </summary>
		public const string conPrefix = "CON__";
	}
}