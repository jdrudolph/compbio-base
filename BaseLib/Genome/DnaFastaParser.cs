using System;
using System.IO;
using BaseLib.Util;
using BaseLibS.Util;

namespace BaseLib.Genome{
	public class DnaFastaParser{
		private readonly string filename;
		private readonly Action<string, DnaSequence> process;

		public DnaFastaParser(string filename, Action<string, DnaSequence> process){
			this.filename = filename;
			this.process = process;
		}

		public void Parse(){
			StreamReader reader = FileUtils.GetReader(filename);
			string line;
			string header = null;
			DnaSequence sequence = new DnaSequence();
			while ((line = reader.ReadLine()) != null){
				if (line.StartsWith(">")){
					if (header != null){
						process(header, sequence);
					}
					header = line.Substring(1);
					sequence = new DnaSequence();
				} else{
					string s = StringUtils.RemoveWhitespace(line.Trim()).ToUpper();
					s = s.Replace('U', 'T');
					sequence.Append(s);
				}
			}
			if (header != null){
				process(header, sequence);
			}
			reader.Close();
		}
	}
}