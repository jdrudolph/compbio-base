using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BaseLib.Util;

namespace BaseLib.Api{
	[Serializable]
	public abstract class ClassificationModel{
		public abstract float[] PredictStrength(float[] x);

		public int PredictClass(float[] x){
			float[] w = PredictStrength(x);
			return ArrayUtils.MaxInd(w);
		}

		public int[] PredictClasses(float[] x){
			float[] w = PredictStrength(x);
			List<int> result = new List<int>();
			for (int i = 0; i < w.Length; i++){
				if (w[i] > 0){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		public void Write(string filePath){
			Stream stream = File.Open(filePath, FileMode.Create);
			BinaryFormatter bFormatter = new BinaryFormatter();
			bFormatter.Serialize(stream, this);
			stream.Close();
		}

		public static ClassificationModel Read(string filePath){
			Stream stream = File.Open(filePath, FileMode.Open);
			BinaryFormatter bFormatter = new BinaryFormatter();
			ClassificationModel m = (ClassificationModel) bFormatter.Deserialize(stream);
			stream.Close();
			return m;
		}
	}
}