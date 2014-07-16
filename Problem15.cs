using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [EulerProblem(15, Title = "Starting in the top left corner in a 20 by 20 grid, how many routes are there to the bottom right corner?")]
    public class Problem15
    {
        public long Solve()
        {
            return PascalsTriangle.GetEntry(2*20, 20);
        }
    }
}
