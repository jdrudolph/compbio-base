namespace BaseLibS.Util{
	public abstract class GenericWorkDispatcher : WorkDispatcher{
		protected readonly bool externalCalculations;

		protected GenericWorkDispatcher(int nThreads, int nTasks, string infoFolder, bool externalCalculations)
			: base(nThreads, nTasks, infoFolder){
			this.externalCalculations = externalCalculations;
		}

		protected sealed override string GetCommandFilename(){
			return "\"" + FileUtils.executablePath + "\\" + Executable64Bit + "\"";
		}

		protected abstract string Executable64Bit { get; }

		protected sealed override bool ExternalCalculation(){
			return externalCalculations;
		}

		protected sealed override string GetCommandArguments(int taskIndex){
			object[] o = GetArguments(taskIndex);
			string[] args = new string[o.Length + 1];
			args[0] = $"\"{Id}\"";
			for (int i = 0; i < o.Length; i++){
				args[i + 1] = $"\"{o[i]}\"";
			}
			return StringUtils.Concat(" ", args);
		}

		protected sealed override void InternalCalculation(int taskIndex){
			Calculation(GetStringArgs(taskIndex));
		}

		public sealed override string GetMessagePrefix(){
			return MessagePrefix + " ";
		}

		private string[] GetStringArgs(int taskIndex){
			object[] o = GetArguments(taskIndex);
			string[] args = new string[o.Length];
			for (int i = 0; i < o.Length; i++){
				args[i] = $"{o[i]}";
			}
			return args;
		}

		public abstract void Calculation(string[] args);
		protected abstract object[] GetArguments(int taskIndex);
		protected abstract int Id { get; }
		protected abstract string MessagePrefix { get; }
	}
}