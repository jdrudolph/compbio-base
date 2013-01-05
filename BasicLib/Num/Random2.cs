using System;
using System.Collections.Generic;
using BasicLib.Util;

namespace BasicLib.Num{
	/// <summary>
	/// Extension of the <c>Random</c> class containing utility methods for 
	/// generating pseudo random numbers.
	/// </summary>
	public class Random2 : Random{
		/// <summary>
		/// Temporary store needed for generating Gaussian random numbers.
		/// </summary>
		private bool iset;
		/// <summary>
		/// Temporary store needed for generating Gaussian random numbers.
		/// </summary>
		private double gset;
		public Random2(int seed) : base(seed) {}
		public Random2() {}

		/// <summary>
		/// This method generates a pseudo random number drawn from a normal distribution
		/// with zero mean and unit variance.
		/// </summary>
		/// <returns> The Gaussian random number.</returns>
		public double NextGaussian(){
			return NumUtils.Gasdev(ref iset, ref gset, this);
		}

		/// <summary>
		/// This method generates a pseudo random number drawn from a normal distribution
		/// with the given mean and standard deviation.
		/// </summary>
		/// <returns> The Gaussian random number.</returns>
		public double NextGaussian(double mean, double stddev){
			double x = NumUtils.Gasdev(ref iset, ref gset, this);
			return x*stddev + mean;
		}

		/// <summary>
		/// This routine generates a random number between 0 and n inclusive,
		/// following the binomial distribution with probability p and n trials. The
		/// routine is based on the BTPE algorithm, described in:
		/// 
		/// Voratas Kachitvichyanukul and Bruce W. Schmeiser: Binomial Random Variate
		/// Generation Communications of the ACM, Volume 31, Number 2, February 1988,
		/// pages 216-222.
		/// 
		/// </summary>
		/// <param name="n">The number of trials.</param>
		/// <param name="p">The probability of a single event. This probability should be less than or equal to 0.5.</param>
		/// <returns>An integer drawn from a binomial distribution with parameters (p, n).</returns>
		public int NextBinomial(int n, double p){
			return Binomial(n, p, this);
		}

		public int[] NextMultinomial(double[] source, int n){
			int colors = source.Length;
			return Multinomial(source, n, colors, this);
		}

		/// <summary>
		/// Produces a random permutation of the integers from 0 to n-1.
		/// </summary>
		/// <param name="n">The length of the vector of permuted integers.</param>
		/// <returns></returns>
		public int[] NextPermutation(int n){
			int[] perm = ArrayUtils.ConsecutiveInts(n);
			for (int i = 0; i < n; i++){
				int pos = Next(i, n);
				int tmp = perm[i];
				perm[i] = perm[pos];
				perm[pos] = tmp;
			}
			return perm;
		}

		/// <summary>
		/// Returns randomized training and test set indices for n-fold cross
		/// validation.
		/// </summary>
		/// <param name="n">The number of items the cross validation will be performed on.</param>
		/// <param name="nfold">The number of cross validation folds.</param>
		/// <param name="train">Contains on output <c>nfold</c> vectors of integers filled with the indices of the training set for the particular fold.</param>
		/// <param name="test">Contains on output <c>nfold</c> vectors of integers filled with the indices of the test set for the particular fold.</param>
		public void NextCrossValidationIndices(int n, int nfold, out int[][] train, out int[][] test){
			if (nfold > n){
				nfold = n;
			}
			int[] perm = NextPermutation(n);
			int[][] subsets = new int[nfold][];
			for (int i = 0; i < nfold; i++){
				int start = (int) Math.Round(i*n/(double) nfold);
				int end = (int) Math.Round((i + 1)*n/(double) nfold);
				subsets[i] = ArrayUtils.SubArray(perm, start, end);
			}
			train = new int[nfold][];
			test = new int[nfold][];
			for (int i = 0; i < nfold; i++){
				test[i] = subsets[i];
				train[i] =
					ArrayUtils.Concat(ArrayUtils.SubArray(subsets, ArrayUtils.RemoveAtIndex(ArrayUtils.ConsecutiveInts(nfold), i)));
			}
		}

		public void RandomSubsamplingIndeces(int n, int nfold, int nsampling, out int[][] train, out int[][] test){
			if (nfold > n){
				nfold = n;
			}
			train = new int[nsampling][];
			test = new int[nsampling][];
			for (int i = 0; i < nsampling; i++){
				int[] perm = NextPermutation(n);
				const int start = 0;
				var end = (int) Math.Round(n/(double) nfold);
				test[i] = ArrayUtils.SubArray(perm, start, end);
				train[i] = ArrayUtils.SubArray(perm, end, perm.Length);
			}
		}

