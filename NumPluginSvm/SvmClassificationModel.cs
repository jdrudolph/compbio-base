﻿using System;
using BaseLibS.Api;
using BaseLibS.Num.Vector;
using NumPluginSvm.Svm;

namespace NumPluginSvm{
	[Serializable]
	public class SvmClassificationModel : ClassificationModel{
		private readonly SvmModel[] models;
		private readonly bool[] invert;

		public SvmClassificationModel(SvmModel[] models, bool[] invert) {
			this.models = models;
			this.invert = invert;
		}

		public override float[] PredictStrength(BaseVector x){
			if (models.Length == 1){
				float[] result = new float[2];
				double[] decVal = new double[1];
				SvmMain.SvmPredictValues(models[0], x, decVal);
				result[0] = invert[0] ? -(float)decVal[0] : (float)decVal[0];
				result[1] = -result[0];
				return result;
			}
			float[] result1 = new float[models.Length];
			for (int i = 0; i < result1.Length; i++){
				double[] decVal = new double[1];
				SvmMain.SvmPredictValues(models[i], x, decVal);
				result1[i] = invert[i] ? -(float)decVal[0] : (float)decVal[0];
			}
			return result1;
		}
	}
}