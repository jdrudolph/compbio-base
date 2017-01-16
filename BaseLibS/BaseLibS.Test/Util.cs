using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BaseLibS.Num.Cluster;

namespace BaseLibS.Test
{
    public static class Util
    {
        public static float[,] ReadMatrix(string path)
        {
            float[,] vals;
            using (var stream = new GZipStream(new FileStream(path, FileMode.Open), CompressionMode.Decompress))
            {
                using (var reader = new StreamReader(stream))
                {
                    var colnames = reader.ReadLine();
                    var coltypes = reader.ReadLine().Replace("#!{Type}", "").Split('\t').TakeWhile(s => s.Equals("E")).ToArray();
                    var m = coltypes.Length;
                    string line;
                    var lines = new List<float[]>();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("#")) { continue; }
                        lines.Add(line.Split('\t').Take(m).Select(float.Parse).ToArray());
                    }
                    var n = lines.Count;
                    vals = new float[n,m];
                    for (int row = 0; row < n; row++)
                    {
                        var rowVals = lines[row];
                        for (int col = 0; col < m; col++)
                        {
                            vals[row, col] = rowVals[col];
                        }
                    }
                }
                
            }
            return vals;
        }
    }
}