		/// <summary>
		/// Static routine doing the actual work of drawing from a binomial distribution.
		/// </summary>
		/// <param name="n">The number of trials.</param>
		/// <param name="p">The probability of a single event. This probability should be less than or equal to 0.5.</param>
		/// <param name="random">An instance of the <c>Random</c> class used to generate uniformly distributed random numbers.</param>
		/// <returns>An integer drawn from a binomial distribution with parameters (p, n).</returns>
		private static int Binomial(int n, double p, Random random){
			double q = 1 - p;
			if (n*p < 30.0){
				// Algorithm BINV
				double s = p/q;
				double a = (n + 1)*s;
				double r = Math.Exp(n*Math.Log(q));
				int x = 0;
				double u = random.NextDouble();
				while (true){
					if (u < r){
						return x;
					}
					u -= r;
					x++;
					r *= (a/x) - s;
				}
			} else{
				// Algorithm BTPE 
				// Step 0 
				double fm = n*p + p;
				int m = (int) Math.Floor(fm);
				double p1 = Math.Floor(2.195*Math.Sqrt(n*p*q) - 4.6*q) + 0.5;
				double xm = m + 0.5;
				double xl = xm - p1;
				double xr = xm + p1;
				double c = 0.134 + 20.5/(15.3 + m);
				double a = (fm - xl)/(fm - xl*p);
				double b = (xr - fm)/(xr*q);
				double lambdal = a*(1.0 + 0.5*a);
				double lambdar = b*(1.0 + 0.5*b);
				double p2 = p1*(1 + 2*c);
				double p3 = p2 + c/lambdal;
				double p4 = p3 + c/lambdar;
				while (true){
					// Step 1
					int y;
					double u = random.NextDouble();
					double v = random.NextDouble();
					u *= p4;
					if (u <= p1){
						return (int) (Math.Floor(xm - p1*v + u));
					}
					// Step 2
					if (u > p2){
						// Step 3
						if (u > p3){
							// Step 4
							y = (int) (xr - Math.Log(v)/lambdar);
							if (y > n){
								continue;
							}
							// Go to step 5
							v = v*(u - p3)*lambdar;
						} else{
							y = (int) (xl + Math.Log(v)/lambdal);
							if (y < 0){
								continue;
							}
							// Go to step 5
							v = v*(u - p2)*lambdal;
						}
					} else{
						double x = xl + (u - p1)/c;
						v = v*c + 1.0 - Math.Abs(m - x + 0.5)/p1;
						if (v > 1){
							continue;
						}
						// Go to step 5
						y = (int) x;
					}
					// Step 5
					// Step 5.0
					int k = Math.Abs(y - m);
					if (k > 20 && k < 0.5*n*p*q - 1.0){
						// Step 5.2
						double rho = (k/(n*p*q))*((k*(k/3.0 + 0.625) + 0.1666666666666)/(n*p*q) + 0.5);
						double t = -k*k/(2*n*p*q);
						double a2 = Math.Log(v);
						if (a2 < t - rho){
							return y;
						}
						if (a2 > t + rho){
							continue;
						}
						// Step 5.3
						double x1 = y + 1;
						double f1 = m + 1;
						double z = n + 1 - m;
						double w = n - y + 1;
						double x2 = x1*x1;
						double f2 = f1*f1;
						double z2 = z*z;
						double w2 = w*w;
						if (a2 >
							xm*Math.Log(f1/x1) + (n - m + 0.5)*Math.Log(z/w) + (y - m)*Math.Log(w*p/(x1*q)) +
								(13860.0 - (462.0 - (132.0 - (99.0 - 140.0/f2)/f2)/f2)/f2)/f1/166320.0 +
								(13860.0 - (462.0 - (132.0 - (99.0 - 140.0/z2)/z2)/z2)/z2)/z/166320.0 +
								(13860.0 - (462.0 - (132.0 - (99.0 - 140.0/x2)/x2)/x2)/x2)/x1/166320.0 +
								(13860.0 - (462.0 - (132.0 - (99.0 - 140.0/w2)/w2)/w2)/w2)/w/166320.0){
							continue;
						}
						return y;
					}
					// Step 5.1 
					int i;
					double s = p/q;
					double aa = s*(n + 1);
					double f = 1.0;
					for (i = m; i < y; f *= (aa/(++i) - s)){}
					for (i = y; i < m; f /= (aa/(++i) - s)){}
					if (v > f){
						continue;
					}
					return y;
				}
			}
		}

		/// <summary>
		/// This function generates a vector of random variates from a multinomial distribution.
		/// </summary>
		/// <param name="source">An input array containing the probability or fraction of each color in the urn.</param>
		/// <param name="n">The number of balls drawn from the urn.</param>
		/// <param name="colors">The number of possible colors.</param>
		/// <param name="random">The random number generator.</param>
		/// <returns>The number of balls of each color.</returns>
		private static int[] Multinomial(IList<double> source, int n, int colors, Random random){
			int[] destination = new int[source.Count];
			double s;
			double sum;
			int i;
			if (n < 0 || colors < 0){
				throw new Exception("Parameter negative in multinomial function");
			}
			if (colors == 0){
				return new int[0];
			}
			// compute sum of probabilities
			for (i = 0, sum = 0; i < colors; i++){
				s = source[i];
				if (s < 0){
					throw new Exception("Parameter negative in multinomial function");
				}
				sum += s;
			}
			if (sum == 0 && n > 0){
				throw new Exception("Zero sum in multinomial function");
			}
			for (i = 0; i < colors - 1; i++){
				// generate output by calling binomial (colors-1) times
				s = source[i];
				int x = sum <= s ? n : Binomial(n, s/sum, random);
				n -= x;
				sum -= s;
				destination[i] = x;
			}
			// get the last one
			destination[i] = n;
			return destination;
		}
	}
}