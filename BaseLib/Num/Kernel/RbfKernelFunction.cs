﻿using System;
using BaseLib.Num.Api;
using BaseLib.Num.Vector;
using BaseLib.Param;

namespace BaseLib.Num.Kernel{
    [Serializable]
    public class RbfKernelFunction : IKernelFunction{
        private double sigma;
        public RbfKernelFunction() : this(1) {}

        public RbfKernelFunction(double sigma){
            this.sigma = sigma;
        }

        public bool UsesSquares{
            get { return false; }
        }

        public string Name{
            get { return "RBF"; }
        }

        public Parameters Parameters{
            get { return new Parameters(new Parameter[]{new DoubleParam("Sigma", sigma)}); }
            set { sigma = value.GetDoubleParam("Sigma").Value; }
        }

        public double Evaluate(BaseVector xi, BaseVector xj, double xSquarei, double xSquarej){
            return Math.Exp(-(xSquarei + xSquarej - 2*xi.Dot(xj))/2.0/xi.Length/sigma/sigma);
        }

        public object Clone(){
            return new RbfKernelFunction(sigma);
        }
    }
}