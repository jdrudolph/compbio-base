namespace BaseLibS.Api
{
    /// <summary>
    /// Distance matrix interface for use in clustering methods where calculating pairwise distances
    /// using <see cref="IDistance"/> would be to expensive.
    /// <remarks>Requires quadratic memory</remarks>
    /// </summary>
    public interface IDistanceMatrix
    {
        /// <summary>
        /// Distance between <code>i</code> and <code>j</code>.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        double this[int i, int j] { get; set; }
    }
}