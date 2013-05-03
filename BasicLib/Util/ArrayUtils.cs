using System;
using System.Collections.Generic;
using System.Linq;
using BasicLib.Num;

namespace BasicLib.Util{
	public static class ArrayUtils{
		/// <summary>
		/// Determines the smallest number in the input array.
		/// </summary>
		/// <param name="x">The input array. It may contain NaN and infinity values.</param>
		/// <returns>The minimum.</returns>
		public static double Min(IList<double> x){
			if (x == null || x.Count == 0){
				return double.NaN;
			}
			double min = double.MaxValue;
			foreach (double val in x){
				if (val < min){
					min = val;
				}
			}
			return min;
		}

		public static float Min(IList<float> x){
			if (x == null || x.Count == 0){
				return float.NaN;
			}
			float min = float.MaxValue;
			foreach (float val in x){
				if (val < min){
					min = val;
				}
			}
			return min;
		}

		public static byte Min(IList<byte> x){
			if (x == null || x.Count == 0){
				return byte.MaxValue;
			}
			byte min = byte.MaxValue;
			foreach (byte val in x){
				if (val < min){
					min = val;
				}
			}
			return min;
		}

		public static int MinInd(IList<decimal> x){
			int n = x.Count;
			decimal min = decimal.MaxValue;
			int ind = -1;
			for (int i = 0; i < n; i++){
				decimal val = x[i];
				if (val <= min){
					min = val;
					ind = i;
				}
			}
			return ind;
		}

		public static int MinInd(IList<double> x){
			int n = x.Count;
			double min = double.MaxValue;
			int ind = -1;
			for (int i = 0; i < n; i++){
				double val = x[i];
				if (val <= min){
					min = val;
					ind = i;
				}
			}
			return ind;
		}

		public static int MinInd(IList<int> x){
			int n = x.Count;
			int min = int.MaxValue;
			int ind = -1;
			for (int i = 0; i < n; i++){
				int val = x[i];
				if (val <= min){
					min = val;
					ind = i;
				}
			}
			return ind;
		}

		public static double Range(IList<double> x){
			return Max(x) - Min(x);
		}

		public static double Max(IList<double> x){
			if (x == null || x.Count == 0){
				return double.NaN;
			}
			double max = double.MinValue;
			foreach (double val in x){
				if (val > max){
					max = val;
				}
			}
			return max;
		}

		public static float Max(IList<float> x){
			if (x == null || x.Count == 0){
				return float.NaN;
			}
			float max = float.MinValue;
			foreach (float val in x){
				if (val > max){
					max = val;
				}
			}
			return max;
		}

		public static byte Max(IList<byte> x){
			if (x == null || x.Count == 0){
				return byte.MinValue;
			}
			byte max = byte.MinValue;
			foreach (byte val in x){
				if (val > max){
					max = val;
				}
			}
			return max;
		}

		public static long Max(IList<long> x){
			if (x == null || x.Count == 0){
				return long.MinValue;
			}
			long max = long.MinValue;
			foreach (long val in x){
				if (val > max){
					max = val;
				}
			}
			return max;
		}

		public static short Max(IList<short> x){
			if (x == null || x.Count == 0){
				return short.MinValue;
			}
			short max = short.MinValue;
			foreach (short val in x){
				if (val > max){
					max = val;
				}
			}
			return max;
		}

		public static int MaxInd(IList<float> x){
			int n = x.Count;
			float max = float.MinValue;
			int ind = -1;
			for (int i = 0; i < n; i++){
				float val = x[i];
				if (val >= max){
					max = val;
					ind = i;
				}
			}
			return ind;
		}

		public static int MaxInd(IList<int> x){
			int n = x.Count;
			int max = int.MinValue;
			int ind = -1;
			for (int i = 0; i < n; i++){
				int val = x[i];
				if (val >= max){
					max = val;
					ind = i;
				}
			}
			return ind;
		}

		public static int[] MaxInds(IList<int> x){
			int maxVal = Max(x);
			List<int> inds = new List<int>();
			for (int i = 0; i < x.Count; i++){
				if (x[i] == maxVal){
					inds.Add(i);
				}
			}
			return inds.ToArray();
		}

		public static double Mean(IList<int> x){
			int n = x.Count;
			if (n == 0){
				return double.NaN;
			}
			double sum = 0;
			for (int i = 0; i < n; i++){
				sum += x[i];
			}
			return sum/n;
		}

		public static double Mean(IList<float> x){
			int n = x.Count;
			if (n == 0){
				return double.NaN;
			}
			double sum = 0;
			for (int i = 0; i < n; i++){
				sum += x[i];
			}
			return sum/n;
		}

		public static double Mean(IList<double> x){
			int n = x.Count;
			if (n == 0){
				return double.NaN;
			}
			double sum = 0;
			for (int i = 0; i < n; i++){
				sum += x[i];
			}
			return sum/n;
		}

		public static double Median(IList<double> x){
			int n = x.Count;
			if (n == 0){
				return double.NaN;
			}
			int[] o = Order(x);
			if (n%2 == 1){
				return x[o[n/2]];
			}
			return 0.5*(x[o[n/2 - 1]] + x[o[n/2]]);
		}

		public static float Median(IList<float> x){
			int n = x.Count;
			if (n == 0){
				return float.NaN;
			}
			int[] o = Order(x);
			if (n%2 == 1){
				return x[o[n/2]];
			}
			return 0.5f*(x[o[n/2 - 1]] + x[o[n/2]]);
		}

		public static float Median(IList<int> x){
			int n = x.Count;
			if (n == 0){
				return int.MinValue;
			}
			int[] o = Order(x);
			if (n%2 == 1){
				return x[o[n/2]];
			}
			return 0.5f*(x[o[n/2 - 1]] + x[o[n/2]]);
		}

