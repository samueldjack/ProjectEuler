using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(67, Title = "Using an efficient algorithm find the maximal sum in the triangle?")]
    public class Problem67
    {
        public long Solve()
        {
            var dataPath = @"ProblemData\Problem67Data.txt";

            return MaximumSumThroughTriangleSolver.SolveFromFile(dataPath);
        }
    }
}
