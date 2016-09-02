using BaseLibS.Api;
using BaseLibS.Num.Matrix;

namespace NumPluginBase.Distance
{
    /// <summary>
    /// Allows for the conversion of any <see cref="IDistance"/> into a distance matrix.
    /// Implementation stores only upper triangular matrix due to symmetry in the distance.
    /// This improves memory footprint by factor 2 at the cost of slightly reduced performance
    /// due to index comparison and calculation.
    /// </summary>
    public class GenericDistanceMatrix : IDistanceMatrix
    {

        private readonly double[] _distances;
        private readonly int n;

        /// <summary>
        /// Create distance matrix from <see cref="IDistance"/>.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="distance"></param>
        public GenericDistanceMatrix(MatrixIndexer data, IDistance distance)
        {
            n = data.RowCount;
            _distances = new double[n * (n - 1) / 2];
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                var xi = data.GetRow(i);
                for (int j = i+1; j < n; j++)
                {
                    _distances[k++] = distance.Get(xi, data.GetRow(j));
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                var comp = i.CompareTo(j);
                if (comp == 0)
                {
                    return 0.0;
                }
                if (comp > 0)
                {
                    int tmp = i;
                    i = j;
                    j = tmp;
                }
                int k = (n*(n - 1)/2) - (n - i)*((n - i) - 1)/2 + j - i - 1;
                return _distances[k];
            }
            set
            {
                int k = (n*(n - 1)/2) - (n - i)*((n - i) - 1)/2 + j - i - 1;
                _distances[k] = value;
            }
        }
    }
}