		public static double TukeyBiweight(IList<double> x) {
			return TukeyBiweightCalc.TukeyBiweight(x);
		}

		public static double TukeyBiweightSe(IList<double> x) {
			double result = TukeyBiweightCalc.TukeyBiweight(x);
			return TukeyBiweightCalc.TukeyBiweightSe(x, result);
		}

		public static double MostFrequentValue(IList<double> data) {
			int n = data.Count;
			if (n <= 3){
				return Median(data);
			}
			double[] x;
			double[] y;
			Histogram(data, out x, out y, false, false);
			int ind = MaxInd(y);
			return x[ind];
		}

		public static int MaxInd(IList<double> x){
			int n = x.Count;
			double max = double.MinValue;
			int ind = -1;
			for (int i = 0; i < n; i++){
				double val = x[i];
				if (val > max){
					max = val;
					ind = i;
				}
			}
			return ind;
		}

		public static double[] ToDoubles(IList<float> floats){
			double[] result = new double[floats.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = floats[i];
			}
			return result;
		}

		public static int[] Complement(IList<int> present, int length){
			HashSet<int> dummy = new HashSet<int>();
			dummy.UnionWith(present);
			List<int> result = new List<int>();
			for (int i = 0; i < length; i++){
				if (!dummy.Contains(i)){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		public static T[] Concat<T>(IList<T> first, IList<T> second){
			if (first == null && second == null){
				return null;
			}
			if (first == null){
				return second.ToArray();
			}
			if (second == null){
				return first.ToArray();
			}
			T[] result = new T[first.Count + second.Count];
			Array.Copy(first.ToArray(), 0, result, 0, first.Count);
			Array.Copy(second.ToArray(), 0, result, first.Count, second.Count);
			return result;
		}

		public static T[] Concat<T>(IList<T> a, T b){
			T[] result = new T[a.Count + 1];
			Array.Copy(a.ToArray(), 0, result, 0, a.Count);
			result[a.Count] = b;
			return result;
		}

		public static T[] Concat<T>(IList<T[]> x){
			int len = 0;
			foreach (T[] t in x){
				len += t.Length;
			}
			T[] result = new T[len];
			int c = 0;
			foreach (T t1 in x.SelectMany(t => t)){
				result[c++] = t1;
			}
			return result;
		}

		public static T[] UniqueValuesPreserveOrder<T>(IList<T[]> array){
			HashSet<T> taken = new HashSet<T>();
			List<T> result = new List<T>();
			foreach (T ty in from tx in array from ty in tx where !taken.Contains(ty) select ty){
				taken.Add(ty);
				result.Add(ty);
			}
			return result.ToArray();
		}

		public static int Min(IList<int> x){
			if (x == null || x.Count == 0){
				return int.MaxValue;
			}
			int min = int.MaxValue;
			foreach (int val in x){
				if (val < min){
					min = val;
				}
			}
			return min;
		}

		public static int Max(IList<int> x){
			if (x == null || x.Count == 0){
				return int.MinValue;
			}
			int max = int.MinValue;
			foreach (int val in x){
				if (val > max){
					max = val;
				}
			}
			return max;
		}

		public static T[,] ExtractColumns<T>(T[,] values, IList<int> cols){
			T[,] result = new T[values.GetLength(0),cols.Count];
			for (int i = 0; i < values.GetLength(0); i++){
				for (int j = 0; j < cols.Count; j++){
					result[i, j] = values[i, cols[j]];
				}
			}
			return result;
		}

		public static List<T> SubList<T>(List<T> list, int[] indices){
			List<T> result = new List<T>();
			foreach (int index in indices){
				result.Add(list[index]);
			}
			return result;
		}

		public static int[] ConsecutiveInts(int to){
			return ConsecutiveInts(0, to);
		}

		/// <summary>
		/// Create a list of consecutive integers.
		/// </summary>
		/// <param name="from">Start index.</param>
		/// <param name="to">End (exclusive).</param>
		/// <returns>The list of consecutive integers.</returns>
		public static int[] ConsecutiveInts(int from, int to){
			int len = to - from;
			int[] result = new int[len];
			for (int i = 0; i < len; i++){
				result[i] = from + i;
			}
			return result;
		}

		/// <summary>
		/// Create a list of consecutive longs.
		/// </summary>
		/// <param name="from">Start index.</param>
		/// <param name="to">End (exclusive).</param>
		/// <returns>The list of consecutive longs.</returns>
		public static long[] ConsecutiveLongs(long from, long to){
			long len = to - from;
			long[] result = new long[len];
			for (int i = 0; i < len; i++){
				result[i] = from + i;
			}
			return result;
		}

		/// <summary>
		/// Extracts the indexed elements from the given array.
		/// </summary>
		/// <typeparam name="T">Arbitrary type of the array elements.</typeparam>
		/// <param name="array">The input array.</param>
		/// <param name="indices">Indices of the elements to be extracted.</param>
		/// <returns>An array containing the elements of the input array indexed by the <code>indices</code> array.</returns>
		public static T[] SubArray<T>(IList<T> array, IList<int> indices){
			T[] result = new T[indices.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = array[indices[i]];
			}
			return result;
		}

		/// <summary>
		/// Extracts the first <code>len</code> elements from the input array. 
		/// </summary>
		/// <typeparam name="T">Arbitrary type of the array elements.</typeparam>
		/// <param name="array">The input array.</param>
		/// <param name="len">Length of the output array.</param>
		/// <returns>The first <code>len</code> elements of the input array.</returns>
		public static T[] SubArray<T>(T[] array, int len){
			if (array.Length <= len){
				return (T[]) array.Clone();
			}
			T[] result = new T[len];
			Array.Copy(array, 0, result, 0, len);
			return result;
		}

		/// <summary>
		/// Extracts the subarrry from the position <code>startPos</code> on. 
		/// </summary>
		/// <typeparam name="T">Arbitrary type of the array elements.</typeparam>
		/// <param name="array">The input array.</param>
		/// <param name="startPos">Start position of the output array.</param>
		/// <returns>The subarrry from the position <code>startPos</code> on.</returns>
		public static T[] SubArrayFrom<T>(T[] array, int startPos){
			if (startPos < 0 || startPos > array.Length){
				return null;
			}
			int len = array.Length - startPos;
			T[] result = new T[len];
			Array.Copy(array, startPos, result, 0, len);
			return result;
		}

		public static T[] SubArray<T>(IList<T> array, int startIndex, int stopIndex){
			int len = stopIndex - startIndex;
			T[] result = new T[len];
			for (int i = 0; i < len; i++){
				result[i] = array[startIndex + i];
			}
			return result;
		}

		public static void Histogram(IList<double> data, out double[] x, out double[] y, bool normalized, bool cumulative){
			data = Remove(data, double.NaN);
			int n = data.Count;
			if (n == 0){
				x = new double[0];
				y = new double[0];
				return;
			}
			if (n == 1){
				x = new[]{data[0]};
				y = new double[]{1};
				return;
			}
			double sdev = StandardDeviation(data);
			double iqr = InterQuartileRange(data);
			double h = 1.06*Math.Min(sdev, iqr/1.34)/Math.Pow(n, 0.2);
			if (h == 0){
				h = 1;
			}
			Histogram(data, out x, out y, normalized, cumulative, h);
		}

		public static double StandardDeviation(IList<double> x){
			return Math.Sqrt(Variance(x));
		}

		public static double StandardDeviation(IList<float> x){
			return Math.Sqrt(Variance(x));
		}

		public static double StandardDeviation(IList<int> x){
			return Math.Sqrt(Variance(x));
		}

		public static double Variance(IList<double> x){
			int n = x.Count;
			double mean = Mean(x);
			double var = 0;
			for (int i = 0; i < n; i++){
				double w = x[i] - mean;
				var += w*w;
			}
			var /= (n - 1);
			return var;
		}

		public static double Variance(IList<float> x){
			int n = x.Count;
			double mean = Mean(x);
			double var = 0;
			for (int i = 0; i < n; i++){
				double w = x[i] - mean;
				var += w*w;
			}
			var /= (n - 1);
			return var;
		}

		public static double Variance(IList<int> x){
			int n = x.Count;
			double mean = Mean(x);
			double var = 0;
			for (int i = 0; i < n; i++){
				double w = x[i] - mean;
				var += w*w;
			}
			var /= (n - 1);
			return var;
		}

		public static T[] Remove<T>(IList<T> x, T elem){
			List<T> result = new List<T>();
			foreach (T t in x){
				if (!elem.Equals(t)){
					result.Add(t);
				}
			}
			return result.ToArray();
		}

		public static void Histogram(IList<double> data, out double[] x, out double[] y, bool normalized, bool cumulative,
			double h, double min, double max){
			int n = data.Count;
			double span = max - min;
			int nbins = (int) Math.Max(Math.Round(span/h), 1);
			x = new double[nbins];
			double binsize = span/nbins;
			for (int i = 0; i < nbins; i++){
				x[i] = min + binsize*(i + 0.5);
			}
			y = new double[nbins];
			try{
				foreach (int index in data.Select(d => (int) Math.Floor((d - min)/binsize))){
					if (index < 0 || index >= y.Length){
						continue;
					}
					if (normalized){
						y[index] += 1.0/binsize/n;
					} else{
						y[index]++;
					}
				}
			} catch (Exception ex){
				Console.WriteLine(ex);
			}
			if (cumulative){
				for (int i = 1; i < y.Length; i++){
					y[i] += y[i - 1];
				}
				for (int i = 0; i < y.Length; i++){
					y[i] *= binsize;
				}
			}
		}

		public static void Histogram(IList<double> data, out double[] x, out double[] y, bool normalized, bool cumulative,
			double h){
			double min;
			double max;
			MinMax(data, out min, out max);
			if (min == max){
				Histogram(data, out x, out y, normalized, cumulative, 0.1, min - 0.05, max + 0.05);
			}
			double span = max - min;
			int nbins = (int) Math.Max(Math.Round(span/h), 1);
			min -= span/2.0/nbins;
			max += span/2.0/nbins;
			Histogram(data, out x, out y, normalized, cumulative, h, min, max);
		}

		public static void MinMax(IList<double> x, out double min, out double max){
			int n = x.Count;
			if (n == 0){
				min = Double.NaN;
				max = Double.NaN;
				return;
			}
			min = Double.MaxValue;
			max = Double.MinValue;
			for (int i = 0; i < n; i++){
				double val = x[i];
				if (Double.IsInfinity(val)){
					continue;
				}
				if (val < min){
					min = val;
				}
				if (val > max){
					max = val;
				}
			}
			if (min == Double.MaxValue){
				min = Double.NaN;
				max = Double.NaN;
			}
		}

		public static void MinMax(IList<float> x, out double min, out double max){
			int n = x.Count;
			if (n == 0){
				min = Double.NaN;
				max = Double.NaN;
				return;
			}
			min = Double.MaxValue;
			max = -Double.MaxValue;
			for (int i = 0; i < n; i++){
				double val = x[i];
				if (val < min){
					min = val;
				}
				if (val > max){
					max = val;
				}
			}
			if (min == Double.MaxValue){
				min = Double.NaN;
				max = Double.NaN;
			}
		}

		/// <summary>
		/// For the sake of simplicity do all sorting tasks in the project always and ever with this method.
		/// </summary>
		/// <typeparam name="T">The array type has to inherit IComparable in order to have a 
		/// criterion to sort on.</typeparam>
		/// <param name="x">The input data to be sorted.</param>
		/// <returns>An array of indices such that if x is accessed with those indices the values are in 
		/// ascending (or to be more precise, non-decending) order.</returns>
		public static int[] Order<T>(IList<T> x) where T : IComparable<T>{
			if (x == null){
				return null;
			}
			int[] order = ConsecutiveInts(x.Count);
			const int low = 0;
			int high = order.Length - 1;
			int[] dummy = new int[order.Length];
			Array.Copy(order, dummy, order.Length);
			SortImpl(x, order, dummy, low, high);
			return order;
		}

		/// <summary>
		/// Private class that implements the sorting algorithm.
		/// </summary>
		private static void SortImpl<T>(IList<T> data, int[] orderDest, int[] orderSrc, int low, int high)
			where T : IComparable<T>{
			if (low >= high){
				return;
			}
			int mid = low + ((high - low) >> 1);
			SortImpl(data, orderSrc, orderDest, low, mid);
			SortImpl(data, orderSrc, orderDest, mid + 1, high);
			if (data[orderSrc[mid]].CompareTo(data[orderSrc[mid + 1]]) <= 0){
				Array.Copy(orderSrc, low, orderDest, low, high - low + 1);
				return;
			}
			if (data[orderSrc[low]].CompareTo(data[orderSrc[high]]) > 0){
				int m = (high - low)%2 == 0 ? mid : mid + 1;
				Array.Copy(orderSrc, low, orderDest, m, mid - low + 1);
				Array.Copy(orderSrc, mid + 1, orderDest, low, high - mid);
				return;
			}
			int tLow = low;
			int tHigh = mid + 1;
			for (int i = low; i <= high; i++){
				if ((tLow <= mid) && ((tHigh > high) || (data[orderSrc[tLow]]).CompareTo(data[orderSrc[tHigh]]) <= 0)){
					orderDest[i] = orderSrc[tLow++];
				} else{
					orderDest[i] = orderSrc[tHigh++];
				}
			}
		}

		public static float[] Quantiles(IList<float> x, double[] qs){
			int n = x.Count;
			float[] result = new float[qs.Length];
			if (n == 0){
				for (int i = 0; i < result.Length; i++){
					result[i] = Single.NaN;
				}
				return result;
			}
			int[] o = Order(x);
			for (int i = 0; i < result.Length; i++){
				int ind = (int) Math.Round((n - 1)*qs[i]);
				result[i] = x[o[ind]];
			}
			return result;
		}

		public static double[] Quantiles(IList<double> x, double[] qs){
			int n = x.Count;
			double[] result = new double[qs.Length];
			if (n == 0){
				for (int i = 0; i < result.Length; i++){
					result[i] = Single.NaN;
				}
				return result;
			}
			int[] o = Order(x);
			for (int i = 0; i < result.Length; i++){
				int ind = (int) Math.Round((n - 1)*qs[i]);
				result[i] = x[o[ind]];
			}
			return result;
		}

		public static double[] Rank<T>(IList<T> data) where T : IComparable<T>{
			return Rank(data, true);
		}

		/// <summary>
		/// Calculates the rank of the given data. The lowest rank value is 0.
		/// The input array type must inherit IComparable.
		/// </summary>
		public static double[] Rank<T>(IList<T> data, bool tieCorrection) where T : IComparable<T>{
			int n = data.Count;
			double[] rank = new double[n];
			int[] index = Order(data);
			for (int j = 0; j < n; j++){
				rank[index[j]] = j;
			}
			/* Fix for equal ranks */
			if (tieCorrection){
				int i = 0;
				while (i < n){
					T value = data[index[i]];
					int j = i + 1;
					while (j < n && data[index[j]].Equals(value)){
						j++;
					}
					int m = j - i;
					double v1 = rank[index[i]] + (m - 1)/2.0;
					for (j = i; j < i + m; j++){
						rank[index[j]] = v1;
					}
					i += m;
				}
			}
			return rank;
		}

		public static float[] RankF<T>(IList<T> data) where T : IComparable<T>{
			return RankF(data, true);
		}

		/// <summary>
		/// Calculates the rank of the given data. The lowest rank value is 0.
		/// The input array type must inherit IComparable.
		/// </summary>
		public static float[] RankF<T>(IList<T> data, bool tieCorrection) where T : IComparable<T>{
			int n = data.Count;
			float[] rank = new float[n];
			int[] index = Order(data);
			for (int j = 0; j < n; j++){
				rank[index[j]] = j;
			}
			/* Fix for equal ranks */
			if (tieCorrection){
				int i = 0;
				while (i < n){
					T value = data[index[i]];
					int j = i + 1;
					while (j < n && data[index[j]].Equals(value)){
						j++;
					}
					int m = j - i;
					double v1 = rank[index[i]] + (m - 1)/2.0;
					for (j = i; j < i + m; j++){
						rank[index[j]] = (float) v1;
					}
					i += m;
				}
			}
			return rank;
		}

		public static T[] RemoveAtIndex<T>(IList<T> x, int index){
			T[] result = new T[x.Count - 1];
			for (int i = 0; i < index; i++){
				result[i] = x[i];
			}
			for (int i = index + 1; i < x.Count; i++){
				result[i - 1] = x[i];
			}
			return result;
		}

		public static T[] Remove<T>(IList<T> x, IList<int> indices){
			List<T> result = new List<T>();
			for (int i = 0; i < x.Count; i++){
				if (!indices.Contains(i)){
					result.Add(x[i]);
				}
			}
			return result.ToArray();
		}

		public static T[] Insert<T>(IList<T> x, T value, int index){
			T[] result = new T[x.Count + 1];
			for (int i = 0; i < index; i++){
				result[i] = x[i];
			}
			result[index] = value;
			for (int i = index; i < x.Count; i++){
				result[i + 1] = x[i];
			}
			return result;
		}

		public static double MeanAndStddev(IList<double> vals, out double stddev){
			double mean = 0;
			int c = 0;
			foreach (double t in vals.Where(t => !Double.IsNaN(t))){
				mean += t;
				c++;
			}
			if (c == 0){
				stddev = Double.NaN;
				return Double.NaN;
			}
			if (c == 1){
				stddev = Double.NaN;
				return mean;
			}
			mean /= c;
			stddev = 0;
			foreach (double v in vals){
				if (!Double.IsNaN(v)){
					double x = v - mean;
					stddev += x*x;
				}
			}
			stddev /= (c - 1.0);
			stddev = Math.Sqrt(stddev);
			return mean;
		}

		public static double MeanAndStddev(IList<float> vals, out double stddev){
			int validCount;
			return MeanAndStddev(vals, out stddev, out validCount);
		}

		public static double MeanAndStddev(IList<float> vals, out double stddev, out int validCount){
			double mean = 0;
			validCount = 0;
			foreach (float t in vals.Where(t => !double.IsNaN(t))){
				mean += t;
				validCount++;
			}
			if (validCount == 0){
				stddev = Double.NaN;
				return Double.NaN;
			}
			if (validCount == 1){
				stddev = Double.NaN;
				return mean;
			}
			mean /= validCount;
			stddev = 0;
			foreach (float v in vals){
				if (!float.IsNaN(v)){
					double x = v - mean;
					stddev += x*x;
				}
			}
			stddev /= (validCount - 1.0);
			stddev = Math.Sqrt(stddev);
			return mean;
		}

		/// <summary>
		/// Returns an array containing all keys from the given dictionary.
		/// </summary>
		public static K[] GetKeys<K, V>(IDictionary<K, V> dict){
			K[] result = new K[dict.Count];
			dict.Keys.CopyTo(result, 0);
			return result;
		}

		/// <summary>
		/// Returns an array containing all values from the given dictionary.
		/// </summary>
		public static V[] GetValues<K, V>(IDictionary<K, V> dict){
			V[] result = new V[dict.Count];
			dict.Values.CopyTo(result, 0);
			return result;
		}

		/// <summary>
		/// Returns an array containing all values from the given dictionary for which there is a key present in the key array.
		/// </summary>
		public static V[] GetValues<K, V>(IEnumerable<K> keys, IDictionary<K, V> dict){
			List<V> result = new List<V>();
			foreach (K key in keys){
				if (dict.ContainsKey(key)){
					result.Add(dict[key]);
				}
			}
			return result.ToArray();
		}

		/// <summary>
		/// Returns the index of the first element in an array that equals a given object. 
		/// </summary>
		/// <param name="p">Array to be searched.</param>
		/// <param name="q">Element to be found.</param>
		/// <returns>Index of first occurence. -1 otherwise.</returns>
		public static int IndexOf<T>(IList<T> p, T q){
			for (int i = 0; i < p.Count; i++){
				if (p[i].Equals(q)){
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Returns all indices of the array elements that equal a given object. 
		/// </summary>
		/// <param name="p">Array to be searched.</param>
		/// <param name="q">Element to be found.</param>
		/// <returns>All indices of occurence.</returns>
		public static int[] IndicesOf<T>(IList<T> p, T q){
			List<int> result = new List<int>();
			for (int i = 0; i < p.Count; i++){
				if (Equals(p[i], q)){
					result.Add(i);
				}
			}
			return result.ToArray();
		}

		public static T[] ToArray<T>(HashSet<T> set){
			T[] array = new T[set.Count];
			set.CopyTo(array);
			return array;
		}

		public static double[] ToDoubles(IList<int> ints){
			double[] result = new double[ints.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = ints[i];
			}
			return result;
		}

		public static float[] ToFloats(IList<double> doubles){
			float[] result = new float[doubles.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = (float) doubles[i];
			}
			return result;
		}

		public static int[] ToInts(IList<float> doubles){
			int[] result = new int[doubles.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = (int) Math.Round(doubles[i]);
			}
			return result;
		}

		public static float[] ToFloats(IList<int> doubles){
			float[] result = new float[doubles.Count];
			for (int i = 0; i < result.Length; i++){
				result[i] = doubles[i];
			}
			return result;
		}

		public static int FloorIndex<T>(T[] array, T value) where T : IComparable<T>{
			int n = array.Length;
			if (n == 0){
				return -1;
			}
			if (value.CompareTo(array[n - 1]) > 0){
				return n - 1;
			}
			if (value.CompareTo(array[0]) < 0){
				return -1;
			}
			int a = Array.BinarySearch(array, value);
			if (a >= 0){
				while (a < array.Length - 1 && array[a + 1].Equals(array[a])){
					a++;
				}
				return a;
			}
			return -2 - a;
		}

		public static int CeilIndex<T>(T[] array, T value) where T : IComparable<T>{
			int n = array.Length;
			if (n == 0){
				return -1;
			}
			if (value.CompareTo(array[n - 1]) > 0){
				return -1;
			}
			if (value.CompareTo(array[0]) < 0){
				return 0;
			}
			int a = Array.BinarySearch(array, value);
			if (a >= 0){
				while (a > 0 && array[a - 1].Equals(array[a])){
					a--;
				}
				return a;
			}
			return -1 - a;
		}

		public static int FloorIndex<T>(List<T> array, T value) where T : IComparable<T>{
			int n = array.Count;
			if (n == 0){
				return -1;
			}
			if (value.CompareTo(array[n - 1]) > 0){
				return n - 1;
			}
			if (value.CompareTo(array[0]) < 0){
				return -1;
			}
			int a = array.BinarySearch(value);
			if (a >= 0){
				while (a < array.Count - 1 && array[a + 1].Equals(array[a])){
					a++;
				}
				return a;
			}
			return -2 - a;
		}

		public static int CeilIndex<T>(List<T> array, T value) where T : IComparable<T>{
			int n = array.Count;
			if (n == 0){
				return -1;
			}
			if (value.CompareTo(array[n - 1]) > 0){
				return -1;
			}
			if (value.CompareTo(array[0]) < 0){
				return 0;
			}
			int a = array.BinarySearch(value);
			if (a >= 0){
				while (a > 0 && array[a - 1].Equals(array[a])){
					a--;
				}
				return a;
			}
			return -1 - a;
		}

		public static void Revert<T>(IList<T> x){
			int n = x.Count;
			for (int i = 0; i < n/2; i++){
				T tmp = x[i];
				x[i] = x[n - i - 1];
				x[n - i - 1] = tmp;
			}
		}

		/// <summary>
		/// Compares the two given arrays and returns the equality of their contents. First the lengths of the
		/// given arrays are compared and then their contents.
		/// </summary>
		/// 
		/// <param name="a">The first array.</param>
		/// <param name="b">The second array.</param>
		/// <returns>True when their contents are equal, false otherwise.</returns>
		public static bool EqualArrays<T>(IList<T> a, IList<T> b){
			if (a.Count != b.Count){
				return false;
			}
			for (int i = 0; i < a.Count; i++){
				if (!a[i].Equals(b[i])){
					return false;
				}
			}
			return true;
		}

		public static bool EqualArraysOfArrays<T>(IList<T[]> a, IList<T[]> b){
			if (a.Count != b.Count){
				return false;
			}
			for (int i = 0; i < a.Count; i++){
				if (!EqualArrays(a[i], b[i])){
					return false;
				}
			}
			return true;
		}

		public static T[] UniqueValues<T>(IList<T> array) where T : IComparable<T>{
			if (array.Count < 1){
				return new T[0];
			}
			T[] tmp = new T[array.Count];
			for (int i = 0; i < tmp.Length; i++){
				tmp[i] = array[i];
			}
			Array.Sort(tmp);
			int counter = 1;
			T lastVal = tmp[0];
			for (int i = 1; i < tmp.Length; i++){
				if (!lastVal.Equals(tmp[i])){
					lastVal = tmp[i];
					tmp[counter++] = lastVal;
				}
			}
			Array.Resize(ref tmp, counter);
			return tmp;
		}

		public static T[] UniqueValuesSorted<T>(IList<T[]> array){
			HashSet<T> hs = new HashSet<T>();
			foreach (T ty in from tx in array from ty in tx where !hs.Contains(ty) select ty){
				hs.Add(ty);
			}
			T[] x = ToArray(hs);
			Array.Sort(x);
			return x;
		}

		public static T[] UniqueValuesSorted<T>(IList<T> array){
			HashSet<T> hs = new HashSet<T>();
			foreach (T ty in from ty in array where !hs.Contains(ty) select ty){
				hs.Add(ty);
			}
			T[] x = ToArray(hs);
			Array.Sort(x);
			return x;
		}

		public static IList<T> UniqueValuesAndCounts<T>(IList<T> array, out IList<int> counters) where T : IComparable<T>{
			T[] unique = UniqueValues(array);
			counters = new List<int>();
			foreach (T u in unique){
				counters.Add(array.Count(x => x.Equals(u)));
			}
			return unique;
		}

		public static T[] UniqueValuesAndCounts<T>(T[] array, out int[] counters) where T : IComparable<T>{
			if (array.Length == 0){
				counters = new int[0];
				return new T[0];
			}
			if (array.Length == 1){
				counters = new[]{1};
				return array;
			}
			T[] sorted = (T[]) array.Clone();
			counters = new int[array.Length];
			Array.Sort(sorted);
			int counter = 1;
			T lastVal = sorted[0];
			counters[0] = 1;
			for (int i = 1; i < sorted.Length; i++){
				if (!lastVal.Equals(sorted[i])){
					lastVal = sorted[i];
					sorted[counter++] = lastVal;
				}
				counters[counter - 1]++;
			}
			Array.Resize(ref counters, counter);
			Array.Resize(ref sorted, counter);
			return sorted;
		}

		public static double Sum(IList<double> x){
			int n = x.Count;
			double sum = 0;
			for (int i = 0; i < n; i++){
				sum += x[i];
			}
			return sum;
		}

		public static double Product(IList<double> x){
			int n = x.Count;
			double prod = 1;
			for (int i = 0; i < n; i++){
				prod *= x[i];
			}
			return prod;
		}

		public static double Sum(IList<float> x){
			int n = x.Count;
			double sum = 0;
			for (int i = 0; i < n; i++){
				sum += x[i];
			}
			return sum;
		}

		public static int Sum(IList<int> x){
			int n = x.Count;
			int sum = 0;
			for (int i = 0; i < n; i++){
				sum += x[i];
			}
			return sum;
		}

		public static int Sum(IList<bool> x){
			int n = x.Count;
			int sum = 0;
			for (int i = 0; i < n; i++){
				if (x[i]){
					sum++;
				}
			}
			return sum;
		}

		public static double Cosine(IList<double> x, IList<double> y){
			if (x.Count < 3){
				return 0;
			}
			double xx = 0;
			double yy = 0;
			double xy = 0;
			for (int i = 0; i < x.Count; i++){
				double wx = x[i];
				double wy = y[i];
				xx += wx*wx;
				yy += wy*wy;
				xy += wx*wy;
			}
			double denom = xx*yy;
			if (denom > 0.0){
				return xy/Math.Sqrt(denom);
			}
			return 0f;
		}

		public delegate T Map<out T>(int i);

		public static T[] FillArray<T>(Map<T> map, int len){
			T[] array = new T[len];
			for (int i = 0; i < len; i++){
				array[i] = map(i);
			}
			return array;
		}

		public static T[] FillArray<T>(T item, int len){
			T[] array = new T[len];
			for (int i = 0; i < len; i++){
				array[i] = item;
			}
			return array;
		}

		public static int ClosestIndex(double[] array, double value){
			if (double.IsNaN(value)){
				return -1;
			}
			int n = array.Length;
			if (n == 0){
				return -1;
			}
			if (n == 1){
				return 0;
			}
			if (value > array[n - 1]){
				return n - 1;
			}
			if (value < array[0]){
				return 0;
			}
			int a = Array.BinarySearch(array, value);
			if (a >= 0){
				return a;
			}
			int b = -1 - a;
			if (b == 0){
				return b;
			}
			if (array[b] - value < value - array[b - 1]){
				return b;
			}
			return b - 1;
		}

		public static int ClosestIndex(float[] array, float value){
			if (float.IsNaN(value)){
				return -1;
			}
			int n = array.Length;
			if (n == 0){
				return -1;
			}
			if (n == 1){
				return 0;
			}
			if (value > array[n - 1]){
				return n - 1;
			}
			if (value < array[0]){
				return 0;
			}
			int a = Array.BinarySearch(array, value);
			if (a >= 0){
				return a;
			}
			int b = -1 - a;
			if (b == 0){
				return b;
			}
			if (b >= n){
				return n - 1;
			}
			if (array[b] < 2*value - array[b - 1]){
				return b;
			}
			return b - 1;
		}

		public static int ClosestIndex(int[] array, int value){
			int n = array.Length;
			if (n == 0){
				return -1;
			}
			if (n == 1){
				return 0;
			}
			if (value > array[n - 1]){
				return n - 1;
			}
			if (value < array[0]){
				return 0;
			}
			int a = Array.BinarySearch(array, value);
			if (a >= 0){
				return a;
			}
			int b = -1 - a;
			if (b == 0){
				return b;
			}
			if (array[b] - value < value - array[b - 1]){
				return b;
			}
			return b - 1;
		}

		public static int GetArrayOfArrayHashCode<T>(T[][] array){
			int hash = 397;
			foreach (T[] elem in array){
				hash = (hash*397) ^ GetArrayHashCode(elem);
			}
			return hash;
		}

		public static int GetArrayHashCode<T>(T[] array){
			int hash = 397;
			foreach (T elem in array){
				hash = (hash*397) ^ elem.GetHashCode();
			}
			return hash;
		}

		public static float[,] ToFloats(double[,] x){
			float[,] result = new float[x.GetLength(0),x.GetLength(1)];
			for (int i = 0; i < x.GetLength(0); i++){
				for (int j = 0; j < x.GetLength(1); j++){
					result[i, j] = (float) x[i, j];
				}
			}
			return result;
		}

		public static double[] ExtractValidValues(IList<double> x){
			List<double> result = new List<double>();
			foreach (double y in x){
				if (!double.IsNaN(y) && !double.IsInfinity(y)){
					result.Add(y);
				}
			}
			return result.ToArray();
		}

		public static float[] ExtractValidValues(IList<float> x){
			List<float> result = new List<float>();
			foreach (float y in x){
				if (!float.IsNaN(y) && !float.IsInfinity(y)){
					result.Add(y);
				}
			}
			return result.ToArray();
		}

		public static float[] SmoothMedian(IList<float> m, int width){
			float[] result = new float[m.Count];
			for (int i = 0; i < result.Length; i++){
				int min = Math.Max(0, i - width);
				int max = Math.Min(result.Length - 1, i + width);
				float[] q = SubArray(m, min, max + 1);
				if (q.Length < 2*width + 1){
					q = Concat(q, new float[2*width + 1 - q.Length]);
				}
				result[i] = Median(q);
			}
			return result;
		}

		public static float[] SmoothMean(IList<float> m, int width){
			float[] result = new float[m.Count];
			for (int i = 0; i < result.Length; i++){
				int min = Math.Max(0, i - width);
				int max = Math.Min(result.Length - 1, i + width);
				result[i] = Average(m, min, max, 2*width + 1);
			}
			return result;
		}

		public static float Average(IList<float> m, int min, int max, int len){
			float sum = 0;
			for (int i = min; i <= max; i++){
				sum += m[i];
			}
			return sum/len;
		}

		public static T[,] Transpose<T>(T[,] x){
			T[,] result = new T[x.GetLength(1),x.GetLength(0)];
			for (int i = 0; i < x.GetLength(0); i++){
				for (int j = 0; j < x.GetLength(1); j++){
					result[j, i] = x[i, j];
				}
			}
			return result;
		}

		public static double Correlation(IList<double> x, IList<double> y){
			if (x.Count < 3){
				return 0;
			}
			double mx = Mean(x);
			double my = Mean(y);
			double xx = 0;
			double yy = 0;
			double xy = 0;
			for (int i = 0; i < x.Count; i++){
				double wx = x[i] - mx;
				double wy = y[i] - my;
				xx += wx*wx;
				yy += wy*wy;
				xy += wx*wy;
			}
			double denom = xx*yy;
			if (denom > 0.0){
				return xy/Math.Sqrt(denom);
			}
			return 0f;
		}

		public static double Correlation(IList<float> x, IList<float> y){
			if (x.Count < 3){
				return 0;
			}
			double mx = Mean(x);
			double my = Mean(y);
			double xx = 0;
			double yy = 0;
			double xy = 0;
			for (int i = 0; i < x.Count; i++){
				double wx = x[i] - mx;
				double wy = y[i] - my;
				xx += wx*wx;
				yy += wy*wy;
				xy += wx*wy;
			}
			double denom = xx*yy;
			if (denom > 0.0){
				return xy/Math.Sqrt(denom);
			}
			return 0f;
		}

		public static double[] SmoothMean(IList<double> m, int width){
			double[] result = new double[m.Count];
			for (int i = 0; i < result.Length; i++){
				int min = Math.Max(0, i - width);
				int max = Math.Min(result.Length - 1, i + width);
				result[i] = Average(m, min, max);
			}
			return result;
		}

		public static double[] SmoothMedian(IList<double> m, int width){
			double[] result = new double[m.Count];
			for (int i = 0; i < result.Length; i++){
				int min = Math.Max(0, i - width);
				int max = Math.Min(result.Length - 1, i + width);
				result[i] = Median(m, min, max);
			}
			return result;
		}

		private static double Median(IList<double> m, int min, int max){
			int len = max - min + 1;
			if (len == 1){
				return m[min];
			}
			if (len == 2){
				return 0.5f*(m[min] + m[max]);
			}
			if (len == 3){
				double m1 = m[min];
				double m2 = m[min + 1];
				double m3 = m[min + 2];
				if (m1 <= m2 && m2 <= m3){
					return m2;
				}
				if (m2 <= m3 && m3 <= m1){
					return m3;
				}
				if (m3 <= m1 && m1 <= m2){
					return m1;
				}
				if (m3 <= m2 && m2 <= m1){
					return m2;
				}
				if (m2 <= m1 && m1 <= m3){
					return m1;
				}
				if (m1 <= m3 && m3 <= m2){
					return m3;
				}
			}
			double[] x = new double[len];
			for (int i = 0; i < len; i++){
				x[i] = m[min + i];
			}
			Array.Sort(x);
			if (len%2 == 0){
				int w = len/2;
				return 0.5f*(x[w - 1] + x[w]);
			} else{
				int w = len/2;
				return x[w];
			}
		}

		private static double Average(IList<double> m, int min, int max){
			double sum = 0;
			for (int i = min; i <= max; i++){
				sum += m[i];
			}
			return sum/(max - min + 1);
		}

		public static double Quantile(IList<double> x, double q){
			int n = x.Count;
			if (n == 0){
				return double.NaN;
			}
			int ind = (int) Math.Round((n - 1)*q);
			int[] o = Order(x);
			return x[o[ind]];
		}

		public static float Quantile(IList<float> x, float q){
			int n = x.Count;
			if (n == 0){
				return float.NaN;
			}
			int ind = (int) Math.Round((n - 1)*q);
			int[] o = Order(x);
			return x[o[ind]];
		}

		public static T[,] ExtractRows<T>(T[,] values, IList<int> rows){
			T[,] result = new T[rows.Count,values.GetLength(1)];
			for (int i = 0; i < rows.Count; i++){
				for (int j = 0; j < values.GetLength(1); j++){
					result[i, j] = values[rows[i], j];
				}
			}
			return result;
		}

		public static float[] Zscore(IList<float> vals){
			int validCount;
			double stddev;
			double mean = MeanAndStddev(vals, out stddev, out validCount);
			float[] result = new float[vals.Count];
			if (validCount < 3){
				for (int i = 0; i < result.Length; i++){
					result[i] = float.NaN;
				}
				return result;
			}
			for (int i = 0; i < result.Length; i++){
				result[i] = (float) ((vals[i] - mean)/stddev);
			}
			return result;
		}

		public static double InterQuartileRange(IList<double> vals){
			return Quantile(vals, 0.75) - Quantile(vals, 0.25);
		}

		public static double FirstQuartile(IList<double> vals){
			return Quantile(vals, 0.25);
		}

		public static double ThirdQuartile(IList<double> vals){
			return Quantile(vals, 0.75);
		}

		public static double Skewness(IList<double> vals){
			int n = vals.Count;
			double mean = Mean(vals);
			double m2 = 0;
			double m3 = 0;
			foreach (double c in vals.Select(val => val - mean)){
				m3 += c*c*c;
				m2 += c*c;
			}
			m2 /= n;
			m3 /= n;
			double g1 = m3/Math.Pow(m2, 1.5);
			return g1*Math.Sqrt(n*(n - 1))/(n - 2);
		}

		public static double Kurtosis(IList<double> vals){
			int n = vals.Count;
			double mean = Mean(vals);
			double m2 = 0;
			double m4 = 0;
			foreach (double c in vals.Select(val => val - mean)){
				m4 += c*c*c*c;
				m2 += c*c;
			}
			m2 /= n;
			m4 /= n;
			double num = n*n*((n + 1)*m4 - 3*(n - 1)*m2*m2)*(n - 1)*(n - 1);
			double denom = (n - 1)*(n - 2)*(n - 3)*n*n*m2*m2;
			return num/denom;
		}

		public static double CoefficientOfVariation(IList<double> x){
			return StandardDeviation(x)/Mean(x);
		}

		public static double MeanAbsoluteDeviation(IList<double> x){
			double median = Median(x);
			List<double> w = new List<double>();
			foreach (double t in x){
				w.Add(Math.Abs(median - t));
			}
			return Median(w.ToArray());
		}

		public static HashSet<T> ToHashSet<T>(IEnumerable<T> x){
			HashSet<T> result = new HashSet<T>();
			foreach (T t in x){
				result.Add(t);
			}
			return result;
		}

		private static int[] SplitIndices(int n, int k){
			int[] result = new int[k + 1];
			for (int i = 0; i < k + 1; i++){
				result[i] = (int) Math.Round(i/(double) k*n);
			}
			return result;
		}

		public static T[][] SplitArray<T>(T[] x, int n){
			n = Math.Min(n, x.Length);
			int[] indices = SplitIndices(x.Length, n);
			T[][] result = new T[n][];
			for (int i = 0; i < n; i++){
				result[i] = SubArray(x, indices[i], indices[i + 1]);
			}
			return result;
		}

		public static Dictionary<T, int> InverseMap<T>(IList<T> list) {
			Dictionary<T, int> result = new Dictionary<T, int>();
			for (int i = 0; i < list.Count; i++) {
				if (!result.ContainsKey(list[i])) {
					result.Add(list[i], i);
				}
			}
			return result;
		}
	}
}