using BaseLib.Param;

namespace BaseLib.Api{
	public interface IClassificationMethod : INamedListItem{
		/// <summary>
		/// Create a classification model based on the given training data x with group assignments in y.
		/// </summary>
		/// <param name="x">The training data for which the group assignment is known. <code>x.Length</code> 
		/// is the number of training instances.</param>
		/// <param name="y">The group assignments. <code>y.Length</code> is the number of training instances.
		/// In principle each training item can be assigned to multiple groups which is why this is an
		/// array of arrays. Each item has to be assigned to at least one group.</param>
		/// <param name="ngroups">The number of groups which has to be at least two.</param>
		/// <param name="param"><code>Parameters</code> object holding the user-defined values for the parameters
		/// of the classification algorithm.</param>
		/// <param name="nthreads">Number of threads the algorithm can use in case it supports parallelization.</param>
		/// <returns></returns>
		ClassificationModel Train(float[][] x, int[][] y, int ngroups, Parameters param, int nthreads);

		/// <summary>
		/// Gets the <code>Parameters</code> object which is to be filled with the user-defined values.
		/// </summary>
		Parameters Parameters { get; }
	}
}