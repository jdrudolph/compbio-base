using System;
using System.IO;
using System.Text;

namespace BaseLibS.Mol{
	public class AaSequence : IComparable, IDisposable{
		public const int aasPerLong = 12;
		private const int basis = 32;
		private const int basis4 = 32*32*32*32;
		private static string aas;
		private static string[] q;
		private ulong[] sequence;
		private readonly int len;
		private int hash;

		public AaSequence(BinaryReader reader){
			len = reader.ReadInt32();
			int n = reader.ReadInt32();
			sequence = new ulong[n];
			for (int i = 0; i < sequence.Length; i++){
				sequence[i] = reader.ReadUInt64();
			}
		}

		public AaSequence() { }

		public AaSequence(string seq){
			len = seq.Length;
			int a = len/aasPerLong;
			int b = len%aasPerLong;
			int n = (b == 0) ? a : a + 1;
			sequence = new ulong[n];
			for (int i = 0; i < a; i++){
				sequence[i] = Encode(seq.Substring(aasPerLong*i, aasPerLong));
			}
			if (b > 0){
				sequence[a] = Encode(seq.Substring(aasPerLong*a, len - aasPerLong*a));
			}
		}

		public int GetNumLongs(){
			int a = len/aasPerLong;
			int b = len%aasPerLong;
			return (b == 0) ? a : a + 1;
		}

		private static ulong Encode(string seq){
			ulong result = 0;
			int n = seq.Length;
			for (int i = 0; i < n; i++){
				char c = seq[n - 1 - i];
				int x = AminoAcids.SingleLetterAas.IndexOf(c);
				if (x == -1){
					x = 20;
				}
				result = (result << 5) + (ulong) x;
			}
			return result;
		}

		private static void Prepare(){
			q = new string[basis4];
			aas = AminoAcids.SingleLetterAas + "X";
			for (int i1 = 0; i1 < 21; i1++){
				int q1 = i1;
				for (int i2 = 0; i2 < 21; i2++){
					int q2 = i2 << 5;
					for (int i3 = 0; i3 < 21; i3++){
						int q3 = i3 << 10;
						for (int i4 = 0; i4 < 21; i4++){
							int q4 = i4 << 15;
							char[] s = new char[4];
							s[0] = aas[i1];
							s[1] = aas[i2];
							s[2] = aas[i3];
							s[3] = aas[i4];
							int n = q1 + q2 + q3 + q4;
							q[n] = new string(s);
						}
					}
				}
			}
		}

		private static void Decode(StringBuilder b, ulong a, int n){
			if (aas == null){
				Prepare();
			}
			int bins = n/4;
			for (int i = 0; i < bins; i++){
				int x = (int) (a%basis4);
				a = a >> 20;
				b.Append(q[x]);
			}
			n -= bins*4;
			for (int i = 0; i < n; i++){
				int x = (int) (a%basis);
				a = a >> 5;
				b.Append(aas[x]);
			}
		}

		public override string ToString(){
			if (sequence.Length == 0){
				return "";
			}
			StringBuilder b = new StringBuilder();
			for (int i = 0; i < sequence.Length - 1; i++){
				Decode(b, sequence[i], aasPerLong);
			}
			int l = len - (sequence.Length - 1)*aasPerLong;
			Decode(b, sequence[sequence.Length - 1], l);
			return b.ToString();
		}

		public override int GetHashCode(){
			int h = hash;
			if (h == 0){
				foreach (ulong t in sequence){
					h = 31*h + (int) t;
				}
				hash = h;
			}
			return h;
		}

		public override bool Equals(object obj){
			if (this == obj){
				return true;
			}
			if (obj is AaSequence){
				AaSequence other = (AaSequence) obj;
				if (other.len != len){
					return false;
				}
				for (int i = 0; i < sequence.Length; i++){
					if (sequence[i] != other.sequence[i]){
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public void Write(BinaryWriter writer){
			writer.Write(len);
			writer.Write(sequence.Length);
			foreach (ulong u in sequence){
				writer.Write(u);
			}
		}

		public int CompareTo(object obj){
			AaSequence other = (AaSequence) obj;
			int min = Math.Min(sequence.Length, other.sequence.Length);
			for (int i = 0; i < min; i++){
				if (sequence[i] != other.sequence[i]){
					if (sequence[i] < other.sequence[i]){
						return -1;
					}
					return 1;
				}
			}
			if (sequence.Length != other.sequence.Length){
				if (sequence.Length < other.sequence.Length){
					return -1;
				}
				return 1;
			}
			if (len != other.len){
				if (len < other.len){
					return -1;
				}
				return 1;
			}
			return 0;
		}

		public void Dispose() { sequence = null; }
	}
}