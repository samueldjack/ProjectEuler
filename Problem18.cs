using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(18, Title = "Find the maximum sum travelling from the top of the triangle to the base.")]
    public class Problem18
    {
        public long Solve()
        {
            var dataPath = @"ProblemData\Problem18Data.txt";

            return MaximumSumThroughTriangleSolver.SolveFromFile(dataPath);
        }
    }
}
