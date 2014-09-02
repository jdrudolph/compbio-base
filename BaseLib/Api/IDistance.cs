using System;
using System.Collections.Generic;
using BaseLib.Num;
using BaseLib.Param;

namespace BaseLib.Api{
	/// <summary>
	/// Ancestor class of all distances. Distances are needed e.g. for hierarchical clustering
	/// or k-nearest neoghbour classification. 
	/// </summary>
	public interface IDistance : ICloneable, INamedListItem{
		Parameters Parameters { get; set; }

		/// <summary>
		/// Calculates the distance between two vectors. The two vectors must have the same length.
		/// </summary>
		/// <param name="x">The first vector.</param>
		/// <param name="y">The second vector.</param>
		/// <returns></returns>
		double Get(IList<float> x, IList<float> y);

		/// <summary>
		/// Calculates the distance between two vectors. The two vectors must have the same length.
		/// </summary>
		/// <param name="x">The first vector.</param>
		/// <param name="y">The second vector.</param>
		/// <returns></returns>
		double Get(IList<double> x, IList<double> y);

		/// <summary>
		/// This method returns the distance between two row or column vectors in two matrices.
		/// The matrices must have the same number of rows or columns (depending on the access type 
		/// being column or row). 
		/// </summary>
		/// <param name="data1">The matrix from which the first row/column is taken.</param>
		/// <param name="data2">The matrix from which the second row/column is taken.</param>
		/// <param name="index1">The row/column index in the first matrix.</param>
		/// <param name="index2">The row/column index in the second matrix.</param>
		/// <param name="access">Specifes whether the kernel function is evaluated on row or column vectors.</param>
		/// <returns></returns>
		double Get(float[,] data1, float[,] data2, int index1, int index2, MatrixAccess access);

		/// <summary>
		/// This method returns the distance between two row or column vectors in two matrices.
		/// The matrices must have the same number of rows or columns (depending on the access type 
		/// being column or row). 
		/// </summary>
		/// <param name="data1">The matrix from which the first row/column is taken.</param>
		/// <param name="data2">The matrix from which the second row/column is taken.</param>
		/// <param name="index1">The row/column index in the first matrix.</param>
		/// <param name="index2">The row/column index in the second matrix.</param>
		/// <param name="access">Specifes whether the kernel function is evaluated on row or column vectors.</param>
		/// <returns></returns>
		double Get(double[,] data1, double[,] data2, int index1, int index2, MatrixAccess access);
	}
}